var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

Dictionary<int, string> countries = new Dictionary<int, string>()
{
 { 1, "United States" },
 { 2, "Canada" },
 { 3, "United Kingdom" },
 { 4, "India" },
 { 5, "Japan" }
};



app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("country", async (context) =>
    {
        foreach (var country in countries)
        {
            context.Response.WriteAsync($"{country.Key} , {country.Value}\n");
        }

    });

    endpoints.MapGet("country/{myId:int:range(1,100)}", async (context) =>
    {
        
        int id = Convert.ToInt32(context.Request.RouteValues["myId"]);

        if (countries.ContainsKey(id))
        {
            await context.Response.WriteAsync($"{countries[id]}\n");
        }
        else 
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("[No Country]\n");
        }
    });

    endpoints.MapGet("country/{myId:int:min(101)}", async (context) =>
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync($"Id should be between 1 and 100\n");
    });

});

app.Run(async context =>
{
    await context.Response.WriteAsync("No response");
});

app.Run();
