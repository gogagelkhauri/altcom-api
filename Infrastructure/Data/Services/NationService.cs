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
    public class NationService : INationService
    {
        private readonly INationRepostory _nationRepository;
        public NationService(INationRepostory nationRepository)
        {
            _nationRepository = nationRepository;
        }

        public async Task<List<Nation>> GetNations()
        {
            return await _nationRepository.GetNations();
        }
    }
}
