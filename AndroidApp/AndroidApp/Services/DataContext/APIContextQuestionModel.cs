using AndroidApp.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using static AndroidApp.APISettings;
using Xamarin.Forms.Internals;
using System.Text;

namespace AndroidApp.Services.DataContext
{
    public class APIContextQuestionModel
        : IGetModel<QuestionModel>, ISaveModel<QuestionModel>
    {
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            var в =client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        /// <summary>
        /// Все данные с сервера
        /// </summary>
        public IEnumerable<QuestionModel> Get()
        {
            using (var client = GetClient())
            {
                var str = client.SendAsync(new HttpRequestMessage() { Method = HttpMethod.Post, RequestUri = new Uri($"{BaseURL}{GetQuestionURI}") })
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    .Result;

                return JsonConvert.DeserializeObject<IEnumerable<QuestionEnity>>(str, serializerSettings)
                    .Select(x => new QuestionModel()
                    {
                        Id = x.Idfeedback,
                        BuyerFirstName = x.Firstname,
                        Code = x.Idfeedback.ToString(),
                        Status = x.Status,
                        QuestionString = x.Problem,
                        Phone = x.Telnumber
                    });
            }
        }

        /// <summary>
        /// Поиск по номеру
        /// </summary>
        public IEnumerable<QuestionModel> Get(string search)
        {
            return string.IsNullOrWhiteSpace(search) ? Get() : this.Get().Where(x => x.Phone.Contains(search));
        }

        /// <summary>
        /// Получение по id
        /// </summary>
        public QuestionModel Get(int id)
        {
            return this.Get().FirstOrDefault(x => x.Id == id);
        }

        public void Save(QuestionModel model)
        {
            using (var client = GetClient())
            {
                var res = JsonConvert.SerializeObject(new QuestionEnity()
                {
                    Idfeedback = model.Id,
                    Firstname = model.BuyerFirstName,
                    Status = model.Status,
                    Problem = model.QuestionString,
                    Telnumber = model.Phone
                });

                if (client.PostAsync($"{BaseURL}{PutQuestionURI}", new StringContent(res, Encoding.UTF8, "application/json"))
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    .Result != "0")
                    Log.Warning("WARNING", $"QuestionModel: {model.Id}.");
            }
        }

        private class QuestionEnity
        {
            public int Idfeedback { get; set; }
            public string Firstname { get; set; }
            public string Status { get; set; }
            public string Problem { get; set; }
            public string Telnumber { get; set; }
        }
    }
}
