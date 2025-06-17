using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class LoginResponseDto
    {
       
        [DataType(DataType.EmailAddress)]
        public string Usermame { get; set; }
        public string[] Roles { get; set; }

        public string JwtToken { get; set; }
    }
}
