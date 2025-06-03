using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

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

        // [ForeignKey("Trainer")]
        // public int TrainerId { get; set; }

        // [JsonIgnore]
        // public Trainer Trainer { get; set; }

        [JsonIgnore]
        public ICollection<GymSession> GymSessions { get; set; }

        public bool IsAvailable { get; set; } = true;

        public Routine()
        {
            RoutineExercises = new HashSet<RoutineExercise>();
            GymSessions = new HashSet<GymSession>();
        }
    }
}

