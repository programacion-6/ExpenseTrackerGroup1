using System.Data;
using System.Data.Common;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.Database.Infrastructure;
using ExpenseTracker.Persistence.Database.Interface;
using ExpenseTracker.Repository;
using Npgsql;

namespace ExpenseTracker.Configuration;

public static class Config
{
    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration)
            .AddRepositories();
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
        return services;
    }
    
}