using Microsoft.Extensions.Hosting;
using OpenMessage.Builders;
using System;

namespace OpenMessage.AWS.SQS.Configuration
{
    /// <summary>
    ///     The builder for an SQS consumer
    /// </summary>
    /// <typeparam name="T">The type to be dispatched</typeparam>
    public interface ISqsDispatcherBuilder<T> : IBuilder
    {
        /// <summary>
        ///     Configure the dispatcher with the specified options
        /// </summary>
        /// <param name="configuration">The configuration action</param>
        /// <returns>The SQS dispatcher builder</returns>
        ISqsDispatcherBuilder<T> FromConfiguration(Action<SQSDispatcherOptions<T>> configuration);

        /// <summary>
        ///     Configure the dispatcher with the specified options
        /// </summary>
        /// <param name="configuration">The configuration action</param>
        /// <returns>The SQS dispatcher builder</returns>
        ISqsDispatcherBuilder<T> FromConfiguration(Action<HostBuilderContext, SQSDispatcherOptions<T>> configuration);

        /// <summary>
        ///     Configure the dispatcher with the specified options
        /// </summary>
        /// <param name="configurationSection">The configuration section to use</param>
        /// <returns>The SQS dispatcher builder</returns>
        ISqsDispatcherBuilder<T> FromConfiguration(string configurationSection);
    }
}