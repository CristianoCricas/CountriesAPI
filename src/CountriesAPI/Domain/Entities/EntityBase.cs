using CountriesAPI.Enums;

namespace CountriesAPI.Domain.Entities
{
    public abstract class EntityBase
    {
        #region FIELDS
        public Guid Id { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion


        #region ACTIONS
        public virtual void CopyFrom(EntityBase newEntity)
        {
            DateUpdated = newEntity.DateUpdated;
        }

        public virtual DataBaseAction ValidateDefaultValues()
        {
            this.DateUpdated = DateTime.Now;
            var dbAction = (this.Id == Guid.Empty) ? DataBaseAction.Insert : DataBaseAction.Update;

            if (dbAction == DataBaseAction.Insert)
                this.Id = Guid.NewGuid();

            return dbAction;
        }
        #endregion
    }
}
