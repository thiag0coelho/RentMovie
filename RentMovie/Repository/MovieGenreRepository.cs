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
    public class MovieGenreRepository : IMovieGenreRepository
    {
        private readonly IConfiguration _config;

        public MovieGenreRepository(IConfiguration config)
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
                string sQuery = "UPDATE MovieGenre SET Active = 0 WHERE MovieGenreId IN @ID";
                var idsInt = ids.Split(',').Select(int.Parse).ToList();
                var result = await conn.ExecuteAsync(sQuery , new { ID = idsInt });
            }
        }

        public async Task<MovieGenre> GetByID(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT MovieGenreId, Name, CreationDate, Active FROM MovieGenre WHERE MovieGenreId = @ID";

                conn.Open();
                var result = await conn.QueryAsync<MovieGenre>(sQuery, new { ID = id });

                return result.FirstOrDefault();
            }
        }
    }
}
