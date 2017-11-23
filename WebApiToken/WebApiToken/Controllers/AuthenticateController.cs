using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiToken.Interfaces;
using WebApiToken.Models;

namespace WebApiToken.Controllers
{
    public class AuthenticateController : ApiController
    {
        private ITokenServices _tokenServices;

        public AuthenticateController()
        {
            _tokenServices = new TokenServices();
        }
        [HttpPost]
        public HttpResponseMessage Authenticate(string username, string password)
        {
            webinarModel webinar = new webinarModel();
            var user = webinar.Users.First(x => x.Username == username && x.Password == password);
            if (user != null)
            {
                return GetAuthToken(user.UserId);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Not authorized");
        }

        private HttpResponseMessage GetAuthToken(int userId)
        {
            var token = _tokenServices.GenerateToken(userId);
            var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");
            response.Headers.Add("Token",token.AuthToken);
            response.Headers.Add("TokenExpiry","5");
            response.Headers.Add("Access-Control-Expose-Headers","Token,TokenExpiry");
            return response;
        }
    }
}
