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
*	@notes: 
*	
*/
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace SITAzure.DAL
{
    public class VehicleCommentModel
    {
        #region [ CLASS FIELDS ]

        private int _commentId;
        private int _commentTypeId;
        private int _userId;
        private int _vehicleId;
        private string _comment;
        private DateTime _commentDate;


        #endregion

        #region [ CONSTRUCTOR ]

        public VehicleCommentModel()
        {
            _commentId = 0;
            _commentTypeId = 0;
            _userId = 0;
            _vehicleId = 0;
            _comment = string.Empty;
            _commentDate = DateTime.MinValue;
        }

        public VehicleCommentModel(int commentId, int commentTypeId, int userId, int vehicleId, string comment, DateTime commentDate)
        {
            _commentId = commentId;
            _commentTypeId = commentTypeId;
            _userId = userId;
            _vehicleId = vehicleId;
            _comment = comment ?? throw new ArgumentNullException(nameof(comment));
            _commentDate = commentDate;
        }


        #endregion

        #region [ PROPERTIES ]
        public int CommentId { get => _commentId; set => _commentId = value; }
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
