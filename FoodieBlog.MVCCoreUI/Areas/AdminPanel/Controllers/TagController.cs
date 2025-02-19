using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Areas.AdminPanel;
using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [AdminFilter]
    public class TagController : Controller
    {
        private readonly ITagBs _tagBs;

        public TagController(ITagBs tagBs)
        {
            _tagBs = tagBs;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            List<TagIndexVm> vm = new List<TagIndexVm>();
            List<Tag> Tags = await _tagBs.GetAll();

            foreach (var item in Tags)
            {
                TagIndexVm Tagvm = new TagIndexVm
                {
                    Id = item.Id,
                    TagName = item.TagName,
                    Active = item.Active
                };
                vm.Add(Tagvm);
            }

            return Json(new { data = vm });
        }

        [HttpPost]
        public IActionResult Add(IFormCollection data)
        {
            Tag Tag = new Tag();
            Tag.TagName = data["TagName"];
            Tag.Active = true;

            _tagBs.Insert(Tag);


            return Json(new { result = true, mesaj = "Tag is added successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Update(IFormCollection data)
        {
            int Id = Convert.ToInt32(data["Id"]);
            Tag Tag = await _tagBs.Get(x => x.Id == Id);

            // Convert active to bool
            if (!String.IsNullOrEmpty(data["Active"]))
            {
                var temp = data["Active"].ToString();
                bool dataActive = bool.Parse(temp);
                Tag.Active = dataActive;
            }
            // ---------------

            Tag.TagName = data["TagName"];


            await _tagBs.Update(Tag);

            List<Tag> Tags = await _tagBs.GetAll();

            TagIndexVm model = new TagIndexVm();

            model.TagName = Tag.TagName;
            model.Active = Tag.Active;

            return Json(new { result = true, message = "Tag is updated successfully", model = model });
        }

        public async Task<IActionResult> Delete(int id)
        {
            Tag k = await _tagBs.Get(x => x.Id == id);

            _tagBs.Delete(k);

            return Json(new { result = true, mesaj = "Tag is deleted successfully" });
        }

        public async Task<IActionResult> ActiveInactive(int id, bool active)
        {
            Tag k = await _tagBs.Get(x => x.Id == id);
            k.Active = active;
            await _tagBs.Update(k);

            //  Thread.Sleep(2000);
            return Json(new { result = true, mesaj = "Activity is updated successfully" });
        }

        public async Task<IActionResult> GetTag(int id)
        {

            Tag k = await _tagBs.Get(x => x.Id == id);


            return Json(new { result = true, model = k });

        }
    }
}
