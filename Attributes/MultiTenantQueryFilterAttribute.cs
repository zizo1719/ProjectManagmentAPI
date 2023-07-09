using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Project_Managment_API._Attributes
{
    public class MultiTenantQueryFilterAttribute : Attribute
    {
        public string TenantIdPropertyName { get; }

        public MultiTenantQueryFilterAttribute(string tenantIdPropertyName)
        {
            TenantIdPropertyName = tenantIdPropertyName;
        }
    }

    public static class MultiTenantQueryFilterExtensions
    {
        public static void ApplyMultiTenantQueryFilters(this ModelBuilder modelBuilder, string currentTenantId)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Check if there is a property named "TenantId" for the entity 
                var tenantIdProperty = entityType.FindProperty("TenantId");
                if (tenantIdProperty != null)
                {
                    var filterAttribute = entityType.ClrType.GetCustomAttributes(typeof(MultiTenantQueryFilterAttribute), true)
                        .FirstOrDefault() as MultiTenantQueryFilterAttribute;

                    if (filterAttribute != null && currentTenantId != null)
                    {
                        // Create a parameter expression for the entity type
                        var parameter = Expression.Parameter(entityType.ClrType, "entity");

                        // Create an expression to access the TenantId property using the specified property name in the attribute
                        var property = Expression.Property(parameter, filterAttribute.TenantIdPropertyName);

                        // Create an expression to compare the TenantId property with the currentTenantId
                        var filterExpression = Expression.Equal(property, Expression.Constant(currentTenantId));

                        // Create a lambda expression using the filter expression and the parameter
                        var lambdaExpression = Expression.Lambda(filterExpression, parameter);

                        // Apply the query filter to the entity type using the lambda expression
                        modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambdaExpression);
                    }
                }
            }
        }
    }
}
