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
        /// Adds AWS ECS container metadata services to the collection.
        /// </summary>
        /// <param name="collection">Service collection.</param>
        /// <param name="lifetime">Service lifetime.</param>
        /// <returns>A reference to the current instance of <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddAwsEcsContainerMetadataServices(this IServiceCollection collection, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Add(new List<ServiceDescriptor>
            {
                ServiceDescriptor.Describe(typeof(IAwsEcsContainerMetadata), typeof(AwsEcsContainerMetadata), lifetime),
                ServiceDescriptor.Describe(typeof(IAwsEcsContainerMetadataClient), typeof(AwsEcsContainerMetadataHttpClient), lifetime),
            });
        }
    }
}
