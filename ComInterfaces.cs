using System.Runtime.InteropServices;

namespace VerifactuDll
{
	// Define a COM-visible interface. Dispatch interface for late binding could use [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)].
	// We use InterfaceIsDual for both early and late binding support.
	[Guid("B6DFE8D7-3B52-4F32-9F2A-7F5B9A6E9E10")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	public interface IVerifactu
	{
		string Echo(string input);
	}

	// Class implementing the COM interface. No auto-generated class interface; only the explicit interface is exposed.
	[Guid("3C1F8A90-07D7-4B36-9E2C-9E3F0D8AF7E4")]
	[ClassInterface(ClassInterfaceType.None)]
	public class VerifactuComClass : IVerifactu
	{
		public string Echo(string input) => $"Echo: {input}";
	}
}
