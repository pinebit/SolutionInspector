namespace SolutionInspector.Contracts
{
    public interface IIssue
    {
        string Code { get; }

        string File { get; }

        string Text { get; }
    }
}