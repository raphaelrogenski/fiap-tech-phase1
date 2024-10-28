using Microsoft.Extensions.DependencyInjection;

namespace Contacts.Infrastructure.DI;

public static class AutoRegisterDI
{
    public static void AddAutoRegister<T>(this IServiceCollection services)
    {
        services.AddAutoRegister<T>("Repository");
        services.AddAutoRegister<T>("Service");
    }

    public static void AddAutoRegister<T>(this IServiceCollection services, string classesEndedWith)
    {
        var classes = typeof(T).Assembly.GetTypes().Where(r => r.Name.EndsWith(classesEndedWith)).ToList();

        foreach (var @class in classes)
        {
            var interfaces = @class.GetInterfaces().Except(@class.BaseType.GetInterfaces()).Where(r => r.Name.EndsWith(classesEndedWith));
            foreach (var @interface in interfaces)
            {
                services.AddScoped(@interface, @class);
            }
        }
    }
}
