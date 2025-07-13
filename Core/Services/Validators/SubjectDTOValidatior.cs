


namespace Services.Validators
{
    internal class SubjectDTOValidatior : AbstractValidator<SubjectDTO>
    {
        public SubjectDTOValidatior()
        {
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.Stop)

                .NotEmpty().WithMessage("Name is Required..!")
                .MinimumLength(ValidationConstants.MIN_NAME_LENGTH)
                .WithMessage($"Min length for Name: {ValidationConstants.MIN_NAME_LENGTH}")

                .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
                .WithMessage($"Max length for Name: {ValidationConstants.MAX_NAME_LENGTH}")

                .Matches("^[\\u0621-\\u064Aa-zA-Z ]+$")
                .WithMessage("Name must contain only Arabic or English letters only");
        }
    }
}
