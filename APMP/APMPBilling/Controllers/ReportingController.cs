using APMPBilling.Helpers;
using APMPBilling.ViewModels;
using APMPRepository;
using APMPRepository.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace APMPBilling.Controllers
{
    public class ReportingController : Controller
    {
        private WorkingHoursRepository workHourDb = new WorkingHoursRepository();
        private WorkSiteRepository workSiteDb = new WorkSiteRepository();
        private UserRepository userDb = new UserRepository();

        // GET: Reporting
        public async Task<ActionResult> Index()
        {
            ReportingViewModel model = new ReportingViewModel();

            var workSites = await workSiteDb.GetWorkSitesAsync();
            List<object> workSiteValues = new List<object>();
            workSiteValues.Add(new { Value = -1, Text = "Tous les chantiers" });
            foreach (var workSite in workSites)
            {
                workSiteValues.Add(new { Value = workSite.WorkSiteId, Text = workSite.Location });
            }
            model.WorkSites = new SelectList(workSiteValues, "Value", "Text", -1);

            var employees = await userDb.GetUsersAsync();
            List<object> employeeValues = new List<object>();
            employeeValues.Add(new { Value = "-1", Text = "Tous les employés" });
            foreach (var employee in employees)
            {
                employeeValues.Add(new { Value = employee.Id, Text = employee.FirstName + " " + employee.LastName });
            }
            model.Employees = new SelectList(employeeValues, "Value", "Text", -1);

            model.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.EndDate = DateTime.Now;

            model.SortBy = new SelectList(
                        new[]
                        {
                            new { Value = ESortOrder.WORKSITE, Text = "Chantier" },
                            new { Value = ESortOrder.EMPLOYEE, Text = "Employé" },
                        }, "Value", "Text", ESortOrder.WORKSITE);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(ReportingViewModel reportingView)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();

                var users = await GetUsers(reportingView.EmployeeId);
                var workSites = await GetWorkSites(reportingView.WorkSiteId);
                string period = FormatterHelper.FormatPeriod(reportingView.StartDate, reportingView.EndDate).Replace(" ", "");
                ExcelPackage package = null;

                if (reportingView.SortOrder == ESortOrder.EMPLOYEE)
                    package = await ExportToExcelByEmployee(users, workSites, period, reportingView.StartDate, reportingView.EndDate);
                else
                    package = await ExportToExcelByWorksite(users, workSites, period, reportingView.StartDate, reportingView.EndDate);

                string filename = "APMP" + period + ".xlsx";
                return ExcelHelper.ExportData(package, filename);
            }
            return View(reportingView);
        }

        public async Task<ExcelPackage> ExportToExcelByEmployee(List<ApplicationUser> users, List<WorkSiteModel> workSites, string period, DateTime startDate, DateTime endDate)
        {
            ExcelPackage package = new ExcelPackage();
           
            var worksheet = GetWorkingHoursWorksheet(package, period, startDate, endDate);

            int startRow = 2;
            
            foreach (var user in users)
            {
                startRow = startRow + 2;
                worksheet.Cells[startRow, 1].Value = (user.FirstName + " " + user.LastName).ToUpper() + " - " + user.Email;
                var userRange = worksheet.Cells["A" + startRow + ":D" + startRow];
                userRange.Merge = true;

                userRange.Style.Font.Bold = true;
                userRange.Style.Font.Size = 14;
                userRange.Style.Font.Color.SetColor(System.Drawing.Color.SteelBlue);
                worksheet.Row(startRow).Height = 17;

                var workingHoursTable = new DataTable("WorkingHours" + user.Id);

                workingHoursTable.Columns.Add("Chantier", typeof(string));
                workingHoursTable.Columns.Add("Date", typeof(string));
                workingHoursTable.Columns.Add("Nombre d'heures", typeof(float));
                workingHoursTable.Columns.Add("Commentaire", typeof(string));


                List<WorkingHourByDayModel> workingHours = await workHourDb.GetWorkingHoursWithWorkSiteAsync(user.Id, startDate, endDate);

                if (workingHours.Count > 0)
                {
                    foreach (var workingHour in workingHours)
                    {
                        workingHoursTable.Rows.Add(workingHour.WorkSite.Location, workingHour.Day.ToString("dddd dd MMMM yyyy"), workingHour.NbHours, workingHour.Info);
                    }

                    startRow += 2;
                    worksheet.Cells[startRow, 1].LoadFromDataTable(workingHoursTable, true, OfficeOpenXml.Table.TableStyles.Light2);
                    startRow += workingHoursTable.Rows.Count;
                }
            }

            worksheet.Cells.AutoFitColumns();
            
            return package;
        }

        public async Task<ExcelPackage> ExportToExcelByWorksite(List<ApplicationUser> users, List<WorkSiteModel> workSites, string period, DateTime startDate, DateTime endDate)
        {
            ExcelPackage package = new ExcelPackage();

            var worksheet = GetWorkingHoursWorksheet(package, period, startDate, endDate);

            int startRow = 2;

            foreach (var workSite in workSites)
            {
                startRow = startRow + 2;
                worksheet.Cells[startRow, 1].Value = workSite.Location.ToUpper();
                var worksiteRange = worksheet.Cells["A" + startRow + ":D" + startRow];
                worksiteRange.Merge = true;
                worksheet.Row(startRow).Height = 17;

                worksiteRange.Style.Font.Bold = true;
                worksiteRange.Style.Font.Size = 14;
                worksiteRange.Style.Font.Color.SetColor(System.Drawing.Color.SteelBlue);

                var workingHoursTable = new DataTable("WorkingHours" + workSite.WorkSiteId);

                workingHoursTable.Columns.Add("Employé", typeof(string));
                workingHoursTable.Columns.Add("Date", typeof(string));
                workingHoursTable.Columns.Add("Nombre d'heures", typeof(float));
                workingHoursTable.Columns.Add("Commentaire", typeof(string));

                foreach (var user in users)
                {
                    List<WorkingHourByDayModel> workingHours = await workHourDb.GetWorkingHoursWithWorkSiteAsync(workSite.WorkSiteId, user.Id, startDate, endDate);

                    foreach (var workingHour in workingHours)
                    {
                        workingHoursTable.Rows.Add(user.FirstName + " " + user.LastName, workingHour.Day.ToString("dddd dd MMMM yyyy"), workingHour.NbHours, workingHour.Info);
                    }
                }

                if (workingHoursTable.Rows.Count > 0)
                {
                    startRow += 2;
                    worksheet.Cells[startRow, 1].LoadFromDataTable(workingHoursTable, true, OfficeOpenXml.Table.TableStyles.Light2);
                    var infoRange = worksheet.Cells["D:" + startRow + ",D:" + (startRow + workingHoursTable.Rows.Count)];
                    startRow += workingHoursTable.Rows.Count;
                }
            }

            worksheet.Cells.AutoFitColumns();

            return package;
        }

        private ExcelWorksheet GetWorkingHoursWorksheet(ExcelPackage package, string period, DateTime startDate, DateTime endDate)
        {
            var worksheet = package.Workbook.Worksheets.Add(period);

            worksheet.Cells[2, 1].Value = "Heures travaillées - " + FormatterHelper.FormatPeriod(startDate, endDate);

            var titleRange = worksheet.Cells["A2:D2"];
            titleRange.Merge = true;

            titleRange.Style.Font.Bold = true;
            titleRange.Style.Font.Size = 18;
            titleRange.Style.Font.Color.SetColor(System.Drawing.Color.DarkBlue);
            worksheet.Row(2).Height = 25;

            return worksheet;
        }

        

        private async Task<List<ApplicationUser>> GetUsers(string userId)
        {
            if (userId == "-1")
                return await userDb.GetUsersAsync();
            return new List<ApplicationUser>(new[] { await userDb.GetUserAsync(userId) });
        }

        private async Task<List<WorkSiteModel>> GetWorkSites(int workSiteId)
        {
            if (workSiteId == -1)
                return await workSiteDb.GetWorkSitesAsync();
            return new List<WorkSiteModel>(new[] { await workSiteDb.GetWorkSiteAsync(workSiteId) });
        }
    }
}