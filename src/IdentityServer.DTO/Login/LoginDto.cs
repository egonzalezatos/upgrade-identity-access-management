using FluentValidation;
using IdentityServer.DTO.Validators;

namespace IdentityServer.DTO.Login
{
    public record LoginDto : Dto
    {
        private readonly LoginDtoValidator _validator = new();

        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        
        public string ReturnUrl { get; set; }
        
        public override bool TryValidate()
        {
            _validator.ValidateAndThrow(this);
            return true;
        }
    }
}