using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiProject.DAL.ViewModel;
using WebApiProject.Service.Service.Logic;

namespace WebApiProject.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WepApiDataController : ApiController
    {
        // GET: api/WepApiData
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var model = await PersonalDetailsLogic.ListAllPersonalDetails();
            return Ok(model);
        }

        // GET: api/WepApiData/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid Id)
        {
            var model = await PersonalDetailsLogic.FindPersonalDetailsById(Id);
            return Ok(model);
        }

        // POST: api/WepApiData
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]PersonalDetailViewModel value)
        {
            var results = await PersonalDetailsLogic.CreatePersonalDetails(value);
            return Ok(results);
        }

        // PUT: api/WepApiData/5
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody]PersonalDetailViewModel value)
        {
            var results = await PersonalDetailsLogic.UpdatePersonalDetails(value);
            return Ok(results);
        }

        // DELETE: api/WepApiData/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid Id)
        {
            var results = await PersonalDetailsLogic.DeletePersonalDetails(Id);
            return Ok(results);
        }
    }
}
