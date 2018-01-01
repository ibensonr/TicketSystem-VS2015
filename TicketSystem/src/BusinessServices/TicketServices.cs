using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;


namespace BusinessServices
{
    /// <summary>
    /// Offers services for ticket specific CRUD operations
    /// </summary>
    public class TicketServices : ITicketServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public TicketServices()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Fetches ticket details by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public TicketEntity GetTicketById(int ticketId)
        {
            var ticket = _unitOfWork.TicketRepository.GetByID(ticketId);
            if (ticket != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblticket, TicketEntity>();
                    //cfg.AddProfile()... etc...
                });
                var mapper = config.CreateMapper();                              
                var ticketModel = mapper.Map<tblticket, TicketEntity>(ticket);
                return ticketModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the tickets.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TicketEntity> GetAllTickets()
        {
            var tickets = _unitOfWork.TicketRepository.GetAll().ToList();
            if (tickets.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblticket, TicketEntity>();
                    //cfg.AddProfile()... etc...
                });
                var mapper = config.CreateMapper();
                var ticketsModel = mapper.Map<List<tblticket>, List<TicketEntity>>(tickets);
                return ticketsModel;
            }
            return null;
        }

        public TicketEntity GetTicketHistoryById(int id)
        {

            var ticketRep = _unitOfWork.TicketRepository;
            
            //var tDeptDetails = ticketRep.Query<tblticket>().ToList();

            var ticket = ticketRep.GetWithInclude(t => t.id == id, "tbltickethistory").ToList().FirstOrDefault();


            //var tickets = from t in _unitOfWork.TicketRepository.GetAll()
            //              join td in _unitOfWork.DepartmentRepository.Get()
            //              on t.deptid equals td.id
            //              select new { Subject = t.subject, };
            if (ticket != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblticket, TicketEntity>();
                    //cfg.AddProfile()... etc...
                });
                var mapper = config.CreateMapper();
                var ticketsModel = mapper.Map<tblticket, TicketEntity>(ticket);
                return ticketsModel;
            }
            return null;
        }



        /// <summary>
        /// Creates a ticket
        /// </summary>
        /// <param name="ticketEntity"></param>
        /// <returns></returns>
        public int CreateTicket(TicketEntity ticketEntity)
        {
            using (var scope = new TransactionScope())
            {
                var ticket = new tblticket
                {
                    subject = ticketEntity.subject,
                    createdby = ticketEntity.createdby,
                    description = ticketEntity.description,
                    deptid = ticketEntity.deptid,
                    comment = ticketEntity.comment
                };
                _unitOfWork.TicketRepository.Insert(ticket);
                _unitOfWork.Save();
                scope.Complete();
                return ticket.id;
            }
        }

        /// <summary>
        /// Updates a ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="ticketEntity"></param>
        /// <returns></returns>
        public bool UpdateTicket(int ticketId, TicketEntity ticketEntity)
        {
            var success = false;
            if (ticketEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var ticket = _unitOfWork.TicketRepository.GetByID(ticketId);
                    if (ticket != null)
                    {
                        ticket.subject = ticketEntity.subject;
                        ticket.deptid = ticketEntity.deptid;
                        ticket.description = ticketEntity.description;
                        ticket.comment = ticketEntity.comment;
                        _unitOfWork.TicketRepository.Update(ticket);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool DeleteTicket(int ticketId)
        {
            var success = false;
            if (ticketId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var ticket = _unitOfWork.TicketRepository.GetByID(ticketId);
                    if (ticket != null)
                    {
                        _unitOfWork.TicketRepository.Delete(ticket);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
