using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Velocidad.Models
{
    public class CarreraViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        public string? DescripcionCarrera { get; set; }
        public List<CorredorModel> Corredor { get; set; }
    }

	public class CorredorModel
	{
        public int Id { get; set; }

        [Range(minimum: 0, maximum: 59)]
        [Display(Name = "Minutos")]
        public int? Minutos { get; set; }

        [Range(minimum: 0, maximum: 59)]
        [Display(Name = "Segundos")]
        public int? Segundos { get; set; }
    }
}

