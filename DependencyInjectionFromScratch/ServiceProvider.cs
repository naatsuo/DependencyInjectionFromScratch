public class ServiceProvider
{
    private readonly Dictionary<Type, Type> _services;
    public ServiceProvider()
    {
        _services = new Dictionary<Type, Type>();
    }

    public void AddTransient<TService>() => AddTransient<TService, TService>();
    public void AddTransient<TService, TImplementation>() => _services.Add(typeof(TService), typeof(TImplementation));
    public TService GetService<TService>() => (TService)GetService(typeof(TService));
    private object GetService(Type type) => InternalGetService(type, new List<Type>());
    private object InternalGetService(Type type, List<Type> alreadyVisitedTypes)
    {
        if (alreadyVisitedTypes.Contains(type))
            throw new Exception($"Circular or recursive dependency found on '{type.FullName}'");
        alreadyVisitedTypes.Add(type);
        if (!_services.ContainsKey(type))
            throw new Exception($"Service '{type.FullName}' not registered");
        var implementationType = _services[type];

        var constructors = implementationType.GetConstructors();
        foreach (var constructor in constructors)
        {
            try
            {
                var constructorParameters = constructor.GetParameters();
                return constructor.Invoke(constructorParameters.Select(i => this.InternalGetService(i.ParameterType, alreadyVisitedTypes.ToList())).ToArray());
            }
            catch { }
        }
        throw new Exception($"Invalid constructor for service {type.FullName}");
    }
}