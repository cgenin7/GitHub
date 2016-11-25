using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APMPBilling.Helpers
{
    public static class ExcelHelper
    {
            public static FileResult ExportData(ExcelPackage package, string fileName)
            {
                return new EpplusResult(package) { FileDownloadName = fileName };
            }
    }

    public class EpplusResult : FileResult
    {
        public EpplusResult(ExcelPackage package)
            : base("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            Package = package;
        }

        public ExcelPackage Package { get; private set; }

        protected override void WriteFile(HttpResponseBase response)
        {
            // grab chunks of data and write to the output stream
            Stream outputStream = response.OutputStream;
            using (Package)
            {
                Package.SaveAs(outputStream);
            }
        }
    }
}