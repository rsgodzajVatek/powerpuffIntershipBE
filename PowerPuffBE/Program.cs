using Microsoft.EntityFrameworkCore;
using PowerPuffBE;
using PowerPuffBE.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureServices();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddDbContext<PowerPuffDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PowerPuffDatabase")));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "PowerPuff API", Version = "v1" });
    
    c.MapType<IFormFile>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Format = "binary"
    });
});

var app = builder.Build();

// using (var serviceScope = app.Services.CreateScope())
// {
//     var context = serviceScope.ServiceProvider.GetRequiredService<PowerPuffDbContext>();
//     context.Database.Migrate();
// }

//Database Seed
ServicesContainer.SeedDatabase(app);

app.MapControllers();
app.UseCors("CorsPolicy");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.Run();