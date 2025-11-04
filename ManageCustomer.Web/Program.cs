using ManageCustomer.Repository.Data;
using ManageCustomer.Domain.Interfaces;
using ManageCustomer.Repository;
using Microsoft.EntityFrameworkCore;
using ManageCustomer.Domain.Entities;
using ManageCustomer.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CustomerDbContext>(options =>
 options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

// Seed sample data
using (var scope = app.Services.CreateScope())
{
 var db = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
 DataSeeder.Seed(db);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
 app.UseExceptionHandler("/Home/Error");
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
