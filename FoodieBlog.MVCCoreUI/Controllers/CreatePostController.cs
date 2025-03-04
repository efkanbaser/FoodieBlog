using AutoMapper;
using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Front;
using FoodieBlog.MVCCoreUI.Filters;
using Infrastructure.CrossCuttingConcern.Comunication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;

namespace FoodieBlog.MVCCoreUI.Controllers
{
    // TODO: If a post has been deleted, the pointers will explode, think of a fix
    // TODO: Add an entity called draft storage and hold the values if the user quits while writing a post
    [UserFilter]
    public class CreatePostController : Controller
    {
        private readonly ICategoryBs _categoryBs;
        private readonly IPostCategoryBs _postCategoryBs;
        private readonly ITagBs _tagBs;
        private readonly IPostTagBs _postTagBs;
        private readonly IUserBs _userBs;
        private readonly ISessionManager _session;
        private readonly IPostBs _postBs;
        private readonly IPostIngredientBs _ingredientsBs;
        private readonly IPostDirectionBs _directionsBs;
        private readonly IMemoryCache _cache;


        public CreatePostController(ICategoryBs categoryBs, ITagBs tagBs, IUserBs userBs, ISessionManager session, IPostBs postBs, IPostDirectionBs directionBs, IPostIngredientBs ingredientBs, IPostTagBs postTagBs, IPostCategoryBs postCategoryBs, IMemoryCache cache)
        {
            _categoryBs = categoryBs;
            _tagBs = tagBs;
            _userBs = userBs;
            _session = session;
            _postBs = postBs;
            _ingredientsBs = ingredientBs;
            _directionsBs = directionBs;
            _postTagBs = postTagBs;
            _postCategoryBs = postCategoryBs;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _categoryBs.GetAll();
            List<Tag> tags = await _tagBs.GetAll();


            AddPostVm model = new AddPostVm();

            model.Categories = categories.Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.Id.ToString() }).ToList();

            model.Tags = tags.Select(x => new SelectListItem() { Text = x.TagName, Value = x.Id.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        [Route("/CreatePost/AddPost",
            Name = "addpost")]
        public async Task<IActionResult> AddPost(AddPostVm vm, IFormFile MainImage, IFormFile SecondaryImage)
        {
            if (ModelState.IsValid)
            {
                // Create new post, active is set to true for now but in publication, it'll be set to false
                Post post = new Post
                {
                    UserId = _session.ActiveUser.Id,
                    Title = vm.Title,
                    ReadingTime = vm.ReadingTime,
                    Contents = vm.Contents,
                    ServingSize = vm.ServingSize,
                    PrepTime = vm.PrepTime,
                    CookTime = vm.CookTime,
                    MiddleText = vm.MiddleText,
                    DescriptionFirst = vm.DescriptionFirst,
                    MoreDetails = vm.MoreDetails,
                    LastText = vm.LastText,
                    DescriptionHeader = vm.DescriptionHeader,
                    DescriptionLast = vm.DescriptionLast,
                    Quote = vm.Quote,
                    Active = true,
                    CreatorId = _session.ActiveUser.Id
                };


                #region add images if they exist
                // Save images to wwroot and give their file path to db
                if (MainImage != null && MainImage.Length > 0)
                {
                    var mainImagePath = await SaveImageToWebRoot(MainImage, "/frontassets/img/main");
                    post.MainImage = mainImagePath;
                }

                if (SecondaryImage != null && SecondaryImage.Length > 0)
                {
                    var secondaryImagePath = await SaveImageToWebRoot(SecondaryImage, "/frontassets/img/secondary");
                    post.SecondaryImage = secondaryImagePath;
                }
                #endregion
                post = await _postBs.Insert(post);
                #region add directions
                string directionsReceived = vm.Directions;
                string directionsCleaned = directionsReceived.Trim('[', ']');
                List<string> directions = directionsCleaned.Split(',')
                                                      .Select(s => s.Trim().Trim('"'))
                                                      .ToList();

                foreach (string item in directions)
                {
                    PostDirection direction = new PostDirection
                    {
                        PostId = post.Id,
                        Directions = item
                    };
                    await _directionsBs.Insert(direction);
                }
                #endregion
                #region add ingredients
                string ingredientsReceived = vm.Ingredients;
                string ingredientsCleaned = ingredientsReceived.Trim('[', ']');
                List<string> ingredients = ingredientsCleaned.Split(',')
                                                      .Select(s => s.Trim().Trim('"'))
                                                      .ToList();

                foreach (string item in ingredients)
                {
                    PostIngredient ingredient = new PostIngredient
                    {
                        PostId = post.Id,
                        Ingredient = item
                    };
                    await _ingredientsBs.Insert(ingredient);
                }
                #endregion
                #region add categories
                string categoriesReceived = vm.CategorieChosen;
                string categoriesCleaned = categoriesReceived.Trim('[', ']');
                List<string> categories = categoriesCleaned.Split(',')
                                                      .Select(s => s.Trim().Trim('"'))
                                                      .ToList();






                foreach (string item in categories)
                {
                   PostCategory category = new PostCategory
                   {
                       PostId = post.Id,
                       CategoryId = Int32.Parse(item) // you can maybe use tryparse
                   };

                   await _postCategoryBs.Insert(category);                   
                }
                #endregion
                #region add tags
                string tagsReceived = vm.TagChosen;
                string tagsCleaned = tagsReceived.Trim('[', ']');
                List<string> tags = tagsCleaned.Split(',')
                                                      .Select(s => s.Trim().Trim('"'))
                                                      .ToList();

                foreach (string item in tags)
                {
                    PostTag tag = new PostTag
                    {
                        PostId = post.Id,
                        TagId = Int32.Parse(item)
                    };
                    await _postTagBs.Insert(tag);
                }
                #endregion
                #region add pointers to previous and next posts
                post.PreviousPostId = post.Id - 1;
                post.NextPostId = post.Id - 2;
                post = await _postBs.Update(post);
                #endregion

                await _postBs.Update(post);

                TempData["PostCreated"] = true;

                return RedirectToAction("Success", "CreatePost", new { newPostId = post.Id });
            }
            else
            {
                //return View("Index", "CreatePost", new { model });
                return View();
            }

        }

        public IActionResult Success(int newPostId)
        {
            if (TempData["PostCreated"] == null || !(bool)TempData["PostCreated"])
            {
                return RedirectToAction("Index", "Home");
            }
            TempData.Remove("PostCreated");


            SuccessfulAddVm model = new SuccessfulAddVm
            {
                PostId = newPostId
            };
            //Post post = await _postBs.Get(x => x.Id == newPostId);
            // THIS'LL BE A CONTROLLER THAT'LL REDIRECT TO THE CREATED POST
            return View(model);
        }

        private async Task<string> SaveImageToWebRoot(IFormFile imageFile, string folderPath)
        {
            // This works but this is terrible and won't work in unix based systems
            // Ensure the folder exists
            var webRootPath = Directory.GetCurrentDirectory() + "/wwwroot" + folderPath;
            if (!Directory.Exists(webRootPath))
            {
                Directory.CreateDirectory(webRootPath);
            }

            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(webRootPath, fileName).Replace("\\", "/");

            // Save the file to the wwwroot folder
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return the relative path to store in the database
            return Path.Combine(folderPath, fileName).Replace("\\", "/");
        }
    }
}
