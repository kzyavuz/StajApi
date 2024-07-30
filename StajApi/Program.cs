using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DTO.DepperContext;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.   
builder.Services.AddTransient<Context>();

builder.Services.AddTransient<IDealerRepository, DealerRepository>();

builder.Services.AddTransient<IWorkRepository, WorkRepository>();

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();

builder.Services.AddTransient<IRoleRepository, RoleRepository>();


// JWT authentication configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(o => true).AllowCredentials());
//});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins"); 
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
