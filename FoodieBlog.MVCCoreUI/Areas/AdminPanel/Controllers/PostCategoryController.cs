using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Areas.AdminPanel;
using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [AdminFilter]
    public class PostCategoryController: Controller
    {
        private readonly IPostCategoryBs _postCategoryBs;
        private readonly IPostBs _postBs;
        private readonly ICategoryBs _categoryBs;

        public PostCategoryController(
            IPostCategoryBs postCcategoryBs,
            IPostBs postBs,
            ICategoryBs categoryBs)
        {
            _postCategoryBs = postCcategoryBs;
            _postBs = postBs;
            _categoryBs = categoryBs;
        }

        public async Task<IActionResult> Index()
        {
            PostCategoryIndexVm vm = new PostCategoryIndexVm();
            List<Category> cat = await _categoryBs.GetAll();

            vm.CategoriesList = cat.Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.CategoryName }).ToList();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            string[] include = new string[2];
            include = ["Category", "Post"];


            List<PostCategoryIndexVm> vm = new List<PostCategoryIndexVm>();
            List<PostCategory> PostCategorys = await _postCategoryBs.GetAll(includelist:include);

            HashSet<int> iterated = new HashSet<int>();

            foreach (var item in PostCategorys)
            {
                if (iterated.Contains((int)item.PostId))
                {
                    continue;
                }
                else
                {
                    PostCategoryIndexVm PostCategoryvm = new PostCategoryIndexVm
                    {
                        Id = item.Id,
                        CategoryName = item.Category.CategoryName,
                        CategoryId = item.CategoryId,
                        PostName = item.Post.Title,
                        PostId = item.PostId,
                        Active = item.Active,
                        CategoryNames = new List<string>()
                    };

                    vm.Add(PostCategoryvm);
                    iterated.Add((int)item.PostId);
                }

            }

            foreach (var item in vm)
            {
                List<string> categories = new List<string>();
                foreach (var pc in PostCategorys)
                {
                    if (item.PostId == pc.PostId)
                    {
                        categories.Add(pc.Category.CategoryName);
                    }
                }

                item.CategoryNames = categories;
            }

            return Json(new { data = vm });
        }

        [HttpPost]
        public async Task<IActionResult> Add(IFormCollection data)
        {
            PostCategory PostCategory = new PostCategory();
            //PostCategory.PostCategoryName = data["PostCategoryName"];
            PostCategory.Active = true;

            await _postCategoryBs.Insert(PostCategory);


            return Json(new { result = true, mesaj = "Post Category is added successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Update(IFormCollection data)
        {
            int Id = Convert.ToInt32(data["Id"]);
            PostCategory PostCategory = new PostCategory();
            List<Post> posts = await _postBs.GetAll();
            List<Category> categories = await _categoryBs.GetAll();

            int postid = 0;
            int categoryid = 0;

            foreach (var item in posts)
            {
                if (data["PostName"] == item.Title)
                {
                    postid = item.Id;
                }
            }

            foreach (var item in categories)
            {
                if (data["CategorySelect"] == item.CategoryName)
                {
                    categoryid = item.Id;
                }
            }

            PostCategory.PostId = postid;
            PostCategory.CategoryId = categoryid;
            PostCategory.Active = true;

            await _postCategoryBs.Insert(PostCategory);

            // no need to send a model currently
            //PostCategoryIndexVm vm = new PostCategoryIndexVm();

            //vm.CategoriesList = categories.Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.CategoryName }).ToList();

            return Json(new { result = true, message = "Post Category is updated successfully"});
        }

        public async Task<IActionResult> Delete(IFormCollection data)
        {
            List<Post> posts = await _postBs.GetAll();
            List<Category> categories = await _categoryBs.GetAll();

            int postid = 0;
            int categoryid = 0;

            foreach (var item in posts)
            {
                if (data["PostName"] == item.Title)
                {
                    postid = item.Id;
                }
            }

            foreach (var item in categories)
            {
                if (data["CategorySelect"] == item.CategoryName)
                {
                    categoryid = item.Id;
                }
            }


            PostCategory k = await _postCategoryBs.Get(x => x.PostId == postid && x.CategoryId == categoryid);

            _postCategoryBs.Delete(k);

            return Json(new { result = true, mesaj = "Post Category is deleted successfully" });
        }

        public async Task<IActionResult> ActiveInactive(int id, bool active)
        {
            PostCategory k = await _postCategoryBs.Get(x => x.Id == id);
            k.Active = active;
            await _postCategoryBs.Update(k);

            //  Thread.Sleep(2000);
            return Json(new { result = true, mesaj = "Activity is updated successfully" });
        }

        public async Task<IActionResult> GetPostCategory(int id)
        {

            PostCategory k = await _postCategoryBs.Get(x => x.Id == id);

            Post p = await _postBs.GetById((int)k.PostId);

            List<Category> c = await _categoryBs.GetAll();
            List<PostCategory> pc = await _postCategoryBs.GetAll(x => x.PostId == k.PostId);

            List<PostCategoryIndexVm> model = new List<PostCategoryIndexVm>();

            foreach (var item in pc)
            {
                foreach (var item1 in c)
                {
                    if (item1.Id == item.CategoryId)
                    {
                        PostCategoryIndexVm vm = new PostCategoryIndexVm
                        {
                            PostName = p.Title,
                            CategoryName = item1.CategoryName
                        };
                        model.Add(vm);
                        break;
                    }
                }
                
            }


            return Json(new { result = true, model = model });

        }
    }
}
