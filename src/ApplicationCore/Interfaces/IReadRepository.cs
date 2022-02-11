
using Ardalis.Specification;

namespace TestJob01.ApplicationCore.Interfaces;
public interface IReadRepository<T>: IReadRepositoryBase<T> where T : class, IAggregateRoot {
}
