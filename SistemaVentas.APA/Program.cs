using SistemaVentas.IOC;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Nos permite crear metodos en objetos ya creados en la app, este m�todo se creo en IOC-DEPENDENCIAS
//AL PASAR EL THIS IServiceCollection, PODEMOS HACER ESTOS (M�TODOS DE EXTENSION)
builder.Services.InyectarDependencias(builder.Configuration);


//Acticar Cors, para poder tener acceso al API desde la aplicaci�n Angular, sin tener problemas
// de URL
// Agrega servicios de CORS (Cross-Origin Resource Sharing) al contenedor de dependencias.
builder.Services.AddCors(options =>
{
    // Define una pol�tica de CORS personalizada llamada "NuevaPolitica".
    options.AddPolicy("NuevaPolitica", app =>
    {
        // Permite solicitudes desde cualquier origen (dominio). 
        // �Atenci�n! Esto puede ser inseguro en producci�n.
        app.AllowAnyOrigin()

        // Permite cualquier encabezado HTTP en las solicitudes (por ejemplo, Authorization, Content-Type).
        .AllowAnyHeader()

        // Permite cualquier m�todo HTTP (GET, POST, PUT, DELETE, etc.).
        .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");


app.UseAuthorization();

app.MapControllers();

app.Run();
