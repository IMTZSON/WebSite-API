namespace TEST_API.Models.JWT
{
    [Serializable]
    public class AuthRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
