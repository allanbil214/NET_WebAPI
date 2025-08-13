// todo list
// 4. JWT user stuff

using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YoutubeAPI.Data;
using YoutubeAPI.Extensions;
using YoutubeAPI.Validators;
using YoutubeAPI.Mapping;
using YoutubeAPI.Repositories.Interfaces;
using YoutubeAPI.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IYoutuberRepository, YoutuberRepository>();

builder.Services.AddBusinessServices();

builder.Services.AddAutoMapperProfiles();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidators(); 

builder.Services.AddScoped<ValidationFilter>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; 
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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