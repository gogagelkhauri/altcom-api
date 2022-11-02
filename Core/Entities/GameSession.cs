using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GameSession
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CorrectAnswers { get; set; }
        public int? WrongAnswers { get; set; }
        public int? QuestionsCount { get; set; }
    }
}
