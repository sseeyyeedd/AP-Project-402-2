namespace SofreDaar.Models.Base
{
    public class User : Entity
    {
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? SureName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? ActivationCode { get; set; }
        public string? PhoneNumber { get; set; }

        public bool Activate(string code)
        {
            if (code == ActivationCode)
            {
                ActivationCode = "0";
                return true;
            }
            return false;
        }
    }
}
