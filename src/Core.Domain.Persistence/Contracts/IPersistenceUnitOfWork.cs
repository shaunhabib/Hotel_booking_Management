using Core.Domain.Persistence.Entities;
using LinqToDB.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Persistence.Contracts
{
    public interface IPersistenceUnitOfWork : IDisposable
    {
        public IRepositoryAsync<Hotel> Hotel { get; }
        public IRepositoryAsync<HotelFeature> HotelFeature { get; }
        public IRepositoryAsync<Room> Room { get; }
        public IRepositoryAsync<RoomFeature> RoomFeature { get; }
        public IRepositoryAsync<RoomType> RoomType { get; }
        public IRepositoryAsync<Booking> Booking { get; }
        public IRepositoryAsync<Review> Review { get; }
        public IRepositoryAsync<Comment> Comment { get; }
        public IRepositoryAsync<Image> Image { get; }
        DataConnection Linq2Db { get; }

        Task<int> SaveChangesAsync();

        Task<IDbContextTransaction> BeginTranscationAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}
