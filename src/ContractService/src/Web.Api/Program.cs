using System.Reflection;
using Api.Extensions;
using Application;
using Application.Abstractions;
using Infrastructure;
using Infrastructure.Clients;
using Web.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IProposalClient, ProposalClient>((serviceProvider, httpClient) =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7117/");
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());



WebApplication app = builder.Build();

app.MapEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


await app.RunAsync();
