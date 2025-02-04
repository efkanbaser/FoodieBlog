using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Areas.AdminPanel;
using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [AdminFilter]
    public class CommentController : Controller
    {
        private readonly ICommentBs _commentBs;

        public CommentController(ICommentBs commentBs)
        {
            _commentBs = commentBs;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            string[] include = new string[2];
            include = ["Post", "User"];

            List<CommentIndexVm> vm = new List<CommentIndexVm>();
            List<Comment> Comments = await _commentBs.GetAll(includelist:include);

            foreach (var item in Comments)
            {
                CommentIndexVm Commentvm = new CommentIndexVm
                {
                    Id = item.Id,                  
                    Active = item.Active,
                    Contents = item.Contents,
                    PostTitle = item.Post.Title,
                    UserName = item.User.UserName,
                    PublicationDate = item.PublicationDate,
                    ModifiedDate = item.ModifiedDate
                };
                vm.Add(Commentvm);
            }

            return Json(new { data = vm });
        }

        public async Task<IActionResult> Delete(int id)
        {
            Comment k = await _commentBs.Get(x => x.Id == id);

            _commentBs.Delete(k);

            return Json(new { result = true, mesaj = "Comment is deleted successfully" });
        }

        public async Task<IActionResult> ActiveInactive(int id, bool active)
        {
            Comment k = await _commentBs.Get(x => x.Id == id);
            k.Active = active;
            await _commentBs.Update(k);

            //  Thread.Sleep(2000);
            return Json(new { result = true, mesaj = "Activity is updated successfully" });
        }

        public async Task<IActionResult> GetComment(int id)
        {

            Comment k = await _commentBs.Get(x => x.Id == id);


            return Json(new { result = true, model = k });

        }
    }
}
