using BlogWPF.Data;
using BlogWPF.Models.Post;
using BlogWPF.Models.Tag;
using BlogWPF.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BlogWPF.Controllers
{
    public static class PostController
    {
        private static readonly HttpClient _httpClient;

        static PostController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5078/api/post/");

            var token = Encoding.UTF8.GetString(TokenManager.Token);
            if (token != null && token.Length != 0)
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public static async Task<List<PostItem>> GetPostListAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var info = JsonConvert.DeserializeObject<List<PostItem>>(content);

                    return info;
                }
                else
                {
                    MessageBox.Show($"Error fetching post info: {response.StatusCode}");
                    return null;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static HttpContent ConvertToFormData(PostCreate post)
        {
            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(post.Title), "Title");
            formData.Add(new StringContent(post.ShortDescription), "ShortDescription");
            formData.Add(new StringContent(post.Description), "Description");
            formData.Add(new StringContent(post.Meta), "Meta");
            formData.Add(new StringContent(post.UrlSlug), "UrlSlug");
            formData.Add(new StringContent(post.Published.ToString()), "Published");
            formData.Add(new StringContent(post.CategoryId.ToString()), "CategoryId");

            for (int i = 0; i < post.Tags.Count; i++)
            {
                formData.Add(new StringContent(post.Tags[i].ToString()), $"Tags[{i}]");
            }

            return formData;
        }

        public static HttpContent ConvertToFormData(PostEdit post)
        {
            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(post.Id.ToString()), "Id");
            formData.Add(new StringContent(post.Title), "Title");
            formData.Add(new StringContent(post.ShortDescription), "ShortDescription");
            formData.Add(new StringContent(post.Description), "Description");
            formData.Add(new StringContent(post.Meta), "Meta");
            formData.Add(new StringContent(post.UrlSlug), "UrlSlug");
            formData.Add(new StringContent(post.Published.ToString()), "Published");
            formData.Add(new StringContent(post.CategoryId.ToString()), "CategoryId");

            for (int i = 0; i < post.Tags.Count; i++)
            {
                formData.Add(new StringContent(post.Tags[i].ToString()), $"Tags[{i}]");
            }

            return formData;
        }

        public static async Task<bool> CreatePost(PostCreate model)
        {
            try
            {
                var httpContent = ConvertToFormData(model);

                var response = await _httpClient.PostAsync("", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error create post: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static async Task<bool> UpdatePost(PostEdit model)
        {
            try
            {
                var httpContent = ConvertToFormData(model);

                var response = await _httpClient.PutAsync("", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error edit post: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static async Task<bool> DeletePost(int id)
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
                    MessageBox.Show($"Error delete post: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static async Task<PostItem> GetById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var info = JsonConvert.DeserializeObject<PostItem>(content);

                    return info;
                }
                else
                {
                    MessageBox.Show($"Error fetching post info: {response.StatusCode}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static async Task<PostItem> GetByUrlSlug(string slug)
        {
            try
            {
                var response = await _httpClient.GetAsync($"urlSlug/{slug}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var info = JsonConvert.DeserializeObject<PostItem>(content);

                    return info;
                }
                else
                {
                    MessageBox.Show($"Error fetching post info: {response.StatusCode}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
    }
}
