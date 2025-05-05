using Application.Interfaces;
using Application.Models.Dtos;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Services
{
    public class RoutineExerciseService: IRoutineExerciseService
    {

        private readonly IRoutineExerciseRepository _routineExerciseRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IRoutineRepository _routineRepository;

        public RoutineExerciseService(IRoutineExerciseRepository routineExerciseRepository, IExerciseRepository exerciseRepository, IRoutineRepository routineRepository)
        {
            _routineExerciseRepository = routineExerciseRepository;
            _exerciseRepository = exerciseRepository;
            _routineRepository = routineRepository;
        }

        public List<RoutineExercise> GetAll()
        {
            return _routineExerciseRepository.GetAll();
        }
        public void Add(RoutineExerciseDTO dto)
        {
            var routine = _routineRepository.GetById(dto.RoutineId);
            var exercise = _exerciseRepository.GetById(dto.ExerciseId);

            if (routine == null || !routine.IsAvailable)
            {
                throw new Exception("La rutina no está disponible.");
            }

            if (exercise == null || !exercise.IsAvailable)
            {
                throw new Exception("El ejercicio no está disponible.");
            }
            // Validar si ya existe la relación
            var existingRelation = _routineExerciseRepository.GetAll()
                                              .FirstOrDefault(re => re.RoutineId == dto.RoutineId && re.ExerciseId == dto.ExerciseId);

            if (existingRelation != null)
            {
                throw new InvalidOperationException("Este ejercicio ya está asignado a la rutina.");
            }

            var relation = new RoutineExercise
            {
                RoutineId = dto.RoutineId,
                ExerciseId = dto.ExerciseId
            };

            _routineExerciseRepository.add(relation);
        }
        public void Update(int id, RoutineExerciseDTO dto)
        {
            // Verificar si la rutina y el ejercicio están disponibles
            var routine = _routineRepository.GetById(dto.RoutineId);
            var exercise = _exerciseRepository.GetById(dto.ExerciseId);

            if (routine == null || !routine.IsAvailable)
            {
                throw new Exception("La rutina no está disponible.");
            }

            if (exercise == null || !exercise.IsAvailable)
            {
                throw new Exception("El ejercicio no está disponible.");
            }

            // Verificar que no se repita el ejercicio en la misma rutina
            var existingRelation = _routineExerciseRepository
                .GetAll()
                .FirstOrDefault(re => re.RoutineId == dto.RoutineId && re.ExerciseId == dto.ExerciseId && re.Id != id);

            if (existingRelation != null)
            {
                throw new Exception("Este ejercicio ya está agregado a la rutina.");
            }

            // Buscar la relación que se va a actualizar
            var routineExercise = _routineExerciseRepository.GetById(id);
            if (routineExercise == null)
            {
                throw new Exception("La relación especificada no existe.");
            }

            // Actualizar la relación
            routineExercise.RoutineId = dto.RoutineId;
            routineExercise.ExerciseId = dto.ExerciseId;

            _routineExerciseRepository.update(routineExercise);
        }

    }
}
