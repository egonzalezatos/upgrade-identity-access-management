using FluentValidation;
using IdentityServer.DTO.Login;
using IdentityServer.I18N.ErrorMessages;

namespace IdentityServer.DTO.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {

        public LoginDtoValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty()
                .WithMessage(LoginErrorMessages.PARAM_REQUIRED(nameof(LoginDto.Username)));
            RuleFor(x => x.Password).NotNull().NotEmpty()
                .WithMessage(LoginErrorMessages.PARAM_REQUIRED(nameof(LoginDto.Password)));
        }
    }
}