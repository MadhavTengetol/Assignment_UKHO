using Assignment_UKHO.Data;
using Assignment_UKHO.Dto;
using FluentValidation;

namespace Assignment_UKHO.Validations
{
    public class BatchValidator : AbstractValidator<BatchDto>
    {

        public BatchValidator()
        {
            RuleFor(b => b.BusinessUnit).NotNull().NotEmpty().WithMessage("Business Unit Should not be null");

            RuleFor(b => b.Acl).NotNull().NotEmpty().WithMessage("Acl should not be null").ChildRules(child =>
            {
                child.RuleForEach(c => c.ReadUsers).NotEmpty().NotNull().WithMessage("User should not be null").ChildRules(child =>
                {
                    child.RuleFor(c => c.User).NotEmpty().NotNull().WithMessage("User Should not be null");
                });
                child.RuleForEach(c => c.ReadGroups).NotEmpty().NotNull().WithMessage("Group should not be null").ChildRules(child =>
                {
                    child.RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("Group Name Should not be null");
                });
            });

            RuleForEach(b => b.Attributes).NotEmpty().NotNull().WithMessage("Attributes should not be null").ChildRules(child =>
            {
                child.RuleFor(c => c.Key).NotNull().NotEmpty().WithMessage("Attribute Key should not be null");
                child.RuleFor(c => c.Value).NotNull().NotEmpty().WithMessage("Attribute Value should not be null");
            });

            RuleFor(b => b.ExpiryDate).Must(b => b > DateTime.Today).WithMessage("Expiry Date must not be in Past")
                                      .NotNull().NotEmpty().WithMessage("Expiry date should not be null");


            RuleForEach(b => b.Files).NotNull().NotEmpty().WithMessage("Atleast One file should be there").ChildRules(child =>
            {
                child.RuleFor(c => c.FileName).NotEmpty().NotNull().WithMessage("Filename is Required");
                child.RuleFor(c => c.FileSize).NotNull().NotEmpty().WithMessage("FileSize should not be null or Zero")
                                                .ExclusiveBetween(1, 200).WithMessage("FileSize should be between 1 -200");
                child.RuleFor(c => c.MimeType).NotEmpty().NotNull().WithMessage("MimeType Should not be null");
                child.RuleFor(c => c.Hash).NotEmpty().NotNull().WithMessage("Hash Should not be null"); ;
                child.RuleForEach(c => c.Attributes).NotNull().NotEmpty().ChildRules(subchild =>
                {
                    subchild.RuleFor(s => s.Key).NotEmpty().WithMessage("File Attribute Key is Required");
                    subchild.RuleFor(s => s.Value).NotEmpty().WithMessage("File Attribute Value is Required");
                });
            });
        }
    }
}
