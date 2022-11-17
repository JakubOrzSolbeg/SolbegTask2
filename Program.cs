using Microsoft.EntityFrameworkCore;
using SolbegTask2.DbContexts;
using SolbegTask2.Services;
using SolbegTask2.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// In production this connection string will move to user Secrets
 builder.Services.AddDbContext<MainDbContext>(options =>
 {
     options.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Integrated Security=false;User Id=jakubo_solbegTask2;Password=741solbeg2");
 });

// Services dependency injection scoped
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IMainGameService, MainGameService1>();

//Singleton services
builder.Services.AddSingleton<IStaticConfigService, StaticConfigService>();
builder.Services.AddSingleton<IMemoryService, MemoryService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();