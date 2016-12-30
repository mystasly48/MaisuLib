# MaisuLib

自分用に作成したライブラリ。

今は Twitter のみ。

Twitch も開発予定。

## MaisuLib.Twitter

自分用に作成したTwitterライブラリ。

`ConsumerKey`, `ConsumerSecret`, `AccessToken`, `AccessTokenSecret` を渡して `Sned(string)` してツイート送信するだけ。

戻り値は `WebResponse` になっているので、~~ご自由に判定してください。~~

リクエスト時に例外発生した場合にはこちら側で判定をして `TwitterException` が投げられるので、実際のところ `WebResponse` が帰ってきても何もないんですよね。ユーザ側で判定するように変更する予定です

[サンプル](https://github.com/mystasly48/MaisuLib/blob/master/TwitterSample/Program.cs)
```csharp
using System;
using System.IO;
using System.Net;
using MaisuLib.Twitter;

namespace TwitterSample {
  class Program {
    static void Main(string[] args) {
      Twitter twitter = new Twitter(
        "Your ConsumerKey",
        "Your ConsumerSecret",
        "Your AccessToken",
        "Your AccessTokenSecret");
      string message = Console.ReadLine();
      WebResponse response = twitter.Send(message);
      using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
        Console.WriteLine("Response: {0}", reader.ReadToEnd());
      }
    }
  }
}
```