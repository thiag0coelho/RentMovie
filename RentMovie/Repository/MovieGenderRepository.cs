using Dapper;
using Microsoft.Extensions.Configuration;
using RentMovie.Domain;
using RentMovie.Repository.Interface;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RentMovie.Repository
{
    public class MovieGenderRepository : IMovieGenderRepository
    {
        private readonly IConfiguration _config;

        public MovieGenderRepository(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task DeleteList(string ids)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                string sQuery = "UPDATE MovieGender SET Active = 0 WHERE MovieGenderId IN @ID";
                var idsInt = ids.Split(',').Select(int.Parse).ToList();
                var result = await conn.ExecuteAsync(sQuery , new { ID = idsInt });
            }
        }

        public async Task<MovieGender> GetByID(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT MovieGenderId, Name, CreationDate, Active FROM MovieGender WHERE MovieGenderId = @ID";

                conn.Open();
                var result = await conn.QueryAsync<MovieGender>(sQuery, new { ID = id });

                return result.FirstOrDefault();
            }
        }
    }
}
