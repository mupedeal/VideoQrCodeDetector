using ConsultaAnaliseApplication.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:9090");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigurarConsultarAnaliseService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
