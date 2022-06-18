using AndroidApp.Models;

using System;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using static AndroidApp.APISettings;
using System.Text;

namespace AndroidApp.Services.DataContext
{
	public class APIContextCameraModel
		: IGetModel<CameraModel>, ISaveModel<CameraModel>
	{
		private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		};

		private HttpClient GetClient()
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Add("Accept", "application/json");
			return client;
		}


		/// <summary>
		/// тут ад
		/// Мы не можем гарантировать, что нам придут все данные, и что они сохранятся нормально.
		/// Поэтому нам необходмо каждый раз подгружать их, чтобы не сломалась синхронизация с бд.
		/// Т.к. синхронизация происходит после каждого запроса, чтобы минимизировать риски несопоставления данных.
		/// А те данные неполные. Там нет полей, которые мы получаем из запроса Get(id) POST.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<CameraModel> Get()
		{
			using (var client = GetClient())
			{
				var str1 = client.SendAsync(new HttpRequestMessage() { Method = HttpMethod.Post, RequestUri = new Uri($"{BaseURL}{GetsCameraURI}") })
					.Result
					.Content
					.ReadAsStringAsync()
					.Result;

				var list = new List<CameraModel>();

				/// получаем список id, для запросов
				foreach (var item in JsonConvert.DeserializeObject<IEnumerable<CameraIdentity>>(str1, serializerSettings))
				{
					list.Add(Get(item.Idgood));
				}

				return list;
			}
		}

		// Не используется
		public IEnumerable<CameraModel> Get(string search)
		{
			return string.IsNullOrWhiteSpace(search) ? Get() : this.Get().Where(x => x.Title.Contains(search));
		}

		/// <summary>
		/// Все данные с сервера по одному
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public CameraModel Get(int id)
		{
			using (var client = GetClient())
			{
				var content = new StringContent(JsonConvert.SerializeObject(new { Idgood = id }), Encoding.UTF8, "application/json");

				var str = client.SendAsync(new HttpRequestMessage() { Method = HttpMethod.Post, RequestUri = new Uri($"{BaseURL}{GetCameraURI}"), Content = content })
					.Result
					.Content
					.ReadAsStringAsync()
					.Result;
				
				var x = JsonConvert.DeserializeObject<CameraEnity>(str, serializerSettings);

				return new CameraModel()
				{
					Id = x.Idgood,
					Category = x.Category,
					Source = x.Source ?? "camera.png",
					Provider = x.Provider,
					Title = x.Name,
					Length = x.Length,
					Weight = x.Weight,
					Width = x.Weight,
					Price = x.Buyprice,
					Cost = x.Sellprice,
					Phone = x.Telephone,
					Height = x.Height,
					Subcategory = x.Subcategory,
					Count = x.Quantity,
				};
			}
		}

		public void Save(CameraModel model)
		{
			/// тут ничего писать не надо
		}

        private class CameraIdentity { public Int32 Idgood { get; set; } }
		private class CameraEnity
		{
			public int Idgood { get; set; }
			public string Name { get; set; }
			public string Provider { get; set; }
			public string Telephone { get; set; }
			public int Length { get; set; }
			public int Weight { get; set; }
			public decimal Buyprice { get; set; }
			public decimal Sellprice { get; set; }
			public int Width { get; set; }
			public int Height { get; set; }
			public string Category { get; set; }
			public string Subcategory { get; set; }
			public int Quantity { get; set; }
            public string Source { get; set; }
        }
	}
}
