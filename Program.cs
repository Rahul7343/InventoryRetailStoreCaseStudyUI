using InventoryUi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("Inventory", client =>
{
    client.BaseAddress = new Uri("https://localhost:7220/"); // replace with your API base URL
});


builder.Services.AddScoped<SupplierApiService>();
builder.Services.AddScoped<CustomerApiService>();
builder.Services.AddScoped<ProductApiService>();
builder.Services.AddScoped<SalesApiService>();


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
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
