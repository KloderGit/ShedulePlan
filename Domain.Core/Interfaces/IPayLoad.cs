using System.Collections.Generic;

namespace Domain.Core.Interfaces
{
    public interface IPayLoad
    {
        Unit Unit { get; set; }
        ICollection<Param> Params { get; set; }
    }
}
