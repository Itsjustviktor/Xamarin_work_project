using AndroidApp.Models;
using AndroidApp.ViewModels;
using Xamarin.Forms;

namespace AndroidApp.Views.Selector
{
    public class QuestionItemDataTemplateSelector // Каждая заявка проходит через этот класс
        : DataTemplateSelector
    {
        public DataTemplate ResolvedQuestionTemplate { get; set; }
        public DataTemplate QuestionTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var question = item as QuestionModel;

            if (question is null)
                return null;

            DataTemplate template;

            /// если задача решена, то она имеет определенный шаблон отображения
            /// все другие должны быть описаны через QuestionTemplate
            switch (question.Status)
            {
                case "Решено": template = ResolvedQuestionTemplate;
                    break;

                default: template = QuestionTemplate;
                    break;
            }
            return template;
        }
    }
}
