using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;
using SITAzure.Common;
using SITAzure.DAO;
using ILogger = Serilog.ILogger;

namespace SITAzure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleCommentController : SITApiBaseController
    {
        #region [ CLASS FIELDS ]

        #endregion

        #region [ CONSTRUCTOR ]
        public VehicleCommentController(ILogger logger, IRepositoryOperations multiRepoManager) : base(logger, multiRepoManager) {}

        #endregion

        #region [ PROPERTIES ]

        #endregion

        #region [ METHODS ]

        [HttpGet("GetVehicleComment/{userId}/{commentId}", Name = "GetVehicleComment")]
        public virtual async Task<ActionResult<VehicleCommentVO>> GetVehicleCommentByUserAndCommentId([FromRoute] int userId, [FromRoute] int commentId)
        {
            ActionResult actionResult = null;
            VehicleCommentVO commentVO = null;

            try
            {
                commentVO = await this._multiRepositoryManager.GetVehicleCommentByUserIdAndCommentIdAsync(userId, commentId);
                if(commentVO?.CommentId > 0)
                {
                    actionResult = new OkObjectResult(commentVO);
                }
                else
                { actionResult = new NotFoundResult(); }
            }
            catch(Exception ex)
            {
                this._logger.Error(ex.ToString());
                actionResult = new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }

            return actionResult;
        }

        [HttpPost("Create")]
        public virtual async Task<ActionResult<VehicleCommentVO>> CreateVehicleComment(InsVehicleCommentVO vehicleCommentVO)
        {
            ActionResult actionResult = null;
            int newRecordId = 0;
            VehicleCommentVO newVehicleCommentVO = null;

            try
            {
                newRecordId = await this._multiRepositoryManager.InsertVehicleCommentAsync(vehicleCommentVO);

                /*
                 * TODO: clean this up!!!
                 * Check newRecordId > 0
                 */
                newVehicleCommentVO = new VehicleCommentVO(newRecordId,
                                                            (VehicleCommentType)vehicleCommentVO.CommentTypeId,
                                                            vehicleCommentVO.UserId,
                                                            vehicleCommentVO.VehicleId,
                                                            vehicleCommentVO.Comment,
                                                            vehicleCommentVO.CommentDate);

                actionResult = new CreatedAtRouteResult("GetVehicleComment",
                                                        new
                                                        {
                                                            userId = vehicleCommentVO.UserId,
                                                            commentId = newRecordId
                                                        },
                                                        newVehicleCommentVO);

            }
            catch(Exception ex)
            { actionResult = StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }

            return actionResult;
        }

        [HttpGet("GetYearList/{userId}")]
        public virtual async Task<ActionResult<IEnumerable<int>>> GetDistinctYearList([FromRoute] int userId)
        {
            ActionResult actionResult = null;
            IReadOnlyList<int> years = null;

            try
            {
                years = await this._multiRepositoryManager.GetVehicleCommentYearListAsync(userId);

                if(years?.Count > 0)
                { actionResult = new OkObjectResult(years); }
                else
                { actionResult = new NotFoundResult(); }
            }
            catch (Exception ex)
            { actionResult = StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }


            return actionResult;
        }

        #endregion

    }
}
