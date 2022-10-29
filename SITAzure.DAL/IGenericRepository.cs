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
    public interface IGenericRepository<T>
    {
        Task<IReadOnlyList<T>> GetReadOnlyListExecStoredProcAsync(string storedProcName, object parameters = null);
        Task<int> InsertEntityExecStoredProcAsync(string storedProcName, object parameters);

        Task<int> InsertEntityGetIdentityExecStoredProcAsync(string storedProcName, object parameters);

        Task<T> GetEntityExecStoredProcAsync(string storedProcName, object parameters);

        Task<int> UpdateEntityExecStoredProcAsync(string storedProcName, object parameters);

        Task<int> DeleteEntityExecStoredProcAsync(string storedProcName, object parameters);

        Task<int> ExecScalarGetIntResultStoredProcAsync(string storedProcName, object parameters=null);

        Task<IReadOnlyList<int>> ExecStoredProcGetListofInt(string storedProcName, object parameters = null);
        
    }
}
