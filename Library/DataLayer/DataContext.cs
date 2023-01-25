using DataLayer.Interface;
using DataLayer.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DataContext : DbContext, IDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookMap());
        }

        #region Methods

        public void Detach(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this.Detach(entity);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = Database.GetCommandTimeout();
                Database.SetCommandTimeout(timeout);
            }

            var result = Database.ExecuteSqlCommand(sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                Database.SetCommandTimeout(previousTimeout);
            }

            //return result
            return result;
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : class, new()
        {
            //add parameters to command
            if (parameters != null && parameters.Length > 0)
            {
                for (var i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        //output parameter
                        commandText += " output";
                    }
                }
            }

            var result = this.Query<TEntity>().FromSql(commandText, parameters);

            return result.ToList();
        }

        public TEntity ExecuteStoredProcedure<TEntity>(Func<DbDataReader, TEntity> mapEntities, string exec, params object[] parameters)
        {
            return ExecuteStoredProcedure<TEntity>(mapEntities, exec, CommandType.StoredProcedure, null, parameters);
        }

        public TEntity ExecuteStoredProcedure<TEntity>(Func<DbDataReader, TEntity> mapEntities, string exec, CommandType commandType, params object[] parameters)
        {
            return ExecuteStoredProcedure<TEntity>(mapEntities, exec, commandType, null, parameters);
        }

        public TEntity ExecuteStoredProcedure<TEntity>(Func<DbDataReader, TEntity> mapEntities, string exec, CommandType commandType = CommandType.StoredProcedure, string connectionString = null, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = this.Database.GetDbConnection().ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(exec, conn))
                {
                    conn.Open();
                    command.CommandTimeout = 120;
                    command.Parameters.AddRange(parameters);
                    command.CommandType = commandType;
                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            TEntity data = mapEntities(reader);
                            return data;
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public IEnumerable<TEntity> SqlQuery<TEntity>(string sql, params object[] parameters) where TEntity : class, new()
        {
            return this.Query<TEntity>().FromSql<TEntity>(sql, parameters);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(
                    entity,
                    validationContext,
                    validateAllProperties: true);
            }

            return base.SaveChanges();
        }

        public IDbContextTransaction CreateTransaction()
        {
            return base.Database.BeginTransaction();
        }

        #endregion
    }
}
