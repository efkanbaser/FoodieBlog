using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Front;
using Infrastructure.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace FoodieBlog.MVCCoreUI.ViewComponents
{
    public class categoryViewComponent : ViewComponent
    {
        private readonly IPostBs _postBs;
        private readonly IUserBs _userBs;
        private readonly ICommentBs _commentBs;
        private readonly ICategoryBs _categoryBs;
        private readonly IPostCategoryBs _postCategoryBs;

        public categoryViewComponent(IPostBs postBs, IUserBs userBs, ICommentBs commentBs, IPostCategoryBs postCategoryBs, ICategoryBs categoryBs)
        {
            _postBs = postBs;
            _userBs = userBs;
            _commentBs = commentBs;
            _categoryBs = categoryBs;
            _postCategoryBs = postCategoryBs;

        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 1, int pageSize = 6, int categoryId = 0)
        {
            if (int.TryParse(HttpContext.Request.Query["page"], out int queryPage))
            {
                page = queryPage;
            }

            // Get raw query string
            var rawQuery = HttpContext.Request.QueryString.Value;

            // Try to parse directly from query string
            if (categoryId == 0)
            {
                string categoryStr = HttpContext.Request.Query["categoryId"].ToString();
                if (!string.IsNullOrEmpty(categoryStr) && int.TryParse(categoryStr, out int queryCategoryId))
                {
                    categoryId = queryCategoryId;
                }
            }

            // Get data based on whether a category is selected
            PagingResult<Post> pagedPosts;
            List<Post> pagedPostsTemp;

            if (categoryId > 0)
            {
                // Get posts for a specific category
                var categoryPosts = await _postCategoryBs.GetAllPaging(page, pageSize, filter: x=> x.CategoryId == categoryId);
                var postIds = categoryPosts.Data.Select(x => x.PostId);
                pagedPostsTemp = await _postBs.GetAll();
                pagedPosts = new PagingResult<Post>(pagedPostsTemp.Where(x => postIds.Contains(x.Id)).ToList(), page, pageSize);
                // Get the category name
                var category = await _categoryBs.GetById(categoryId);
                ViewBag.CategoryName = category?.CategoryName ?? "Category";
            }
            else
            {
                // Get all posts
                pagedPosts = await _postBs.GetAllPaging(page, pageSize);
                ViewBag.CategoryName = "Recipes";
            }


            List<CategoryComponentVm> model = new();

            // Get users, comments, categories, and post categories
            List<User> users = await _userBs.GetAll();
            List<Comment> comments = await _commentBs.GetAll();
            List<Category> categories = await _categoryBs.GetAll();
            List<PostCategory> postCategories = await _postCategoryBs.GetAll();

            ViewBag.AllCategories = categories;
            foreach (var item in pagedPosts.Data)
            {
                #region get category names attached to this post
                // OMG THIS IS SO BAD I DON'T EVEN WANT TO LOOK AT THIS
                List<PostCategory> attachedCategories = postCategories.FindAll(x => x.PostId == item.Id);

                List<string> catNames = new List<string>();

                foreach (var cat in attachedCategories)
                {
                    foreach (var catName in categories)
                    {
                        if (cat.CategoryId == catName.Id)
                        {
                            catNames.Add(catName.CategoryName);
                        }
                    }
                }
                int monthNumber = item.PublicationDate.Value.Month;
                string month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(monthNumber);
                int day = item.PublicationDate.Value.Day;
                #endregion
                CategoryComponentVm vm = new CategoryComponentVm
                {
                    SecondaryImage = item.SecondaryImage,
                    PublicationDay = day,
                    PublicationMonth = month,
                    Categories = catNames,
                    Url = item.Url,
                    Title = item.Title,
                    UserName = users.FirstOrDefault(x => x.Id == item.UserId).UserName,
                    ReadingTime = item.ReadingTime,
                    Comments = comments.Count(x => x.PostId == item.Id),
                    Contents = item.Contents
                };
                model.Add(vm);
            }


            int totalPages = (int)Math.Ceiling((double)pagedPosts.TotalCount / pageSize);

            // Add pagination information to ViewBag to access in the view
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = pagedPosts.TotalCount;

            return View(model);
        }
    }
}
