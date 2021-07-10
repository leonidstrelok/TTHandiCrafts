using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;

namespace TTHandiCrafts.Infrastructure.Persistences
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public async Task InvokeTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            if (Database.CurrentTransaction != null)
            {
                await action();
                return;
            }

            using var transaction = await Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await action();
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<T> InvokeTransactionAsync<T>(Func<Task<T>> action,
            CancellationToken cancellationToken = default)
        {
            if (Database.CurrentTransaction != null)
            {
                return await action();
            }

            using var transaction = await Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await action();
                await transaction.CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }


        async Task IApplicationDbContext.AddAsync<T>(T entity, CancellationToken cancellationToken)
        {
            await AddAsync(entity, cancellationToken);
        }

        public async Task AddRange<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            AddRange(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return Database.BeginTransactionAsync(cancellationToken);
        }
    }
}