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
                .Select(exercise => new ExerciseDTO
                {
                    Name = exercise.Name,
                    Sets = exercise.Sets,
                    Reps = exercise.Reps,
                    RestTime = exercise.RestTime,
                })
                .ToList();
        }

        public ExerciseDTO Create(ExerciseDTO exerciseDTO)
        {
            var exrcise = exerciseDTO.ToExercise();
            var addExrcise = _excerciseRepository.add(exrcise);
            return ExerciseDTO.FromExercise(addExrcise);
        }
    }
}
