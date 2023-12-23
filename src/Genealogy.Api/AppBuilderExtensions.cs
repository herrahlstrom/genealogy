using MediatR;
using Microsoft.OpenApi.Models;

namespace Genealogy.Api;

internal static class AppBuilderExtensions
{
    public static TBuilder AddOpenApi<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services
               .AddEndpointsApiExplorer()
               .AddSwaggerGen(options =>
               {
                   options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                   {
                       Scheme = "Bearer",
                       BearerFormat = "JWT",
                       Description = "JWT Authorization header using the Bearer scheme.",
                       Name = "Authorization",
                       In = ParameterLocation.Header,
                       Type = SecuritySchemeType.Http,
                   });
                   options.AddSecurityRequirement(new OpenApiSecurityRequirement
                       {
                       {
                           new OpenApiSecurityScheme
                           {
                               Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                           },
                           new string[] { }
                       }
                       });
               });

        return builder;
    }

    public static IEndpointRouteBuilder MapEndpoint<TParameter>(this IEndpointRouteBuilder builder, string pattern, bool requireAuthorization = true)
        where TParameter : notnull
    {
        var routeHandlerBuilder = builder.MapGet(pattern, async (IMediator mediator, [AsParameters] TParameter request) => await mediator.Send(request));
        if (requireAuthorization)
        {
            routeHandlerBuilder.RequireAuthorization();
        }

        return builder;
    }
}