using FluentValidation;
using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Front;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Business.ValidationRules.Front
{
    public class AddPostValidator : AbstractValidator<AddPostVm>
    {
        private readonly ITagBs _tagBs;
        private readonly ICategoryBs _categoryBs;

        public AddPostValidator(ITagBs tagBs, ICategoryBs categoryBs)
        {
            _tagBs = tagBs;
            _categoryBs = categoryBs;

            DefineValidationRules();
        }

        private void DefineValidationRules()
        {
            // MainImage: Required, must be a valid URL or base64 string
            //RuleFor(x => x.MainImage)
            //    .NotEmpty().WithMessage("Main image is required.")
            //    .Must(BeAValidImage).WithMessage("Main image must be a valid URL or base64 string.");

            // Title: Required, minimum length of 5, maximum length of 100
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(5, 100).WithMessage("Title must be between 5 and 100 characters.");

            // ReadingTime: Optional, must be a positive number if provided
            RuleFor(x => x.ReadingTime)
                .GreaterThan(0).When(x => x.ReadingTime.HasValue)
                .WithMessage("Reading time must be a positive number.");

            // Contents: Required, minimum length of 50
            RuleFor(x => x.Contents)
                .NotEmpty().WithMessage("Contents are required.")
                .MinimumLength(50).WithMessage("Contents must be at least 50 characters.");

            // SecondaryImage: Optional, must be a valid URL or base64 string if provided
            //RuleFor(x => x.SecondaryImage)
            //    .Must(BeAValidImage).When(x => !string.IsNullOrEmpty(x.SecondaryImage))
            //    .WithMessage("Secondary image must be a valid URL or base64 string.");

            // ServingSize: Required, maximum length of 50
            RuleFor(x => x.ServingSize)
                .NotEmpty().WithMessage("Serving size is required.")
                .MaximumLength(50).WithMessage("Serving size cannot exceed 50 characters.");

            // PrepTime: Optional, must be a positive number if provided
            RuleFor(x => x.PrepTime)
                .GreaterThan(0).When(x => x.PrepTime.HasValue)
                .WithMessage("Preparation time must be a positive number.");

            // CookTime: Optional, must be a positive number if provided
            RuleFor(x => x.CookTime)
                .GreaterThan(0).When(x => x.CookTime.HasValue)
                .WithMessage("Cooking time must be a positive number.");

            // MiddleText: Optional, maximum length of 500
            RuleFor(x => x.MiddleText)
                .MaximumLength(500).WithMessage("Middle text cannot exceed 500 characters.");

            // DescriptionFirst: Optional, maximum length of 500
            RuleFor(x => x.DescriptionFirst)
                .MaximumLength(500).WithMessage("First description cannot exceed 500 characters.");

            // MoreDetails: Optional, maximum length of 1000
            RuleFor(x => x.MoreDetails)
                .MaximumLength(1000).WithMessage("More details cannot exceed 1000 characters.");

            // LastText: Optional, maximum length of 500
            RuleFor(x => x.LastText)
                .MaximumLength(500).WithMessage("Last text cannot exceed 500 characters.");

            // DescriptionHeader: Optional, maximum length of 100
            RuleFor(x => x.DescriptionHeader)
                .MaximumLength(100).WithMessage("Description header cannot exceed 100 characters.");

            // DescriptionLast: Optional, maximum length of 500
            RuleFor(x => x.DescriptionLast)
                .MaximumLength(500).WithMessage("Last description cannot exceed 500 characters.");

            // Quote: Optional, maximum length of 200
            RuleFor(x => x.Quote)
                .MaximumLength(200).WithMessage("Quote cannot exceed 200 characters.");

            // Directions: Required, minimum length of 50
            RuleFor(x => x.Directions)
                .NotEmpty().WithMessage("Directions are required.");
            //.MinimumLength(10).WithMessage("Directions must be at least 50 characters.");

            // Ingredients: Required, minimum length of 20
            RuleFor(x => x.Ingredients)
                .NotEmpty().WithMessage("Ingredients are required.");
            //.MinimumLength(10).WithMessage("Ingredients must be at least 20 characters.");

            // Validate TagChosen
            RuleFor(x => x.TagChosen)
                .NotEmpty().WithMessage("At least one tag is required.");

            // Validate CategorieChosen
            RuleFor(x => x.CategorieChosen)
                .NotEmpty().WithMessage("At least one category is required.");
        }

        // Custom validation method for images
        //private bool BeAValidImage(string image)
        //{
        //    // Check if the image is a valid URL or base64 string
        //    return Uri.TryCreate(image, UriKind.Absolute, out _) ||
        //           image.StartsWith("data:image/");
        //}

        //private async Task<bool> BeAValidTag(string tag)
        //{
        //    bool result = false;
        //    List<Tag> validTags = await _tagBs.GetAll();




        //    return result;
        //}

        //private bool BeAValidCategory(string category)
        //{
        //    return !string.IsNullOrEmpty(category);
        //}
    }
}
