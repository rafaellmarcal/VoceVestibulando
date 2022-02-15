using System;
using VV.Core.DomainObjects;

namespace VV.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot { }
}
