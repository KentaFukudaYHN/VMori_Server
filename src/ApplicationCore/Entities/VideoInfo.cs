using ApplicationCore.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// 動画情報Entity
    /// </summary>
    public class VideoInfo : BaseEntity
    {
        /// <summary>
        /// 動画タイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 動画プラットフォームの種類
        /// </summary>
        //public PlatFormKinds PlatFormKinds { get; set; }

        public VideoInfo() { }
    }
}
