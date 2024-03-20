using System.Text;

namespace RusClimate.EmailVerificationService.BLL.Service.Helpers
{
    public static class HashGeneratorHelper
    {
        public static string HashFunc(string text, string key)
        {
            List<byte> resStr = new List<byte>();
            int i = 0;
            foreach (var c in text)
            {
                resStr.Add((byte)(c ^ key[i++]));
                i = i % key.Length;
            }
            text = Encoding.Default.GetString(resStr.ToArray());
            return text;
        }
    }
}
