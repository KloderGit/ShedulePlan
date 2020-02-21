using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IDBProxy<T> where T : INodeHub<T>
    {
        void LinkToDBRecord(T node);
        void SetParentDBRecord(T node);
        void ResetParentDBRecord();
    }
}