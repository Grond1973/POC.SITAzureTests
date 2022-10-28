using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SITAzure.Common;
using SITAzure.DAO;
using ILogger = Serilog.ILogger;
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
namespace SITAzure.Controllers
{
    [Route("api/Car")]
    [ApiController]
    public class CarController : SITApiBaseController
    {
        #region [ CLASS FIELDS ]

        #endregion

        #region [ CONSTRUCTOR ]
        public CarController(ILogger logger, IRepositoryOperations multiRepoManager) : base(logger, multiRepoManager) {}

        #endregion

        #region [ PROPERTIES ]

        #endregion

        #region [ METHODS ]

        [HttpGet("GetActiveCars/{userId}")]
        public virtual async Task<ActionResult<IEnumerable<CarVO>>> GetActiveCarsByUserId([FromRoute] int userId)
        {
            ActionResult actionResult = null;

            try
            {
                var cars = await this._multiRepositoryManager.GetActiveCarsByUserIdAsync(userId);
                
                if(cars?.Count > 0)
                { actionResult = new OkObjectResult(cars); }
                else
                { actionResult = new NotFoundResult(); }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return actionResult;
        }

        #endregion

    }
}
