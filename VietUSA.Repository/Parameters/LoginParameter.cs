namespace VietUSA.Repository.Parameters
{
    public class LoginParameter
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public bool IsRememberPassword { get; set; }
        public string ReturnUrl { get; set; }
        public int DepId { get; set; }
        public string DepName { get; set; }
    }
}
