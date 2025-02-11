using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Areas.AdminPanel;
using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{

    [Area("AdminPanel")]
    [AdminFilter]
    public class CategoryController : Controller
    {
        private readonly ICategoryBs _categoryBs;

        public CategoryController(ICategoryBs categoryBs)
        {
            _categoryBs = categoryBs;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            List<CategoryIndexVm> vm = new List<CategoryIndexVm>();
            List<Category> Categorys = await _categoryBs.GetAll();

            foreach (var item in Categorys)
            {
                CategoryIndexVm Categoryvm = new CategoryIndexVm
                {
                    Id = item.Id,
                    CategoryName = item.CategoryName,
                    Active = item.Active
                };
                vm.Add(Categoryvm);
            }

            return Json(new { data = vm });
        }

        [HttpPost]
        public async Task<IActionResult> Add(IFormCollection data)
        {
            Category Category = new Category();
            Category.CategoryName = data["CategoryName"];
            Category.Active = true;

            await _categoryBs.Insert(Category);


            return Json(new { result = true, mesaj = "Category is added successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Update(IFormCollection data)
        {
            int Id = Convert.ToInt32(data["Id"]);
            Category Category = await _categoryBs.Get(x => x.Id == Id);

            // Convert active to bool
            if (!String.IsNullOrEmpty(data["Active"]))
            {
                var temp = data["Active"].ToString();
                bool dataActive = bool.Parse(temp);
                Category.Active = dataActive;
            }
            // ---------------

            Category.CategoryName = data["CategoryName"];


            await _categoryBs.Update(Category);

            List<Category> Categorys = await _categoryBs.GetAll();

            CategoryIndexVm model = new CategoryIndexVm();

            model.CategoryName = Category.CategoryName;
            model.Active = Category.Active;

            return Json(new { result = true, message = "Category is updated successfully", model = model });
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category k = await _categoryBs.Get(x => x.Id == id);

            _categoryBs.Delete(k);

            return Json(new { result = true, mesaj = "Category is deleted successfully" });
        }

        public async Task<IActionResult> ActiveInactive(int id, bool active)
        {
            Category k = await _categoryBs.Get(x => x.Id == id);
            k.Active = active;
            await _categoryBs.Update(k);

            //  Thread.Sleep(2000);
            return Json(new { result = true, mesaj = "Activity is updated successfully" });
        }

        public async Task<IActionResult> GetCategory(int id)
        {

            Category k = await _categoryBs.Get(x => x.Id == id);


            return Json(new { result = true, model = k });

        }
    }
}


