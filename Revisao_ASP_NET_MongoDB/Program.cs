using Microsoft.EntityFrameworkCore;

using Revisao_ASP_NET_MongoDB.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração necessária para "enganar" a aplicação.

builder.Services.AddDbContext<PseudoContextMongoDB>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("PseudoContextMongoDB") ?? throw new InvalidOperationException("Connection string 'PseudoContextMongoDB' not found."));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Variáveis de configuração para utilização do MongoDB na aplicação.

ContextMongoDB.Connection_String = builder.Configuration.GetSection("MongoDBConnection:ConnectionString").Value;

ContextMongoDB.Database_Name = builder.Configuration.GetSection("MongoDBConnection:DatabaseName").Value;

ContextMongoDB.Is_Ssl = Convert.ToBoolean(builder.Configuration.GetSection("MongoDBConnection:IsSsl").Value);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
