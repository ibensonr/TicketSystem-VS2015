using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TicketSystem.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserServices _userServices;

        public UserController()
        {
            _userServices = new UserServices();
        }

        // GET: api/User
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        public HttpResponseMessage Get(int id)
        {
            var user = _userServices.GetUserById(id);
            if (user != null)
                return Request.CreateResponse(HttpStatusCode.OK, user);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No user found for this id");
        }

        // GET: api/User/a/a
        public HttpResponseMessage Get(string uname, string pwd)
        {
            var user = _userServices.GetUserDetails(uname, pwd);
            if (user != null)
                return Request.CreateResponse(HttpStatusCode.OK, user);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No user found");
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
