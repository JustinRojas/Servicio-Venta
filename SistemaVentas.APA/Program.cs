using SistemaVentas.IOC;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Nos permite crear metodos en objetos ya creados en la app, este método se creo en IOC-DEPENDENCIAS
//AL PASAR EL THIS IServiceCollection, PODEMOS HACER ESTOS (MÉTODOS DE EXTENSION)
builder.Services.InyectarDependencias(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
