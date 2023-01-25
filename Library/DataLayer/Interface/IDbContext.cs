using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DataLayer.Interface
{
    public interface IDbContext
    {
        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns>Result</returns>
        int SaveChanges();

        /// <summary>
        /// Execute stores procedure and load a list of entities at the end
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="commandText">Command text</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Entities</returns>
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
            where TEntity : class, new();

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        IEnumerable<TEntity> SqlQuery<TEntity>(string sql, params object[] parameters) where TEntity : class, new();

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="doNotEnsureTransaction">false - the transaction creation is not ensured; true - the transaction creation is ensured.</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        /// <summary>
        /// Execute stored procedure with multiple result sets and static result
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="mapEntities">Callback mapper</param>
        /// <param name="exec">Command text</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Result</returns>
        TEntity ExecuteStoredProcedure<TEntity>(Func<DbDataReader, TEntity> mapEntities, string exec, params object[] parameters);

        /// <summary>
        /// Execute stored procedure with multiple result sets and static result
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="mapEntities">Callback mapper</param>
        /// <param name="exec">Command text</param>
        /// <param name="commandType">Command type</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Result</returns>
        TEntity ExecuteStoredProcedure<TEntity>(Func<DbDataReader, TEntity> mapEntities, string exec, CommandType commandType, params object[] parameters);

        /// <summary>
        /// Execute stored procedure with multiple result sets and static result
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="mapEntities">Callback mapper</param>
        /// <param name="exec">Command text</param>
        /// <param name="commandType">Command type</param>
        /// <param name="connectionString">Optional connection string</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Result</returns>
        TEntity ExecuteStoredProcedure<TEntity>(Func<DbDataReader, TEntity> mapEntities, string exec, CommandType commandType, string connectionString = null, params object[] parameters);

        /// <summary>
        /// Detach an entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Detach(object entity);

        /// <summary>
        /// Create Database Transaction
        /// </summary>
        /// <returns>IDbContextTransaction to be used.</returns>
        IDbContextTransaction CreateTransaction();
    }
}
