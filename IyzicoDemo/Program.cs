using IyzicoDemo;
using IyzicoDemo.Entity;
using IyzicoDemo.Identity;
using IyzicoDemo.Services;
using IyzicoDemo.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();

// Iyzico
builder.Services.Configure<IyzicoOption>(
    builder.Configuration.GetSection(IyzicoOption.SectionName)
);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    options.User.RequireUniqueEmail = true;

})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// JWT
builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Api:Authority"];
        options.Audience = builder.Configuration["Api:Audience"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Api:ValidIssuer"],
            ValidateLifetime = true
        };
    });

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://localhost:3000"
            )
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Services
builder.Services.AddScoped<IPaymentService, IyzicoPaymentService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IOrderService, OrderService>();


var app = builder.Build();


// Swagger
app.UseSwagger();
app.UseSwaggerUI();


// Database Migration + Seed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var dbContext = services.GetRequiredService<AppDbContext>();

    await dbContext.Database.MigrateAsync();

    await SeedData.SeedAdminAsync(services);
}


// HTTPS
if (!app.Environment.IsEnvironment("Docker"))
{
    app.UseHttpsRedirection();
}


app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();


// Controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


// Payment success
app.MapGet("/payment/success", (string? session_id) =>
{
    return Results.Ok(new
    {
        message = "Ödeme başarılı sayfasına geldiniz.",
        sessionId = session_id
    });
});


// Payment cancel
app.MapGet("/payment/cancel", () =>
{
    return Results.Ok(new
    {
        message = "Ödeme iptal edildi."
    });
});


app.Run();