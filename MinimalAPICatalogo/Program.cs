using MinimalAPICatalogo.ApiEndpoints;
using MinimalAPICatalogo.AppServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();

var app = builder.Build();

app.MapAuthenticationEndpoint();
app.MapCategoriesEndpoints();
app.MapProductsEndpoints();

var envionment = app.Environment;

app.UseExceptionHandling(envionment)
    .UseSwaggerMiddleware()
    .UseAppCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();