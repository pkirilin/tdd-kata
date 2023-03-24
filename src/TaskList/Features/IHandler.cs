namespace TaskList.Features;

public interface IHandler<in TRequest>
{
    void Handle(TRequest request);
}