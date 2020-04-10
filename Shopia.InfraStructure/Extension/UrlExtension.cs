using System.Net;

namespace Shopia.InfraStructure
{
    public static class UrlExtension
    {
        public static string ToEncodedUrl(this string url)
            => WebUtility.UrlEncode(url);

        public static string ToDecodedUrl(this string url)
            => WebUtility.UrlDecode(url);

    }
}
