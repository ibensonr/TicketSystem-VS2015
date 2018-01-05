using DataModel.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.UnitOfWork
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        #region Private member variables...
        private TicketSystemEntities _context = null;
        private GenericRepository<tbluser> _userRepository;
        private GenericRepository<tbldepartment> _departmentRepository;
        private GenericRepository<tblticket> _ticketRepository;
        private GenericRepository<tbluserdepartment> _userdepartmentRepository;
        #endregion

        public UnitOfWork()
        {
            _context = new TicketSystemEntities();
        }

        #region Public Repository Creation properties...

        /// <summary>
        /// Get/Set Property for userdepartment repository.
        /// </summary>
        public GenericRepository<tbluserdepartment> UserDepartmentRepository
        {
            get
            {
                if (this._userdepartmentRepository == null)
                    this._userdepartmentRepository = new GenericRepository<tbluserdepartment>(_context);
                return _userdepartmentRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for department repository.
        /// </summary>
        public GenericRepository<tbldepartment> DepartmentRepository
        {
            get
            {
                if (this._departmentRepository == null)
                    this._departmentRepository = new GenericRepository<tbldepartment>(_context);
                return _departmentRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public GenericRepository<tbluser> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new GenericRepository<tbluser>(_context);
                return _userRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for ticket repository.
        /// </summary>
        public GenericRepository<tblticket> TicketRepository
        {
            get
            {
                if (this._ticketRepository == null)
                    this._ticketRepository = new GenericRepository<tblticket>(_context);
                return _ticketRepository;
            }
        }
        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
