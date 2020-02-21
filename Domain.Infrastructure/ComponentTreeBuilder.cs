using Domain.Core;
using Domain.Core.Interfaces;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Domain.Infrastructure
{
    public class ComponentTreeBuilder : IComponentBuilder<Node>
    {
        public IEnumerable<IComponent> Build(IEnumerable<Node> nodes)
        {
            if (nodes == null || nodes.Any() == false) throw new ArgumentException("Последовательность записей пуста");
                return nodes.Select(x => Recursion(x));
        }

        public IComponent Recursion(Node node)
        {
            if (node == null || node.Unit == null) throw new ArgumentNullException("Запись равна null либо не привязан Unit");

            var component = node.Children?.Count > 0 ? ConvertToComposit(node) : ConvertToModel(node);
            if (component is Composit) ((Composit)component).AddRange(Build(node.Children));

            return component;
        }

        IComponent ConvertToComposit(Node node)
        {
            var component = (Composit)node.Unit.ShallowCopy<Composit>();
            (component as UnitProxy).LinkToDBRecord(node);

            return component;
        }

        IComponent ConvertToModel(Node node)
        {
            var component = node.Unit.ShallowCopy<Module>();
            (component as UnitProxy).LinkToDBRecord(node);

            return component;
        }
    }
}
