using TimeAngleAPI;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/CalculateTimeAngle", TimeAngleEndpoints.CalculateTimeAngle);

app.Run();