using RetailFlow.Application.Interfaces;

namespace RetailFlow.Api.Middleware
{
    public class TenantContextMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ITenantContext tenantContext)
        {
            // Les endpoints de gestion des tenants n'ont pas encore
            // besoin d'un tenant courant dans notre V1.
            if (context.Request.Path.StartsWithSegments("/api/tenants") ||
                context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            var tenantHeader =
                context.Request.Headers["X-Tenant-Id"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(tenantHeader))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(new
                {
                    error = "X-Tenant-Id header is required."
                });

                return;
            }

            if (!Guid.TryParse(tenantHeader, out var tenantId) ||
                tenantId == Guid.Empty)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(new
                {
                    error = "X-Tenant-Id must be a valid GUID."
                });

                return;
            }

            tenantContext.SetTenant(tenantId);

            await _next(context);
        }

    }
}
