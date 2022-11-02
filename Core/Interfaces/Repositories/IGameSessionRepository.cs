using Core.DTO;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IGameSessionRepository
    {
        Task<GameSession> StartSession();

        Task<List<Image>> GetRandomImages();

        Task<bool> SubmitAnswer(SubmitAnswerRequestDTO dto);

        int GetGameSessionPoints(int gameSessionId);
    }
}
