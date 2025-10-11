namespace GettingStartedAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            app.Run(async (HttpContext context) =>
            {
                if (context.Request.Method == "GET" & context.Request.Path == "/")
                {
                    int num1=0,num2=0;
                    string? operation;
                    long? result = null;


                    if (context.Request.Query.ContainsKey("num1"))
                    {
                        string num1Str = context.Request.Query["num1"][0]!;
                        if (!string.IsNullOrEmpty(num1Str))
                        {
                            num1 = int.Parse(num1Str);
                        }
                        else
                        {
                            context.Response.StatusCode = 400;
                            await context.Response.WriteAsync("Invalid input for 'num1'\n");

                        }
                    } 
                    else
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid input for 'num1'\n");
                    }


                    if (context.Request.Query.ContainsKey("num2"))
                    {
                        string num2Str = context.Request.Query["num2"][0]!;
                        if (!string.IsNullOrEmpty(num2Str))
                        {
                            num2 = int.Parse(num2Str);
                        }
                        else
                        {
                            context.Response.StatusCode = 400;
                            await context.Response.WriteAsync("Invalid input for 'num2'\n");
                        }
                    }
                    else
                    {
                        if(context.Response.StatusCode != 400)
                        {
                            context.Response.StatusCode = 400;
                        }
                        await context.Response.WriteAsync("Invalid input for 'num2'\n");
                    }

                   if (context.Request.Query.ContainsKey("operation"))
                   {
                        operation = context.Request.Query["operation"][0]!;
                        if (!string.IsNullOrEmpty(operation))
                        {
                            if (context.Response.StatusCode == 200)
                            {
                                switch (operation)
                                {
                                    case "add":
                                        result = num1 + num2;
                                        break;
                                    case "sub":
                                        result = num1 - num2;
                                        break;
                                    case "mul":
                                        result = num1 * num2;
                                        break;
                                    case "div":
                                        result = num1 / num2;
                                        break;
                                    case "mod":
                                        result = num1 % num2;
                                        break;
                                    default:
                                        if (context.Response.StatusCode != 400)
                                        {
                                            context.Response.StatusCode = 400;
                                        }
                                        await context.Response.WriteAsync("Invalid input for 'operation'\n");
                                        break;
                                }

                            }
                        }
                        else
                        {
                            if (context.Response.StatusCode != 400)
                            {
                                context.Response.StatusCode = 400;
                            }
                            await context.Response.WriteAsync("Invalid input for 'operation'\n");
                        }

                        if (result.HasValue)
                        {
                            await context.Response.WriteAsync(result.ToString()!);
                        }
                        }
                        else
                        {
                        if (context.Response.StatusCode != 400)
                        {
                            context.Response.StatusCode = 400;
                        }
                        await context.Response.WriteAsync("Invalid input for 'operation'\n");
                        }
                    

                }
            });

            app.Run();
        }
    }
}
