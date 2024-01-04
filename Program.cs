using LojaLivros.Data;
using LojaLivros.Services.Autenticacao;
using LojaLivros.Services.Emprestimo;
using LojaLivros.Services.Home;
using LojaLivros.Services.Livro;
using LojaLivros.Services.Sessao;
using LojaLivros.Services.Usuario;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Home/Login/");
        options.AccessDeniedPath = new PathString("/Home/Index/");
    });

builder.Services.AddDbContext<DataDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();
builder.Services.AddScoped<ILivroInterface, LivroService>();
builder.Services.AddScoped<IHomeInterface, HomeService>();
builder.Services.AddScoped<IAutenticacaoInterface, AutenticacaoService>();
builder.Services.AddScoped<ISessao, Sessao>();
builder.Services.AddScoped<IEmprestimoInterface, EmprestimoService>();


builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
}
);

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();


app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
