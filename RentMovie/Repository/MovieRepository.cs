using Dapper;
using Microsoft.Extensions.Configuration;
using RentMovie.Repository.Interface;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RentMovie.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IConfiguration _config;

        public MovieRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config["Movies:ConnectionString"]);
            }
        }

        public async Task DeleteList(string ids)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                string sQuery = "UPDATE Movie SET Active = 0 WHERE MovieId IN @ID";
                var idsInt = ids.Split(',').Select(int.Parse).ToList();
                var result = await conn.ExecuteAsync(sQuery , new { ID = idsInt });
            }
        }
    }
}
