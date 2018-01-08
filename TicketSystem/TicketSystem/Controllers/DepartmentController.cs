using BusinessEntities;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TicketSystem.Controllers
{
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentServices _departmentServices;

        public DepartmentController(IDepartmentServices departmentServices)
        {
            _departmentServices = departmentServices;
        }

        // GET: api/Department
        public HttpResponseMessage Get()
        {
            var departments = _departmentServices.GetAllDepartments();
            if (departments != null)
            {
                var departmentEntities = departments as List<DepartmentEntity> ?? departments.ToList();
                if (departmentEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, departmentEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Departments not found");
        }

        // GET: api/Department/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Department
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Department/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Department/5
        public void Delete(int id)
        {
        }
    }
}
