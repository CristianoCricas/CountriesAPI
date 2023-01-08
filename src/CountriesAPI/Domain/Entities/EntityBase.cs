namespace CountriesAPI.Domain.Entities
{
    public abstract class EntityBase
    {
        #region FIELDS
        public Guid Id { get; set; }

        public DateTime? DateUpdated { get; set; } = DateTime.Now;
        #endregion


        #region ACTIONS
        public virtual void CopyFrom(EntityBase newEntity)
        {
            DateUpdated = newEntity.DateUpdated;
        }

        public bool IsInserting()
        {

            return (Id == Guid.Empty);
        }
        #endregion
    }
}
