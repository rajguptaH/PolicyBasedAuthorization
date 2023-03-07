using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PolicyBasedAuthorization.Service;

namespace PolicyBasedAuthorization.Policies.Handlers;

public class SalaryHandler : AuthorizationHandler<SalaryRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, SalaryRequirement requirement)
    {
        var nameClaim = context.User.FindFirst(
            c => c.Type == "Salary");
       
        if (nameClaim != null)
        {
            var salary = int.Parse(nameClaim.Value);
            if (salary >= requirement.Salary)
            {
                context.Succeed(requirement);
            }
        }
        return Task.CompletedTask;
    }
}