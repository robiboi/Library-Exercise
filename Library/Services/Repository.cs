using DataLayer;
using DataLayer.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Services
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _context;
        private DbSet<T> _entities;
        public Repository(IDbContext context)
        {
            _context = context;
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }
        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch(ValidationException dbEx)
            {
                throw new Exception(dbEx.Message, dbEx);
            }
        }

        public void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    Entities.Remove(entity);

                _context.SaveChanges();
            }
            catch(ValidationException dbEx)
            {
                throw new Exception(dbEx.Message, dbEx);
            }
        }

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Add(entity);

                _context.SaveChanges();
            }
            catch (ValidationException dbEx)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(dbEx.Message, dbEx);
            }
        }

        public T Insert(T entity, bool returnResult = true)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Add(entity);

                _context.SaveChanges();
            }
            catch (ValidationException dbEx)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(dbEx.Message, dbEx);
            }

            return entity;
        }

        public void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    Entities.Add(entity);

                _context.SaveChanges();
            }
            catch (ValidationException dbEx)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(dbEx.Message, dbEx);
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.SaveChanges();
            }
            catch (ValidationException dbEx)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(dbEx.Message, dbEx);
            }
        }

        public void Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                _context.SaveChanges();
            }
            catch (ValidationException dbEx)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(dbEx.Message, dbEx);
            }
        }
    }
}
