namespace SFA.DAS.EmployerDemand.Domain.Configuration;

public class EnvironmentConfiguration(string environmentName)
{
    public string EnvironmentName { get;} = environmentName;
}