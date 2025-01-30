using FoodieBlog.Model.Entity;

namespace FoodieBlog.MVCCoreUI.Filters
{
    public interface ISessionManager
    {
        public User ActiveUser { get; set; }
        public User ActiveAdmin { get; set; }
        public string Captcha { get; set; }
        public bool IsAllowed(int MenuId, int UserId);
    }
}
