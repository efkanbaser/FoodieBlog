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
            RuleFor(x => x.UserName)
                .NotNull().WithMessage("Enter a user name")
                .NotEmpty().WithMessage("Enter a user name");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Enter your password")
                .NotEmpty().WithMessage("Enter your password")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").WithMessage("Your password should contain least enter 8 characters, and at least one number");

            RuleFor(x => x.ConfirmPassword)
                .NotNull().WithMessage("Re-enter your password")
                .NotEmpty().WithMessage("Re-enter your password")
                .Equal(x => x.Password).WithMessage("Passwords should match");


            RuleFor(x => x.Email)
                .NotNull().WithMessage("Enter your email")
                .NotEmpty().WithMessage("Enter your email")
                .EmailAddress().WithMessage("Enter a valid email");
                //.MustAsync(IsUsed).WithMessage("This email is already used")
        }

        //public async Task<bool> IsUsed(string arg, CancellationToken cancellationToken)
        //{

        //    User u = await _userBs.Get(x => x.Email == arg);
        //    return u == null;

        //}

        //public bool SecildiMi(int arg)
        //{
        //    return !(arg == 0);

        //}
    }
}
