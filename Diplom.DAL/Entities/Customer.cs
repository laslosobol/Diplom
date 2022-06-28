using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom.Core.Entities;

[NotMapped]
public class Customer : User
{
    [Encrypted]
    public string Name { get; set; }
    [Encrypted]
    public string Surname { get; set; }
    [Encrypted]
    public string CreditCard { get; set; }
}