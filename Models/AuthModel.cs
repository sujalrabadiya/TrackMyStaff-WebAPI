namespace TrackMyStaffWebApplication.Models
{
    public class RegisterationModel
    {
        public string CName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Admin";
    }

    public class LoginModel
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    public class ProfileModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? email { get; set; }
        public string? img_url { get; set; }
        public string role { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
    }
}
