using Microsoft.AspNetCore.Mvc;
using SITAzure.Common;
using SITAzure.DAO;
using ILogger = Serilog.ILogger;

namespace SITAzure.Controllers
{
    [Route("api/GasType")]
    [ApiController]
    public class GasTypeController : SITApiBaseController
    {
        #region [ CLASS FIELDS ]

        #endregion

        #region [ CONSTRUCTOR ]

        public GasTypeController(ILogger logger, IRepositoryOperations multiRepoManager) : base(logger, multiRepoManager) {}

        #endregion

        #region [ PROPERTIES ]

        #endregion

        #region [ METHODS ]

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<GasTypeVO>>> GetGasTypes()
        {
            ActionResult actionResult = null;

            try
            {
                var gasTypes = await this._multiRepositoryManager.GetGasTypeVOsAsync();

                if(gasTypes?.Count > 0)
                { actionResult = new OkObjectResult(gasTypes); }
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
