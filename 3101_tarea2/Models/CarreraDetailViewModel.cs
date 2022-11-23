using System;
using System.ComponentModel.DataAnnotations;

namespace Velocidad.Models
{
	public class CarreraDetailViewModel
	{
        public int Id { get; set; }
        public List<CorredorDetalle> CorredorDetalle { get; set; }

        [Display(Name = "Mayor velocidad")]
        [DisplayFormat(DataFormatString = "{0:F} m/s")]
        public decimal MayorVelocidad { get; set; }

        [Display(Name = "Menor velocidad")]
        [DisplayFormat(DataFormatString = "{0:F} m/s")]
        public decimal MenorVelocidad { get; set; }

        [Display(Name = "Velocidad promedio")]
        [DisplayFormat(DataFormatString = "{0:F} m/s")]
        public decimal PromedioVelocidad { get; set; }
    }

    public class CorredorDetalle
    {
        public int Id { get; set; }

        [Display(Name = "Tiempo (minutos, segundos)")]
        [DisplayFormat(DataFormatString = "{0:mm':'ss}")]
        public TimeSpan Tiempo { get; set; }

        [Display(Name = "Velocidad (m/s)")]
        [DisplayFormat(DataFormatString = "{0:F}")]
        public decimal Velocidad { get; set; }
    }
}

