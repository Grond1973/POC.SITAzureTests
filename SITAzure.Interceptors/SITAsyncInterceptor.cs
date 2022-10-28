using Castle.DynamicProxy;
using Serilog;
/***
*
*	@author: Lawrence F. Sullivan
*
*	@date: 8-27-19/11-12-19/12-2-21/10-28-22
*
*	@purpose: DATA ACCESS LAYER ASYNCHRONOUS INTERCEPTOR
* 
*	@modifications: 
*	
*	@notes: updated to implement IAsyncInterceptor   
*	
*/
namespace SITAzure.Interceptors
{
    public class SITAsyncInterceptor : IAsyncInterceptor
    {
        #region [ CLASS FIELDS ]

        private ILogger _logger;
        private string _environmentTag;
        #endregion

        #region [ CONSTRUCTOR ]

        public SITAsyncInterceptor(ILogger logger, string environmentTag)
        {
            _logger = logger;
            _environmentTag = environmentTag;
        }

        #endregion

        #region [ PROPERTIES ]

        #endregion

        #region [ METHODS ]
        public void InterceptAsynchronous(IInvocation invocation)
        {
            this._logger.Information("In Method InterceptAsynchronous()...");
            invocation.ReturnValue = this._internalInterceptAsynchronous(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            this._logger.Information("In Method InterceptAsynchronous<TResult>()...");
            invocation.ReturnValue = this._internalInterceptAsynchronous<TResult>(invocation);
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            this._logger.Information("In Method InterceptSynchronous()...");

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
                Guid guid = Guid.NewGuid();
                this._logger.Error(ex, _environmentTag);
                throw new Exception(_environmentTag);
            }
        }

        private async Task _internalInterceptAsynchronous(IInvocation invocation)
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
                var task = (Task)invocation.ReturnValue;
                await task;

                this._logger.Information(invocation.Method.Name + " completed...result (if any) was: {MethodName}"
                                    , ((invocation.ReturnValue != null) ? invocation.ReturnValue : " NULL RETURN VALUE"));
            }
            catch (Exception ex)
            {
                Guid guid = Guid.NewGuid();
                this._logger.Error(ex, _environmentTag);
                throw new Exception(_environmentTag);
            }
        }

        private async Task<TResult> _internalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            TResult result = default(TResult);

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
                var task = (Task<TResult>)invocation.ReturnValue;
                result = await task;

                this._logger.Information(invocation.Method.Name + " completed...result (if any) was: {MethodName}"
                                    , ((invocation.ReturnValue != null) ? invocation.ReturnValue : " NULL RETURN VALUE"));
            }
            catch (Exception ex)
            {
                Guid guid = Guid.NewGuid();
                this._logger.Error(ex, _environmentTag);
                throw new Exception(_environmentTag);
            }

            return result;
        }

        #endregion

    }
}
