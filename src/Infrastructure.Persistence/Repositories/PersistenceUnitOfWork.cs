using Core.Domain.Persistence.Contracts;
using Core.Domain.Persistence.Entities;
using Infrastructure.Persistence.Context;
using LinqToDB.Data;
using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class PersistenceUnitOfWork : IPersistenceUnitOfWork
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

        private readonly AppDbContext _dbContext;
        private bool _disposed;

        public PersistenceUnitOfWork(AppDbContext appDbContext,
             IRepositoryAsync<Hotel> hotelRepository,
             IRepositoryAsync<HotelFeature> hotelFeatureRepository,
             IRepositoryAsync<Room> roomRepository,
             IRepositoryAsync<RoomFeature> roomFeatureRepository,
             IRepositoryAsync<RoomType> roomTypeRepository,
             IRepositoryAsync<Booking> bookingRepository,
             IRepositoryAsync<Review> reviewRepository,
             IRepositoryAsync<Image> imageRepository,
             IRepositoryAsync<Comment> commentRepository
             )
        {
            _dbContext = appDbContext;
            Hotel = hotelRepository;
            HotelFeature = hotelFeatureRepository;
            Room = roomRepository;
            RoomFeature = roomFeatureRepository;
            RoomType = roomTypeRepository;
            Booking = bookingRepository;
            Review = reviewRepository;
            Image = imageRepository;
            Comment = commentRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DataConnection Linq2Db => _dbContext.CreateLinqToDbConnection();



        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing) _dbContext.Dispose();
            _disposed = true;
        }

        public async Task<IDbContextTransaction> BeginTranscationAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
    }
}
