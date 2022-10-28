using SITAzure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/***
*
*	@author: Lawrence F. F. Sullivan
*
*	@date:	October 15, 2022
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
    public class GasPurchaseDataModel
    {
        #region [ CLASS FIELDS ]

        private int _purchaseId;
        private int _carId;
        private int _userId;
        private double _totalGallons;
        private double _totalTripMiles;
        private decimal _purchaseCost;
        private DateTime _purchaseDate;
        private int _purchaseTypeId;
        private int _gasTypeId;

        #endregion

        #region [ CONSTRUCTOR ]


        public GasPurchaseDataModel()
        {
            _purchaseId = 0;
            _carId = 0;
            _userId = 0;
            _totalGallons = 0.0;
            _totalTripMiles = 0.0;
            _purchaseCost = 0M;
            _purchaseDate = DateTime.MinValue;
            _purchaseTypeId = 0;
            _gasTypeId = 0;
        }

        public GasPurchaseDataModel(int puchaseId, int carId, int userId, double totalGallons, double totalTripMiles,
                                    decimal purchaseCost, DateTime purchaseDate, int purchaseTypeId,
                                    int gasTypeId)
        {
            _purchaseId = puchaseId;
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

        public int PurchaseId { get => _purchaseId; set => _purchaseId = value; }
        public int CarId { get => _carId; set => _carId = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public double TotalGallons { get => _totalGallons; set => _totalGallons = value; }
        public double TotalTripMiles { get => _totalTripMiles; set => _totalTripMiles = value; }
        public decimal PurchaseCost { get => _purchaseCost; set => _purchaseCost = value; }
        public DateTime PurchaseDate { get => _purchaseDate; set => _purchaseDate = value; }
        public int PurchaseTypeId { get => _purchaseTypeId; set => _purchaseTypeId = value; }
        public int GasTypeId { get => _gasTypeId; set => _gasTypeId = value; }

        #endregion

        #region [ METHODS ]

        #endregion
    }
}
