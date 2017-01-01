# MaisuLib

自分用に作成したライブラリ。

今は Twitter のみ。

Twitch も開発予定。

## MaisuLib.Twitter

自分用に作成したTwitterライブラリ。

`ConsumerKey`, `ConsumerSecret`, `AccessToken`, `AccessTokenSecret` を渡して `Sned(string)` してツイート送信するだけ。

~~戻り値は `WebResponse` になっているので、ご自由に判定してください。~~

戻り値はなくしました。問題がある場合には`TwitterExceptipn`が発生するので、例外処理で対応してください。

[サンプル](https://github.com/mystasly48/MaisuLib/blob/master/TwitterSample/Program.cs)
```csharp
using System;
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
      twitter.Send(message);
    }
  }
}
```