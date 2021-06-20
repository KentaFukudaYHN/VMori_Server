using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.Json;

namespace ApplicationCore.Services
{
    public class NikoNikoService : INikoNikoService, IOutsourcePlatFormVideoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string NIKONIKO_API_URL = "https://api.search.nicovideo.jp/api/v2/snapshot/video/contents/search";
        private const string USER_AGENT = "Vtuber no Mori";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="clientFactory"></param>
        public NikoNikoService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        public async Task<IOutsourveVideoServiceRes> GetVideo(string nikonikoVideoId)
        {
            var nikonikoData = await this.GetNikoNikoData(nikonikoVideoId);
            if (nikonikoData == null)
                return null;

            return new OutsourceVideoSummaryServiceRes()
            {
                ChannelId = nikonikoData.channelId,
                ChannelTitle = "",
                Description = nikonikoData.description,
                VideoId = nikonikoVideoId,
                VideoTitle = nikonikoData.title,
                ThumbnailLink = nikonikoData.thumbnailUrl,
                VideoLink = this.CreateVideoLink(nikonikoVideoId),
                PublishDateTime = DateTime.Parse(nikonikoData.startTime)
            };
        }

        /// <summary>
        /// ニコニコ動画IDの取得
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public string GetVideoId(Uri uri)
        {
            try
            {
                var id = uri.Segments[uri.Segments.Length - 1];
                return id;
            }catch(Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// ニコニコ動画のリンクを生成
        /// </summary>
        /// <param name="nikonikoVideoId"></param>
        /// <returns></returns>
        public string CreateVideoLink(string nikonikoVideoId)
        {
            return "https://nico.ms/ " + nikonikoVideoId;
        }

        /// <summary>
        /// 動画の統計情報を取得
        /// </summary>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        public async Task<IOutsourceVideoStatisticsServiceRes> GetVideoStatistics(string nikonikoVideoId)
        {
            var nikonikoData = await this.GetNikoNikoData(nikonikoVideoId);
            if (nikonikoData == null)
                throw new Exception("統計情報を取得できませんでした");

            return new OutsourceVideoStatisticsServiceRes()
            {
                CommentCount = nikonikoData.commentCounter,
                LikeCount = nikonikoData.likeCounter,
                ViewCount = nikonikoData.viewCounter
            };
        }

        /// <summary>
        /// ニコニコ動画情報取得
        /// </summary>
        /// <param name="nikonikoVideoId"></param>
        /// <returns></returns>
        private async Task<NikoNIkoApiData> GetNikoNikoData(string nikonikoVideoId)
        {
            //HttpClientの設定
            var q = ""; //検索キーワード IDをFilterで絞り込むので検索キーワードは空にする
            var sort = "viewCounter"; //ソート 設定必須なので再生回数でソートする ※IDで絞り込むので、一件しか取らないので何でもよい
            var context = "Vtuber no Mori"; //サービス名
            var fields = "contentId,channelId,description,title,thumbnailUrl,startTime,commentCounter,likeCounter,viewCounter"; //取得するフィールド
            var filters = "filters[contentId][0]="; //動画IDでフィルター

            var requestUrl = NIKONIKO_API_URL + "?q=" + q +
                        "&_sort=" + sort +
                        "&_context=" + context +
                        "&fields=" + fields +
                        "&" + filters + nikonikoVideoId;

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Add("User-Agemt", USER_AGENT);

            var client = _clientFactory.CreateClient();

            try
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var nikonikoRes = await JsonSerializer.DeserializeAsync<NikoNikoApiRes>(responseStream);

                    //想定外の結果はNULL!!
                    if (nikonikoRes.data.Length == 0 || nikonikoRes.data.Length > 1 || nikonikoRes.data[0].contentId != nikonikoVideoId)
                    {
                        return null;
                    }

                    //サムネイルのリンクがそのままだと低画質なので.Lを付けて高画質に
                    nikonikoRes.data[0].thumbnailUrl += ".L";

                    return  nikonikoRes.data[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// ニコニコAPIレスポンス
        /// </summary>
        private class NikoNikoApiRes
        {
            /// <summary>
            /// メタ情報
            /// </summary>
            public NikoNikoApiMeta meta { get; set; }
            /// <summary>
            /// 取得結果
            /// </summary>
            public NikoNIkoApiData[] data { get; set; }
        }

        /// <summary>
        /// ニコニコAPI メタ情報
        /// </summary>
        private class NikoNikoApiMeta
        {
            /// <summary>
            /// ステータス
            /// </summary>
            public int status { get; set; }
            /// <summary>
            /// 取得結果数
            /// </summary>
            public int totalCount { get; set; }
            /// <summary>
            /// ID
            /// </summary>
            public string id { get; set; }
        }

        /// <summary>
        /// ニコニコAPI データ情報
        /// </summary>
        private class NikoNIkoApiData
        {
            /// <summary>
            /// コンテンツID
            /// </summary>
            public string contentId { get; set; }
            /// <summary>
            /// タイトル
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 説明
            /// </summary>
            public string description { get; set; }
            /// <summary>
            /// ユーザーID
            /// </summary>
            public int userId { get; set; }
            /// <summary>
            /// チャンネルID
            /// </summary>
            public string channelId { get; set; }
            /// <summary>
            /// 再生回数
            /// </summary>
            public ulong viewCounter { get; set; }
            /// <summary>
            /// マイリスト数またはお気に入り数
            /// </summary>
            public ulong mylistCounter { get; set; }
            /// <summary>
            /// いいね数
            /// </summary>
            public ulong likeCounter { get; set; }
            /// <summary>
            /// 再生時間(秒)
            /// </summary>
            public ulong lengthSeconds { get; set; }
            /// <summary>
            /// サムネイルURL
            /// </summary>
            public string thumbnailUrl { get; set; }
            /// <summary>
            /// コンテンツの投稿時間 ISO8601形式
            /// </summary>
            public string startTime { get; set; }
            /// <summary>
            /// 最新のコメント
            /// </summary>
            public string lastResBody { get; set; }
            /// <summary>
            /// コメント数
            /// </summary>
            public ulong commentCounter { get; set; }
            /// <summary>
            /// 最新コメント時間 ISO8601形式
            /// </summary>
            public string lastCommentTime { get; set; }
            /// <summary>
            /// カテゴリタグ
            /// </summary>
            public string categoryTags { get; set; }
            /// <summary>
            /// タグ(空白区切り)
            /// </summary>
            public string tags { get; set; }
            /// <summary>
            /// タグ完全一致(空白区切り)
            /// </summary>
            public string tagsExact { get; set; }
            /// <summary>
            /// ジャンル
            /// </summary>
            public string genre { get; set; }
        }
    }
}
