using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public int GameSessionId { get; set; }
        public GameSession GameSession { get; set; }
        public int AnsweredNationId { get; set; }
    }
}
