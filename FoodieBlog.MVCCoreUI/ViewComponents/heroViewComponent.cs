using FoodieBlog.Business.Abstract;
using FoodieBlog.Business.Concrete.Base;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Front;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;

        public heroViewComponent(IPostBs postBs, IUserBs userBs, ICommentBs commentBs, IPostCategoryBs postCategoryBs, ICategoryBs categoryBs, IMemoryCache memoryCache)
        {
            _postBs = postBs;
            _userBs = userBs;
            _commentBs = commentBs;
            _categoryBs = categoryBs;
            _postCategoryBs = postCategoryBs;
            _memoryCache = memoryCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Use GetOrCreateAsync with proper null check handling
            var model = await _memoryCache.GetOrCreateAsync("RandomHeroPosts", async entry =>
            {
                // Set cache options
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                entry.SetPriority(CacheItemPriority.High);

                #region get data
                List<Post> allPosts = await _postBs.GetAll();
                List<User> users = await _userBs.GetAll();
                List<Comment> comments = await _commentBs.GetAll();
                List<Category> categories = await _categoryBs.GetAll();
                List<PostCategory> postCategories = await _postCategoryBs.GetAll();
                #endregion

                // Get random posts
                Random random = new Random();
                var randomPosts = allPosts
                    .OrderBy(p => random.Next())
                    .Take(4)
                    .ToList();

                var result = randomPosts.Select(post => {
                    var postCategoryIds = postCategories
                        .Where(pc => pc.PostId == post.Id)
                        .Select(pc => pc.CategoryId);

                    var categoryNames = categories
                        .Where(c => postCategoryIds.Contains(c.Id))
                        .Select(c => c.CategoryName)
                        .ToList();

                    // Get date parts
                    var publicationDate = post.PublicationDate ?? DateTime.Now;
                    string month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(publicationDate.Month);
                    int day = publicationDate.Day;

                    // Find user with null check
                    var user = users.FirstOrDefault(x => x.Id == post.UserId);
                    var userName = user?.UserName ?? "Unknown";

                    // Create view model
                    return new HeroComponentVm
                    {
                        MainImage = post.MainImage,
                        SecondaryImage = post.SecondaryImage,
                        PublicationDay = day,
                        PublicationMonth = month,
                        Categories = categoryNames,
                        Url = post.Url,
                        Title = post.Title,
                        UserName = userName,
                        ReadingTime = post.ReadingTime,
                        Comments = comments.Count(x => x.PostId == post.Id)
                    };
                }).ToList();

                return result;
            });

            return View(model); 
        }
    }
}
