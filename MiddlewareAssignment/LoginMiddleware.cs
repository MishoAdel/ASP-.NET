using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace MiddlewareAssignment
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.Request.Path == "/" & httpContext.Request.Method == "POST")
            {
                string? email=null,password =null;
                StreamReader reader = new StreamReader(httpContext.Request.Body);
                string body = await reader.ReadToEndAsync();

                Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

                if (queryDict.ContainsKey("email")){
                    email = Convert.ToString( queryDict["email"][0]!);
                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync("email input is invalid\n");
                }


                if (queryDict.ContainsKey("password"))
                {
                    password = Convert.ToString(queryDict["password"][0]!);
                }
                else
                {
                    if(httpContext.Response.StatusCode != 400)
                    {
                        httpContext.Response.StatusCode = 400;
                    }
                    await httpContext.Response.WriteAsync("password input is invalid\n");
                }

                bool loggedIn = false;
                const string validEmail = "me@gmail.com", validPassword = "1234";
            
                if(email == validEmail & password == validPassword)
                {
                    loggedIn = true;
                }

                if (loggedIn)
                {
                    await httpContext.Response.WriteAsync("Logged in\n");
                }
                else
                {
                    if (httpContext.Response.StatusCode != 400)
                    {
                        httpContext.Response.StatusCode = 400;
                    }
                    await httpContext.Response.WriteAsync("Invalid login");
                }

            }
            else
            {
                await _next(httpContext);
            }



        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
