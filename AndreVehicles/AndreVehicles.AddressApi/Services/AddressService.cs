using AndreVehicles.AddressApi.Utils;
using Models;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace AndreVehicles.AddressApi.Services
{
    public class AddressService
    {
        static readonly HttpClient address = new HttpClient();

        public async Task<AddressViacep> GetViacepAddress(string cep)
        {
            try
            {
                HttpResponseMessage response = await address.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<AddressViacep>(await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException e) { throw; }
        }
    }
}
