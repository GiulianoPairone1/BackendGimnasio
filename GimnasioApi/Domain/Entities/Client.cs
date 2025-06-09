using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : User
    {
        public Client()
        {
            UserType = UserType.Client;
            ClientGymSessions= new List<ClientGymSession>();
        }

        public double Weight { get; set; }
        public double Height { get; set; }

        [InverseProperty(nameof(ClientGymSession.Client))] //yuda a EF Core a mapear la relación correctamente si hay más relaciones similares.
        public ICollection<ClientGymSession> ClientGymSessions { get; set; }
    }
}
