using System.Reflection;
using WebApi.Extensions;
using Application;
using Application.Abstractions;
using Infrastructure;
using Infrastructure.Clients;
using Web.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IProposalClient, ProposalClient>((serviceProvider, httpClient) =>
{
    string? proposalServiceUrl = builder.Configuration.GetValue<string>("Services:ProposalService");

    httpClient.BaseAddress = new Uri(proposalServiceUrl!);
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

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
