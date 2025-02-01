public static class Services
{
    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<T>(T service)
    {
        if (_services.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException($"Service for type {typeof(T)} already registered");
        }
        // _services.Add(typeof(T), service);
        _services[typeof(T)] = service;
    }

    public static T Get<T>()
    {
        if (!_services.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException($"Service for type {typeof(T)} is not registered");
        }
        return (T)_services[typeof(T)];
    }

}