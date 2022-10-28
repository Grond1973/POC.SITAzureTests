using Microsoft.AspNetCore.Mvc;
using Serilog;
using SITAzure.DAO;
using ILogger = Serilog.ILogger;
/***
*
*	@author: Lawrence F. F. Sullivan
*
*	@date:	
*
*	@purpose: 
* 
* 
*	@modifications: 
*	
*	@notes: 
*	
*/
namespace SITAzure.Controllers
{
    public class SITApiBaseController : ControllerBase
    {
        #region [ CLASS FIELDS ]

        protected ILogger _logger;
        protected IRepositoryOperations _multiRepositoryManager;

        #endregion

        #region [ CONSTRUCTOR ]

        public SITApiBaseController(ILogger logger, IRepositoryOperations multiRepoManager)
        {
            _logger = logger;
            _multiRepositoryManager = multiRepoManager;
            _logger.Information("In SITApiBase ctor()...");

        }

        #endregion

        #region [ PROPERTIES ]

        #endregion

        #region [ METHODS ]

        #endregion
    }
}
