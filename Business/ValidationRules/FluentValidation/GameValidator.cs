using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class GameValidator : AbstractValidator<Game>
    {
        public GameValidator()
        {
            RuleFor(g => g.GameName).NotEmpty();
            RuleFor(g => g.ReleaseDate).NotEmpty();
            RuleFor(g => g.GameName).MinimumLength(1);
            RuleFor(g => g.CategoryID).NotEmpty();
            RuleFor(g => g.DeveloperID).NotEmpty();
            RuleFor(g => g.PublisherID).NotEmpty();
            //RuleFor(b => ....).Must(KendiKuralın);
        }
    }
}
