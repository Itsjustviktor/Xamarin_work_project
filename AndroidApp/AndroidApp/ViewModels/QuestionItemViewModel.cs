using AndroidApp.Models;

using System;

namespace AndroidApp.ViewModels
{
    public class QuestionItemViewModel
        : BaseViewModel
    {
        public QuestionModel Question { get; set; }

        /// Неизменяемые поля
        public String Code { get => Question.Code; }
        public String Phone { get => Question.Phone; }

        /// Изменяемые поля
        public String Status
        {
            get => Question.Status;
            set
            {
                Question.Status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public String QuestionString
        {
            get => Question.QuestionString;
            set
            {
                Question.QuestionString = value;
                OnPropertyChanged(nameof(QuestionString));
            }
        }
    }
}
