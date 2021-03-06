﻿using System;
using System.Data.Entity;
using ContactLibrary.Data.Repositories;

namespace ContactLibrary.Data.Common
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;

        private IContactRepository contactRepository;

        public IContactRepository ContactRepository
        {
            get
            {
                return this.contactRepository ?? (this.contactRepository = new ContactRepository(this._dbContext));
            }
        }

        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
        }
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}
