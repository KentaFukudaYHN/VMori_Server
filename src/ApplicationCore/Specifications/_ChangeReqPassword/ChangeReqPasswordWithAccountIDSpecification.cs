using ApplicationCore.Entities;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// パスワード変更要求検索条件
    /// </summary>
    public class ChangeReqPasswordWithAccountIDSpecification : BaseSpecification<ChangeReqPassword>
    {
        /// <summary>
        /// tokenで検索
        /// </summary>
        /// <param name="token"></param>
        public ChangeReqPasswordWithAccountIDSpecification(string accountID)
            : base( (x) => x.AccountID == accountID) { }
    }
}
