using Microsoft.Extensions.DependencyInjection;

namespace CqrsPoc.App;

public interface ICommand<T>;

public interface ICommandHandler;

public interface ICommandHandler<TCommand, TResponse> : ICommandHandler
    where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken ct = default);
}

public interface IMediator
{
    Task<TResponse> Handle<TResponse>(ICommand<TResponse> command, CancellationToken ct = default);
}

public class Mediator(IServiceProvider sp) : IMediator
{
    public Task<TResponse> Handle<TResponse>(ICommand<TResponse> command, CancellationToken ct = default)
    {
        if (command is null)
            throw new InvalidOperationException("Command must not be null.");

        var commandType = command.GetType();

        var handlerType = typeof(ICommandHandler<,>)
            .MakeGenericType(commandType, typeof(TResponse));

        var handler = sp.GetService(handlerType)
            ?? throw new InvalidOperationException("Handler not registered.");

        var method = handlerType.GetMethod(nameof(ICommandHandler<,>.Handle))!;

        var response = method.Invoke(handler, [command, ct]);

        return (Task<TResponse>)response!;
    }
}

public static class Utils
{
    public static IServiceCollection RegisterHandler<T>(this IServiceCollection services)
        where T : class, ICommandHandler
    {
        var handlerType = typeof(T);
        var handlerGenericType = typeof(ICommandHandler<,>);

        var contracts = handlerType.GetInterfaces()
            .Where(i => i.IsGenericType && handlerGenericType.Equals(i.GetGenericTypeDefinition()))
            .ToList();

        foreach (var c in contracts)
        {
            services.AddScoped(c, handlerType);
        }

        return services;
    }
}