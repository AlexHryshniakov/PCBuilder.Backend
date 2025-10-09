namespace PCBuidler.Domain.Shared.Saga;


public class SagaOrchestrator
{
    private readonly Stack<SagaStep> _executedSteps = new Stack<SagaStep>();

    public async Task Execute(IEnumerable<SagaStep> steps)
    {
        foreach (var step in steps)
        {
            try
            {
                await step.Execute();
                _executedSteps.Push(step);
            }
            catch (Exception ex)
            {
                await CompensateAllCompleted(ex);
                throw;
            }
        }
    }

    private async Task CompensateAllCompleted(Exception originalException)
    {
        while (_executedSteps.Count > 0)
        {
            var stepToCompensate = _executedSteps.Pop();
            try
            {
                await stepToCompensate.Compensate();
            }
            catch (Exception compEx)
            {
                throw new InvalidOperationException(
                    $"INVALID OPERATION: compensate wasn't success. {originalException.Message}", compEx);
            }
        }
    }
}