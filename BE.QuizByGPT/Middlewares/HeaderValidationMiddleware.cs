namespace BE.QuizByGPT.Middlewares
{
    public class HeaderValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string? _xClientId;
        private readonly string? _xClientSecret;

        public HeaderValidationMiddleware(RequestDelegate next)
        {
            _next = next;
            _xClientId = Environment.GetEnvironmentVariable("XClientId");
            _xClientSecret = Environment.GetEnvironmentVariable("XClientSecret");
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-Client-Id", out var headerClientId) || headerClientId != _xClientId)
            {
                throw new Exception("Invalid or missing X-Client-Id.");
            }

            if (!context.Request.Headers.TryGetValue("X-Client-Secret", out var headerClientSecret) || headerClientSecret != _xClientSecret)
            {
                throw new Exception("Invalid or missing X-Client-Secret.");
            }

            await _next(context);
        }
    }
}
