using Api.Services.SentezService.Abstract;
using Api.Services.SentezService.Concrete;
using Api.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddTransient<ISentezService, SentezService>();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(config =>
{
    config.OperationFilter<SwaggerAddHeader>();
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SentezConnect API V1");
    });

}

app.UseAuthorization();

app.MapControllers();

app.Run();
