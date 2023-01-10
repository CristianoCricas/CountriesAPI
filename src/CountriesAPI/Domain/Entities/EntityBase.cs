namespace CountriesAPI.Domain.Entities
{
    public abstract class EntityBase
    {
        #region FIELDS
        public Guid Id { get; set; }

        public DateTime? DateUpdated { get; set; } = DateTime.Now;
        #endregion


        #region ACTIONS
        /// <summary>
        /// Copy information from an Entity
        /// </summary>
        /// <param name="newEntity"></param>
        public virtual void CopyFrom(EntityBase newEntity)
        {
            DateUpdated = newEntity.DateUpdated;
        }

        /// <summary>
        /// Return if this Entity is Inserting (when ID is empty):
        /// </summary>
        public bool IsInserting()
        {

            return (Id == Guid.Empty);
        }
        #endregion
    }
}
