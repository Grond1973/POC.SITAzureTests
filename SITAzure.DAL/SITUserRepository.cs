using Serilog;
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
namespace SITAzure.DAL
{
    public class SITUserRepository : SITRepositoryBase<SITUser>
    {
        #region [ CLASS FIELDS ]

        #endregion

        #region [ CONSTRUCTOR ]

        public SITUserRepository(string connectionString, ILogger logger) : base(connectionString, logger) { }

        #endregion

        #region [ PROPERTIES ]

        #endregion

        #region [ METHODS ]

        #endregion

    }
}
