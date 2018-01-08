using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TicketSystem.Controllers
{
    public class TicketHistoryController : ApiController
    {
        private readonly ITicketServices _ticketServices;

        public TicketHistoryController(ITicketServices ticketServices)
        {
            _ticketServices = ticketServices;
        }
        // GET: api/TicketHistory
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TicketHistory/5
        public HttpResponseMessage Get(int id)
        {
            var ticket = _ticketServices.GetTicketHistoryById(id);
            if (ticket != null)
                return Request.CreateResponse(HttpStatusCode.OK, ticket);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No ticket found for this id");
        }

        // POST: api/TicketHistory
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TicketHistory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TicketHistory/5
        public void Delete(int id)
        {
        }
    }
}
