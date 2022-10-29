using Castle.DynamicProxy;
using HIM4DotNet5.Common;
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
        private string _exMsg;
        private AppLayer _appEnum;
        #endregion

        #region [ CONSTRUCTOR ]

        public SITAsyncInterceptor(ILogger logger, AppLayer appEnum, string exMsg)
        {
            _logger = logger;
            _appEnum = appEnum;
            _exMsg = exMsg;
            _logger.Debug("IN AsyncInterceptor ctor()...");
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
                Guid guid = this.GenerateExceptionId();
                this._logger.Error("Exception ID: {GUID}", guid.ToString());
                this._logger.Error(ex.ToString());
                throw new SITInternalAppException(guid, this._appEnum, this._exMsg);
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
                Guid guid = this.GenerateExceptionId();
                this._logger.Error("Exception ID: {GUID}", this.GenerateExceptionId().ToString());
                this._logger.Error(ex.ToString());
                throw new SITInternalAppException(guid, this._appEnum, this._exMsg);
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
                Guid guid = this.GenerateExceptionId();
                this._logger.Error("Exception ID: {GUID}", guid.ToString());
                this._logger.Error(ex.ToString());
                throw new SITInternalAppException(guid, this._appEnum, this._exMsg);
            }

            return result;
        }

        protected Guid GenerateExceptionId() { return Guid.NewGuid(); }

        #endregion

    }
}
