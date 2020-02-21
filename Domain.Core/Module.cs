using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core
{
    public class Module : UnitProxy, IComponent, INode<IComponent>
    {
        public Module(){}

        public Module(Node node) : base(node)
        {}

        public int Order
        {
            get => this.node.Order;
            set => this.node.Order = value;
        }

        public IComponent Parent { get; set; }

        public void AcceptVisitor()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public T GetListNodeValue<T>() where T : SheduleUnit, new()
        {
            var value = new T { BasedOn = this, Date = DateTimeOffset.Now };

            return value;
        }

        public LinkedList<T> GetSheduleSchema<T>()
        {
            throw new NotImplementedException();
        }
    }
}