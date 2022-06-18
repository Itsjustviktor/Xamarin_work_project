using AndroidApp.Models;
using AndroidApp.Services.DataContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace AndroidApp.Services
{
    public class FeedbackDataStore
        : IFeedbackDataStore
    {
        public DataBaseContextQuestionModel DbContext { get; }
        public APIContextQuestionModel ApiContext { get; }

        public FeedbackDataStore(DataBaseContextQuestionModel dbContext, APIContextQuestionModel apiContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            ApiContext = apiContext ?? throw new ArgumentNullException(nameof(apiContext));
        }

        public IEnumerable<QuestionModel> Get()
        {
            IEnumerable<QuestionModel> list;

            try
            {
                list = ApiContext.Get();

                SaveAll(list);
            }
            catch (Exception ex)
            {
                Log.Warning("WARNING", ex.Message);
                list = DbContext.Get();
            }

            return list;
        }
        public IEnumerable<QuestionModel> Get(string search)
        {
            IEnumerable<QuestionModel> list;

            try
            {
                list = ApiContext.Get(search);
            }
            catch (Exception ex)
            {
                Log.Warning("WARNING", ex.Message);
                list = DbContext.Get(search);
            }

            return list;
        }

        public async void Save(QuestionModel model)
        {
            try
            {
                ApiContext.Save(model);

                SaveAll(Get());
            }
            catch (Exception ex)
            {
                Log.Warning("WARNING", ex.Message);
                await Task.Run(() => SaveAll(Get()));
            }
        }

        private async void SaveAll(IEnumerable<QuestionModel> list)
        {
            await Task.Run(() => { 
                foreach (var item in list)
                {
                    DbContext.Save(item);
                }
            });
        }
    }
}
