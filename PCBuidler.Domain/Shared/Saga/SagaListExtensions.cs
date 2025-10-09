namespace PCBuidler.Domain.Shared.Saga;

public static class SagaListExtensions
{
    public static List<SagaStep> AddStep(
        this List<SagaStep> steps, SagaStep stepToAdd, bool? condition = true)
    {
        if (condition??true)
            steps.Add(stepToAdd);

        return steps; 
    }
   
}