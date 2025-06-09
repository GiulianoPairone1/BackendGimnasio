using Application.Interfaces;
using Application.Models.Dtos;

using Domain.Interfaces;

namespace Application.Services
{
    public class ExerciseService: IExerciseService
    {
        private readonly IExerciseRepository _excerciseRepository;

        public ExerciseService (IExerciseRepository excerciseRepository)
        {
            _excerciseRepository = excerciseRepository;
        }

        public List<ExerciseDTO> GetAll()
        {
            return _excerciseRepository.GetAll()
                .Where(e => e.IsAvailable)
                .Select(exercise => new ExerciseDTO
                {
                    Name = exercise.Name,
                    Sets = exercise.Sets,
                    Reps = exercise.Reps,
                    RestTime = exercise.RestTime,
                    IsAvailable = exercise.IsAvailable
                })
                .ToList();
        }

        public List<ExerciseDTO> GetExercisesAvailable() {
            var exercises = _excerciseRepository.GetExercisesAvaiable()
                                  ?? throw new KeyNotFoundException("No se encontraron ejercicios disponibles");

            return exercises.Select(ExerciseDTO.FromExercise).ToList();
        }


        public ExerciseDTO CreateExercise(ExerciseDTO exerciseDTO) {

            if (string.IsNullOrWhiteSpace(exerciseDTO.Name))
                throw new ArgumentException("El nombre del ejercicio no puede estar vacío.");

            if (exerciseDTO.Sets <= 0)
                throw new ArgumentException("La cantidad de sets debe ser mayor a 0.");

            if (exerciseDTO.Reps <= 0)
                throw new ArgumentException("La cantidad de repeticiones debe ser mayor a 0.");

            if (exerciseDTO.RestTime <= 0)
                throw new ArgumentException("El tiempo de descanso debe ser mayor a 0.");

            var exercise = exerciseDTO.ToExercise();
            var created = _excerciseRepository.add(exercise);

            return ExerciseDTO.FromExercise(created);
        }

        public ExerciseDTO UpdateExercise(int id, ExerciseDTO updatedData) {

            if (string.IsNullOrWhiteSpace(updatedData.Name))
                throw new ArgumentException("El nombre del ejercicio no puede estar vacío.");

            if (updatedData.Sets <= 0)
                throw new ArgumentException("La cantidad de sets debe ser mayor a 0.");

            if (updatedData.Reps <= 0)
                throw new ArgumentException("La cantidad de repeticiones debe ser mayor a 0.");

            if (updatedData.RestTime <= 0)
                throw new ArgumentException("El tiempo de descanso debe ser mayor a 0.");

            var existingExercise = _excerciseRepository.GetById(id)
                                   ?? throw new KeyNotFoundException("No se encontró el ejercicio.");

            updatedData.UpdateExercise(existingExercise);
            _excerciseRepository.update(existingExercise);

            return ExerciseDTO.FromExercise(existingExercise);
        }
        public bool DeleteExercise(int id) {
            var existingExercise = _excerciseRepository.GetById(id)
                                      ?? throw new KeyNotFoundException("No se encontró el ejercicio.");


            if (!existingExercise.IsAvailable)
            {
                throw new InvalidOperationException("El ejercicio ya ha sido cancelado.");
            }


            existingExercise.IsAvailable = false;
            _excerciseRepository.update(existingExercise);

            return true;
        }
    }
}
