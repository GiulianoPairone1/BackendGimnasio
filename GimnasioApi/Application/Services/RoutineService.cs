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
    public class RoutineService: IRoutineService
    {
        private readonly IRoutineRepository _routineRepository;

        public RoutineService (IRoutineRepository routineRepository)
        {
            _routineRepository = routineRepository;
        }

        public List<RoutineDTO> GetAll()
        {
            return _routineRepository.GetAll()
                .Select(rutine => new RoutineDTO
                {
                    Name= rutine.Name,
                })
                .ToList();
        }

        public RoutineDTO Create(RoutineDTO routineDTO)
        {
            var routine = routineDTO.ToRoutine();
            var addsroutine = _routineRepository.add(routine);
            return RoutineDTO.FromRoutine(addsroutine);
        }
    }
}
