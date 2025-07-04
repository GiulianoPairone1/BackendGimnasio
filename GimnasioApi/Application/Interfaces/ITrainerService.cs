﻿using Application.Models.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITrainerService
    {
        List<TrainerDTO> GetAll();
        TrainerDTO Create(TrainerDTO trainerDTO);

        TrainerDTO UpdateProfile(int trainerId, TrainerDTO trainerDto);
    }
}
