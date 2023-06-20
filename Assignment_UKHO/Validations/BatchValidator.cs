using Assignment_UKHO.Data;
using FluentValidation;

namespace Assignment_UKHO.Validations
{
    public class BatchValidator : AbstractValidator<Batch>
    {

        public BatchValidator()
        {
            RuleFor(b => b.BusinessUnit).NotNull().NotEmpty().WithMessage("Business Unit Should not be null");

            RuleForEach(b => b.Files).NotEmpty().WithMessage("Atleast One file should be there").ChildRules(child =>
            {
                child.RuleFor(c => c.FileName).NotEmpty().NotNull().WithMessage("Filename is Required");
                child.RuleForEach(c => c.Attributes).NotNull().NotEmpty().ChildRules(subchild =>
                {
                    subchild.RuleFor(s => s.Key).NotEmpty().WithMessage("File Attribute Key is Required");
                    subchild.RuleFor(s => s.Value).NotEmpty().WithMessage("File Attribute Value is Required");
                });
            });
        }
    }
}
