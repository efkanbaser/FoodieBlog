using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Front;
using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodieBlog.MVCCoreUI.Controllers
{
    //[UserFilter]
    public class CreatePostController : Controller
    {
        private readonly ICategoryBs _categoryBs;
        private readonly ITagBs _tagBs;

        public CreatePostController(ICategoryBs categoryBs, ITagBs tagBs)
        {
            _categoryBs = categoryBs;
            _tagBs = tagBs;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _categoryBs.GetAll();
            List<Tag> tags = await _tagBs.GetAll();

            AddPostVm model = new AddPostVm();

            model.Categories = categories.Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.Id.ToString() }).ToList();
            model.Categories.Insert(0, new SelectListItem() { Text = "Please select a category", Value = "0", Selected = true });

            model.Tags = tags.Select(x => new SelectListItem() { Text = x.TagName, Value = x.Id.ToString() }).ToList();
            model.Tags.Insert(0, new SelectListItem() { Text = "Please select a tag", Value = "0", Selected = true });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostVm vm)
        {
            // Mapledikten sonra nextpostid ve prevpostid vermeyi unutma, userid de önemli sessiondan çek
            int a = 5;

            return View();
        }
    }
}
