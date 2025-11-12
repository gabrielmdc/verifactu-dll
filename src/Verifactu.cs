using System.Security.Cryptography;
using System.Text;

namespace VerifactuDll;

public class Verifactu
{
	// Returns lowercase hex SHA-256 of the UTF-8 bytes of msg
	public static string GetMessageHash(string msg)
	{
		var sb = new StringBuilder();

		using (var sha256 = SHA256.Create())
		{
			// Java: new Base16(false).encodeAsString(...) => uppercase hex
			var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(msg));

			foreach (var b in hash)
			{
				sb.Append(b.ToString("X2")); // uppercase hex
			}
		}

		return sb.ToString();
	}

	private static String GetReferenciaRegistroAlta(String nifEmisor, String numFacturaSerie, String fechaExpedicion, String tipoFactura, String cuotaTotal, String importeTotal, String huellaAnterior, String fechaHoraUsoRegistro)
	{
		var fieldList = new[]
		{
			GetFieldValue("IDEmisorFactura", nifEmisor),
			GetFieldValue("NumSerieFactura", numFacturaSerie),
			GetFieldValue("FechaExpedicionFactura", fechaExpedicion),
			GetFieldValue("TipoFactura", tipoFactura),
			GetFieldValue("CuotaTotal", cuotaTotal),
			GetFieldValue("ImporteTotal", importeTotal),
			GetFieldValue("Huella", huellaAnterior),
			GetFieldValue("FechaHoraHusoGenRegistro", fechaHoraUsoRegistro)
		};

		return String.Join("&", fieldList);
	}

	private static String GetFieldValue(String name, String value)
	{
		var field = name + "=" + value;

		return field;
	}

	public static String GetVerifactuHash(String nifEmisor, String numFacturaSerie, DateTime fechaExpedicion,
		String tipoFactura, String cuotaTotal, String importeTotal, String huellaAnterior, DateTime fechaHoraUsoRegistro)
	{
		String str = GetReferenciaRegistroAlta(nifEmisor, numFacturaSerie,
			fechaExpedicion.ToString("dd-MM-yyyy"), tipoFactura, cuotaTotal, importeTotal,
			huellaAnterior, fechaHoraUsoRegistro.ToString("yyyy-MM-ddTHH:mm:ssK"));

		return GetMessageHash(str);
	}
}
