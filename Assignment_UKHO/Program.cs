using Assignment_UKHO.Data;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen(x =>
{
    x.EnableAnnotations();
   
});
builder.Services.AddSwaggerGen();

#region " Serilog"
var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
#endregion
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();


string connectionStringName = "connectionString";
var url = builder.Configuration["AzureKeyVault:url"];
var client = new SecretClient(new Uri(url), new DefaultAzureCredential());
var connectionString = await client.GetSecretAsync(connectionStringName);


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString.Value.Value));


var app = builder.Build();

// configure the http request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Assignment UKHO API"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
