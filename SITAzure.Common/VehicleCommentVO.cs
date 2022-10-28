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
*	@notes: Should this class (and VehicleCommentModel) be derived from a base class Called VehicleComment base???
*	
*/
using System;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace SITAzure.Common
{
    public class VehicleCommentVO
    {
        #region [ CLASS FIELDS ]

        private int _commentId;
        private VehicleCommentType _commentType;
        private int _userId;
        private int _vehicleId;
        private string _comment;
        private DateTime _commentDate;

        #endregion

        #region [ CONSTRUCTOR ]

        public VehicleCommentVO()
        {
            _commentId = 0;
            _commentType = VehicleCommentType.UNKNOWN;
            _userId = 0;
            _vehicleId = 0;
            _comment = String.Empty;
            _commentDate = DateTime.MinValue;
        }

        public VehicleCommentVO(int commentId, VehicleCommentType commentType, int userId, int vehicleId, string comment, DateTime commentDate)
        {
            _commentId = commentId;
            _commentType = commentType;
            _userId = userId;
            _vehicleId = vehicleId;
            _comment = comment ?? throw new ArgumentNullException(nameof(comment));
            _commentDate = commentDate;
        }


        #endregion

        #region [ PROPERTIES ]

        public int CommentId { get => _commentId; set => _commentId = value; }
        public VehicleCommentType CommentType { get => _commentType; set => _commentType = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public int VehicleId { get => _vehicleId; set => _vehicleId = value; }
        public string Comment { get => _comment; set => _comment = value; }
        public DateTime CommentDate { get => _commentDate; set => _commentDate = value; }

        #endregion

        #region [ METHODS ]

        #endregion
    }
}
