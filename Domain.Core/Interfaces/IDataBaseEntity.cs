using System;

namespace Domain.Core.Interfaces
{
    public interface IDataBaseEntity
    {
        int Id { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Modified { get; set; }
    }
}
