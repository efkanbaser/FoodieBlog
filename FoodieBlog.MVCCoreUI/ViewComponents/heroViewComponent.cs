using FoodieBlog.Business.Abstract;
using FoodieBlog.Business.Concrete.Base;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Front;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FoodieBlog.MVCCoreUI.ViewComponents
{
    public class heroViewComponent : ViewComponent
    {
        private readonly IPostBs _postBs;
        private readonly IUserBs _userBs;
        private readonly ICommentBs _commentBs;
        private readonly ICategoryBs _categoryBs;
        private readonly IPostCategoryBs _postCategoryBs;

        public heroViewComponent(IPostBs postBs, IUserBs userBs, ICommentBs commentBs, IPostCategoryBs postCategoryBs, ICategoryBs categoryBs)
        {
            _postBs = postBs;
            _userBs = userBs;
            _commentBs = commentBs;
            _categoryBs = categoryBs;
            _postCategoryBs = postCategoryBs;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Post> allPosts = await _postBs.GetAll();
            Random random = new Random();

            var randomPosts = allPosts
                .OrderBy(p => random.Next()) 
                .Take(4)
                .ToList();

            // Get users, comments, categories, and post categories
            List<User> users = await _userBs.GetAll();
            List<Comment> comments = await _commentBs.GetAll();
            List<Category> categories = await _categoryBs.GetAll();
            List<PostCategory> postCategories = await _postCategoryBs.GetAll();

            List<HeroComponentVm> model = new();

            foreach (var item in randomPosts)
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
                HeroComponentVm vm = new HeroComponentVm
                {
                    MainImage = item.MainImage,
                    SecondaryImage = item.SecondaryImage,
                    PublicationDay = day,
                    PublicationMonth = month,
                    Categories = catNames,
                    Url = item.Url,
                    Title = item.Title,
                    UserName = users.FirstOrDefault(x => x.Id == item.UserId).UserName,
                    ReadingTime = item.ReadingTime,
                    Comments = comments.Count(x => x.PostId == item.Id)
                };
                model.Add(vm);
            }

            return View(model);
        }
    }
}
