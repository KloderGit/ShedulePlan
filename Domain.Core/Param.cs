using Domain.Core.Interfaces;
using System;

namespace Domain.Core
{
    public class Param : IDataBaseEntity
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public int UnitLayoutId { get; set; }
        public virtual Node Layout { get; set; }

        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
    }
}
