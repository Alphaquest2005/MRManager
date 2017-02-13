using System.ComponentModel.Composition;
using SystemInterfaces;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    [Export(typeof(IQuestionInfo))]
    public partial class QuestionInfo : EntityView<IQuestions>, IQuestionInfo
    {
        public int InterviewId { get; set; }
        public string Description { get; set; }
        public int EntityAttributeId { get; set; }
        public string Interview { get; set; }
        public string Phase { get; set; }
        public string Category { get; set; }
        public string Entity { get; set; }
        public string Attribute { get; set; }
        public string Type { get; set; }
        public int QuestionNumber { get; set; }
        public int QuestionResponseTypeId { get; set; }
    }
}