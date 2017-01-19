# MaisuLib

自分用に作成したライブラリ。

現時点では Twitter と Slack のみ。

Twitch は開発中。

## MaisuLib.Twitter

ツイートの送信のみに対応したTwitterライブラリ。

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

## MaisuLib.Slack

メッセージの送信のみに対応したSlackライブラリ。

[Incoming Webhook](https://api.slack.com/incoming-webhooks) を登録して生成された `Webhook URL` を指定して  
`Send(text, channel, username, icon_emoji, icon_url)` を実行するだけ。

[サンプル](https://github.com/mystasly48/MaisuLib/blob/master/SlackSample/Program.cs)
```csharp
using System;
using MaisuLib.Slack;

namespace SlackSample {
  class Program {
    static void Main(string[] args) {
	  Slack slack = new Slack("Your webhook url");
	  string message = Console.ReadLine();
	  string response = slack.Send(message);
	  Console.WriteLine(response);
	}
  }
}
```
