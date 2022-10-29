using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
/***
*
*	@author: Lawrence F. F. Sullivan
*
*	@date:	October 10, 2022
*
*	@purpose: 
* 
* 
*	@modifications: 
*	
*	@notes: 
*	
*/
namespace SITAzure.DAL
{
    public abstract class SITRepositoryBase<T> : IGenericRepository<T> where T : class
    {
        #region [ CLASS FIELDS ]

        protected string _connectionString;
        protected ILogger _logger;

        #endregion

        #region [ CONSTRUCTOR ]

        public SITRepositoryBase()
        {
            this._connectionString = String.Empty;
        }

        public SITRepositoryBase(string connectionString, ILogger logger)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region [ PROPERTIES ]

        public string ConnectionString { get => _connectionString; set => _connectionString = value; }

        #endregion

        #region [ METHODS ]

        private SqlConnection _createConnection()
        {
            return new SqlConnection(this._connectionString);
        }

        public virtual async Task<T> GetEntityExecStoredProcAsync(string storedProcName, object parameters)
        {
            T t = default(T);
            SqlConnection sqlConn = null;

            try
            {
                sqlConn = this._createConnection();
                sqlConn.Open();

                t = await sqlConn.QueryFirstOrDefaultAsync<T>(sql: storedProcName, parameters,
                                                commandType: System.Data.CommandType.StoredProcedure);
            }
            catch
            { throw; }
            finally
            { sqlConn?.Dispose(); }

            return t;
        }

        public virtual async Task<IReadOnlyList<T>> GetReadOnlyListExecStoredProcAsync(string storedProcName, 
                                                                                        object parameters = null)
        {
            IReadOnlyList<T> list = null;
            SqlConnection sqlConn = null;

            try
            {
                sqlConn = this._createConnection();
                sqlConn.Open();

                if (parameters != null)
                {
                    list = (await sqlConn.QueryAsync<T>(sql: storedProcName, parameters,
                                                        commandType: CommandType.StoredProcedure)
                                            .ConfigureAwait(false)).AsList();
                }
                else
                {
                    list = (await sqlConn.QueryAsync<T>(sql: storedProcName,
                                                        commandType: CommandType.StoredProcedure)
                                            .ConfigureAwait(false)).AsList();
                }
            }
            catch
            { throw; }
            finally
            { sqlConn?.Dispose(); }

            return list;
        }

        public virtual async Task<int> InsertEntityExecStoredProcAsync(string storedProcName, object parameters)
        {
            int rowsAffectedCnt = 0;
            IDbConnection sqlConn = null;

            try
            {
                sqlConn= this._createConnection();
                sqlConn?.Open();
                rowsAffectedCnt = await sqlConn.ExecuteAsync(sql: storedProcName, parameters,
                                                    commandType: CommandType.StoredProcedure)
                                            .ConfigureAwait(false);
            }
            catch
            { throw; }
            finally
            { sqlConn?.Dispose(); }
            return rowsAffectedCnt;
        }

        public virtual async Task<int> UpdateEntityExecStoredProcAsync(string storedProcName, object parameters)
        {
            int rowsAffectedCnt = 0;
            IDbConnection sqlConn = null;

            try
            {
                sqlConn = this._createConnection();
                sqlConn?.Open();
                rowsAffectedCnt = await sqlConn.ExecuteAsync(sql: storedProcName, parameters,
                                                            commandType: CommandType.StoredProcedure)
                                                .ConfigureAwait(false);

            }
            catch
            { throw; }
            finally
            { sqlConn?.Dispose(); }

            return rowsAffectedCnt;
        }

        public virtual async Task<int> DeleteEntityExecStoredProcAsync(string storedProcName, object parameters)
        {
            int rowsAffectedCnt = 0;
            IDbConnection sqlConn = null;

            try
            {
                sqlConn = this._createConnection();
                sqlConn.Open();

                rowsAffectedCnt = await sqlConn.ExecuteAsync(sql: storedProcName, parameters, 
                                                                commandType: CommandType.StoredProcedure)
                                                .ConfigureAwait(false);
            }
            catch
            { throw; }
            finally
            { sqlConn?.Dispose(); }

            return rowsAffectedCnt;
        }

        public virtual async Task<int> ExecScalarGetIntResultStoredProcAsync(string storedProcName, object parameters= null)
        {
            int iResult = 0;
            IDbConnection sqlConn = null;

            try
            {
                sqlConn = this._createConnection();
                sqlConn.Open();

                if (parameters != null)
                {
                    iResult = await sqlConn.ExecuteScalarAsync<int>(storedProcName, parameters,
                                                            commandType: CommandType.StoredProcedure)
                                        .ConfigureAwait(false);
                }
                else
                {
                    iResult = await sqlConn.ExecuteScalarAsync<int>(storedProcName, null,
                                                            commandType: CommandType.StoredProcedure)
                                                .ConfigureAwait(false);
                }
            }
            catch
            { throw; }
            finally
            { sqlConn?.Dispose(); }

            return iResult;
        }

        public virtual async Task<int> InsertEntityGetIdentityExecStoredProcAsync(string storedProcName, object parameters)
        {
            int identityId = 0;
            IDbConnection sqlConn = null;

            try
            {
                sqlConn = this._createConnection();
                sqlConn.Open();

                identityId = await sqlConn.ExecuteScalarAsync<int>(sql: storedProcName, parameters, commandType: CommandType.StoredProcedure)
                         .ConfigureAwait(false);
            }
            catch
            { throw; }
            finally
            { sqlConn?.Dispose(); }

            return identityId;
        }

        public virtual async Task<IReadOnlyList<int>> ExecStoredProcGetListofInt(string storedProcName, object parameters = null)
        {
            List<int> ints = null;
            IDbConnection sqlConn = null;
            IEnumerable<int> entities = null;
            try
            {
                sqlConn= this._createConnection();
                sqlConn?.Open();

                if(parameters != null)
                {
                    entities = await sqlConn.QueryAsync<int>(sql: storedProcName, parameters,
                                                                    commandType: CommandType.StoredProcedure)
                                                .ConfigureAwait(false);
                }
                else
                {
                    entities = await sqlConn.QueryAsync<int>(sql: storedProcName, null,
                                                                    commandType: CommandType.StoredProcedure)
                                                .ConfigureAwait(false);
                }

                if(entities != null)
                {   ints = new List<int>(entities); }
                
            }
            catch(Exception ex)
            {
                this._logger.Error(ex.ToString());
                throw;
            }
            finally
            { sqlConn?.Dispose(); }

            return ints.AsReadOnly();
        }

        #endregion
    }
}
