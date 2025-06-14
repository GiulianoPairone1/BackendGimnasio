﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GymSessionRepository : EfRepository<GymSession>, IGymSessionRepository
    {
        public GymSessionRepository(ApplicationDbContext context) : base(context) { }

        public List<GymSession> GetAll()
        {
            return _applicationDbContext.GymSessions
                .Include(g => g.Trainer)
                .Include(g => g.Routine)
                .Include(g => g.ClientGymSessions)
                .Include((x) => x.Routine)
                .ToList();
        }

        public List<GymSession> GetGymSessionAvaiable()
        {
            return _applicationDbContext.GymSessions
                .Include(g => g.Trainer)
                .Include(g => g.Routine)
                .Include(g => g.ClientGymSessions)
                .Include((x) => x.Routine)
                .Where(g => !g.IsCancelled && g.SessionDate > DateTime.Now)
                .ToList();
        }

        public List<GymSession> GetMyGymSessions(int trainerId)
        {
            return _applicationDbContext.GymSessions
                .Include(g => g.Trainer)
                .Include(g => g.Routine)
                .Include(g => g.ClientGymSessions)
                .ThenInclude(cgs => cgs.Client)
                .Include((x) => x.Routine)
                .Where(g => g.TrainerId == trainerId && !g.IsCancelled && g.SessionDate > DateTime.Now)
                .ToList();
        }

        public GymSession? GetGymSessionWithClients(int sessionId)
        {
            return _applicationDbContext.GymSessions
                .Include(gs => gs.ClientGymSessions)
                .ThenInclude(cgs => cgs.Client)
                .Include((x) => x.Routine)
                .Where(g => !g.IsCancelled && g.SessionDate > DateTime.Now)
                .FirstOrDefault(gs => gs.Id == sessionId);
        }


        public async Task<IEnumerable<GymSession>> GetSessionsByDateAsync(DateTime date)
        {
            return await _applicationDbContext.GymSessions
                .Include(g => g.Trainer)
                .Include(g => g.Routine)
                .Include(g => g.ClientGymSessions)
                .ThenInclude(cgs => cgs.Client)
                .Where(g =>
                g.SessionDate >= date.Date &&
                g.SessionDate < date.Date.AddDays(1) &&
                (!g.IsCancelled && g.SessionDate > DateTime.Now))
                .ToListAsync();
        }

    }
}