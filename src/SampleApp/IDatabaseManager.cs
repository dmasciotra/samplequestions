using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SampleApp
{
    public interface IDatabaseManager
    {
        Task<List<SomeEntity>> GetEntities();

        Task SaveEntities(List<SomeEntity> entities);
    }
}
