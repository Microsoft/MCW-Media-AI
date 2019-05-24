using System.Configuration;

namespace ContosoLearning.Data
{
    public static class CosmosDbAuthInfoFactory
    {
        public static CosmosDbAuthInfo Create()
        {
            var info = new CosmosDbAuthInfo();

            info.Endpoint = ConfigurationManager.AppSettings["CosmosDB_Endpoint"];
            info.AuthKey = ConfigurationManager.AppSettings["CosmosDB_AuthKey"];
            info.Database = ConfigurationManager.AppSettings["CosmosDB_Database"];
            info.Collection = ConfigurationManager.AppSettings["CosmosDB_Collection"];

            return info;
        }
    }
}
