namespace BlogWebApi.Models.Account
{
    public class LoginViewModel
    {
        /// <summary>
        /// Username
        /// </summary>
        /// <example>admin@gmail.com</example>
        public string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        /// <example>admin</example>
        public string Password { get; set; }
    }
}
