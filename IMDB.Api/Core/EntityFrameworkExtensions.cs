using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace IMDB.Api.Core
{
    public static class EntityFrameworkExtensions
    {
        public static void ApplyCascadeDeletes(this IEnumerable<EntityEntry> entities)
        {
            foreach (var entry in entities.Where(
               e => (e.State == EntityState.Modified || e.State == EntityState.Added)
                        && e.GetInternalEntityEntry().HasConceptualNull).ToList())
            {
                entry.GetInternalEntityEntry().HandleConceptualNulls(false);
            }

            foreach (var entry in entities.Where(e => e.State == EntityState.Deleted).ToList())
            {
                //entry.GetInternalEntityEntry().CascadeDelete();
            }
        }

        public static InternalEntityEntry GetInternalEntityEntry(this EntityEntry entityEntry)
        {
            var internalEntry = (InternalEntityEntry)entityEntry
                .GetType()
                .GetProperty("InternalEntry", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(entityEntry);

            return internalEntry;
        }


    
    }
}
