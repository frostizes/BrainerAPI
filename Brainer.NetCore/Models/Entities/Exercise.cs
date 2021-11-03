using Brainer.NetCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Models
{
    public class Exercise : Entity
    {
        public string Name { get; set; }
        public string MyProperty { get; set; }
        public int BestScore { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public int NumberOfQuestions { get; set; }
        public int Timer { get; set; }
        public ICollection<Question> Questions { get; set; }



    }
}
