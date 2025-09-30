namespace PCBuidler.Domain.Shared.Saga;

public class SagaStep(Func<Task> execute, Func<Task> compensate)
{
    public Func<Task> Execute { get; } = 
        execute ?? throw new ArgumentNullException(nameof(execute));
    public Func<Task> Compensate { get; } =
        compensate ?? throw new ArgumentNullException(nameof(compensate));
}
