using System.Net;
using System.Net.Mime;
using System.Text.Json;
using EmirOtomotiv.Core.Application.Exceptions;
using EmirOtomotiv.Core.Domain.DTOs;

namespace EmirOtomotiv.Presentation.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try { await this._next(context); }
            catch (Exception exception)
            {
                HttpResponse response = context.Response;

                response.ContentType = MediaTypeNames.Application.Json;

                HttpStatusCode statusCode = exception switch
                {
                    NotFoundUserException => HttpStatusCode.NotFound,
                    AuthenticationFailedException => HttpStatusCode.Forbidden,
                    PasswordChangeFailedException => HttpStatusCode.Forbidden,
                    UserCreationErrorException => HttpStatusCode.Forbidden,
                    _ => HttpStatusCode.InternalServerError
                };

                response.StatusCode = (int)statusCode;

                string? userName = context.User.Identity?.Name;

                if (string.IsNullOrEmpty(userName) == false) { this._logger.LogInformation($"Username : {userName}"); }

                IPAddress? ipAdress = context.Connection.RemoteIpAddress;

                if (ipAdress is not null) { this._logger.LogInformation($"Ip Adress : {ipAdress}"); }

                this._logger.LogError(exception.Message);

                bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

                ErrorResult errorResult = new ErrorResult
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Bir hata oluştu.",
                    Detail = isDevelopment ? exception.ToString() : null // Exception nesnesinin kendisini değil, sadece mesajını al
                };

                string result = JsonSerializer.Serialize(errorResult);

                await response.WriteAsync(result);
            }
        }
    }
}