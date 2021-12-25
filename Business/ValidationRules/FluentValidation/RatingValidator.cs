using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class RatingValidator:AbstractValidator<Rating>
    {
        public RatingValidator()
        {
            RuleFor(r => r.Comment).NotEmpty();
            RuleFor(r => r.Comment).Length(1,256);
            RuleFor(r => r.GameID).NotNull();
            RuleFor(r => r.UserID).NotNull();
            RuleFor(r => r.Rate).InclusiveBetween(1, 5);
        }
    }
}
