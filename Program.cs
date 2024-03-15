using Bendecido.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddCors(options => 
{
    options.AddPolicy(name: "MyPolicy", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("MyPolicy");
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
