using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace VMori.Attribute
{
    /// <summary>
    /// プリフライトリクエス用のAttribute
    /// ①ヘッダーに値を追加することで、ブラウザからプリフライトリクエストをもらう
    /// ②CORSを設定しておくことで、続きのリクエストがキャンセルされ結果としてCSRF対策になる
    /// ※CORSは読み取りを禁止するだけなので、CORSだけではPOSTでデータを登録されてしまう
    /// </summary>
    /// <
    public class PreflightRequestAttribute : ActionFilterAttribute
    {
        const string headerKey = "vmori-csrf";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PreflightRequestAttribute() { }

        /// <summary>
        /// アクションメソッドが呼び出される直前
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method == "POST" && context.HttpContext.Request.Headers.Keys.Contains(headerKey) == false)
                context.Result = new BadRequestObjectResult("許可されてない通信方法です");
        }
    }
}
