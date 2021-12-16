using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class DeveloperValidator:AbstractValidator<Developer>
    {
        public DeveloperValidator()
        {
            RuleFor(d => d.DeveloperName).NotEmpty();
            RuleFor(d => d.DeveloperName).MinimumLength(1);
        }
    }
}
