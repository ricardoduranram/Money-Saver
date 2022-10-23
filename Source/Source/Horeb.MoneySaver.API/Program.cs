using AutoMapper;
using Horeb.MoneySaver.API.Services.MappingServices;
using Horeb.MoneySaver.Persistency;
using Horeb.MoneySaver.Service;
using Horeb.MoneySaver.Service.BookkeepingModule;
using Horeb.MoneySaver.Service.PeriodModule;
using Horeb.MoneySaver.Service.SettingsModule;
using Horeb.Service;
using Horeb.Service.WalletModule;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .Build();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

MapperConfiguration config = new (config => {
    config.AddProfile(new RepositoryMappingProfile());
    config.AddProfile(new ControllerMappingProfile());
});
builder.Services.AddSingleton<IMapper>(config.CreateMapper());
builder.Services.AddTransient<IBookkeepingService, BookkeepingService>();
builder.Services.AddTransient<IBalanceStatementService, BalanceStatementService>();
builder.Services.AddTransient<IPeriodService, MonthlyPeriodService>();
builder.Services.AddTransient<ICategoryService, TransactionCategoryService>();
builder.Services.AddTransient<IWalletService, WalletService>();
builder.Services.AddTransient<IAppSettingsService, AppSettingsService>();
builder.Services.AddTransient<IIterationTimeService, IterationTimeService>();

var connectionString = configuration.GetConnectionString("PortalContext");
builder.Services.AddDbContext<DapDbContext>(options =>
    options.UseSqlServer(connectionString));

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
