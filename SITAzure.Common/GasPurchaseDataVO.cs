using System;
/***
*
*	@author: Lawrence F. F. Sullivan
*
*	@date:	October 16, 2022
*
*	@purpose: 
* 
* 
*	@modifications: 
*	
*	@notes: 
*	
*/
namespace SITAzure.Common
{
    public class GasPurchaseDataVO
    {
        #region [ CLASS FIELDS ]

        private int _puchaseId;
        private int _carId;
        private int _userId;
        private double _totalGallons;
        private double _totalTripMiles;
        private decimal _purchaseCost;
        private DateTime _purchaseDate;
        private PurchaseType _purchaseTypeId;
        private GasType _gasTypeId;

        #endregion

        #region [ CONSTRUCTOR ]

        public GasPurchaseDataVO()
        {
            _puchaseId = 0;
            _carId = 0;
            _userId = 0;
            _totalGallons = 0.0;
            _totalTripMiles = 0.0;
            _purchaseCost = 0M;
            _purchaseDate = DateTime.MinValue;
            _purchaseTypeId = PurchaseType.UNKNOWN;
            _gasTypeId = GasType.UNKNOWN;
        }

        public GasPurchaseDataVO(int puchaseId, int carId, int userId, double totalGallons, double totalTripMiles,
                                    decimal purchaseCost, DateTime purchaseDate, PurchaseType purchaseTypeId,
                                    GasType gasTypeId)
        {
            _puchaseId = puchaseId;
            _carId = carId;
            _userId = userId;
            _totalGallons = totalGallons;
            _totalTripMiles = totalTripMiles;
            _purchaseCost = purchaseCost;
            _purchaseDate = purchaseDate;
            _purchaseTypeId = purchaseTypeId;
            _gasTypeId = gasTypeId;
        }


        #endregion

        #region [ PROPERTIES ]

        public int PuchaseId { get => _puchaseId; set => _puchaseId = value; }
        public int CarId { get => _carId; set => _carId = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public double TotalGallons { get => _totalGallons; set => _totalGallons = value; }
        public double TotalTripMiles { get => _totalTripMiles; set => _totalTripMiles = value; }
        public decimal PurchaseCost { get => _purchaseCost; set => _purchaseCost = value; }
        public DateTime PurchaseDate { get => _purchaseDate; set => _purchaseDate = value; }
        public PurchaseType PurchaseTypeId { get => _purchaseTypeId; set => _purchaseTypeId = value; }
        public GasType GasTypeId { get => _gasTypeId; set => _gasTypeId = value; }

        #endregion

        #region [ METHODS ]

        #endregion
    }
}
