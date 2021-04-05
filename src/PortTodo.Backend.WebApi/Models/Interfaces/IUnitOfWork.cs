using System.Threading.Tasks;

namespace PortTodo.Backend.WebApi.Models.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}