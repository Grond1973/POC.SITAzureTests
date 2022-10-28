using Microsoft.AspNetCore.Mvc;
using Serilog;
using SITAzure.Common;
using SITAzure.DAO;
using ILogger = Serilog.ILogger;
/***
*
*	@author: Lawrence F. F. Sullivan
*
*	@date:	October 23, 2022
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
    [Route("api/GasPurchaseData")]
    [ApiController]
    public class GasPurchaseDataController : SITApiBaseController
    {
        #region [ CLASS FIELDS ]

        #endregion

        #region [ CONSTRUCTOR ]

        public GasPurchaseDataController(ILogger logger, IRepositoryOperations multiRepoManager) : base(logger, multiRepoManager)
        {
        }

        #endregion

        #region [ PROPERTIES ]

        #endregion

        #region [ METHODS ]

        [HttpGet("GetPurchaseDataByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<GasPurchaseDataVO>>> GetPurchaseDataByUserId([FromRoute] int userId)
        {
            var results = await this._multiRepositoryManager.GetGasPurchaseDataListByUserIdAsync(userId);

            return new OkObjectResult(results);
        }
        #endregion

    }
}
