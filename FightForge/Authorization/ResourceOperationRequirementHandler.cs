using Microsoft.AspNetCore.Authorization;

namespace FightForge.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Gym>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            ResourceOperationRequirement requirement, Gym gym)
        {
            if (requirement.OperationType == OperationType.Read ||
                requirement.OperationType == OperationType.Create) 
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (gym.CreatedById == int.Parse(userId)) 
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
