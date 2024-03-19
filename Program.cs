using Bendecido.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


//builder.Services.AddCors(options => 
//{
//    options.AddPolicy(name: "MyPolicy", policy =>
//    {
//        policy.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod();
//    });
//});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();


app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefoultConnection");

    builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));
}
