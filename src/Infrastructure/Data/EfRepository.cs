using Ardalis.Specification.EntityFrameworkCore;

using TestJob01.ApplicationCore.Interfaces;

namespace TestJob01.Infrastructure.Data {
    public class EfRepository<T>: RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot {
        public EfRepository(TestJob01Context dbContext) : base(dbContext) {
        }
    }
}
