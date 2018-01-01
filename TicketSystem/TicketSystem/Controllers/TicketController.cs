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
    public class TicketController : ApiController
    {
        private readonly ITicketServices _ticketServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public TicketController()
        {
            _ticketServices = new TicketServices();
        }
        #endregion

        // GET api/ticket
        public HttpResponseMessage Get()
        {
            var tickets = _ticketServices.GetAllTickets();
            if (tickets != null)
            {
                var ticketEntities = tickets as List<TicketEntity> ?? tickets.ToList();
                if (ticketEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, ticketEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tickets not found");
        }

        // GET api/ticket/5
        public HttpResponseMessage Get(int id)
        {
            var ticket = _ticketServices.GetTicketById(id);
            if (ticket != null)
                return Request.CreateResponse(HttpStatusCode.OK, ticket);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No ticket found for this id");
        }

        // POST api/ticket
        public int Post(int userid, [FromBody]TicketEntity ticketEntity)
        {
            ticketEntity.createdby = userid;
            return _ticketServices.CreateTicket(ticketEntity);
        }

        // PUT api/ticket/5
        public bool Put(int id, [FromBody]TicketEntity ticketEntity)
        {
            if (id > 0)
            {
                return _ticketServices.UpdateTicket(id, ticketEntity);
            }
            return false;
        }

        // DELETE api/ticket/5
        public bool Delete(int id)
        {
            if (id > 0)
                return _ticketServices.DeleteTicket(id);
            return false;
        }

    }
}
