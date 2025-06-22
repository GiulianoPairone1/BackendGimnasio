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

        public TrainerDTO UpdateProfile(int trainerId, TrainerDTO trainerDto)
        {

            var trainer = _trainerRepository.GetById(trainerId)
                         ?? throw new KeyNotFoundException("Cliente no encontrado.");


            if (!trainer.IsAvailable)
                throw new InvalidOperationException("El cliente ha sido dado de baja y no puede ser actualizado.");
            if (string.IsNullOrWhiteSpace(trainerDto.Email))
                throw new InvalidOperationException("El correo ingresado ya está en uso.");
                if (string.IsNullOrWhiteSpace(trainerDto.Surname))
                    throw new InvalidOperationException("El apellido no puede estar vacío.");
            if (!System.Text.RegularExpressions.Regex.IsMatch(trainerDto.Phone, @"^\+?\d{7,15}$"))
                throw new InvalidOperationException("El número de teléfono ingresado no es válido.");
     

            trainerDto.UpdateTrainer(trainer);
            _trainerRepository.update(trainer);
            return TrainerDTO.FromTrainer(trainer);
        }

        public TrainerDTO Create(TrainerDTO trainerDTO)
        {
            var trainer = trainerDTO.ToTrainer();
            var addTrainer = _trainerRepository.add(trainer);
            return TrainerDTO.FromTrainer(addTrainer);
        }
    }
}
