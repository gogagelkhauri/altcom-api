using Core.DTO;
using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class GameSessionRepository : IGameSessionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GameSessionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetGameSessionPoints(int gameSessionId)
        {
            var questionsCount = _dbContext.Questions.Include(x => x.Image).Where(x => x.GameSessionId == gameSessionId && x.Image.NationId == x.AnsweredNationId).Count();

            return questionsCount;
        }

        public async Task<List<Image>> GetRandomImages()
        {
            return await _dbContext.Images.OrderBy(o => Guid.NewGuid()).Take(10).ToListAsync();
        }

        public async Task<GameSession> StartSession()
        {
            var session = new GameSession()
            {
                StartDate = DateTime.Now,
            };
            await _dbContext.GameSessions.AddAsync(session);
            await _dbContext.SaveChangesAsync();

            return session;
        }

        public async Task<bool> SubmitAnswer(SubmitAnswerRequestDTO dto)
        {
            try
            {
                var question = new Question()
                {
                    ImageId = dto.ImageId,
                    GameSessionId = dto.GameSessionId,
                    AnsweredNationId = dto.AnsweredNationId
                };

                await _dbContext.Questions.AddAsync(question);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
