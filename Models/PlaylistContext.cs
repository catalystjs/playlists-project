// Common Libraries
using System;
using System.Collections.Generic;
// LINQ Libraries
using System.Linq;
// ASP.NET Entity Framework
using Microsoft.EntityFrameworkCore;

namespace beltexam3.Models
{
    public class PlaylistContext : DbContext
    {
        public PlaylistContext(DbContextOptions<PlaylistContext> options) : base(options) { }
        // These are the tables
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        // Override the SaveChanges to handle CreatedAt and UpdatedAt
        public override int SaveChanges()
        {
            // update entities that are tracked and inherit from BaseEntity (UpdatedAt Property)
            var trackables = ChangeTracker.Entries<BaseEntity>().Where(x => x.State == EntityState.Modified);
            if (trackables != null)
            {
                // added and modified changes only
                foreach (var item in trackables)
                {
                    item.Entity.UpdatedAt = System.DateTime.Now;
                }
            }
            // Return the base SaveChanges
            return base.SaveChanges();
        }
        // Full populate methods for DRY
        public User PopulateUserSingle(int userid)
        {
            return Users.Where(x => x.Id == userid).Include(x => x.Playlists).ThenInclude(x => x.Song).SingleOrDefault();
        }
        public ICollection<User> PopulateUsersAll()
        {
            return Users.Include(x => x.Playlists).ThenInclude(x => x.Song).ToList();
        }
        public Song PopulateSongSingle(int songid)
        {
            return Songs.Where(x => x.Id == songid).Include(x => x.Playlists).ThenInclude(x => x.User).SingleOrDefault();
        }
        public ICollection<Song> PopulateSongsAll()
        {
            return Songs.Include(x => x.Playlists).ThenInclude(x => x.User).ToList();
        }
        public ICollection<Song> PopulateSongsAllOrderbyCreatedAt()
        {
            return Songs.Include(x => x.Playlists).OrderByDescending(x => x.CreatedAt).ToList();
        }
        public Playlist PopulatePlaylistSingle(int songid)
        {
            return Playlists.Where(x => x.Id == songid).Include(x => x.User).Include(x => x.Song).SingleOrDefault();
        }
        public ICollection<Playlist> PopulatePlaylistsAll()
        {
            return Playlists.Include(x => x.User).Include(x => x.Song).ToList();
        }
    }
}