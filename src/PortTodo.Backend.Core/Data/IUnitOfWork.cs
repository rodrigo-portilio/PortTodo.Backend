using System.Threading.Tasks;

namespace PortTodo.Backend.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}