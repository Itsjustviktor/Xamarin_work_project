using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AndroidApp.Models
{
    [Table("QuestionModel")]
    public class QuestionModel
        : BaseModel
    {
        private string status;

        public string BuyerFirstName { get; set; }
        public string BuyerMiddleName { get; set; }
        public string BuyerLastName { get; set; }
        public string QuestionString { get; set; }

        public string Code { get; set; }
        public string Phone { get; set; }

        public string Status
        {
            get => status;
            set { status = States.FirstOrDefault(x => value == x); OnPropertyChanged(nameof(Status)); }
        } //Обновление статуса

        [Ignore] 
        public IList<String> States { get; } = new List<String>()
        {
            "Перезвонить",
            "Решено"
        };
    }
}
