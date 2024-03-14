using BlogWPF.Data;
using BlogWPF.Models.Account;
using BlogWPF.Models.Category;
using BlogWPF.Models.Tag;
using BlogWPF.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace BlogWPF.Controllers
{
    public static class TagController
    {
        private static readonly HttpClient _httpClient;

        static TagController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5078/api/tag/");

            var token = Encoding.UTF8.GetString(TokenManager.Token);
            if (token != null && token.Length != 0)
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public static async Task<List<TagItem>> GetTagsListAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var info = JsonConvert.DeserializeObject<List<TagItem>>(content);

                    return info;
                }
                else
                {
                    MessageBox.Show($"Error fetching tags info: {response.StatusCode}");
                    return null;
                }


            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static async Task<bool> CreateTag(TagCreate model)
        {
            try
            {
                string formData = $"Name={model.Name}&UrlSlug={model.UrlSlug}&Description={model.Description}";

                byte[] formDataBytes = Encoding.UTF8.GetBytes(formData);

                var httpContent = new ByteArrayContent(formDataBytes);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await _httpClient.PostAsync("", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error create tag: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static async Task<bool> UpdateTag(TagEdit model)
        {
            try
            {
                string formData = $"Id={model.Id}&Name={model.Name}&UrlSlug={model.UrlSlug}&Description={model.Description}";

                byte[] formDataBytes = Encoding.UTF8.GetBytes(formData);

                var httpContent = new ByteArrayContent(formDataBytes);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await _httpClient.PutAsync("", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error edit tag: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static async Task<bool> DeleteTag(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{id}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error delete tag: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static async Task<TagItem> GetById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var info = JsonConvert.DeserializeObject<TagItem>(content);

                    return info;
                }
                else
                {
                    MessageBox.Show($"Error fetching tag info: {response.StatusCode}");
                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static async Task<TagItem> GetByUrlSlug(string slug)
        {
            try
            {
                var response = await _httpClient.GetAsync($"urlSlug/{slug}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var info = JsonConvert.DeserializeObject<TagItem>(content);

                    return info;
                }
                else
                {
                    MessageBox.Show($"Error fetching tag info: {response.StatusCode}");
                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
    }
}
