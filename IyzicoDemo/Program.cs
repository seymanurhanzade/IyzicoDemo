using IyzicoDemo;
using IyzicoDemo.Entity;
using IyzicoDemo.Identity;
using IyzicoDemo.Services;
using IyzicoDemo.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();

builder.Services.Configure<IyzicoOption>(builder.Configuration.GetSection(IyzicoOption.SectionName));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>(option => {     
    option.Password.RequireDigit = true;
    option.Password.RequiredLength = 6;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = true;
    option.Password.RequireLowercase = true;

    option.User.RequireUniqueEmail = true;
    
}).AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Api:Authority"];
    options.Audience = builder.Configuration["Api:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        //ValidAudience = builder.Configuration["Api:Audience"],
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Api:ValidIssuer"],
        ValidateLifetime = true
    };

});

builder.Services.AddCors(builder =>
{
    builder.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IPaymentService, IyzicoPaymentService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    await SeedData.SeedRoleAsync(services);
//}
using ( var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.SeedAdminAsync(services);
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapGet("/payment/success", (string? session_id) =>
{
    return Results.Ok(new
    {
        message = "Ödeme baþarýlý sayfasýna geldiniz.",
        sessionId = session_id
    });
});

app.MapGet("/payment/cancel", () =>
{
    return Results.Ok(new
    {
        message = "Ödeme iptal edildi."
    });
});
app.Run();
