using System;
using System.Collections.Generic;
using System.Net;
using Exterminator.Models;
using Exterminator.Models.Exceptions;
using Exterminator.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

/*
(40%) A global exception handling should be setup within the
UseGlobalExceptionHandler method which resides within the file
ExceptionHandlerExtensions. The global exception handler should do the following:
a. Retrieve information about the current exception using the
IExceptionHandlerFeature
b. Set a default return status code of Internal Server Error (500)
c. Handle different exceptions:
i. ResourceNotFoundException should return a status code Not Found (404)
ii. ModelFormatException should return a status code Precondition Failed (412)
iii. ArgumentOutOfRangeException should return a status code Bad Request (400)
d. The response should have the Content-Type header set as application/json
e. The exception should be logged using the ILogService.LogToDatabase() method
which accepts an ExceptionModel as parameter. The ExceptionModel should be
properly filled out using information from the exception
f. The response should be written out with the exception model in string format (JSON)
 */

namespace Exterminator.WebApi.ExceptionHandlerExtensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            // TODO: Implement
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {   
                    // To retrieve info about the current exp
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        var exception = exceptionHandlerFeature.Error;
                        // Default return status code
                        var statusCode = (int) HttpStatusCode.InternalServerError;
                        // ContentType header as application/json
                        context.Response.ContentType = "application/json";

                        // TODO: LOG the error

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

                        await context.Response.WriteAsync(new ExceptionModel
                        {
                            StatusCode = statusCode,
                            ExceptionMessage = exception.Message,
                        }.ToString());

                    }
                });
            });
        }
    }
}