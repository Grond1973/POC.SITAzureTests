/***
*
*	@author: Lawrence F. F. Sullivan
*
*	@date:	October 12, 2022
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
    public class CarRepository : SITRepositoryBase<CarDataModel>
    {
        #region [ CLASS FIELDS ]

        #endregion

        #region [ CONSTRUCTOR ]

        public CarRepository(string connectionString, ILogger logger) : base(connectionString, logger) {}

        #endregion

        #region [ PROPERTIES ]

        #endregion

        #region [ METHODS ]

        #endregion

    }
}
