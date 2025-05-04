using Application.Interfaces;
using Application.Models.Dtos;

using Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;

        public TrainerService(ITrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }


        public List<TrainerDTO> GetAll()
        {
            return _trainerRepository.GetAll()
                .Select(trainer => new TrainerDTO
                {
                    Name = trainer.Name,
                    Surname = trainer.Surname,
                    Email = trainer.Email,
                })
                .ToList();
        }

        public TrainerDTO Create(TrainerDTO trainerDTO)
        {
            var trainer = trainerDTO.ToTrainer();
            var addTrainer = _trainerRepository.add(trainer);
            return TrainerDTO.FromTrainer(addTrainer);
        }
    }
}
