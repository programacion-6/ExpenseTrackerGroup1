var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar los controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ExpenseTracker API v1");
        options.RoutePrefix = string.Empty; // Swagger está disponible en la raíz
    });
}

app.UseHttpsRedirection();

// Registrar los controladores
app.MapControllers();

app.Run();