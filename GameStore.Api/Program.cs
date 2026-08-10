//Sezione di application configuration
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Sezione HTTP Request Pipeline
app.MapGet("/", () => "Hello World!");


app.Run();
