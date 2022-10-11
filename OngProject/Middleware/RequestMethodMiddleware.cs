using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Middleware
{
	public class RequestMethodMiddleware
	{    
		private readonly RequestDelegate _next;

		public RequestMethodMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			IList<string> httpMethods = new string[4] { "POST", "PATCH", "PUT", "DELETE" };
			string requestMethod = context.Request.Method;
			string requestPath = context.Request.Path;

			if (httpMethods.Contains(requestMethod) && !requestPath.Contains("Auth"))
			{
				var role = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

				if (!role.Value.Equals("Admin"))
					context.Response.StatusCode = 401;
			}

			await _next.Invoke(context);
		}
	}

	public static class RequestMethodMiddlewareExtensions
	{
		public static IApplicationBuilder UseRequestMethod(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<RequestMethodMiddleware>();
		}
	}
}
