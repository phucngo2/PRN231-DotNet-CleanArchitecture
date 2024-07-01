using PRN231.Domain.Entities;
using PRN231.Domain.Interfaces.UnitOfWork;
using System.Security.Claims;

namespace PRN231.API.Middlewares;

public class AuditLogMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    private const string _controllerKey = "controller";
    private const string _actionKey = "action";
    private const string _idKey = "id";
    private readonly string[] _needAuditMethods = ["POST", "PUT", "DELETE"];

    public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
    {
        await _next(context);
        var request = context.Request;
        var requestMethod = request.Method;
        if (request.Path.StartsWithSegments("/api") && _needAuditMethods.Contains(requestMethod))
        {
            // Controller Name
            request.RouteValues.TryGetValue(_controllerKey, out var controllerValue);
            var controllerName = (string)(controllerValue ?? string.Empty);
            // Action
            request.RouteValues.TryGetValue(_actionKey, out var actionValue);
            var action = (string)(actionValue ?? string.Empty);
            // Entity Id
            request.RouteValues.TryGetValue(_idKey, out var entityIdValue);
            var entityIdString = (string)(entityIdValue ?? string.Empty);
            _ = int.TryParse(entityIdString, out int entityId);
            // UserId
            var userIdStr = context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            _ = int.TryParse(userIdStr, out int userId);

            var auditLog = new AuditLog
            {
                EntityName = controllerName,
                Action = action,
                Method = request.Method,
                Path = request.Path,
                QueryString = request.QueryString.ToString(),
                AuditDate = DateTime.UtcNow,
                UserId = userId == 0 ? null : userId,
                EntityId = entityId == 0 ? null : entityId,
            };

            await unitOfWork.AuditLogRepository.AddAsync(auditLog);
            await unitOfWork.CommitAsync();
        }
    }
}
