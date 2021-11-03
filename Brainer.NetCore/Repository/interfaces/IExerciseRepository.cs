using Brainer.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Repository
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetBrainerExercises();
        Task<Exercise> GetBrainerExercise(int id);
    }
}
