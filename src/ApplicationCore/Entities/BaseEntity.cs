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
        [Column(Order = 0)]
        public virtual string ID { get; set; }
    }
}
