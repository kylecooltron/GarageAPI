var builder = WebApplication.CreateBuilder(args);

// Add CORS policy (Allow all origins, methods, headers for now, can be restricted later)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddControllers(); // This registers the controllers with the DI container
builder.Services.AddSingleton<IRoomService, RoomService>(); // Register the service

var app = builder.Build();

// Use the CORS policy defined above
app.UseCors("AllowAll");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
