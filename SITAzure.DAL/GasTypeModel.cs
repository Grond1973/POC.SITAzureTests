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
namespace SITAzure.DAL
{
    public class GasTypeModel
    {
        #region [ CLASS FIELDS ]

        private int _gasTypeId;
        private string _gasTypeDesc;

        #endregion

        #region [ CONSTRUCTOR ]

        public GasTypeModel()
        {
            _gasTypeId = 0;
            _gasTypeDesc = String.Empty;
        }

        public GasTypeModel(int gasTypeId, string gasTypeDesc)
        {
            _gasTypeId = gasTypeId;
            _gasTypeDesc = gasTypeDesc ?? throw new ArgumentNullException(nameof(gasTypeDesc));
        }

        #endregion

        #region [ PROPERTIES ]
        public int GasTypeId { get => _gasTypeId; set => _gasTypeId = value; }
        public string GasTypeDesc { get => _gasTypeDesc; set => _gasTypeDesc = value; }

        #endregion

        #region [ METHODS ]

        public override string ToString()
        {
            return $"GasTypeId: {_gasTypeId} GasTypeDesc: {_gasTypeDesc}";
        }

        #endregion
    }
}
