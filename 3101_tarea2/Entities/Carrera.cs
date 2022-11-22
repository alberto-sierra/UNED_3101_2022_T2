using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Velocidad.Entities;

[Table("carrera")]
public partial class Carrera
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Descripcion { get; set; }

    [InverseProperty("IdCarreraNavigation")]
    public virtual ICollection<Corredor> Corredors { get; } = new List<Corredor>();
}
