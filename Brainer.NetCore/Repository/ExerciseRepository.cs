using Brainer.NetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Repository
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly AppDBContext appDBContext;

        public ExerciseRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public async Task<Exercise> GetBrainerExercise(int id)
        {
            return await appDBContext.Exercises.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Exercise>> GetBrainerExercises()
        {
            return await appDBContext.Exercises.ToListAsync();
        }
    }
}
