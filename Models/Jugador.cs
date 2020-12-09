using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AfaApi.Models
{
    [DataContract]
    public class Jugador
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
        [DataMember(Name = "clubId")]
        public int clubId { get; set; }
        [DataMember(Name = "nombre")]
        public string nombre { get; set; }

        [DataMember(Name = "numeroInscripcion")]
        public int numeroInscripcion { get; set; }

        [DataMember(Name = "posicion")]
        public int posicion { get; set; }
    }
}
