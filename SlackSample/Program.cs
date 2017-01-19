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
