using Brainer.NetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Repository
{
    public class BrainerExerciseRepository : IBrainerExerciseRepository
    {
        private readonly AppDBContext appDBContext;

        public BrainerExerciseRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public async Task<BrainerExercise> GetBrainerExercise(int id)
        {
            return await appDBContext.BrainerExercises.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<BrainerExercise>> GetBrainerExercises()
        {
            return await appDBContext.BrainerExercises.ToListAsync();
        }
    }
}
