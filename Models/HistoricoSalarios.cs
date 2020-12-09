using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AfaApi.Models
{
    [DataContract]
    public class HistoricoSalarios
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
        [DataMember(Name = "clubId")]
        public int clubId { get; set; }
        [DataMember(Name = "jugadorId")]
        public int jugadorId { get; set; }

        [DataMember(Name = "año")]
        public int año { get; set; }

        [DataMember(Name = "salarioPrm")]
        public double salarioPrm { get; set; }
    }
}
