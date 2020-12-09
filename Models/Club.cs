using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AfaApi.Models
{
    [DataContract]
    public class Club
    {
        [DataMember(Name = "id")]
        public int id { get; set; }

        [DataMember(Name = "nombre")]
        public string nombre { get; set; }

        [DataMember(Name = "ciudad")]
        public string ciudad { get; set; }
        [DataMember(Name = "provincia")]
        public string provincia { get; set; }
        [DataMember(Name = "fundacion")]
        public DateTime fundacion { get; set; }
    }
}
