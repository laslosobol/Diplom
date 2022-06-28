using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace Diplom.MVC.ViewModels;

public class SpecialOrderViewModel
{
    [Microsoft.Build.Framework.Required]
    [Display(Name = "Description")]
    public string Description { get; set; }
    public byte[] ReceiptPicture { get; set; }
    public Point Location { get; set; }
}