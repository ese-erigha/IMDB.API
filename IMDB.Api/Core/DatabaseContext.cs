using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using IMDB.Api.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace IMDB.Api.Core
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Entities.ContentRating> ContentRatings { get; set; }
        public DbSet<Entities.Country> Countries { get; set; }
        public DbSet<Entities.Genre> Genres { get; set; }
        public DbSet<Entities.Language> Languages { get; set; }
        public DbSet<Entities.Movie> Movies { get; set; }
        public DbSet<Entities.MovieGenre> MovieGenres { get; set; }
        public DbSet<Entities.MoviePerson> MoviePersons { get; set; }
        public DbSet<Entities.MoviePlotKeyword> MoviePlotKeywords { get; set; }
        public DbSet<Entities.Person> Persons { get; set; }
        public DbSet<Entities.PlotKeyword> PlotKeywords { get; set; }
        public DbSet<Entities.User> Users { get; set; }

        readonly DateTime myDate = DateTime.ParseExact("1000-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        const string _isDeletedProperty = "IsDeleted";
        static readonly MethodInfo _propertyMethod = typeof(EF).GetMethod(nameof(EF.Property), BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(typeof(bool));

        public DatabaseContext(DbContextOptions<DatabaseContext> options) :base(options)
        {
            Database.Migrate(); //Creates the database on first initialization
        }

        public DatabaseContext() :base()
        {
            Database.Migrate(); //Creates the database on first initialization
        }

        private static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            var parm = Expression.Parameter(type, "it");
            var prop = Expression.Call(_propertyMethod, parm, Expression.Constant(_isDeletedProperty));
            var condition = Expression.MakeBinary(ExpressionType.Equal, prop, Expression.Constant(false));
            var lambda = Expression.Lambda(condition, parm);
            return lambda;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Entities.Movie>()
                        .HasOne(m => m.ContentRating)
                        .WithMany(c => c.Movies)
                        .HasForeignKey(m => m.ContentRatingId);

            modelBuilder.Entity<Entities.Movie>()
                        .HasOne(m => m.Country)
                        .WithMany(c => c.Movies)
                        .HasForeignKey(m => m.CountryId);

            modelBuilder.Entity<Entities.Movie>()
                        .HasOne(m => m.Language)
                        .WithMany(l => l.Movies)
                        .HasForeignKey(m => m.LanguageId);

            modelBuilder.Entity<Entities.Movie>()
                        .HasOne(m => m.Director)
                        .WithMany(d => d.DirectedMovies)
                        .HasForeignKey(dm => dm.DirectorId);

            modelBuilder.Entity<Entities.MovieGenre>()
                        .HasKey(mv => new { mv.MovieId, mv.GenreId });

            modelBuilder.Entity<Entities.MovieGenre>()
                        .HasOne(mv => mv.Movie)
                        .WithMany(m => m.MovieGenres)
                        .HasForeignKey(mv => mv.MovieId);

            modelBuilder.Entity<Entities.MovieGenre>()
                        .HasOne(mv => mv.Genre)
                        .WithMany(m => m.MovieGenres)
                        .HasForeignKey(mv => mv.GenreId);

            modelBuilder.Entity<Entities.MoviePerson>()
                        .HasKey(mp => new { mp.MovieId, mp.PersonId });

            modelBuilder.Entity<Entities.MoviePerson>()
                        .HasOne(mp => mp.Movie)
                        .WithMany(m => m.MoviePersons)
                        .HasForeignKey(mp => mp.MovieId);

            modelBuilder.Entity<Entities.MoviePerson>()
                        .HasOne(mp => mp.Person)
                        .WithMany(m => m.MoviePersons)
                        .HasForeignKey(mp => mp.PersonId);

            modelBuilder.Entity<Entities.MoviePlotKeyword>()
                        .HasKey(mpk => new { mpk.MovieId, mpk.PlotKeywordId });

            modelBuilder.Entity<Entities.MoviePlotKeyword>()
                        .HasOne(mpk => mpk.Movie)
                        .WithMany(mv => mv.MoviePlotKeywords)
                        .HasForeignKey(mpk => mpk.MovieId);

            modelBuilder.Entity<Entities.MoviePlotKeyword>()
                        .HasOne(mpk => mpk.PlotKeyword)
                        .WithMany(mv => mv.MoviePlotKeywords)
                        .HasForeignKey(mpk => mpk.PlotKeywordId);

            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                if(typeof(ISoftDeletable).IsAssignableFrom(entity.ClrType) == true)
                {
                    entity.GetOrAddProperty(_isDeletedProperty, typeof(bool));

                    modelBuilder.Entity(entity.ClrType)
                                .HasQueryFilter(GetIsDeletedRestriction(entity.ClrType));
                }
            }


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {

            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSave();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        void OnBeforeSave()
        {
            SoftDelete();
            AddTimestamps();
        }

        void AddTimestamps()
        {
            var entries = ChangeTracker.Entries();
            var now = DateTime.UtcNow;
            foreach (var entry in entries)
            {
                
                if(entry.Entity is Entities.BaseEntity entity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entity.UpdatedAt = now;
                            break;

                        case EntityState.Added:
                            entity.CreatedAt = now;
                            entity.UpdatedAt = now;
                            break;
                    }
                }
            }
        }

        void SoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            {
                if (entry.Entity is ISoftDeletable)
                {
                    entry.Property(_isDeletedProperty).CurrentValue = true;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
