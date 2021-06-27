﻿using ApplicationCore.Config;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationCore.Services
{
    /// <summary>
    /// Youtubeサービス
    /// </summary>
    public class YoutubeService : IYoutubeService
    {
        private readonly YoutubeConfig _youtubeConfig;
        const string YOUTUBE_DOMAIN = "https://www.youtube.com";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public YoutubeService(IOptions<YoutubeConfig> youtubeConfig)
        {
            _youtubeConfig = youtubeConfig.Value;
        }

        /// <summary>
        /// 動画情報取得
        /// </summary>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        public async Task<IOutsourveVideoServiceRes> GetVideo(string youtubeVideoId)
        {
            var youtubeService = this.CreateYoutubeSearvice();

            var videoSearchRequest = youtubeService.Videos.List("snippet");
            videoSearchRequest.Id = youtubeVideoId;


            try
            {
                //var searchRes = await searchReq.ExecuteAsync();
               var searchRes = await videoSearchRequest.ExecuteAsync();

                if (searchRes.Items == null || searchRes.Items.Count == 0)
                    return null;

                var  result = searchRes.Items.Where(x => x.Id == youtubeVideoId).FirstOrDefault();

                if (result == null)
                    return null;

                return new OutsourceVideoSummaryServiceRes()
                {
                    VideoId = youtubeVideoId,
                    VideoTitle = result.Snippet.Title,
                    VideoLink = this.CreateVideoLink(youtubeVideoId),
                    ChannelId = result.Snippet.ChannelId,
                    ChannelTitle = result.Snippet.ChannelTitle,
                    Description = result.Snippet.Description,
                    ThumbnailLink = result.Snippet.Thumbnails.High.Url,
                    PublishDateTime = result.Snippet.PublishedAt.Value
                };
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        /// <summary>
        /// チャンネル情報を取得
        /// </summary>
        /// <returns></returns>
        public async Task<OutsourceVideoChannel> GetChannel(string channelId)
        {
            var youtubeService = this.CreateYoutubeSearvice();

            //チャンネル情報を取得
            var searchReq = youtubeService.Channels.List("snippet, statistics");

            searchReq.Id = channelId;

            var channelResult = await searchReq.ExecuteAsync();

            if (channelResult.Items == null || channelResult.Items.Count == 0)
                return null;

            var channnel = channelResult.Items.Where(x => x.Id == channelId).FirstOrDefault();

            if (channnel == null)
                return null;

            var thmbnailUrl = "";
            if(channnel.Snippet.Thumbnails != null && channnel.Snippet.Thumbnails.High != null)
            {
                thmbnailUrl = channnel.Snippet.Thumbnails.High.Url;
            }

            return new OutsourceVideoChannel()
            {
                ID = channnel.Id,
                Title = channnel.Snippet.Title,
                CommentCount = channnel.Statistics.CommentCount,
                Description = channnel.Snippet.Description,
                PublishAt = channnel.Snippet.PublishedAt,
                ThumbnailUrl = thmbnailUrl,
                SubscriverCount = channnel.Statistics.SubscriberCount,
                VideoCount = channnel.Statistics.VideoCount,
                ViewCount = channnel.Statistics.ViewCount
            };
        }

        /// <summary>
        /// 動画の統計情報取得
        /// </summary>
        /// <param name="outsourceVideoId"></param>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        public async Task<IOutsourceVideoStatisticsServiceRes> GetVideoStatistics(string youtubeVideoId)
        {
            var youtubeService = this.CreateYoutubeSearvice();

            //統計情報を取得
            var searchVideoReq = youtubeService.Videos.List("contentDetails, statistics");
            //動画IDで検索
            searchVideoReq.Id = youtubeVideoId;

            var videoResult = await searchVideoReq.ExecuteAsync();

            if (videoResult.Items == null || videoResult.Items.Count == 0)
                return null;

            //動画じゃなければnull
            var targetVideoDetail = videoResult.Items[0];
            if (targetVideoDetail.Kind != "youtube#video")
                return null;

            ulong commentCount = 0;
            ulong likeCount = 0;
            ulong viewCount = 0;
            if (targetVideoDetail.Statistics.CommentCount != null)
                commentCount = targetVideoDetail.Statistics.CommentCount.Value;
            if (targetVideoDetail.Statistics.LikeCount != null)
                likeCount = targetVideoDetail.Statistics.LikeCount.Value;
            if (targetVideoDetail.Statistics.ViewCount != null)
                viewCount = targetVideoDetail.Statistics.ViewCount.Value;

            return new OutsourceVideoStatisticsServiceRes()
            {
                CommentCount = commentCount,
                LikeCount = likeCount,
                ViewCount = viewCount,
            };
        }

        /// <summary>
        /// YoutubeのUrlからIDを取得
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public string GetVideoId(Uri uri)
        {
            try
            {
                var id = HttpUtility.ParseQueryString(uri.Query).Get("v");

                return id;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// OutsourceServiceの生成
        /// </summary>
        /// <returns></returns>
        private YouTubeService CreateYoutubeSearvice()
        {
            return new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _youtubeConfig.ApiKey
            });
        }

        /// <summary>
        /// VideoIDから動画リンクを生成
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public string CreateVideoLink(string youtubeVideoId)
        {
            return YOUTUBE_DOMAIN + "/watch?v=" + youtubeVideoId;
        }
    }
}
