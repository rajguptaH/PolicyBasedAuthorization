using Microsoft.AspNetCore.Authorization;

namespace PolicyBasedAuthorization.Service;

public class SalaryRequirement : IAuthorizationRequirement
{
    public SalaryRequirement(int salary) =>
        Salary = salary;

    public int Salary { get; }
}