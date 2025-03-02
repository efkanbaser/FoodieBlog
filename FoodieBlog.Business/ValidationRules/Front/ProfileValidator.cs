using FluentValidation;
using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.ViewModel.Front;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Business.ValidationRules.Front
{
    public class ProfileValidator : AbstractValidator<MyAccountVm>
    {
        public ProfileValidator()
        {

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Please enter a username")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters")
                .MaximumLength(30).WithMessage("Username cannot exceed 30 characters");


            RuleFor(x => x.Password)
                .NotNull().WithMessage("Enter your password")
                .NotEmpty().WithMessage("Enter your password")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[_#?!@$%^&*-]).{8,}$").WithMessage("Your password should contain least enter 8 characters, and at least one number");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");

            RuleFor(x => x.Bio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters");

            //RuleFor(x => x.AgreeToTerms)
            //    .Equal(true)
            //    .WithMessage("You must accept the terms and conditions");
        }
    }
}
