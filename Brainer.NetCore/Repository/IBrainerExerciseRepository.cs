using Brainer.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Repository
{
    public interface IBrainerExerciseRepository
    {
        Task<IEnumerable<BrainerExercise>> GetBrainerExercises();
        Task<BrainerExercise> GetBrainerExercise(int id);
    }
}
