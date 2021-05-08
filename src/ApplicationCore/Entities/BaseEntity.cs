using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
