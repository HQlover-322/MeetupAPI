using Meetup;
using Meetup.Data;
using Meetup.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddTransient<EventService>();
builder.Services.AddTransient<OrganizerService>();
builder.Services.AddTransient<SpeakerService>();
builder.Services.AddTransient<PlaceService>();
builder.Services.AddSwaggerGen();

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<EfDbContex>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    x => x.MigrationsAssembly(typeof(EfDbContex).Assembly.FullName)
    ));

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
