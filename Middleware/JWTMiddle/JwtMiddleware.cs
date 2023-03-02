using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AppServices.JWTRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Obj_Common;

namespace Middleware.JWTMiddle
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTRepositories jWTManager;

        public JwtMiddleware(RequestDelegate next, JWTRepositories jWTManager)
        {
            _next = next;
            this.jWTManager = jWTManager;
        }

        public void ReturnUnauthorized(HttpContext context) {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return;
        }

        public async Task Invoke(HttpContext context)
        {
            var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (accessToken == null) ReturnUnauthorized(context);

            try
            {
                // Validate the access token
                var principal = new ClaimsPrincipal();
                var accessToken_isValid = jWTManager.ValidateToken(accessToken, ref principal);

                // Access token has expired
                if (!accessToken_isValid)
                {
                    var refreshToken = context.Request.Headers["refreshToken"].FirstOrDefault();
                    if (refreshToken == null) ReturnUnauthorized(context);

                    // Validate the refresh token
                    var principal_refreshToken = new ClaimsPrincipal();
                    var refreshToken_isValid = jWTManager.ValidateToken(refreshToken, ref principal_refreshToken);

                    if (!refreshToken_isValid) ReturnUnauthorized(context);

                    // Renew the access token using the refresh token
                    var sid = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
                    var role = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                    Payload data = new Payload
                    {
                        AccountId = sid,
                        Role = (Role)Enum.Parse(typeof(Role), role)
                    };

                    var newAccessToken = jWTManager.GenerateAccessToken(data);
                    context.Response.Headers.Add("Authorization", "Bearer " + newAccessToken);
                }        
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
            
            await _next(context);
        }
    }

}
