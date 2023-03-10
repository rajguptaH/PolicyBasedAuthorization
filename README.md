# Policy Based Authorization in asp dot net core Mvc
- You Have To Tell In The Startup You Are going to use authentication write below code in start.cs and this says you are adding the authentication service in your application using a cookies
```c#
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
```
### Now You have To Add Policy in Startup
```c#
builder.Services.AddAuthorization(options =>
{
    //"AtLeast30K" is policy name and the SalaryRequirement Is Requirment for this Policy Parameter 
    options.AddPolicy("AtLeast30K", policy =>policy.Requirements.Add(new SalaryRequirement(30000)));
});
```
### Then you have add Singleton
```c#
builder.Services.AddSingleton<IAuthorizationHandler, SalaryHandler>();
```
### after that you have to tell the application to add authentication and authorization
```c#
app.UseAuthentication();
app.UseAuthorization();
```
### After that you have to create a Requirment For Claim
```c#
using Microsoft.AspNetCore.Authorization;
namespace PolicyBasedAuthorization.Service;
public class SalaryRequirement : IAuthorizationRequirement
{
    public SalaryRequirement(int salary) =>
        Salary = salary;
    public int Salary { get; }
}
```
### And Then Create a Handler For That requirment and you can also handle multiple requirments in single handler 
```c#
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
```
### now you have to use Policy base authorization for a method or a controller so you can simply provide a parameter called Policy to the Authorize attribute Like this [Authorize(Policy = "AtLeast30K")]
```c#
[Authorize(Policy = "AtLeast30K")]
public IActionResult Privacy()
{
  return View();
}
```
- Now This Method Will only accessible to The User Whose Have Policy With Requirment Matchs That It 
## Thanks
- Visit To The Repository Where Role Based Authorization Has been implemented 
- [RoleBaseAuthorization](https://github.com/rajguptaH/PolicyBasedAuthorization)
