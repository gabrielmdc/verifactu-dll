using System.Runtime.InteropServices;

namespace VerifactuDll
{
	// Dual para early + late binding
	[Guid("be6df6c0-28f6-4d89-94bf-22cbae73bb49")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[ComVisible(true)]
	public interface IVerifactu
	{
		[DispId(1)]
		string getHash(string msg);
	}
	// Clase COM visible
	[Guid("e847e560-fc0a-4937-9e77-8c9a2bf97f1e")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ProgId("Verifactu.VerifactuComClass")]
	public class VerifactuComClass : IVerifactu
	{
		public string getHash(string msg) => Verifactu.GetMessageHash(msg);
	}
}
