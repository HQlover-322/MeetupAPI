using FluentValidation.AspNetCore;
using Meetup;
using Meetup.DAO;
using Meetup.DAO.Interfaces;
using Meetup.Data;
using Meetup.Data.Entities;
using Meetup.Models;
using Meetup.Models.Event;
using Meetup.Services;
using Meetup.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
 .AddFluentValidation(fv =>
 { 
    fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    fv.ImplicitlyValidateChildProperties = true;
    fv.ImplicitlyValidateRootCollectionElements = true;
 }).ConfigureApiBehaviorOptions(options=>options.InvalidModelStateResponseFactory = context =>
 {
     return new BadRequestObjectResult(new
     {
         message = "One or more validators are corrupted",
         errors = context.ModelState.SelectMany(pair => pair.Value.Errors.Select(error => error.ErrorMessage))
     });
 });
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 5;   
    opts.Password.RequireNonAlphanumeric = false;   
    opts.Password.RequireLowercase = false; 
    opts.Password.RequireUppercase = false; 
    opts.Password.RequireDigit = false;
})
               .AddEntityFrameworkStores<EfDbContex>();

builder.Services.AddScoped(typeof(IBaseDAO<,,,>),typeof(BaseDAO<,,,>));
builder.Services.AddSwaggerGen();

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<EfDbContex>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    x => x.MigrationsAssembly(typeof(EfDbContex).Assembly.FullName)
    ));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EfDbContex>();
    await context.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(x => { x.DocumentTitle = "Swagger"; x.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerMeetup"); x.RoutePrefix = string.Empty; });

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(x => { x.AllowAnyHeader(); x.AllowAnyMethod(); x.AllowAnyOrigin(); });

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExeptionMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
