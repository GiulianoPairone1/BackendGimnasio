using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Exercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Sets { get; set; }
        [Required]
        public int Reps { get; set; }
        [Required]
        public int RestTime { get; set; }

        [ForeignKey("Routine")]
        public int RoutineId { get; set; }
        public Routine Routine { get; set; }
        public bool IsAvailable { get; set; } = true;

    }
}
