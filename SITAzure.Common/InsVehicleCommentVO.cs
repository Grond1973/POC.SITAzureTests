/***
*
*	@author: Lawrence F. F. Sullivan
*
*	@date:	October 27, 2022
*
*	@purpose: 
* 
* 
*	@modifications: 
*	
*	@notes: TODO: should this class derive from some sort of vehicle model base class also???
*	
*/
using System;

namespace SITAzure.Common
{
    public class InsVehicleCommentVO
    {
        #region [ CLASS FIELDS ]

        private int _commentTypeId;
        private int _userId;
        private int _vehicleId;
        private string _comment;
        private DateTime _commentDate;

        #endregion

        #region [ CONSTRUCTOR ]

        public InsVehicleCommentVO()
        {
            _commentTypeId = 0;
            _userId = 0;
            _vehicleId = 0;
            _comment = String.Empty;
            _commentDate = DateTime.MinValue;
        }

        public InsVehicleCommentVO(int commentTypeId, int userId, int vehicleId, string comment, DateTime commentDate)
        {
            _commentTypeId = commentTypeId;
            _userId = userId;
            _vehicleId = vehicleId;
            _comment = comment ?? throw new ArgumentNullException(nameof(comment));
            _commentDate = commentDate;
        }

        #endregion

        #region [ PROPERTIES ]

        public int CommentTypeId { get => _commentTypeId; set => _commentTypeId = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public int VehicleId { get => _vehicleId; set => _vehicleId = value; }
        public string Comment { get => _comment; set => _comment = value; }
        public DateTime CommentDate { get => _commentDate; set => _commentDate = value; }

        #endregion

        #region [ METHODS ]

        #endregion
    }
}
