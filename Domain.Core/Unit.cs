using Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Domain.Core
{
    public class Unit : IDataBaseEntity
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public bool Draft { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }

        public virtual ICollection<Node> Layouts { get; set; }
        public virtual ICollection<Param> Params { get; set; }

        public T ShallowCopy<T>() where T : Unit
        {
            T result = Activator.CreateInstance<T>();

            result.Active = this.Active;
            result.Created = this.Created;
            result.Draft = this.Draft;
            result.Guid = this.Guid;
            result.Id = this.Id;
            result.Layouts = this.Layouts;

            result.Modified = this.Modified;
            result.Params = this.Params;
            result.Title = this.Title;

            return (T)result;            
        }
    }
}
