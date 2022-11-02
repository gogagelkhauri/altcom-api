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
    public class NationRepository : INationRepostory
    {
        private readonly ApplicationDbContext _dbContext;

        public NationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Nation>> GetNations()
        {
            return await _dbContext.Nations.ToListAsync();
        }
    }
}
