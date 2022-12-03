using System.Net;

namespace MiddlewareExample.Web.Middlewares
{
    public class WhiteIpAddressControlMiddleware
    {
        // bir class ın middleware olabilmesi için mutlaka RequestDelegate içeren bir constructor ı ve InvokeAsync methodu olmalıdır
        private readonly RequestDelegate _requestDelegate;
        private const string WhiteIpAddress = "::1";
        public WhiteIpAddressControlMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // IPV4 => 127.0.0.1 => localhost
            // IPV6 => ::1 => localhost

            var requestIpAddress = context.Connection.RemoteIpAddress;

            bool anyWhiteIpAddress = IPAddress.Parse(WhiteIpAddress).Equals(requestIpAddress);

            if (anyWhiteIpAddress)
            {
                await _requestDelegate(context);
            }
            else
            {
                context.Response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
                await context.Response.WriteAsync("Forbidden");
            }
        }
    }
}
