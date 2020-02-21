using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface INodeHub<T> : INode<T>
    {
        ICollection<T> Children { get; set; }
    }
}
