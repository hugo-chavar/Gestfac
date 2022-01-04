using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.Services.Creators
{
    public interface ICreator<T>
    {
        Task Create(T t);
    }
}
