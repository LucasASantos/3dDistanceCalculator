using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FARO.Manager3d.Application.ViewModels
{
    public class ActualPointViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The X axis is Required")]
        [DisplayName("X axis")]
        public double X { get; set; }
        
        [Required(ErrorMessage = "The Y axis is Required")]
        [DisplayName("Y axis")]
        public double Y { get; set; }
        
        [Required(ErrorMessage = "The Z axis is Required")]
        [DisplayName("Z axis")]
        public double Z { get; set; }
        public NominalPointViewModel NominalPoint { get; set; }
        public double Distance { get; set; }
        
    }
}