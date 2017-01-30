using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public interface IQuestionInfo : IEntityView<IQuestions>
    {
        int InterviewId { get; }
        string Description { get; }
        int EntityAttributeId { get; }
        string Interview { get; }
        string Phase { get; }
        string Category { get; }
        string Entity { get; }
        string Attribute { get; }
        string Type { get; }
        int QuestionNumber { get; }
    }
}