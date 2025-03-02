using Newtonsoft.Json;

namespace FoodieBlog.MVCCoreUI.Extensions
{
    public static class SessionExtension 
    {
        public static void SetObject(this ISession session, string key, object value)
        {

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            string json = JsonConvert.SerializeObject(value, settings);

            if (!string.IsNullOrEmpty(json))
            {
                session.SetString(key, json);

            }
        }
        public static T? GetObject<T>(this ISession session, string key)
        {
            string json = session.GetString(key);
            if (!string.IsNullOrEmpty(json))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            else
            {
                return default;
            }
        }
    }
}
