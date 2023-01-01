using Minimal_Web_API.DataContext;
using Minimal_Web_API.Repositories;
using Minimal_Web_API.Services;
using NuGet.Protocol.Plugins;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PTTContext>();
builder.Services.AddTransient(typeof(TransportStopService));
builder.Services.AddTransient(typeof(TransportStopRepository));
builder.Services.AddTransient(typeof(UserService));
builder.Services.AddTransient(typeof(UserRepository));

//builder.Services.AddCors();

var app = builder.Build();

//app.UseCors(x => x
//               .AllowAnyMethod()
//               .AllowAnyHeader()
//               .SetIsOriginAllowed(origin => true) // allow any origin
//               .AllowCredentials()); // allow credentials


app.MapGet("/", () => "Hello World!");

app.MapGet("/users", async (UserService userService)
    => await userService.GetUsersAsync());
app.MapGet("/users/{login}/{password}", async (UserService userService, string login, string password)
    => await userService.GetUserByLoginAndPassword(login, password));

app.MapGet("/stops", async (TransportStopService pttService)
    => await pttService.GetTransportStops());

app.MapGet("/stops/{stopId}/timetable", async (TransportStopService pttService, string stopId)
    => await pttService.GetStopTimetableByStopId(stopId));

app.Run();