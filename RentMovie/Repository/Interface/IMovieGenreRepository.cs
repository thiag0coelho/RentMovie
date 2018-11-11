using RentMovie.Domain;
using System.Threading.Tasks;

namespace RentMovie.Repository.Interface
{
    public interface IMovieGenreRepository
    {
        Task DeleteList(string ids);
        Task<MovieGenre> GetByID(int id);
    }
}
