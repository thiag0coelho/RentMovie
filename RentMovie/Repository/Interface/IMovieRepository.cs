using System.Threading.Tasks;

namespace RentMovie.Repository.Interface
{
    public interface IMovieRepository
    {
        Task DeleteList(string ids);
    }
}
