using FluentValidation;
using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Front;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Business.ValidationRules.Front
{
    public class SignUpValidator : AbstractValidator<SignUpVm>
    {
        private readonly IUserBs _userBs;

        public SignUpValidator(IUserBs userBs)
        {
            _userBs = userBs;

            RuleFor(x => x.UserNameSignUp)
                .NotEmpty().WithMessage("Please enter a username")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters")
                .MaximumLength(30).WithMessage("Username cannot exceed 30 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Please enter your email address")
                .EmailAddress().WithMessage("Please enter a valid email address")
                .WithMessage("This email is already registered");

            RuleFor(x => x.PasswordSignUp)
                .NotNull().WithMessage("Enter your password")
                .NotEmpty().WithMessage("Enter your password")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[_#?!@$%^&*-]).{8,}$").WithMessage("Your password should contain least enter 8 characters, and at least one number");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.PasswordSignUp)
                .WithMessage("Passwords do not match");

            //RuleFor(x => x.AgreeToTerms)
            //    .Equal(true)
            //    .WithMessage("You must accept the terms and conditions");
        }
    }
}
