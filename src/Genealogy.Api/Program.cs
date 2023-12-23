using Genealogy.Api;
using Genealogy.Api.Auth;
using Genealogy.Application.Request;
using Genealogy.Infrastructure;
using Genealogy.Infrastructure.Application;

var builder = WebApplication.CreateBuilder(args)
                            .AddOpenApi()
                            .AddAuth()
                            .AddInfrastructure()
                            .AddApplication();

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Open API
    app.UseSwagger().UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuth();

/* Endpoints */

app.MapEndpoint<LoginRequest>("/auth/login", requireAuthorization: false)
   .MapEndpoint<GetPersonRequest>("/person/{id}")
   .MapEndpoint<SearchPersonsRequest>("/person/search");

app.Run();
