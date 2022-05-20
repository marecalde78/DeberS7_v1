using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DeberS7_v1.Models
{
    public class Alumno
    {
        [PrimaryKey,AutoIncrement]
        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
    }
}
