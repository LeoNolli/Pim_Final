using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Localization;
using MySql.Data.MySqlClient;
using PIM_Fazenda_Urbana.Interfaces;
using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Repository;
using PIM_Fazenda_Urbana.Services;
using System.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo> { cultureInfo };
    options.DefaultRequestCulture = new RequestCulture(cultureInfo);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Configura��o de autentica��o por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Autenticacao/Login"); // Redireciona para Login em caso de 401
        options.AccessDeniedPath = new PathString("/Autenticacao/Login"); // Redireciona para Login em caso de 403
    });

// Configura��o da conex�o com o banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<IDbConnection>((sp) => new MySqlConnection(connectionString));

// Configura��o de reposit�rios e servi�os
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IInsumoRepository, InsumoRepository>();
builder.Services.AddScoped<IInsumoService, InsumoService>();

builder.Services.AddScoped<IControleProducaoRepository, ControleProducaoRepository>();
builder.Services.AddScoped<IControleProducaoService, ControleProducaoService>();

builder.Services.AddScoped<IInsumoProducaoRepository, InsumoProducaoRepository>();
builder.Services.AddScoped<IInsumoProducaoService, InsumoProducaoService>();

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddScoped<IVendaRepository, VendaRepository>();
builder.Services.AddScoped<IVendaService, VendaService>();

// Configura��o de sess�o com expira��o de 30 minutos
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Adiciona suporte para controladores e views
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Exibe p�gina de erros detalhada em modo de desenvolvimento
}

app.UseStaticFiles();
app.UseRouting();

// Configura��o de pol�tica de cookies
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always
});

// Middleware para habilitar sess�es
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// Configura��o da rota padr�o
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Autenticacao}/{action=Login}/{id?}");

app.Run();
