using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// EntityのBaseクラス
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        public virtual string ID { get; set; }
    }
}
