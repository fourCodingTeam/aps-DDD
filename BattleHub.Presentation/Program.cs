using BattleHub.Application.Interfaces;
using BattleHub.Application.Services;
using BattleHub.Domain.Interfaces;
using BattleHub.Infraestrutura.DI;
using BattleHub.Infraestrutura.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraestrutura(builder.Configuration);
builder.Services.AddScoped<ITorneioAppService, TorneioAppService>();
builder.Services.AddScoped<IParticipanteAppService, ParticipanteAppService>();
builder.Services.AddScoped<IChaveamento, ChaveamentoAleatorio>();
builder.Services.AddScoped<ITorneioAppService, TorneioAppService>();
builder.Services.AddScoped<IParticipanteAppService, ParticipanteAppService>();
builder.Services.AddScoped<IPartidaAppService, PartidaAppService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Torneios}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
