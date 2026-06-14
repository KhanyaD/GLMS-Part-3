using GLMS.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("BackendApi", client =>
{
    var baseUrl = builder.Configuration["BackendApi:BaseUrl"] ?? "https://localhost:7035/";
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<ICurrencyApiService, CurrencyApiService>(client =>
{
    var baseUrl = builder.Configuration["CurrencyApi:BaseUrl"] ?? "https://api.exchangerate.host/";
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<IApiTokenService, ApiTokenService>();
builder.Services.AddScoped<IContractFileService, ContractFileService>();
builder.Services.AddScoped<ICurrencyCalculator, CurrencyCalculator>();
builder.Services.AddScoped<IFileValidationService, FileValidationService>();
builder.Services.AddScoped<IServiceRequestWorkflowService, ServiceRequestWorkflowService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (!app.Configuration.GetValue<bool>("DisableHttpsRedirection"))
{
    app.UseHttpsRedirection();
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
