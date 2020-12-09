using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AfaApi.Models
{
    [DataContract]
    public class HistoricoGoles
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
        [DataMember(Name = "jugadorId")]
        public int jugadorId { get; set; }
        [DataMember(Name = "cantGoles")]
        public int cantGoles { get; set; }

    }
}
