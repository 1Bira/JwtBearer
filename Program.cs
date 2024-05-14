using JwtBearer.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

var app = builder.Build();


app.MapGet("/", (TokenService service)
=> service.Generate(new JwtBearer.Models.User(1,"walter@gmail.com","walter", ["standard","premium"])));

app.Run();
