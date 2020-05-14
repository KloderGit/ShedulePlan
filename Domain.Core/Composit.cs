using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace Domain.Core
{
    public class Composit : UnitProxy, ICollection<IComponent>, IComponent, INodeHub<IComponent>
    {
        public bool IsReadOnly => false;

        public IComponent Parent { get; set; }
        public ICollection<IComponent> Children { get; set; } = new List<IComponent>();

        public Composit()
        { }
        public Composit(Node node) : base(node)
        { }

        public int Order { 
            get => this.node.Order; 
            set => this.node.Order = value; 
        }

        public int Count => Children.Count();

        public void Add(IComponent item)
        {
            (item as UnitProxy).SetParentDBRecord(this.node);
            this.Children.Add(item);
        }

        public void AddRange(IEnumerable<IComponent> array)
        {
            array.ToList().ForEach(x => Add(x));
        }

        public void Clear()
        {
            Children.Clear();
        }

        public bool Contains(IComponent item)
        {
            return Children.Contains(item);
        }

        public void CopyTo(IComponent[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IComponent> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        public bool Remove(IComponent item)
        {
            return Children.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        //public LinkedList<T> GetSheduleSchema<T>()
        //{
        //    var list = new LinkedList<SheduleUnit>();

        //}

        //public LinkedListNode<SheduleUnit> GetListNodeValue<SheduleUnit>()
        //{
        //    var list = new LinkedList<SheduleUnit>();

        //    foreach (var component in this.Children)
        //    {
        //        list.AddLast(GetListNodeValue<SheduleUnit>());
        //    }
        //}
    }
}
