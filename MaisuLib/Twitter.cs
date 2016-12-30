using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MaisuLib.Twitter {
  public class Twitter {
    /// <summary>
    /// Gets ConsumerKey
    /// </summary>
    public string ConsumerKey { get; private set; }
    /// <summary>
    /// Gets ConsumerSecret
    /// </summary>
    public string ConsumerSecret { get; private set; }
    /// <summary>
    /// Gets AccessToken
    /// </summary>
    public string AccessToken { get; private set; }
    /// <summary>
    /// Gets AccessTokenSecret
    /// </summary>
    public string AccessSecret { get; private set; }

    private Dictionary<string, string> Parameters = new Dictionary<string, string>();
    private Dictionary<string, string> CustomParameters = new Dictionary<string, string>();

    private const string VERSION = "1.0";
    private const string SIGNATURE_METHOD = "HMAC-SHA1";
    private const string REQUEST_URL = "https://api.twitter.com/1.1/statuses/update.json";
    private const string REQUEST_METHOD = "POST";

    /// <summary>
    /// Sets a authorization information
    /// </summary>
    /// <param name="ConsumerKey">Your ConsumerKey</param>
    /// <param name="ConsumerSecret">Your ConsumerSecret</param>
    /// <param name="AccessToken">Your AccessToken</param>
    /// <param name="AccessSecret">Your AccessTokenSecret</param>
    public Twitter(string ConsumerKey, string ConsumerSecret, string AccessToken, string AccessSecret) {
      this.ConsumerKey = ConsumerKey;
      this.ConsumerSecret = ConsumerSecret;
      this.AccessToken = AccessToken;
      this.AccessSecret = AccessSecret;
    }

    /// <summary>
    /// Post a status message
    /// </summary>
    /// <param name="text">Message</param>
    /// <returns>Response</returns>
    public void Send(string text) {
      CustomParameters.Add("status", text.ToRFC3986());
      Execute();
    }

    private void Execute() {
      var basicParameters = GetBasicParameters();
      Parameters = basicParameters.Concat(CustomParameters).ToDictionary(x => x.Key, x => x.Value);
      var signature = GenerateSignature(Parameters);
      Parameters.Add("oauth_signature", signature);
      var header = GenerateHeader(Parameters);

      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(REQUEST_URL);
      request.Method = REQUEST_METHOD;
      request.ContentType = "application/x-www-form-urlencoded";
      request.Headers.Add("Authorization", header);

      byte[] body = Encoding.ASCII.GetBytes(CustomParameters.Select(x => string.Format("{0}={1}", x.Key, x.Value)).Join("&"));
      using (Stream stream = request.GetRequestStream()) {
        stream.Write(body, 0, body.Length);
      }

      try {
        WebResponse response = request.GetResponse();
      } catch (WebException ex) {
        using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream())) {
          string error = reader.ReadToEnd();
          Match regx = new Regex(@"(\""errors""\:\[\{""code""\:)(?<code>.+?)(,""message""\:"")(?<msg>.+?)("")", RegexOptions.IgnoreCase).Match(error);
          int code = int.Parse(regx.Groups["code"].Value);
          string msg = regx.Groups["msg"].Value;
          throw new TwitterException(msg);
        }
      } catch {
        throw;
      } finally {
        request.Abort();
      }
    }

    private Dictionary<string, string> GetBasicParameters() {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary.Add("oauth_consumer_key", ConsumerKey);
      dictionary.Add("oauth_nonce", GenerateNonce());
      dictionary.Add("oauth_signature_method", SIGNATURE_METHOD);
      dictionary.Add("oauth_timestamp", GenerateTimestamp());
      dictionary.Add("oauth_token", AccessToken);
      dictionary.Add("oauth_version", VERSION);
      return dictionary;
    }

    private string GenerateHeader(Dictionary<string, string> dictionary) {
      return "OAuth " +
        dictionary.Where(x => x.Key.StartsWith("oauth_")).Select(x => $"{x.Key}=\"{x.Value.ToRFC3986()}\"").Join(", ");
    }

    private string GenerateSignature(Dictionary<string, string> dictionary) {
      string message = $"{REQUEST_METHOD}&{REQUEST_URL.ToRFC3986()}&" + dictionary.OrderBy(x => x.Key)
        .Select(x => $"{x.Key}={x.Value}")
        .Join("&").ToRFC3986();
      string key = $"{ConsumerSecret.ToRFC3986()}&{AccessSecret.ToRFC3986()}";
      using (HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.ASCII.GetBytes(key))) {
        byte[] hash = hmacsha1.ComputeHash(Encoding.ASCII.GetBytes(message));
        return Convert.ToBase64String(hash);
      }
    }

    private static string GenerateTimestamp() {
      return ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
    }

    private static string GenerateNonce() {
      return new Random().Next(0x0000000, 0x7fffffff).ToString("X8");
    }
  }

  internal static class TwitterExtention {
    public static string ToRFC3986(this string value) {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      string encoded = Uri.EscapeDataString(value);
      return Regex.Replace(encoded, "(%[0-9a-f][0-9a-f])", c => c.Value.ToUpper())
          .Replace("(", "%28")
          .Replace(")", "%29")
          .Replace("$", "%24")
          .Replace("!", "%21")
          .Replace("*", "%2A")
          .Replace("'", "%27")
          .Replace("%7E", "~");
    }

    public static string Join<T>(this IEnumerable<T> items, string separator) {
      return string.Join(separator, items.ToArray());
    }
  }

  internal class TwitterException : Exception {
    public TwitterException() { }
    public TwitterException(string message) : base(message) { }
    public TwitterException(string message, Exception inner) : base(message, inner) { }
  }
}
