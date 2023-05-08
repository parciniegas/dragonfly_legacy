using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using NLog;

namespace Dragonfly.Core.Mvc
{
    public class TraceAttribute : ActionFilterAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region MyRegion
        DateTime _start;
        private string _action;
        private string _controller;
        private string _params;
        #endregion

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _start = DateTime.Now;
            GetActionInfo(filterContext);
            var msg = $"Executing action {_action} on controller {_controller} with parameters {_params}";
            Logger.Trace(msg);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var stop = DateTime.Now;
            var elapsep = stop - _start;
            var msg = $"Executed action: {_action} on controller: {_controller}. Lapsed time: {elapsep}";
            Logger.Trace(msg);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var msg = $"Executing action result: {_action} on controller: {_controller}";
            Logger.Trace(msg);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var msg = $"Executed action result: {_action} on controller: {_controller}";
            Logger.Trace(msg);
        }

        private void GetActionInfo(ActionExecutingContext context)
        {
            _controller = context.ActionDescriptor.ControllerDescriptor.ControllerName;
            _action = context.ActionDescriptor.ActionName;
            _params = JsonConvert.SerializeObject(context.ActionParameters, Formatting.None);
        }
    }
}
