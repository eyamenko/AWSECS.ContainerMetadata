using System;
using System.Collections.Generic;
using AWSECS.ContainerMetadata.Contracts;
using AWSECS.ContainerMetadata.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AWSECS.ContainerMetadata.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds AWS ECS container metadata service to the collection.
        /// </summary>
        /// <param name="collection">Service collection.</param>
        /// <param name="lifetime">Service lifetime.</param>
        /// <returns>A reference to the current instance of <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddAWSContainerMetadataService(this IServiceCollection collection, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Add(new List<ServiceDescriptor>
            {
                ServiceDescriptor.Describe(typeof(IAWSContainerMetadata), typeof(AWSContainerMetadata), lifetime),
                ServiceDescriptor.Describe(typeof(IAWSContainerMetadataClient), typeof(AWSContainerMetadataHttpClient), lifetime),
            });
        }
    }
}
