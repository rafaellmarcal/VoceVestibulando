using System.Threading.Tasks;

namespace VV.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
