using Diplom.Core.Entities;

namespace Diplom.BLL.DTO;

public class GoodDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SourceId { get; set; }
    public double Price { get; set; }
}