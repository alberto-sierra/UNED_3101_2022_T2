using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Velocidad.Entities;

[Table("corredor")]
public partial class Corredor
{
    [Key]
    public int Id { get; set; }

    public int IdCarrera { get; set; }

    public TimeSpan? Tiempo { get; set; }

    [ForeignKey("IdCarrera")]
    [InverseProperty("Corredors")]
    public virtual Carrera IdCarreraNavigation { get; set; } = null!;
}
