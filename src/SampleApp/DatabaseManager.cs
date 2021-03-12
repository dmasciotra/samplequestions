using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SampleApp
{
    /// <summary>
    /// This implementation doesnt actually read from a database.
    /// It simply exists to demonstrate the bad pratice of using heavy concrete implementations in the IDoEverything class.
    /// </summary>
    public class DatabaseManager : IDatabaseManager
    {
        public Task<List<SomeEntity>> GetEntities()
        {
            var result = new List<SomeEntity>();
            return Task.FromResult(result);
        }

        public Task SaveEntities(List<SomeEntity> entities)
        {
            return Task.CompletedTask;
        }
    }
}