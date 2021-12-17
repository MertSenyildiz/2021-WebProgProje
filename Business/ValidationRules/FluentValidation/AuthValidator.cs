using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthValidator:AbstractValidator<UserForRegisterDto>
    {
        public AuthValidator()
        {
            RuleFor(u => u.Password).Length(6, 25);
            RuleFor(u => u.Password).Equal(ud => ud.PasswordValidation).WithMessage("Passwords must be the same");
        }
    }
}
