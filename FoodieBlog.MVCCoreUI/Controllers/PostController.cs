﻿using FoodieBlog.Business.Abstract;
using FoodieBlog.Business.Concrete.Base;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Front;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;

namespace FoodieBlog.MVCCoreUI.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostBs _postBs;
        private readonly IUserBs _userBs;
        private readonly IPostIngredientBs _ingredientBs;
        private readonly IPostDirectionBs _directionBs;

        public PostController(IPostBs postBs, IUserBs userBs, IPostIngredientBs ingredientBs, IPostDirectionBs directionBs)
        {
            _postBs = postBs;
            _userBs = userBs;
            _ingredientBs = ingredientBs;
            _directionBs = directionBs;
        }

        //TODO: Make the url look like FoodieBlog/how-to-make-the-most-delicious-smash-burger-evet
        //TODO: Return an error when postname doesn't exist
        //TOOD: Add a user filter to create posts
        [Route("Home/Posts/{postId}")]
        public async Task<IActionResult> Index(int postId)
        {
            string[] list = new string[2];
            list = ["Comments", "PostCategories"];

            // Take the required parts from db
            Post post = await _postBs.Get(x => x.Id == postId, includelist: list);
            User user = await _userBs.Get(x => x.Id == post.UserId);

            #region create month and day for the post
            int monthNumber = post.PublicationDate.Value.Month;
            string month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(monthNumber);
            int day = post.PublicationDate.Value.Day;
            #endregion

            // Take Ingredients and Directions list, give them to model
            List<PostIngredient> ingredientsEntity = await _ingredientBs.GetAll(x => x.PostId == post.Id);
            List<PostDirection> directionsEntity = await _directionBs.GetAll(x => x.PostId == post.Id);

            List<string> ingredients = ingredientsEntity
                .Select(x => x.Ingredient)
                .ToList();

            List<string> directions = directionsEntity
                .Select(x => x.Directions)
                .ToList();

            // Get previous posts for name, dates
            Post prevPost = await _postBs.Get(x => x.Id == post.PreviousPostId);
            #region create month and day for the previous post
            int prevMonthNumber = prevPost.PublicationDate.Value.Month;
            string prevMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(prevMonthNumber);
            int prevDay = prevPost.PublicationDate.Value.Day;
            #endregion

            // Get next posts for name, dates
            Post nextPost = await _postBs.Get(x => x.Id == post.NextPostId);
            #region create month and day for the next post
            int nextMonthNumber = nextPost.PublicationDate.Value.Month;
            string nextMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(nextMonthNumber);
            int nextDay = nextPost.PublicationDate.Value.Day;
            #endregion


            // Create the model
            // TODO: Use userId to send signalR messages when a comment is made
            // Reminder: when creating a post, the previous post will be postid-1 and next post will be postid-2
            PostIndexVm model = new PostIndexVm
            {
                Id = post.Id,
                Title = post.Title,
                Contents = post.Contents,
                MainImage = post.MainImage,
                ReadingTime = post.ReadingTime,
                SecondaryImage = post.SecondaryImage,
                PrepTime = post.PrepTime,
                CookTime = post.CookTime,
                ServingSize = post.ServingSize,
                Ingredients = ingredients,
                Directions = directions,
                UserName = user.UserName,
                PublicationDay = day,
                PublicationMonth = month,
                Comments = post.Comments,
                PostCategories = post.PostCategories,
                UserId = user.Id,
                MiddleText = post.MiddleText,
                DescriptionFirst = post.DescriptionFirst,
                DescriptionHeader = post.DescriptionHeader,
                DescriptionLast = post.DescriptionLast,
                MoreDetails = post.MoreDetails,
                LastText = post.LastText,
                Quote = post.Quote,
                UserBio = user.Bio,
                UserPic = user.ProfilePic,
                PreviousPostId = prevPost.Id,
                PrevTitle = prevPost.Title,
                PrevPublicationMonth = prevMonth,
                PrevPublicationDay = prevDay,
                NextPostId = nextPost.Id,
                NextTitle = nextPost.Title,
                NextPublicationMonth = nextMonth,
                NextPublicationDay = nextDay
            };

            

            return View(model);
        }
    }
}
