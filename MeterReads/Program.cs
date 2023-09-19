using Microsoft.EntityFrameworkCore;
using MeterReads.Data;
using MeterReads.Services;
using MeterReads.Validators;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IMeterReadValidator, MeterReadValueValidator>();
builder.Services.AddScoped<IMeterReadValidator, MeterReadAlreadyExistsValidator>();
builder.Services.AddScoped<IMeterReadValidator, MeterReadHasValidCustomerAccountValidator>();
builder.Services.AddScoped<IMeterReadValidator, MeterReadIsNewerValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<DbContextClass>();
//var secret = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<DbContextClass>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

//builder.Services.AddDbContext<DbContextClass>(options => options.UseSqlServer(Configuration["DefaultConnection"]));
builder.Services.AddScoped<IMeterReadFileService, MeterReadFileService>();
//builder.Services.AddScoped<ICSVService, CsvService>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();