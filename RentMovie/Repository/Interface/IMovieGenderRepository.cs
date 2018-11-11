using RentMovie.Domain;
using System.Threading.Tasks;

namespace RentMovie.Repository.Interface
{
    public interface IMovieGenderRepository
    {
        Task DeleteList(string ids);
        Task<MovieGender> GetByID(int id);
    }
}
