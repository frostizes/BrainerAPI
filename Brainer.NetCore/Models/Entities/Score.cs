using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Models.Entities
{
    public class Score : Entity
    {
        public ApplicationUser User { get; set; }
        public Exercise Exercise { get; set; }
        public int ScoreValue { get; set; }
    }
}
