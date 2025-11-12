using System.Security.Cryptography;
using System.Text;

namespace Sha256HelperDll;

public class Sha256Helper
{
	// Returns lowercase hex SHA-256 of the UTF-8 bytes of msg
	public static string GetMessageHash(string msg)
	{
		var sb = new StringBuilder();

		using (var sha256 = SHA256.Create())
		{
			var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(msg));

			foreach (var b in hash)
			{
				sb.Append(b.ToString("X2")); // uppercase hex
			}
		}

		return sb.ToString();
	}
}
