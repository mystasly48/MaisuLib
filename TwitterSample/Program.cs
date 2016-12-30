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
