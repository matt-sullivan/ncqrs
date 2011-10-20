namespace Ncqrs.Config.Autofac
{
    using System;
    using global::Autofac;
    using Ncqrs.Domain;
    using Ncqrs.Domain.Storage;

    /// <summary>
    /// Use Autofac to create AggregateRoots for the domain repository. (This allows the domain
    /// repository to re-construct AggregateRoots to their current state, but also have
    /// dependencies injected.
    /// </summary>
    public class AutofacAggregateRootCreationStrategy : AggregateRootCreationStrategy
    {
        private readonly ILifetimeScope _containerScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacAggregateRootCreationStrategy"/> class.
        /// Note: This takes an autofac ILifetimeScope to control when objects resolved by this
        /// are disposed. It seems like this might be useful to isolate units of work, however
        /// I'm not sure how best to have ncqrs open/close new container scopes.
        /// </summary>
        /// <param name="containerScope">The container scope.</param>
        public AutofacAggregateRootCreationStrategy(ILifetimeScope containerScope)
        {
            _containerScope = containerScope;
        }

        protected override AggregateRoot CreateAggregateRootFromType(Type aggregateRootType)
        {
            return (AggregateRoot)_containerScope.Resolve(aggregateRootType);
        }
    }
}