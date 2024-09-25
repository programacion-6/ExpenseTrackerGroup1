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
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.ConnectionStrings));
        services.AddScoped<IDbConnection>(sp =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection"); 
            return new NpgsqlConnection(connectionString); 
        });
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped<IDbInit, DbInit>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        //services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        //services.AddScoped<IIncomeRepository, IncomeRepository>();
        return services;
    }
    
}