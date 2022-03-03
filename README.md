# サイト名 Vtuberの森
Vtuber専門の動画サイトです ※こちらは就職活動用にポートフォリオとして公開しています<br>
<br>
Youtube上では他のYoutuberの動画によって、Vtuberの動画が探しにくい状況でしたので、専用のサイトを作ってみました<br>
 
# デモ
動画での説明です<br>
 https://youtu.be/IlPmrpzTLHU
 
# 機能
・Youtubeの動画を簡単にアップロード<br>
・ジャンルやタグ等で細かく検索<br>
・ニコニコ動画の様にコメントを流す(※ニコニコ動画の著作権に引っかからない事を確認しています)<br>
・アカウント機能<br>
・Vtuberファンの方同士で交流できるSNS(※未実装)
 
# 構成
フレームワーク：ASP.NET Core 5.0
DB：Azure SQL Server
画像等のストレージ：Azure Blob Container
アカウント認証方式：JWTを使用
 
# アーキテクチャ
 バックエンドとフロントエンドでプロジェクトを完全に分けています<br>
 フロントエンド：https://github.com/KentaFukudaYHN/VMori_Client ※Vue3 + TypeScript <br>
 <br>
 マイクロソフトのクリーンアーキテクチャに沿って実装しています<br>
 https://docs.microsoft.com/ja-jp/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures
