using BlogWPF.Data;
using BlogWPF.Data.Entities.Identity;
using BlogWPF.Models.Account;
using BlogWPF.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BlogWPF.Controllers
{
    public static class AccountController
    {
        private static readonly HttpClient _httpClient;

        static AccountController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5078/api/account/");
        }

        public static async Task LoginAsync(AccountLogin login)
        {
            var jsonContent = JsonConvert.SerializeObject(login);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("login", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string token = JsonConvert.DeserializeObject<AccountResponce>(await response.Content.ReadAsStringAsync()).Token;
                    var user = await GetUserByTokenAsync(token);

                    TokenManager.Token = Encoding.UTF8.GetBytes(token);
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(errorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task RegistrationAsync(AccountRegister login)
        {
            var jsonContent = JsonConvert.SerializeObject(login);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("register", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string token = JsonConvert.DeserializeObject<AccountResponce>(await response.Content.ReadAsStringAsync()).Token;
                    var user = await GetUserByTokenAsync(token);

                    TokenManager.Token = Encoding.UTF8.GetBytes(token);
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(errorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task<UserEntity> GetUserByTokenAsync(string token)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var info = JsonConvert.DeserializeObject<AccountInfo>(content);
                var user = new UserEntity
                {
                    Username = info.Username,
                    Email = info.Email,
                    Image = info.Image,
                    Token = token,
                    Roles = string.Join(",", info.Roles)
                };

                return user;
            }
            else
            {
                MessageBox.Show($"Error fetching user info: {response.StatusCode}");
                return null;
            }
        }
    }
}
