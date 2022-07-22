using Microsoft.EntityFrameworkCore;
//using Smarticube.API.SilverBear.Data;
//using System.IO;
//using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<Smarticube.API.DemoService.Data.IProductRepository, Smarticube.API.DemoService.Data.ProductRepository>();
builder.Services.AddScoped<Smarticube.API.DemoService.Data.IJobDemoServiceRepository, Smarticube.API.DemoService.Data.JobDemoServiceRepository>();
builder.Services.AddScoped<Smarticube.API.HKJC.Football.Data.IHkjcFootballRepository, Smarticube.API.HKJC.Football.Data.HkjcFootballRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add:Inject Dbcontext
builder.Services.AddDbContext<Smarticube.API.DemoService.Data.ProductsDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SilverBearDbConnectionString")));

builder.Services.AddDbContext<Smarticube.API.HKJC.Football.Data.HkjcFootballDbContext>(options1 =>
options1.UseSqlServer(builder.Configuration.GetConnectionString("HkjcDbConnectionString")));


//add:Set access right between different api
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors("default"); // add:

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider
//    (
//        Path.Combine(Directory.GetCurrentDirectory(), "Photos")
//    ), 
//    RequestPath = "/Photos"
        
//});

app.Run();
