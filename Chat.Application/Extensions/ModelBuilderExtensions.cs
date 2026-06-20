using Chat.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Extensions
{
    public static class ModelBuilderExtensions
    {

        public static void ApplyGlobalFilters(
      this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplySoftDeleteQueryFilter();
        }
        public static void ApplySoftDeleteQueryFilter(
            this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(
                        entityType.ClrType,
                        "e");

                    var property = Expression.Property(
                        parameter,
                        nameof(BaseEntity.IsDeleted));

                    var condition = Expression.Equal(
                        property,
                        Expression.Constant(false));

                    var lambda = Expression.Lambda(
                        condition,
                        parameter);

                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(lambda);
                }
            }
        }
    }
}
