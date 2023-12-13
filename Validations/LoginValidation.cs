using FluentValidation;
using TestLatviaProject.View_Models;

namespace TestLatviaProject.Validations
{
    public class LoginValidation : AbstractValidator<LoginVM>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(4);
        }
    }
}
