using ApplicationCore.Entities;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// パスワード変更要求検索条件
    /// </summary>
    public class ChangeReqPasswordWithCodeSpecification : BaseSpecification<ChangeReqPassword>
    {
        /// <summary>
        /// tokenで検索
        /// </summary>
        /// <param name="token"></param>
        public ChangeReqPasswordWithCodeSpecification(string token)
            : base( (x) => x.Code == token) { }
    }
}
