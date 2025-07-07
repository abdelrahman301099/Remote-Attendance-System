using NetBlaze.SharedKernel.HelperUtilities.Constants;
using System.Collections;
using System.Reflection;

namespace NetBlaze.SharedKernel.HelperUtilities.General
{
    /// <summary>
    /// Attribute to mark properties that should be ignored during mapping validation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreReflectionMappingAttribute : Attribute;

    /// <summary>
    /// Class for mapping properties between objects using reflection.
    /// </summary>
    public static class ReflectionMapper
    {
        /// <summary>
        /// Maps a source object to a new instance of the destination type.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object to be mapped.</typeparam>
        /// <typeparam name="TDestination">The type of the destination object to be created.</typeparam>
        /// <param name="sourceObject">The source object to be mapped.</param>
        /// <returns>A new instance of the destination type with mapped properties.</returns>
        /// <exception cref="InvalidOperationException">Thrown if any source property (except those marked with IgnoreReflectionMapping) lacks a matching target property (case-sensitive) with the same type.</exception>
        public static TDestination MapToNew<TSource, TDestination>(TSource sourceObject)
            where TSource : class
            where TDestination : new()
        {
            ArgumentNullException.ThrowIfNull(sourceObject);

            var destinationObject = new TDestination();

            var collectionsValidationResult = AreMappingObjectsImplementingIEnumberable(typeof(TSource), typeof(TDestination));

            if (collectionsValidationResult)
            {
                throw new InvalidOperationException("Mapping between collections is not supported; try use MapToNewList<TSource, TDestination> instead.");
            }

            var sourceProperties = typeof(TSource).GetProperties().Where(p => p.CanRead).ToList();
            var destinationProperties = typeof(TDestination).GetProperties().Where(p => p.CanWrite).ToList();

            ValidatePropertyMapping(sourceProperties, destinationProperties, typeof(TDestination), requireTypeMatch: true);

            foreach (var sp in sourceProperties)
            {
                var targetDestinationProperty = destinationProperties.Find(dp => dp.Name == sp.Name);

                if (targetDestinationProperty is not null)
                {
                    var targetSourcePropertyValue = sp.GetValue(sourceObject);
                    targetDestinationProperty.SetValue(destinationObject, targetSourcePropertyValue);
                }
            }

            return destinationObject;
        }

        /// <summary>
        /// Maps a collection of source objects to a new list of destination objects.
        /// </summary>
        /// <typeparam name="TSource">The type of the source objects in the collection.</typeparam>
        /// <typeparam name="TDestination">The type of the destination objects in the collection.</typeparam>
        /// <param name="sourceCollection">The source collection to be mapped.</param>
        /// <returns>A list of new destination objects with mapped properties.</returns>
        /// <exception cref="InvalidOperationException">Thrown if any source property (except those marked with IgnoreReflectionMapping) lacks a matching target property (case-sensitive) with the same type.</exception>
        public static List<TDestination> MapToNewList<TSource, TDestination>(IEnumerable<TSource> sourceCollection)
            where TSource : class
            where TDestination : new()
        {
            ArgumentNullException.ThrowIfNull(sourceCollection);

            var destinationList = new List<TDestination>();

            foreach (var sourceItem in sourceCollection)
            {
                var destinationItem = MapToNew<TSource, TDestination>(sourceItem);
                destinationList.Add(destinationItem);
            }

            return destinationList;
        }

        /// <summary>
        /// Maps properties from a source object to an existing target object, updating its properties.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object to be mapped.</typeparam>
        /// <typeparam name="TTarget">The type of the target object to be updated.</typeparam>
        /// <param name="target">The target object to update.</param>
        /// <param name="source">The source object whose properties are mapped to the target.</param>
        /// <exception cref="InvalidOperationException">Thrown if any source property (except those marked with IgnoreReflectionMapping) lacks a matching target property (case-sensitive) with the same type.</exception>
        public static void MapToExisting<TSource, TTarget>(this TTarget target, TSource source)
            where TSource : class
            where TTarget : class
        {
            ArgumentNullException.ThrowIfNull(target);
            ArgumentNullException.ThrowIfNull(source);

            var sourceProperties = typeof(TSource).GetProperties().Where(p => p.CanRead).ToList();
            var destinationProperties = typeof(TTarget).GetProperties().Where(p => p.CanWrite).ToList();

            ValidatePropertyMapping(sourceProperties, destinationProperties, typeof(TTarget), requireTypeMatch: true);

            foreach (var sourceProp in sourceProperties)
            {
                var destProp = destinationProperties.FirstOrDefault(dp => dp.Name == sourceProp.Name && dp.PropertyType == sourceProp.PropertyType);

                if (destProp != null)
                {
                    var value = sourceProp.GetValue(source);

                    if (destProp.Name == MiscConstants.Name || destProp.Name == MiscConstants.Description)
                    {
                        value = value is string str ? str.Trim() ?? string.Empty : string.Empty;
                    }

                    destProp.SetValue(target, value);
                }
            }
        }

        #region Helper Methods
        /// <summary>
        /// Validates that all readable source properties (except those marked with IgnoreReflectionMapping) have matching writable destination properties by name (case-sensitive).
        /// </summary>
        /// <param name="sourceProperties">The list of source properties.</param>
        /// <param name="destinationProperties">The list of destination properties.</param>
        /// <param name="destinationType">The destination type for error messaging.</param>
        /// <param name="requireTypeMatch">Whether to require matching property types (used by MapToExisting).</param>
        /// <exception cref="InvalidOperationException">Thrown if any non-ignored source property lacks a matching destination property.</exception>
        private static void ValidatePropertyMapping(List<PropertyInfo> sourceProperties, List<PropertyInfo> destinationProperties, Type destinationType, bool requireTypeMatch = false)
        {
            var unmatchedProperties = sourceProperties
                .Where(sp =>
                    sp.GetCustomAttribute<IgnoreReflectionMappingAttribute>() == null &&
                    !destinationProperties.Any(dp => dp.Name == sp.Name && (!requireTypeMatch || dp.PropertyType == sp.PropertyType)))
                .Select(sp => sp.Name)
                .ToList();

            if (unmatchedProperties.Count > 0)
            {
                throw new InvalidOperationException(
                    $"The following source properties do not have matching {(requireTypeMatch ? "name and type" : "name")} properties in {destinationType.Name}: {string.Join(", ", unmatchedProperties)}");
            }
        }

        /// <summary>
        /// Checks if either the source or destination type implements the IEnumerable interface.
        /// </summary>
        /// <param name="sourceType">The type of the source object being mapped.</param>
        /// <param name="destinationType">The type of the destination object being mapped to.</param>
        /// <returns>True if either type implements IEnumerable, false otherwise.</returns>
        private static bool AreMappingObjectsImplementingIEnumberable(Type sourceType, Type destinationType)
        {
            return typeof(IEnumerable).IsAssignableFrom(sourceType) && sourceType != typeof(string) ||
                   typeof(IEnumerable).IsAssignableFrom(destinationType) && destinationType != typeof(string);
        }
        #endregion
    }
}