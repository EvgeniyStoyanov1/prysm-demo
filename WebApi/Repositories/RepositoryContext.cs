using Prysm.Monitoring.WebApi.Models;
using System;
using System.Data.Entity;

namespace Prysm.Monitoring.WebApi.Repositories
{
    public class RepositoryContext : IDisposable
    {
        private readonly DatabaseContext _databaseContext;
        private readonly GenericRepository<Ticket> _ticketRepository;
        private readonly GenericRepository<Department> _departamentRepository;
        private bool _disposed = false;

        public GenericRepository<Ticket> TicketRepository
        {
            get
            {
                return _ticketRepository;
            }
        }

        public GenericRepository<Department> DepartamentRepository
        {
            get
            {
                return _departamentRepository;
            }
        }

        public RepositoryContext()
        {
            _databaseContext = new DatabaseContext();
            _ticketRepository = new GenericRepository<Ticket>(_databaseContext);
            _departamentRepository = new GenericRepository<Department>(_databaseContext);
        }
   

        public void Save()
        {
            _databaseContext.SaveChanges();
        }

        public void Dispose()
        {
            if(!_disposed)
            {
                _databaseContext.Dispose();
            }

            _disposed = true;
        }
    }
}