using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICleanupRepository
    {
        Task CleanUp(CleanUp cleanup);
    }
}
