using Application.Models.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoutineService
    {
        List<RoutineDTO> GetAll();
        ICollection<RoutineDTO> GetAllRoutinesAvailable();
        //ICollection<RoutineDTO> GetMyRoutinesAvailable(int id);
        RoutineDTO CreateRoutine(RoutineDTO newRoutineDto);
        RoutineDTO UpdateRoutine(int id, RoutineDTO updatedData);
        bool DeleteRoutine(int? routineId);

        List<RoutineWithExercisesDTO> GetRoutinesByTrainerId(int trainerId);

    }
}
