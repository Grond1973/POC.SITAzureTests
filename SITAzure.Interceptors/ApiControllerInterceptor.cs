using Castle.DynamicProxy;
using Serilog;
/**
*
*	@author: Lawrence F. Sullivan
*
*	@date:	1-1-19
*
*	@purpose: API Controller Interceptor
* 
* 
*	@modifications: 
*	
*	@notes: 
*	
*/
namespace SITAzure.Interceptors
{
    public class ApiControllerInterceptor : IInterceptor
    {
        #region [ CLASS FIELDS ]

        private ILogger _logger;
        private static string EX_MSG = "Web API Exception";
        #endregion

        #region [ CONSTRUCTOR ]
        public ApiControllerInterceptor(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region [ PROPERTIES ]

        #endregion

        #region [ METHODS ]
        public void Intercept(IInvocation invocation)
        {
            try
            {
                if (invocation.Arguments != null && invocation.Arguments.Length > 0)
                {

                    this._logger.Information("Entering method: {MethodName} with parameters: {Parameters}", invocation.InvocationTarget.ToString() + "." + invocation.Method.Name
                                                , String.Join(", ", invocation.Arguments.Select(itm => (itm ?? "").ToString()).ToArray()));
                }
                else
                { this._logger.Information("Entering method: {MethodName}", invocation.Method.Name); }
                invocation.Proceed();

                this._logger.Information(invocation.Method.Name + " completed...result (if any) was: {MethodName}"
                                    , ((invocation.ReturnValue != null) ? invocation.ReturnValue : " NULL RETURN VALUE"));
            }
            catch (Exception ex)
            {
                Guid guid = this.GenerateExceptionId();
                this._logger.Error(ex, EX_MSG);
                //throw new SITInternalAppException(guid, AppLayer.WEB_API, EX_MSG);
                throw new Exception(EX_MSG);
            }
        }

        protected Guid GenerateExceptionId() { return Guid.NewGuid(); }

        #endregion
    }
}
