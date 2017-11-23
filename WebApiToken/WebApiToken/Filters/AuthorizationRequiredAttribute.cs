using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApiToken.Interfaces;

namespace WebApiToken.Filters
{
    public class AuthorizationRequiredAttribute:ActionFilterAttribute
    {
        private const string Token = "token";
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ITokenServices tokenServices = new TokenServices();
            if (actionContext.Request.Headers.Contains(Token))
            {
                var tokenValue = actionContext.Request.Headers.GetValues(Token).First();
                if (!tokenServices.ValidateToken(tokenValue))
                {
                    var responseMessage =
                        new HttpResponseMessage(HttpStatusCode.Unauthorized) {ReasonPhrase = "Invalid Request"};
                    actionContext.Response = responseMessage;
                }
                base.OnActionExecuting(actionContext);
            }
        }
    }
}