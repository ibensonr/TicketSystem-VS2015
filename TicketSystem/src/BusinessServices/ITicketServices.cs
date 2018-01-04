using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    /// <summary>
    /// Ticket Service Contract
    /// </summary>
    public interface ITicketServices
    {
        TicketEntity GetTicketById(int ticketId);
        IEnumerable<TicketEntity> GetAllTickets();
        int CreateTicket(TicketEntity ticketEntity);
        bool UpdateTicket(int userid, int ticketId, TicketEntity ticketEntity);
        bool DeleteTicket(int ticketId);
        TicketEntity GetTicketHistoryById(int ticketId);
        IEnumerable<TicketEntity> GetAllTicketsForUser(int userid);
        IEnumerable<TicketEntity> GetAllTicketsForAgent(int userid);
        bool CloseTicket(int userid, int ticketId, string comment);

    }
}
