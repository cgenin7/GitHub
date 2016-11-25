using APMPRepository;
using APMPRepository.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace APMPWebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/WorkSite")]
    public class WorkSiteController : Controller
    {
        WorkSiteRepository _workSiteRepo = new WorkSiteRepository();

        [System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("WorkSites")]
        public async Task<IEnumerable<WorkSiteModel>> WorkSitesInfo()
        {
            return await _workSiteRepo.GetWorkSitesAsync();
        }

        [System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("WorkSite")]
        public async Task<WorkSiteModel> WorkSiteInfo(int workSiteId)
        {
            return await _workSiteRepo.GetWorkSiteAsync(workSiteId);
        }

        [System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("SetWorkSite")]
        public async Task<bool> SetWorkSite(WorkSiteModel workSite)
        {
            return await _workSiteRepo.AddWorkSiteAsync(workSite);
        }
        
    }
}