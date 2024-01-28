namespace BE.QuizByGPT.Middlewares
{
    public class HeaderRequestIdMiddleware
    {
        private const string HeaderName = "X-Request-Id";
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public HeaderRequestIdMiddleware(RequestDelegate next, ILogger<HeaderRequestIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(HeaderName, out var requestId))
            {
                requestId = Guid.NewGuid().ToString();
                context.Request.Headers[HeaderName] = requestId;
            }

            using (_logger.BeginScope("{RequestId}", requestId))
            {
                await _next(context);
            }
        }
    }
}
