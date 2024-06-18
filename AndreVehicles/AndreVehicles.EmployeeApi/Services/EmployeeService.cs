using Models;
using Newtonsoft.Json;
using System.Text;

namespace AndreVehicles.EmployeeApi.Services
{
    public class EmployeeService
    {

        public async Task<Address> GetAddress(Address address)
        {
            using (HttpClient client = new HttpClient())
            {
                string addressApiUrl = "https://localhost:7273/api/addresses";
                string jsonEmployeeAddress = JsonConvert.SerializeObject(address);
                StringContent content = new StringContent(jsonEmployeeAddress, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(addressApiUrl, content);

                string jsonResponse = await response.Content.ReadAsStringAsync();

                Address viacepAddress = JsonConvert.DeserializeObject<Address>(jsonResponse);

                if (viacepAddress == null) { throw new Exception("CEP inválido."); }

                var streetSplit = viacepAddress.Street.Split(" ");

                address.Id = viacepAddress.Id;
                address.Neighborhood = viacepAddress.Neighborhood;
                address.City = viacepAddress.City;
                address.State = viacepAddress.State;
                address.StreetType = viacepAddress.StreetType;
                address.Street = viacepAddress.Street;

                return address;
            }
        }
    }
}
