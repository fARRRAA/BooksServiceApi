using BooksServiceApi.Interfaces;
using BooksServiceApi.Models;
using BooksServiceApi.Requests;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
namespace BooksServiceApi.Services
{
    public class ReaderService : IReaderService
    {

        private readonly HttpClient _httpClient;
        private readonly string URL = "https://localhost:7008/api/Reader";
        public ReaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Readers GetById(int id)
        {
            var response = _httpClient.GetAsync($"{URL}/getReaderById/{id}");
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var jsonResponse =  result.Content.ReadAsStringAsync();
                jsonResponse.Wait();
                var jsonResult = jsonResponse.Result;
                var reader = JsonConvert.DeserializeObject<ReadersResponce>(jsonResult);
                return reader.Reader;
            }
            else
            {
                throw new Exception($"Произошла ошибка при вызове API. Статус: {result.StatusCode}");
            }
        }
        public bool Exists(int id)
        {
            return true;
        }
    }
}
