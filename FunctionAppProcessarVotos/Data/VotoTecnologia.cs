using System;
using Dapper.Contrib.Extensions;

namespace FunctionAppProcessarVotos.Data
{
    [Table("dbo.VotoTecnologia")]
    public class VotoTecnologia
    {
        [Key]
        public int Id { get; set; }
        public string IdVoto { get; set; }
        public DateTime? Horario { get; set; }
        public string Tecnologia { get; set; }
        public string Consumer { get; set; }
    }
}