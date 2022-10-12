using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OngProject.Middleware
{
    public class OwnerShipMiddleWare
    {
        private readonly RequestDelegate _next;

        public OwnerShipMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method == HttpMethod.Put.Method || httpContext.Request.Method == HttpMethod.Delete.Method)
            {
                var role = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                var claimId = httpContext.User.FindFirst("Identifier").Value;
                var paramId = (string)httpContext.Request.RouteValues["id"];
                if (paramId != null && paramId != "")
                {
                    var excludePaths = new List<string>() { "/users" };
                    var currentPath = httpContext.Request.Path.ToString().ToLower();
                    foreach (var path in excludePaths)
                    {
                        if (currentPath.Contains(path))
                        {
                            if (Int32.Parse(claimId) != Int32.Parse(paramId) && !role.Value.Equals("Administrator"))
                            {
                                httpContext.Response.StatusCode = 403;
                                return;
                            }
                        }
                    }
                }
            }
            await _next.Invoke(httpContext);
        }
    }
}