namespace NotebookDemo.Core.Data
{
	/// <summary>
	/// Defines functionality for mapping between an entity and a domain model that represent the same object.
	/// </summary>
	/// <typeparam name="TEntity">The entity.</typeparam>
	/// <typeparam name="TDomainModel">The domain model.</typeparam>
	public interface IEntityMapper<TEntity, TDomainModel>
	{
		/// <summary>
		/// Maps the entity to a domain model.
		/// </summary>
		/// <param name="entity">The entity to map from.</param>
		/// <returns>The domain model.</returns>
		TDomainModel MapFromEntity(TEntity entity);

		/// <summary>
		/// Maps the domain model to an entity.
		/// </summary>
		/// <param name="domainModel">The domain model to map from.</param>
		/// <returns>The entity.</returns>
		TEntity MapToEntity(TDomainModel domainModel);
	}
}
