using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DTO.DepperContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<Context>();

builder.Services.AddTransient<IDealerRepository, DealerRepository>();

builder.Services.AddTransient<IWorkRepository, WorkRepository>();

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
