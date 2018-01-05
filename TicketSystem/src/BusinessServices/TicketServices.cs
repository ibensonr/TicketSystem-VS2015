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

        public IEnumerable<TicketEntity> GetAllTicketsForUser(int userid)
        {
            var tickets = _unitOfWork.TicketRepository.GetMany(t => t.createdby == userid).ToList();
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

        public IEnumerable<TicketEntity> GetAllTicketsForAgent(int userid)
        {
            var tickets = _unitOfWork.TicketRepository.GetMany(t => t.assignedtoid == userid).ToList();
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

                var tckcrtdusr = GetUser(ticket.modifiedby);
                if (tckcrtdusr != null)
                {
                    ticketsModel.tbluser = mapper.Map<UserEntity, UserEntity>(tckcrtdusr);
                }

                if(ticketsModel.tbltickethistory != null && ticketsModel.tbltickethistory.Count > 0)
                {
                    var result = ticketsModel.tbltickethistory.OrderByDescending(a => a.id).ToList();
                    ticketsModel.tbltickethistory = result;
                    foreach (var th in result)
                    {
                        var tckmdfddusr = GetUser(th.modifiedby);
                        if (tckmdfddusr != null)
                        {
                            th.tbluser = mapper.Map<UserEntity, UserEntity>(tckmdfddusr);
                        }
                    }
                }

                //UserServices us = new UserServices();
                //var user = us.GetUserById(ticket.createdby);
                //if(user != null)
                //{
                //    ticketsModel.tbluser = mapper.Map<UserEntity, UserEntity>(user);
                //}

                return ticketsModel;
            }
            return null;
        }

        private UserEntity GetUser(int? id)
        {
            UserServices us = new UserServices();
            var user = us.GetUserById(id);

            return user;
        }



        /// <summary>
        /// Creates a ticket
        /// </summary>
        /// <param name="ticketEntity"></param>
        /// <returns></returns>
        public int CreateTicket(TicketEntity ticketEntity)
        {
            int agentId = GetRandomAgentUserIdByDeptId(ticketEntity.deptid) ?? 0;
            //using (var scope = new TransactionScope())
            //{
            //    var ticket = new tblticket
            //    {
            //        subject = ticketEntity.subject,
            //        createdby = ticketEntity.createdby,
            //        description = ticketEntity.description,
            //        deptid = ticketEntity.deptid,
            //        comment = ticketEntity.comment
            //    };
            //    _unitOfWork.TicketRepository.Insert(ticket);
            //    _unitOfWork.Save();
            //    scope.Complete();
            //    return ticket.id;
            //}



            return 1;
        }

        private int? GetRandomAgentUserIdByDeptId(int? deptid)
        {
            var users = _unitOfWork.UserDepartmentRepository.GetMany(u => u.deptid == deptid).ToList();
            if (users.Any())
            {
                var userIdList = users.Select(ud => ud.userid).ToList();
                return GetRandomAgentUserId(userIdList);
                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<tblticket, TicketEntity>();
                //    //cfg.AddProfile()... etc...
                //});
                //var mapper = config.CreateMapper();
                //var usersModel = mapper.Map<List<tbluserdepartment>, List<UserDepartmentEntity>>(users);
                //return usersModel.Select(u => u.userid);
            }
            return null;
        }

        private int?  GetRandomAgentUserId(List<int?> userid)
        {

            var ticketassignedcount1 = _unitOfWork.TicketRepository.GetMany(t => userid.Contains(t.assignedtoid)).GroupBy(a => a.assignedtoid).Select(c => new { Key = c.Key, total = c.Count() });
            var notassignedlist1 = userid.Where(u => ticketassignedcount1.Any(a => a.Key != u)).ToList();

            //var tickets = _unitOfWork.TicketRepository.GetWithInclude(t => userid.Contains(t.assignedtoid), include: "assignedtoid").Select(a => a.assignedtoid).ToList();

            //var notassignedlist = userid.Where(u => !tickets.Contains(u)).ToList();

            if (notassignedlist1.Count > 0)
            {
                if (notassignedlist1.Count == 1)
                {
                    return notassignedlist1.FirstOrDefault().Value;
                }
                if (notassignedlist1.Count > 1)
                {
                    notassignedlist1.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                }
            }
            else
            {
                //var ticketassignedcount = _unitOfWork.TicketRepository.GetMany(t => userid.Contains(t.assignedtoid)).GroupBy(a => a.assignedtoid).Select(c => new { Key = c.Key, total = c.Count() });
                var mincount = ticketassignedcount1.Min(a => a.total);
                var selectedUserID = ticketassignedcount1.Where(t => t.total == mincount).OrderBy(a => Guid.NewGuid()).FirstOrDefault();
                if (selectedUserID != null)
                {
                    return selectedUserID.Key;
                }
            }
             
            return null;
        }



        private void assignTicketToAgent()
        {

        }

        /// <summary>
        /// Updates a ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="ticketEntity"></param>
        /// <returns></returns>
        public bool UpdateTicket(int userid, int ticketId, TicketEntity ticketEntity)
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
                        ticket.modifiedby = userid;
                        _unitOfWork.TicketRepository.Update(ticket);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool CloseTicket(int userid, int ticketId, string comment)
        {
            var success = false;

            using (var scope = new TransactionScope())
            {
                var ticket = _unitOfWork.TicketRepository.GetByID(ticketId);
                if (ticket != null)
                {
                    ticket.comment = comment;
                    ticket.modifiedby = userid;
                    ticket.status = 1;
                    _unitOfWork.TicketRepository.Update(ticket);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
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
