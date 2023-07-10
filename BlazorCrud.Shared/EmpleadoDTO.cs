using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class EmpleadoDTO
    {
        [Required(ErrorMessage ="El Campo {0} es requerido")]
        public int IdEmpleado { get; set; }

        public string NombreCompleto { get; set; } = null!;
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1,int.MaxValue, ErrorMessage ="El campo {0} es requerido")]
        public int IdDepartamento { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int Sueldo { get; set; }

        public DateTime FechaContrato { get; set; }

        public DepartamentoDTO Departamento { get; set; }
    }
}
