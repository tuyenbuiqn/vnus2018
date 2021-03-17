using System.Data.SqlClient;

namespace VietUSA.Repository.Common.Models
{
    public class ConnectionInfo
    {
        public SqlConnection SqlConnection { get; set; }
        public string SchemaName { get; set; } = "dbo";
    }
}
