using JobPortal.core.IUnitOfWork;
using JobPortal.core.Repository;
using JobPortal.EF;
using JobPortal.EF.Repositories;
using JobPortal.EF.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<JobPortalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly(typeof(JobPortalDbContext).Assembly.FullName)));

//builder.Services.AddTransient(typeof(IBaseRepository<> ), typeof(BaseRepository<>));
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
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
