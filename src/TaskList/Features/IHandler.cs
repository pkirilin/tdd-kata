namespace TaskList.Features;

public interface IHandler<in TRequest>
{
    void Handle(TRequest request);
}

public interface IHandler<in TRequest, out TResponse>
{
    TResponse Handle(TRequest request);
}