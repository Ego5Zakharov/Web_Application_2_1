using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAntiforgery(options => options.SuppressXFrameOptionsHeader = true); ;

builder.Services.AddRazorPages().AddRazorPagesOptions(x
    => x.Conventions
    .ConfigureFilter(new IgnoreAntiforgeryTokenAttribute())).AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();


app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    // если обращение идет по адресу "/postuser", получаем данные формы
    if (context.Request.Path == "/postuser")
    {
        var dollar = 427.59;
        var euro = 443.63;
        var form = context.Request.Form;
      
        await context.Response.WriteAsync($"<div><p>Euro:{euro} tenge</p> " +
                                          $"<p>Dollar: {dollar} tenge</p></div>");
    }

    else
    {
        await context.Response.SendFileAsync("Pages/Shared/Index.cshtml");
        //await context.Response.SendFileAsync("html/cshtml.css");
    }
});

app.Run();

