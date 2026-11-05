using TrainingCenter.Api.Services;
using TrainingCenter.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<ICorporateEmailService, CorporateEmailService>();
builder.Services.AddScoped<IFallbackEmailService, FallbackEmailService>();
builder.Services.AddScoped<ILeadService, LeadService>();

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
