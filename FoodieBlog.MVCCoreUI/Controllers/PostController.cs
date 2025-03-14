﻿using FoodieBlog.Business.Abstract;
using FoodieBlog.Business.Concrete.Base;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Front;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.AspNetCore.SignalR;

namespace FoodieBlog.MVCCoreUI.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostBs _postBs;
        private readonly IUserBs _userBs;
        private readonly IPostIngredientBs _ingredientBs;
        private readonly IPostDirectionBs _directionBs;
        private readonly IPostCategoryBs _postCategoryBs;
        private readonly ICategoryBs _categoryBs;
        private readonly IPostTagBs _postTagBs;
        private readonly ITagBs _tagBs;
        private readonly ICommentBs _commentBs;
        private readonly ISessionManager _session;
        private readonly IHubContext<NotificationHub> _hubContext;

        public PostController(IPostBs postBs, 
            IUserBs userBs, 
            IPostIngredientBs ingredientBs, 
            IPostDirectionBs directionBs, 
            IPostCategoryBs postCategoryBs, 
            ICategoryBs categoryBs, 
            IPostTagBs postTagBs, 
            ITagBs tagBs,
            ICommentBs commentBs, 
            ISessionManager session,
            IHubContext<NotificationHub> hubContext)
        {
            _postBs = postBs;
            _userBs = userBs;
            _ingredientBs = ingredientBs;
            _directionBs = directionBs;
            _postCategoryBs = postCategoryBs;
            _categoryBs = categoryBs;
            _postTagBs = postTagBs;
            _tagBs = tagBs;
            _commentBs = commentBs;
            _session = session;
            _hubContext = hubContext;
        }

        //TODO: Make the url look like FoodieBlog/how-to-make-the-most-delicious-smash-burger
        [Route("Post/{postName}")]
        public async Task<IActionResult> Index(string postName)
        {
            List<Post> allPosts = await _postBs.GetAllByActive();
            bool doesExist = false;
            foreach (var item in allPosts)
            {
                if (item.Url == postName)
                {
                    doesExist = true;
                }
            }


            if (doesExist)
            {

                string[] list = new string[1];
                list = ["PostCategories"];

                // Take the required parts from db
                Post post = await _postBs.Get(x => x.Url == postName, includelist: list);
                User user = await _userBs.Get(x => x.Id == post.UserId);

                #region create month and day for the post
                int monthNumber = post.PublicationDate.Value.Month;
                string month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(monthNumber);
                int day = post.PublicationDate.Value.Day;
                #endregion

                // Take Ingredients and Directions list, give them to model
                List<PostIngredient> ingredientsEntity = await _ingredientBs.GetAll(x => x.PostId == post.Id);
                List<PostDirection> directionsEntity = await _directionBs.GetAll(x => x.PostId == post.Id);

                // Take categories of the post
                List<PostCategory> postCategories = await _postCategoryBs.GetAll(x => x.PostId == post.Id);
                List<Category> categories = await _categoryBs.GetAll();
                List<string> postCategoryNames = new List<string>();
                foreach (var postcat in postCategories)
                {
                    foreach (var cat in categories)
                    {
                        if (postcat.CategoryId == cat.Id)
                        {
                            postCategoryNames.Add(cat.CategoryName);
                        }
                    }
                }

                // Take tags of the post
                List<PostTag> postTags = await _postTagBs.GetAll(x => x.PostId == post.Id);
                List<Tag> tags = await _tagBs.GetAll();
                List<string> postTagNames = new List<string>();
                foreach (var postcat in postTags)
                {
                    foreach (var cat in tags)
                    {
                        if (postcat.TagId == cat.Id)
                        {
                            postTagNames.Add(cat.TagName);
                        }
                    }
                }

                List<string> ingredients = ingredientsEntity
                    .Select(x => x.Ingredient)
                    .ToList();

                List<string> directions = directionsEntity
                    .Select(x => x.Directions)
                    .ToList();


                // Take comments of the post
                string[] listComment = new string[1];
                listComment = ["User"];
                List<PostCommentVm> comments = new List<PostCommentVm>();
                List<Comment> commentsDb = await _commentBs.GetAll(includelist: listComment);
                foreach (var item in commentsDb)
                {
                    if(item.PostId == post.Id)
                    {
                        PostCommentVm temp = new PostCommentVm
                        {
                            UserName = item.User.UserName,
                            Date = item.PublicationDate,
                            Contents = item.Contents,
                            ProfilePic = item.User.ProfilePic
                        };
                        comments.Add(temp);
                    }
                }

                // Create the model
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
                    Comments = comments,
                    PostCategories = postCategoryNames,
                    PostTags = postTagNames,
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
                };

                if (post.PreviousPostUrl != null)
                {
                    // Get previous posts for name, dates
                    try
                    {
                        Post prevPost = await _postBs.Get(x => x.Url == post.PreviousPostUrl);
                        #region create month and day for the previous post
                        int prevMonthNumber = prevPost.PublicationDate.Value.Month;
                        string prevMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(prevMonthNumber);
                        int prevDay = prevPost.PublicationDate.Value.Day;

                        model.PreviousPostUrl = prevPost.Url;
                        model.PrevTitle = prevPost.Title;
                        model.PrevPublicationMonth = prevMonth;
                        model.PrevPublicationDay = prevDay;
                        #endregion
                    }
                    catch (Exception)
                    {

                        return RedirectToAction("Error", "Home");
                    }

                }

                if (post.NextPostUrl != null)
                {
                    try
                    {
                        // Get next posts for name, dates
                        Post nextPost = await _postBs.Get(x => x.Url == post.NextPostUrl);
                        #region create month and day for the next post
                        int nextMonthNumber = nextPost.PublicationDate.Value.Month;
                        string nextMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(nextMonthNumber);
                        int nextDay = nextPost.PublicationDate.Value.Day;

                        model.NextPostUrl = nextPost.Url;
                        model.NextTitle = nextPost.Title;
                        model.NextPublicationMonth = nextMonth;
                        model.NextPublicationDay = nextDay;
                        #endregion
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Error", "Home");
                    }

                }


                // TODO: Use userId to send signalR messages when a comment is made

                return View(model);

            }

            else
            {
                return RedirectToAction("Error", "Home");
            }




        }


        [HttpPost]
        [Route("Post/Comment")]
        public async Task<IActionResult> Comment([FromForm] int id, [FromForm] string commentContent)
        {
            Comment comment = new Comment
            {
                Contents = commentContent,
                UserId = _session.ActiveUser.Id,
                PostId = id
            };
            await _commentBs.Insert(comment);

            #region send notification to the post owner
            // Get post and author information for notification
            var post = await _postBs.Get(p => p.Id == id);
            if (post != null && post.UserId != _session.ActiveUser.Id) // Don't notify if commenting on own post
            {
                // Get commenter's username
                var commenterName = _session.ActiveUser.UserName; // Use appropriate property for username

                // Get connections for the post author
                var connections = NotificationHub.GetConnectionsForUser(post.UserId.ToString());

                // Send notification to each connection
                if (connections.Any())
                {
                    foreach (var connectionId in connections)
                    {
                        await _hubContext.Clients.Client(connectionId).SendAsync(
                            "ReceiveCommentNotification",
                            post.Id,
                            commenterName,
                            post.Title // Use appropriate property for post title
                        );
                    }
                }
            }
            #endregion







            return Json( new { result = true, message = "Comment is added successfully" } );
        }
    }
}
