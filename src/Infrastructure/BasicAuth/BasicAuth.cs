

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;

namespace Infrastructure.BasicAuth
{
    public class BasicAuth
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<BasicAuth> _logger;

        public BasicAuth(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<BasicAuth>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context, IOptionsSnapshot<AppCredentialsOptions> config)
        {
            try
            {
                _logger.LogInformation("Starting validation of request credentials.");

                var configValue = config.Value;
                var requestAuthHeader = context.Request.Headers["Authorization"];

                if (requestAuthHeader.Count < 1)
                {
                    _logger.LogWarning("Authorization Header value missing from request.");
                    InvalidCredentialsResponse(context);

                    return;
                }

                var requestAuthHeaderParsed = AuthenticationHeaderValue.Parse(requestAuthHeader);
                var requestAuthCredentialsBytes = Convert.FromBase64String(requestAuthHeaderParsed.Parameter);
                var requestAuthCredentails = Encoding.UTF8.GetString(requestAuthCredentialsBytes).Split(':', 2);

                var requestAuthUsername = requestAuthCredentails[0] ?? "";
                var requestAuthPassword = requestAuthCredentails[1] ?? "";

                var configAuthUserName = configValue.userName;
                var configAuthPassword = configValue.password;

                if (!configValue.IsUserNameConfigured || !configValue.IsPasswordConfigured)
                {
                    throw new Exception("Service credentials are not in place. Please check appsettings.json");
                }

                if (!configAuthUserName.Equals(requestAuthUsername))
                {
                    _logger.LogError($"Invalid request password detected: '{requestAuthUsername}'");
                    InvalidCredentialsResponse(context);
                    return;
                }

                if (!configAuthPassword.Equals(requestAuthPassword))
                {
                    _logger.LogError($"Invalid request password detected: '{requestAuthPassword}'");
                    InvalidCredentialsResponse(context);
                    return;
                }

                _logger.LogInformation("Request credentials validated successfully.");
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Error trying to validate service credentails!");
                context.Response.StatusCode = 503;

                return;
            }
        }

        private static void InvalidCredentialsResponse(HttpContext context)
        {
            // Header 'WWW-Authenticate' set to trigger login popup in browsers
            context.Response.Headers["WWW-Authenticate"] = "basic realm=\"\", charset=\"UTF-8\"";
            context.Response.StatusCode = 401;
        }
    }
}

