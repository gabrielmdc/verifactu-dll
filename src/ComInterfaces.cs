using System.Runtime.InteropServices;

namespace Sha256HelperDll
{
	// Dual para early + late binding
	[Guid("e022405d-2039-407b-aff8-f784f6b66063")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[ComVisible(true)]
	public interface ISha256Helper
	{
		[DispId(1)]
		string getHash(string msg);
	}
	// Clase COM visible
	[Guid("98897e60-b8fe-4b01-9f3b-548d34ad3968")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ProgId("Sha256Helper.Sha256HelperComClass")]
	public class Sha256HelperComClass : ISha256Helper
	{
		public string getHash(string msg) => Sha256Helper.GetMessageHash(msg);
	}
}
