using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestfac.Services.Updaters
{
    public interface IUpdater<T>
    {
        Task Update(IEnumerable<T> t);
    }
}
