using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Filters
{
    public class AuthFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting( ActionExecutingContext context )
        {
            var session = context.HttpContext.Session;
            var username = session.GetString( "Username" );

            if ( string.IsNullOrEmpty( username ) )
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
            base.OnActionExecuting( context );
        }
    }
}
