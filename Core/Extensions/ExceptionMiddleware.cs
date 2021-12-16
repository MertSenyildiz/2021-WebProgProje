using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            IEnumerable<ValidationFailure> errors;
            string message = "Internal Server Error";
            if (e.GetType() == typeof(ValidationException))
            {
                message = e.Message;
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode= (int)HttpStatusCode.BadRequest,//400 
                    Message = message,
                    Errors = errors
                }.ToString());
            }

            else if (e.GetType() == typeof(InvalidCredentialException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return httpContext.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,//401
                    Message = message,
                }.ToString());
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
