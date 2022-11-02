using Core.DTO;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{
    public class GameSessionService : IGameSessionService
    {
        private readonly IGameSessionRepository _gameSessionRepository;

        public GameSessionService(IGameSessionRepository gameSessionRepository)
        {
            _gameSessionRepository = gameSessionRepository;
        }

        public int GetGameSessionPoints(int gameSessionId) => _gameSessionRepository.GetGameSessionPoints(gameSessionId);
        public async Task<List<Image>> GetRandomImages() => 
            await _gameSessionRepository.GetRandomImages();

        public async Task<GameSession> StartSession() =>
            await _gameSessionRepository.StartSession();

        public async Task<bool> SubmitAnswer(SubmitAnswerRequestDTO dto) => 
            await _gameSessionRepository.SubmitAnswer(dto);
    }
}
