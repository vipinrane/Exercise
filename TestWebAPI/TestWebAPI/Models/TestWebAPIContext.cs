using System.Data.Entity;
using TestWebAPI.Controllers;

namespace TestWebAPI.Models
{
    public class TestWebAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<TestWebAPI.Models.TestWebAPIContext>());

        public TestWebAPIContext() : base("name=TestWebAPIContext")
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
