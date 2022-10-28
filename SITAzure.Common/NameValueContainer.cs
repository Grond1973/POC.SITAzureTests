/***
 *
 *	@author: Lawrence F. Sullivan
 *
 *	@date:	3-22-2012
 *
 *	@purpose: simple data storage class for widgets like
 *	            dropdown lists, etc.
 * 
 * 
 *	@modifications:
 *	                
 *
 *	@notes: copied from SIT.Util on 11-30-19
 *	        moved to .NET Standard Library on 11-14-20
 *
 *
 */
namespace SITAzure.Common
{
    public class NameValueContainer<T>
    {
        #region [ MEMBER VARIABLES ]

        private string _key;
        private T _value;

        #endregion

        #region [ CONSTRUCTORS ]
        public NameValueContainer() { }

        public NameValueContainer(string key, T value)
        {
            this._key = key;
            this._value = value;
        }

        #endregion

        #region [ PROPERTIES ]

        /// <summary>
        /// the string used to identify the value
        /// stored in this data structure
        /// </summary>
        public string Key
        {
            get { return this._key; }
            set { this._key = value; }
        }

        /// <summary>
        /// the data item stored within the container
        /// which is referenced by the string Key
        /// </summary>
        public T Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        #endregion

        #region [ METHODS ]

        public override string ToString()
        {
            return this._key;
        }

        #endregion
    }
}
