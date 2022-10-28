/***
*
*	@author: Lawrence F. F. Sullivan
*
*	@date:	October 13, 2022
*
*	@purpose: 
* 
* 
*	@modifications: 
*	
*	@notes: 
*	
*/
using Serilog;

namespace SITAzure.DAL
{
    public class GasPurchaseDataRepository :  SITRepositoryBase<GasPurchaseDataModel> 
    {
        #region [ CLASS FIELDS ]

        #endregion

        #region [ CONSTRUCTOR ]

        public GasPurchaseDataRepository(string connectionString, ILogger logger) : base(connectionString, logger) {}

        #endregion

        #region [ PROPERTIES ]

        #endregion

        #region [ METHODS ]

        #endregion

    }
}
