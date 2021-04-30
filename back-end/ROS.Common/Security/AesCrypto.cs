using System.Security.Cryptography;
using System.Text;

namespace ROS.Common.Security
{
	public static class AesCrypto
	{
		private static byte[] key = Encoding.UTF8.GetBytes("yUhTNu@X0@D#&mIKhN@hTr0s");

		private static Aes CreateAes()
		{
			var aes = Aes.Create();
			aes.Key = key;
			aes.IV = new byte[aes.BlockSize / 8];

			return aes;
		}

		public static string Encrypt(string plain)
		{
			var aes = CreateAes();
			var ct = aes.CreateEncryptor();
			var input = Encoding.Unicode.GetBytes(plain);
			var result = ct.TransformFinalBlock(input, 0, input.Length);

			return result.ToBase62();
		}

		public static string Decrypt(string cipherText)
		{
			var b = cipherText.FromBase62();
			var aes = CreateAes();
			var ct = aes.CreateDecryptor();
			var output = ct.TransformFinalBlock(b, 0, b.Length);

			return Encoding.Unicode.GetString(output);
		}
	}
}
