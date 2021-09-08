using MessengerManager.Domain.Entities;
using MessengerManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MessengerManager.Infrastructure.Data
{
    public class MessengerManagerDbContext : DbContext, IUnitOfWork
    {
        public DbSet<ChatThreadEntity> ChatThreads { get; protected set; }
        public DbSet<MessageEntity> Messages { get; protected set; }
        public MessengerManagerDbContext() : base()
        { }

        public MessengerManagerDbContext(DbContextOptions<MessengerManagerDbContext> options)
        :base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                    "User ID=postgres;Password=root;Server=localhost;Port=5432;Database=messengerManager;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ChatThreadEntity
            modelBuilder.Entity<ChatThreadEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<ChatThreadEntity>().HasIndex(x => x.ThreadName)
                .IsUnique();
            modelBuilder.Entity<ChatThreadEntity>().HasIndex(x => x.SupChatId);
            
            //MessageEntity
            modelBuilder.Entity<MessageEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<MessageEntity>().HasIndex(x => x.Owner);
            modelBuilder.Entity<MessageEntity>().HasIndex(x => x.Sent);
            modelBuilder.Entity<MessageEntity>().HasIndex(x => x.ChatThreadName);
            modelBuilder.Entity<MessageEntity>().HasIndex(x => x.Date);
            
            base.OnModelCreating(modelBuilder);
        }
        
        //TODO: запечатать изменения
    }
}