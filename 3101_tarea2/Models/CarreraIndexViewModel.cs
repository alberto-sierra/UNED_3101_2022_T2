using System;
using System.ComponentModel.DataAnnotations;

namespace Velocidad.Models
{
	public class CarreraIndexViewModel
	{
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }
        [Display(Name = "Cantidad de Corredores")]
        public int TotalCorredores { get; set; }
    }
}

