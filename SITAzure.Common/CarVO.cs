using System;
/***
*
*	@author: Lawrence F. F. Sullivan
*
*	@date:	October 26, 2022
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
    public class CarVO
    {
        #region [ CLASS FIELDS ]

        private int _carId;
        private string _carModel;
        private string _carMake;
        private int _userId;
        private bool _isActive;

        #endregion

        #region [ CONSTRUCTOR ]

        public CarVO()
        {
            _carId = 0;
            _carModel = String.Empty;
            _carMake = String.Empty;
            _userId = 0;
            _isActive = false;
        }

        public CarVO(int carId, string carModel, string carMake, int userId, bool isActive)
        {
            _carId = carId;
            _carModel = carModel ?? throw new ArgumentNullException(nameof(carModel));
            _carMake = carMake ?? throw new ArgumentNullException(nameof(carMake));
            _userId = userId;
            _isActive = isActive;
        }


        #endregion

        #region [ PROPERTIES ]
        public int CarId { get => _carId; set => _carId = value; }
        public string CarModel { get => _carModel; set => _carModel = value; }
        public string CarMake { get => _carMake; set => _carMake = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }

        #endregion

        #region [ METHODS ]

        public override string ToString()
        {
            return $"CarId: {_carId} CarModel: {_carModel} CarMake: {_carMake} UserId: {_userId} IsActive {_isActive}";
        }

        #endregion
    }
}
