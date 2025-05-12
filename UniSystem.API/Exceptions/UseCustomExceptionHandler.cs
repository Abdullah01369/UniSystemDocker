using Microsoft.AspNetCore.Diagnostics;

using SharedLayer.Dtos;
using System.Text.Json;
using UniSystem.Service.ExceptionServices;

namespace UniSystem.API.Exceptions
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomExceptionHandlers(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var feature = context.Features.Get<IExceptionHandlerFeature>();

                    var statuscode = feature.Error switch
                    {
                        ClientSideExceptions => 400,
                        _ => 500
                    };

                    context.Response.StatusCode = statuscode;

                    var response = Response<NoDataDto>.Fail(feature.Error.Message, context.Response.StatusCode, true);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
