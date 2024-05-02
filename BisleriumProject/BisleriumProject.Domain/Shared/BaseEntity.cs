namespace BisleriumProject.Domain.Shared
{
    public abstract class BaseEntity
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
