using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IMDB.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public bool Disable { get; set; } //parameter passed to filter i.e. [ValidateModel(Disable = false)]


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (Disable) return;

            if (!context.ModelState.IsValid) context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
