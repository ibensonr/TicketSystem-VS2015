﻿using BusinessEntities;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace TicketSystem.Controllers
{
    public class TicketController : ApiController
    {
        private readonly ITicketServices _ticketServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public TicketController(ITicketServices ticketServices)
        {
            _ticketServices = ticketServices;
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

        // GET api/ticket/5        
        [HttpGet]
        [Route("api/ticket/GetTicketForUser/{id}")]
        public HttpResponseMessage GetTicketForUser(int id)
        {
            var ticket = _ticketServices.GetAllTicketsForUser(id);
            if (ticket != null)
                return Request.CreateResponse(HttpStatusCode.OK, ticket);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No ticket found for this id");
        }

        // GET api/ticket/5        
        [HttpGet]
        [Route("api/ticket/GetTicketForAgent/{id}")]
        public HttpResponseMessage GetTicketForAgent(int id)
        {
            var ticket = _ticketServices.GetAllTicketsForAgent(id);
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
        public bool Put(int userid, int ticketid, [FromBody]TicketEntity ticketEntity)
        {
            if (userid > 0 && ticketid > 0)
            {
                return _ticketServices.UpdateTicket(userid, ticketid, ticketEntity);
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

        // DELETE api/ticket/5/5
        public bool Delete(int userid, int ticketid, string comment)
        {
            if (userid > 0 && ticketid > 0)
                return _ticketServices.CloseTicket(userid, ticketid, comment);
            return false;
        }

    }
}
