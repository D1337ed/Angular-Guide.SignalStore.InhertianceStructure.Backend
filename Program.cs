using Angular.SignalStore.InheritanceStructure.Backend.Data;
using Angular.SignalStore.InheritanceStructure.Backend.Interfaces;
using Angular.SignalStore.InheritanceStructure.Backend.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<CpuDbContext>();
builder.Services.AddScoped<SCore>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors();
app.UseHttpsRedirection();

app.MapGet("/", () => "Get or set cpus");
app.MapGet("/all", (SCore s_Core) =>
{
    var cpus = s_Core.All();
    return cpus is not null ? Results.Ok(cpus) : Results.NotFound();
});
app.MapGet("/all/{id}", (SCore s_Core, int id) =>
{
    var cpu = s_Core.Get(id);
    return cpu is not null ? Results.Ok(cpu) : Results.NotFound();
});
app.MapPost("/new", (SCore s_Core, Cpu entity) =>
{
    bool result = s_Core.New(entity);
    return result ? Results.Ok("Created new cpu") : Results.BadRequest("Error occurred while trying to create new cpu");
});
app.MapPatch("/update/{id}", (SCore s_Core, int id, Cpu patch) =>
{
    var cpu = s_Core.Update(id, patch);
    return Results.Ok(cpu);
});
app.MapDelete("/delete/{id}", (SCore s_Core, int id) =>
{
    s_Core.Delete(id);
    return Results.Ok();
});
app.Run();
