using AspNetCore.Identity.Mongo.Model;

namespace WordApp.Data
{
    public class ApplicationUser : MongoUser
    {
        public string FullName { get; set; }
    }



    public class ApplicationRole : MongoRole
    {
        public List<string> Permissions { get; set; }
    }
}
