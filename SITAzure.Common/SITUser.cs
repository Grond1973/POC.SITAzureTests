/***
*
*	@author: Lawrence F. F. Sullivan
*
*	@date:	October 8, 2022
*
*	@purpose: 
* 
* 
*	@modifications: 
*	
*	@notes: 
*	
*/
namespace SITAzure.Common
{
    public class SITUser
    {
        #region [ CLASS FIELDS ]

        private int _userId;
        private string _userName;
        private string _password;
        private bool _isAdmin;


        #endregion

        #region [ CONSTRUCTOR ]

        #endregion

        #region [ PROPERTIES ]
        public int UserId { get => _userId; set => _userId = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public bool IsAdmin { get => _isAdmin; set => _isAdmin = value; }

        #endregion

        #region [ METHODS ]

        public override string ToString()
        {
            return $"UserId: {_userId} Username: {_userName} Is Admin: {_isAdmin}";
        }

        #endregion
    }
}
