using SearchPlaceApp.Client.Pages;
using SearchPlaceApp.Components;
using Microsoft.Extensions.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient("KerridgeApi", client => client.BaseAddress = new Uri(builder.Configuration["KerridgeApi:BaseUrl"]))
                .AddHttpMessageHandler<AuthorizationMessageHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SearchPlaceApp.Client._Imports).Assembly);

app.Run();
