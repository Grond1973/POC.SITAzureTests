using SITAzure.Common;
using SITAzure.DAL;
/***
*
*	@author: Lawrence F. F. Sullivan
*
*	@date:	October 28, 2022
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
    public class MultiDataRepositoryManager : IRepositoryOperations
    {
        #region [ CLASS FIELDS ]

        private string _connectionString;

        #endregion

        #region [ CONSTRUCTOR ]

        public MultiDataRepositoryManager()
        {
            _connectionString = String.Empty;
        }

        public MultiDataRepositoryManager(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        #endregion

        #region [ PROPERTIES ]

        #endregion

        #region [ METHODS ]

        public virtual async Task<IReadOnlyList<GasPurchaseDataVO>> GetGasPurchaseDataListByUserIdAsync(int userId)
        {
            List<GasPurchaseDataVO> gasPurchaseDataList = null;
            GasPurchaseDataRepository gasPurchaseDataRepo =
                                        new GasPurchaseDataRepository(this._connectionString);

            IReadOnlyList<GasPurchaseDataModel> dbRecs = await gasPurchaseDataRepo.GetReadOnlyListExecStoredProcAsync("spSelGasPurchaseDataByUserId",
                                                                                                                    new { intUserId = userId });
            if (dbRecs?.Count > 0)
            {
                gasPurchaseDataList = new List<GasPurchaseDataVO>();
                foreach (GasPurchaseDataModel gasPurchaseData in dbRecs)
                {
                    gasPurchaseDataList.Add(new GasPurchaseDataVO(gasPurchaseData.PurchaseId,
                                                                    gasPurchaseData.CarId,
                                                                    gasPurchaseData.UserId,
                                                                    gasPurchaseData.TotalGallons,
                                                                    gasPurchaseData.TotalTripMiles,
                                                                    gasPurchaseData.PurchaseCost,
                                                                    gasPurchaseData.PurchaseDate,
                                                                    (PurchaseType)gasPurchaseData.PurchaseTypeId,
                                                                    (GasType)gasPurchaseData.GasTypeId));
                }
            }

            return gasPurchaseDataList.AsReadOnly();
        }

        public virtual async Task<int> UpdateUserPasswordAsync(int userId, string newPassword)
        {
            int rowsAffectedCnt = 0;
            SITUserRepository userRepo = new SITUserRepository(this._connectionString);

            rowsAffectedCnt = await userRepo.UpdateEntityExecStoredProcAsync("spUpdUserPassword",
                                                                                new
                                                                                {
                                                                                    intUserId = userId,
                                                                                    vchPassWord = newPassword
                                                                                });
            return rowsAffectedCnt;

        }

        public virtual async Task<int> InsertGasPurchaseDataAsync(GasPurchaseDataVO gasPurchaseDataVO)
        {
            int rowsAffectedCnt = 0;
            GasPurchaseDataRepository gasPurchaseDataRepo = new GasPurchaseDataRepository(this._connectionString);

            rowsAffectedCnt = await gasPurchaseDataRepo.InsertEntityExecStoredProcAsync("spInsGasPurchaseData",
                                                                                        new
                                                                                        {
                                                                                            intUserId = gasPurchaseDataVO.UserId,
                                                                                            mnyPurchaseCost = gasPurchaseDataVO.PurchaseCost,
                                                                                            intCarId = gasPurchaseDataVO.CarId,
                                                                                            fltTotalGallons = gasPurchaseDataVO.TotalGallons,
                                                                                            fltTotalTrip = gasPurchaseDataVO.TotalTripMiles,
                                                                                            dtePurchaseDate = gasPurchaseDataVO.PurchaseDate,
                                                                                            intGasTypeId = (int)gasPurchaseDataVO.GasTypeId
                                                                                        });

            return rowsAffectedCnt;
        }

        public virtual async Task<int> DeleteGasPurchaseDataRecordAsync(int userId, int gasPurchaseDataId)
        {
            int rowsAffected = 0;
            GasPurchaseDataRepository gasPurchaseDataRepo =
                                                new GasPurchaseDataRepository(this._connectionString);

            rowsAffected = await gasPurchaseDataRepo.DeleteEntityExecStoredProcAsync("spDelDeleteGasPurchaseDataRecord",
                                                                                        new
                                                                                        {
                                                                                            intUserId = userId,
                                                                                            intGasPurchaseId = gasPurchaseDataId
                                                                                        });
            return rowsAffected;
        }

        public virtual async Task<int> GetMaxGasPurchaseIdAsync()
        {
            int maxGasPurchaseId = 0;
            GasPurchaseDataRepository gasPurchaseDataRepo = new GasPurchaseDataRepository(this._connectionString);

            maxGasPurchaseId = await gasPurchaseDataRepo.ExecScalarGetIntResultStoredProcAsync("spSelScalarMaxGasPurchaseDataId");

            return maxGasPurchaseId;
        }

        public virtual async Task<int> GetCountOfGasPurchaseDataRecordsAsync()
        {
            int totalGasPurchaseDataRecs = 0;
            GasPurchaseDataRepository gasPurchDataRepo = new GasPurchaseDataRepository(this._connectionString);

            totalGasPurchaseDataRecs = await gasPurchDataRepo.ExecScalarGetIntResultStoredProcAsync("spSelCountGasPurchaseData");
            return totalGasPurchaseDataRecs;
        }

        public virtual async Task<IReadOnlyList<CarVO>> GetActiveCarsByUserIdAsync(int userId)
        {
            List<CarVO> carVOs = null;
            CarRepository carRepo = new CarRepository(this._connectionString);

            var carModels = await carRepo.GetReadOnlyListExecStoredProcAsync("spSelCarsByUserId", new { UserId = userId, IsActive = true });

            if (carModels?.Count > 0)
            {
                carVOs = new List<CarVO>();
                foreach (var car in carModels)
                {
                    carVOs.Add(new CarVO(car.CarId, car.CarModel, car.CarMake, userId, true));
                }
            }

            return carVOs?.AsReadOnly();
        }

        public virtual async Task<IReadOnlyList<GasTypeVO>> GetGasTypeVOsAsync()
        {
            List<GasTypeVO> gasTypes = null;
            GasTypeRepository gasTypeRepo = new GasTypeRepository(this._connectionString);
            var GasTypeModels = await gasTypeRepo.GetReadOnlyListExecStoredProcAsync("spSelGasolineTypes");

            if (GasTypeModels?.Count > 0)
            {
                gasTypes = new List<GasTypeVO>();

                foreach (var gasType in GasTypeModels)
                { gasTypes.Add(new GasTypeVO(gasType.GasTypeId, gasType.GasTypeDesc)); }
            }

            return gasTypes?.AsReadOnly();
        }

        public virtual async Task<VehicleCommentVO> GetVehicleCommentByUserIdAndCommentIdAsync(int userId, int commentId)
        {
            VehicleCommentVO commentVO = null;
            VehicleCommentRepository vehicleCommentRepo = new VehicleCommentRepository(this._connectionString);

            var vehicleCommentModel = await vehicleCommentRepo.GetEntityExecStoredProcAsync("spSelVehicleCommentById",
                                                                                            new
                                                                                            {
                                                                                                intUserId = userId,
                                                                                                intCommentId = commentId
                                                                                            });

            if (vehicleCommentModel?.CommentId > 0)
            {
                commentVO = new VehicleCommentVO(vehicleCommentModel.CommentId,
                                                    (VehicleCommentType)vehicleCommentModel.CommentTypeId,
                                                    vehicleCommentModel.UserId,
                                                    vehicleCommentModel.VehicleId,
                                                    vehicleCommentModel.Comment,
                                                    vehicleCommentModel.CommentDate);
            }

            return commentVO;
        }

        public virtual async Task<int> InsertVehicleCommentAsync(InsVehicleCommentVO vehicleCommentVO)
        {
            int insertRecCnt = 0;
            VehicleCommentRepository vehicleCommentRepo = new VehicleCommentRepository(this._connectionString);

            insertRecCnt = await vehicleCommentRepo.InsertEntityGetIdentityExecStoredProcAsync("spInsVehicleComment",
                                                                                        new
                                                                                        {
                                                                                            intCommentTypeId = vehicleCommentVO.CommentTypeId,
                                                                                            intUserId = vehicleCommentVO.UserId,
                                                                                            intVehicleId = vehicleCommentVO.VehicleId,
                                                                                            vchComment = vehicleCommentVO.Comment,
                                                                                            dteCommentDate = vehicleCommentVO.CommentDate
                                                                                        });
            return insertRecCnt;
        }

        #endregion
    }
}
