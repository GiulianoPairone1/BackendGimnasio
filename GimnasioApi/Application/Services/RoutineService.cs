﻿using Application.Interfaces;
using Application.Models.Dtos;
using Domain.Entities;
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

        public ICollection<RoutineDTO> GetAllRoutinesAvailable()
        {
            var routines = _routineRepository.GetRoutineAvaiable()
                              ?? throw new KeyNotFoundException("No se encontraron rutinas disponibles");

            return routines.Select(RoutineDTO.FromRoutine).ToList();
        }


        //public ICollection<RoutineDTO> GetMyRoutinesAvailable(int id) {
        //    var routines = _routineRepository.GetMyRoutines(id)
        //                   ?? throw new KeyNotFoundException("No se encontraron rutinas de este entrenador");
        //
        //    return routines.Select(RoutineDTO.FromRoutine).ToList();
        //}

        public RoutineDTO CreateRoutine(RoutineDTO newRoutineDto) {

            if (string.IsNullOrWhiteSpace(newRoutineDto.Name))
                throw new ArgumentException("El nombre de la rutina no puede estar vacío.");

            var routine = newRoutineDto.ToRoutine();
            var created = _routineRepository.add(routine);

            return RoutineDTO.FromRoutine(created);
        }


        public RoutineWithExercisesDTO RangeCreateRoutine(RoutineWithExercisesDTO newRoutineDto)
        {

            if (string.IsNullOrWhiteSpace(newRoutineDto.Name))
                throw new ArgumentException("El nombre de la rutina no puede estar vacío.");

            var routine = new Routine
            {
                Name = newRoutineDto.Name,

            };

            var exercises = newRoutineDto.Exercises.Select(x => new Exercise
            {
                Name = x.Name,
                Sets = x.Sets,
                Reps = x.Reps,
                RestTime = x.RestTime,
            }).ToList();

            var routineExercise = exercises.Select(x => new RoutineExercise
            {
                Routine = routine,
                RoutineId = routine.Id,
                Exercise = x,
                ExerciseId = x.Id
            }).ToList();

            var created = _routineRepository.addRange(routine, exercises, routineExercise);

            return RoutineWithExercisesDTO.FromRoutine(created);
        }



        public RoutineDTO UpdateRoutine(int id, RoutineDTO updatedData) {

            if (string.IsNullOrWhiteSpace(updatedData.Name))
                throw new ArgumentException("El nombre de la rutina no puede estar vacío.");

            var existingRoutine = _routineRepository.GetById(id)
                                  ?? throw new KeyNotFoundException("No se encontró la rutina");

            updatedData.UpdateRoutine(existingRoutine);
            _routineRepository.update(existingRoutine);

            return RoutineDTO.FromRoutine(existingRoutine);
        }
        public bool DeleteRoutine(int? routineId) {
            var existingRoutine = _routineRepository.GetById(routineId)
                                   ?? throw new KeyNotFoundException("No se encontró la rutina");


            if (!existingRoutine.IsAvailable)
            {
                // Ya estaba eliminada, no hacer nada
                return true;
            }


            existingRoutine.IsAvailable = false;
            _routineRepository.update(existingRoutine);

            return true;
        }

        public List<RoutineWithExercisesDTO> GetRoutinesByTrainerId(int trainerId)
        {
            var routines = _routineRepository.GetRoutinesByTrainer(trainerId);
            return routines.Select(r => RoutineWithExercisesDTO.FromRoutine(r)).ToList();
        }


    }
}
