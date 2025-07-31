namespace InventoryUi.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

public class AuthorizationTokenResponse
{
    public string token { get; set; }
}
