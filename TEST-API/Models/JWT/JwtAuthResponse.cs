using System.ComponentModel.DataAnnotations;

namespace TEST_API.Models.JWT
{
    [Serializable]
    public class JwtAuthResponse
    {
        public required string Token { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public int Expires_In { get; set; }
    }
}
