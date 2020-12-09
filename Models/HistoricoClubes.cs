using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AfaApi.Models
{
    [DataContract]
    public class HistoricoClubes
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
        [DataMember(Name = "clubId")]
        public int clubId { get; set; }
        [DataMember(Name = "jugadorId")]
        public int jugadorId { get; set; }
        [DataMember(Name = "fechaIngreso")]
        public DateTime  fechaIngreso { get; set; }
        [DataMember(Name = "fechaEgreso")]
        public DateTime fechaEgreso { get; set; }
        [DataMember(Name = "posicion")]
        public int posicion { get; set; }

    }
}
