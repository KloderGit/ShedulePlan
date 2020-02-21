using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core.Interfaces
{
    public interface IComponentBuilder<in T>
    {
        IEnumerable<IComponent> Build(IEnumerable<T> nodes);
    }
}
