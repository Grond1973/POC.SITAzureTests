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
using System;

namespace SITAzure.Common
{
    public class GasTypeVO
    {
        #region [ CLASS FIELDS ]

        private int _gasTypeId;
        private string _description;

        #endregion

        #region [ CONSTRUCTOR ]

        public GasTypeVO()
        {
            _gasTypeId = 0;
            _description = String.Empty;
        }

        public GasTypeVO(int gasTypeId, string description)
        {
            _gasTypeId = gasTypeId;
            _description = description ?? throw new ArgumentNullException(nameof(description));
        }


        #endregion

        #region [ PROPERTIES ]

        public int GasTypeId { get => _gasTypeId; set => _gasTypeId = value; }
        public string Description { get => _description; set => _description = value; }

        #endregion

        #region [ METHODS ]

        public override string ToString()
        { return $"GasTypeId: {_gasTypeId} Description: {_description}";  }

        #endregion
    }
}
