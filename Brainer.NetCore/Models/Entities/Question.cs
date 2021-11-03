using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Models.Entities
{
    public class Question : Entity
    {
        public string Text { get; set; }
        public ICollection<ExerciseType> Categories { get; set; }

    }
}
