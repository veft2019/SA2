using System;
using System.Collections.Generic;
using System.Net;
using Exterminator.Models;
using Exterminator.Models.Exceptions;
using Exterminator.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Exterminator.WebApi.ExceptionHandlerExtensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {   
                    // To retrieve info about the current exp
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        var exception = exceptionHandlerFeature.Error;
                        // Default return exp status code
                        var statusCode = (int) HttpStatusCode.InternalServerError;

                        if(exception is ResourceNotFoundException)
                        {
                            statusCode = (int) HttpStatusCode.NotFound;
                        }
                        else if (exception is ModelFormatException)
                        {
                            statusCode = (int) HttpStatusCode.PreconditionFailed;
                        }
                        else if (exception is ArgumentOutOfRangeException)
                        {
                            statusCode = (int) HttpStatusCode.BadRequest;
                        }

                        // ContentType header as application/json
                        context.Response.ContentType = "application/json";
                        // This is needed to get the right Statuscode header in postman
                        context.Response.StatusCode = statusCode;
                        //ExceptionModel filled
                        ExceptionModel exceptionModel = new ExceptionModel 
                        {
                            StatusCode = statusCode,
                            ExceptionMessage = exception.Message,
                            StackTrace = exception.StackTrace.ToString()
                        };

                        // LOG the error
                        var logService = app.ApplicationServices.GetService(typeof(ILogService)) as ILogService;
                        logService.LogToDatabase(exceptionModel);

                        await context.Response.WriteAsync(new ExceptionModel
                        {
                            StatusCode = statusCode,
                            ExceptionMessage = exception.Message,
                            StackTrace = exception.StackTrace.ToString()
                        }.ToString());
                    }
                });
            });
        }
    }
}