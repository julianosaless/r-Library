using RoyalLibrary.Api.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
    xmlFiles.ForEach(xmlfile => c.IncludeXmlComments(xmlfile));
});

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

builder.Services.AddHttpContextAccessor();

ServiceRegister.Register(builder.Services, configuration: builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseDefaultFiles()
    .UseStaticFiles()
    .UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/schema.json")
    .UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint($"/swagger/v1/schema.json", $"V1");
        c.DocumentTitle = @"Roay Library API";
        c.InjectJavascript("/swagger-custom.js");

    })
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

app.UseAuthorization();

app.MapControllers();

app.Run();
