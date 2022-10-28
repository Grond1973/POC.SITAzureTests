using SITAzure.Common;
/***
*
*	@author: Lawrence F. F. Sullivan
*
*	@date:	October 17, 2022
*
*	@purpose: 
* 
* 
*	@modifications: 
*	
*	@notes: 
*	
*/
namespace SITAzure.DAO
{
    public interface IRepositoryOperations
    {
        Task<IReadOnlyList<GasPurchaseDataVO>> GetGasPurchaseDataListByUserIdAsync(int userId);

        Task<int> UpdateUserPasswordAsync(int userId, string newPassword);

        Task<int> InsertGasPurchaseDataAsync(GasPurchaseDataVO gasPurchaseDataVO);

        Task<int> DeleteGasPurchaseDataRecordAsync(int userId, int gasPurchaseDataId);

        Task<int> GetMaxGasPurchaseIdAsync();

        Task<int> GetCountOfGasPurchaseDataRecordsAsync();

        Task<IReadOnlyList<CarVO>> GetActiveCarsByUserIdAsync(int userId);

        Task<IReadOnlyList<GasTypeVO>> GetGasTypeVOsAsync();

        Task<VehicleCommentVO> GetVehicleCommentByUserIdAndCommentIdAsync(int userId, int commentId);

        Task<int> InsertVehicleCommentAsync(InsVehicleCommentVO vehicleCommentVO);

    }
}
