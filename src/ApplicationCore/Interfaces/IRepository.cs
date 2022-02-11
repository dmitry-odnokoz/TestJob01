using Ardalis.Specification;

namespace TestJob01.ApplicationCore.Interfaces {
    public interface IRepository<T>: IRepositoryBase<T> where T : class {
    }
}
