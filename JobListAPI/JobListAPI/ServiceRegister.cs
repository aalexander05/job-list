using JobListAPI.Data.DataManagers;
using JobListAPI.Services;
using System.Reflection;

namespace JobListAPI;

public static class ServiceRegister
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection @this)
    {
        var serviceType = typeof(Service);
        var definedTypes = serviceType.Assembly.DefinedTypes;

        var services = definedTypes
            .Where(x => x.GetTypeInfo().GetCustomAttribute<Service>() != null);

        foreach (var service in services)
        {
            @this.AddTransient(service);
        }

        @this.AddTransient<IJobManager, JobManager>();
    

        return @this;
    }
}