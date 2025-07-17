namespace Services.Validators
{
    internal class SubjectConfigurationDTOValidator : AbstractValidator<SubjectConfigurationDTO>
    {
        public SubjectConfigurationDTOValidator()
        {

            RuleFor(x => x.DurationInMinutes)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Exam Duration Must be Greater than 1 Minutes");

            RuleFor(x => x.QuestionNumbers)
                .GreaterThan(0)
                .WithMessage("The number of questions must be greater than 0.");

            RuleFor(x => x.HardPercentage)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Hard percentage must be greater than 0.");

            RuleFor(x => x.NormalPercentage)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Normal percentage must be greater than 0.");

            RuleFor(x => x.LowPercentage)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Low percentage must be greater than 0.");

            RuleFor(x => x)
                .Must(x => x.HardPercentage + x.NormalPercentage + x.LowPercentage == 100)
                .WithMessage("The sum of hard, normal, and low percentages must equal 100.");
        }
    }
}
