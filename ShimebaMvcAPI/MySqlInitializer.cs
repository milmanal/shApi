//using ShimebaMvcAPI.Models;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;

//namespace ShimebaMvcAPI
//{
//    public class MySqlInitializer : IDatabaseInitializer<ShimebaMvcAPI.Models.Store.ApplicationDbContext>
//    {
//        public void InitializeDatabase(ShimebaMvcAPI.Models.Store.ApplicationDbContext context)
//        {
//            if (!context.Database.Exists())
//            {
//                // if database did not exist before - create it
//                context.Database.Create();
//            }
//            else
//            {
//                // query to check if MigrationHistory table is present in the database 
//                var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
//                string.Format(
//                  "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{0}' AND table_name = '__MigrationHistory'",
//                  "Database=as_0b2ec441dd9c91b;Data Source=eu-cdbr-azure-north-b.cloudapp.net;User Id=b86a5698b52863;Password=cf44ee8b"));

//                // if MigrationHistory table is not there (which is the case first time we run) - create it
//                if (migrationHistoryTableExists.FirstOrDefault() == 0)
//                {
//                    context.Database.Delete();
//                    context.Database.Create();
//                }
//            }
//        }
//    }
//}