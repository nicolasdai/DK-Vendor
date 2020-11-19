using System;
using System.Text;

public class XOREncrypter
{
    static string key = "BOWSER_VS_DONKEYKON";

    public static byte[] Encrypt(byte[] source)
    {
        byte[] dest = new byte[source.Length];
        for (int i = 0; i < source.Length; ++i)
        {
            dest[i] = (byte)(source[i] ^ (byte)key[i % key.Length]);
        }
        return dest;
    }

    public static byte[] Decrypt(byte[] source)
    {
        return Encrypt(source);
    }

    public static byte[] EncryptToBytes(string source)
    {
        return Encrypt(Encoding.UTF8.GetBytes(source));
    }

    public static string DecryptToString(byte[] source)
    {
        return Encoding.UTF8.GetString(Decrypt(source));
    }
}
