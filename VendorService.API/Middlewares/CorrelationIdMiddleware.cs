namespace VendorService.API.Middlewares
{
    public class CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
    {
        private const string CorrelationIdHeader = "X-Correlation-Id";
        private readonly RequestDelegate _next = next;
        private readonly ILogger<CorrelationIdMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            // Check if the request already has a Correlation ID
            string correlationId = context.Request.Headers.ContainsKey(CorrelationIdHeader)
                ? context.Request.Headers[CorrelationIdHeader].ToString()
                : Guid.NewGuid().ToString();


            context.Response.Headers[CorrelationIdHeader] = correlationId;


            using (_logger.BeginScope("{CorrelationId}", correlationId))
            {
                _logger.LogInformation("Processing request with CorrelationId: {CorrelationId}", correlationId);
                await _next(context);
            }
        }
    }
}
