using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FARO.Manager3d.Application.ViewModels
{
    public class NominalPointViewModel
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

        
        public double XAvg { get; set; }
        public double YAvg { get; set; }
        public double ZAvg { get; set; }


        public void AddAvg(double xAvg, double yAvg, double zAvg)
        {
            this.XAvg =xAvg;
            this.YAvg =yAvg;
            this.ZAvg =zAvg;
        }
    }
}