using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Routine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<RoutineExercise> RoutineExercises { get; set; }

        public ICollection<GymSession> GymSessions { get; set; } // Ahora GymSessions en vez de GymSessionRoutine

        public bool IsAvailable { get; set; } = true;

        public Routine()
        {
            RoutineExercises = new HashSet<RoutineExercise>();
            GymSessions = new HashSet<GymSession>();
        }
    }
}

