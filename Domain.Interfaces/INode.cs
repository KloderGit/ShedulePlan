using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface INode<T>
    {
        T Parent { get; set; }
        int Order { get; set; }
    }
}
