using HGO.ASPNetCore.FileManager.CommandsProcessor;
using HGO.ASPNetCore.FileManager.ViewComponents;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// HGO.AspNetCore.FileManager -------
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(c => c.FileProviders.Add(new EmbeddedFileProvider(typeof(FileManagerComponent)
    .GetTypeInfo().Assembly, "HGO.ASPNetCore.FileManager")));
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(typeof(IFileManagerCommandsProcessor), typeof(FileManagerCommandsProcessor));
//-----------------------------------

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

// HGO.AspNetCore.FileManager -------
var embeddedProvider = new EmbeddedFileProvider(typeof(FileManagerComponent)
    .GetTypeInfo().Assembly, "HGO.ASPNetCore.FileManager.hgofilemanager");

app.UseSession();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = embeddedProvider,
    RequestPath = new PathString("/hgofilemanager")
});
//-----------------------------------

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
