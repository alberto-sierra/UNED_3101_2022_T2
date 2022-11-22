using System;
using Microsoft.AspNetCore.Mvc;

namespace Velocidad.Models
{
	public class CarreraViewModel
	{
        [BindProperty]
        public CarreraModel Carrera { get; set; }
        public CorredorModel Corredor { get; }
    }
}