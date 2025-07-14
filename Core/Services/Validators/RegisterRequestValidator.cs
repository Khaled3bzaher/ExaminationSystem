namespace Services.Validators
{
    internal class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[@$!%*?&]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(ValidationConstants.MIN_NAME_LENGTH)
                .WithMessage($"Min length for Name: {ValidationConstants.MIN_NAME_LENGTH}")

                .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
                .WithMessage($"Max length for Name: {ValidationConstants.MAX_NAME_LENGTH}")

                .Matches(@"^[\p{L} ]+$").WithMessage("Name must contain only letters (Arabic or English) and spaces.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^01[0125][0-9]{8}$").WithMessage("Phone number must be a valid Egyptian number.");
        }
    }
}
