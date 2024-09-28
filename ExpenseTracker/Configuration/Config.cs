using System.Data;
using System.Data.Common;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.Service;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.Database.Infrastructure;
using ExpenseTracker.Persistence.Database.Interface;
using ExpenseTracker.Repository;
using ExpenseTracker.Service;
using ExpenseTracker.Services;
using Npgsql;

namespace ExpenseTracker.Configuration;

public static class Config
{
    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.
            AddDatabase(configuration)
            .AddRepositories()
            .AddServices();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbOptions>(configuration.GetSection(DbOptions.ConnectionStrings));
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped<IDbInit, DbInit>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBudgetService, BudgetService>();
        services.AddScoped<IGoalService, GoalService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IIncomeService, IncomeService>();
        return services;
    }
}