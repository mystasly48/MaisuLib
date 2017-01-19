using System;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Specialized;

namespace MaisuLib.Slack {
  public class Slack {
    Uri uri;
    Encoding enc = new UTF8Encoding();
    
    /// <summary>
    /// Sets an authorization information.
    /// </summary>
    /// <param name="webhookUri">Your webhook url</param>
    public Slack(string webhookUri) {
      uri = new Uri(webhookUri);
    }

    /// <summary>
    /// Send a message.
    /// </summary>
    /// <param name="text">Message</param>
    /// <param name="channel">Destination channel</param>
    /// <param name="username">Your username</param>
    /// <param name="icon_emoji">Emoji of your icon</param>
    /// <param name="icon_url">Image url of your icon</param>
    /// <returns>Returns a WebClient response message.</returns>
    public string Send(string text, string channel = null, string username = null, string icon_emoji = null, string icon_url = null) {
      SlackPayload payload = new SlackPayload() { Text = text, Channel = channel, Username = username, IconEmoji = icon_emoji, IconUrl = icon_url };
      return Send(payload);
    }
    
    private string Send(SlackPayload payload) {
      using (MemoryStream ms = new MemoryStream())
      using (StreamReader sr = new StreamReader(ms))
      using (WebClient wc = new WebClient()) {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SlackPayload));
        ser.WriteObject(ms, payload);
        ms.Position = 0;
        NameValueCollection data = new NameValueCollection();
        data["payload"] = sr.ReadToEnd();
        byte[] response = wc.UploadValues(uri, "POST", data);
        return enc.GetString(response);
      }
    }
  }

  [DataContract(Name = "payload")]
  internal class SlackPayload {
    [DataMember(Name = "channel")]
    public string Channel { get; set; }

    [DataMember(Name = "username")]
    public string Username { get; set; }
    
    [DataMember(Name = "text")]
    public string Text { get; set; }
    
    [DataMember(Name = "icon_emoji")]
    public string IconEmoji { get; set; }
    
    [DataMember(Name = "icon_url")]
    public string IconUrl { get; set; }
  }
}