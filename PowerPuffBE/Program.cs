using Microsoft.EntityFrameworkCore;
using PowerPuffBE;
using PowerPuffBE.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureServices();

builder.Services.AddDbContext<PowerPuffDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PowerPuffDatabase")));

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<PowerPuffDbContext>();
    context.Database.Migrate();
}

//Database Seed
ServicesContainer.SeedDatabase(app);

app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();