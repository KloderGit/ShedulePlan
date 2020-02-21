using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core
{
    public abstract class UnitProxy : Unit, IDBProxy<Node>
    {
        protected Node node = null;

        protected UnitProxy() { }

        protected UnitProxy(Node node)
        {
            this.node = node;
        }

        public void LinkToDBRecord(Node node)
        {
            this.node = node;
        }

        public void SetParentDBRecord(Node node)
        {
            this.node.Parent = node;
        }

        public void ResetParentDBRecord()
        {
            throw new NotImplementedException();
        }
    }
}