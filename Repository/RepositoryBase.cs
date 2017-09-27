using LiteDB;
using System.Configuration;
using System.IO;

namespace MDW.Repository
{
    public class RepositoryBase
    {
        private string Filename { get { return Path.Combine(ConfigurationManager.AppSettings["mdw.db.path"], @"mdw.db"); } }

        protected LiteDatabase Database { get; set; }

        public RepositoryBase()
        {
            Database = new LiteDatabase(Filename);
        }
    }
}