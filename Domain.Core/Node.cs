using Domain.Core.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Core
{
    public class Node : INodeHub<Node>, IPayLoad, IDataBaseEntity
    {
        public Node()
        {
            Children = new List<Node>();
            Params = new List<Param>();
        }

        public int Id { get; set; }
        public int Order { get; set; }

        public int? ParentId { get; set; }
        public virtual Node Parent { get; set; }
        public virtual ICollection<Node> Children { get; set; }

        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<Param> Params { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
    }
}
