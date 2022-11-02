using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class SubmitAnswerRequestDTO
    {
        public int GameSessionId { get; set; }
        public int ImageId { get; set; }
        public int AnsweredNationId { get; set; }
    }
}
