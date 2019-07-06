using System;
using System.IO;
using System.Runtime.InteropServices;

using Microsoft.Win32.SafeHandles;

/// <summary>
/// Defines the <see cref="ConsoleHelper" />
/// </summary>
internal static class ConsoleHelper {
	/// <summary>
	/// The Initialize
	/// </summary>
	/// <param name="alwaysCreateNewConsole">The alwaysCreateNewConsole<see cref="bool"/></param>
	static public void Initialize(bool alwaysCreateNewConsole = true) {
		bool consoleAttached = true;
		if(alwaysCreateNewConsole
			|| (AttachConsole(ATTACH_PARRENT) == 0
			&& Marshal.GetLastWin32Error() != ERROR_ACCESS_DENIED)) {
			consoleAttached = AllocConsole() != 0;
		}

		if(consoleAttached) {
			InitializeOutStream();
			InitializeInStream();
		}
	}

	/// <summary>
	/// The InitializeOutStream
	/// </summary>
	private static void InitializeOutStream() {
		var fs = CreateFileStream("CONOUT$", GENERIC_WRITE, FILE_SHARE_WRITE, FileAccess.Write);
		if(fs != null) {
			var writer = new StreamWriter(fs) { AutoFlush = true };
			Console.SetOut(writer);
			Console.SetError(writer);
		}
	}

	/// <summary>
	/// The InitializeInStream
	/// </summary>
	private static void InitializeInStream() {
		var fs = CreateFileStream("CONIN$", GENERIC_READ, FILE_SHARE_READ, FileAccess.Read);
		if(fs != null) {
			Console.SetIn(new StreamReader(fs));
		}
	}

	/// <summary>
	/// The CreateFileStream
	/// </summary>
	/// <param name="name">The name<see cref="string"/></param>
	/// <param name="win32DesiredAccess">The win32DesiredAccess<see cref="uint"/></param>
	/// <param name="win32ShareMode">The win32ShareMode<see cref="uint"/></param>
	/// <param name="dotNetFileAccess">The dotNetFileAccess<see cref="FileAccess"/></param>
	/// <returns>The <see cref="FileStream"/></returns>
	private static FileStream CreateFileStream(string name, uint win32DesiredAccess, uint win32ShareMode,
							FileAccess dotNetFileAccess) {
		var file = new SafeFileHandle(CreateFileW(name, win32DesiredAccess, win32ShareMode, IntPtr.Zero, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero), true);
		if(!file.IsInvalid) {
			var fs = new FileStream(file, dotNetFileAccess);
			return fs;
		}
		return null;
	}

	/// <summary>
	/// The AllocConsole
	/// </summary>
	/// <returns>The <see cref="int"/></returns>
	[DllImport("kernel32.dll",
		EntryPoint = "AllocConsole",
		SetLastError = true,
		CharSet = CharSet.Auto,
		CallingConvention = CallingConvention.StdCall)]
	private static extern int AllocConsole();

	/// <summary>
	/// The AttachConsole
	/// </summary>
	/// <param name="dwProcessId">The dwProcessId<see cref="UInt32"/></param>
	/// <returns>The <see cref="UInt32"/></returns>
	[DllImport("kernel32.dll",
		EntryPoint = "AttachConsole",
		SetLastError = true,
		CharSet = CharSet.Auto,
		CallingConvention = CallingConvention.StdCall)]
	private static extern UInt32 AttachConsole(UInt32 dwProcessId);

	/// <summary>
	/// The CreateFileW
	/// </summary>
	/// <param name="lpFileName">The lpFileName<see cref="string"/></param>
	/// <param name="dwDesiredAccess">The dwDesiredAccess<see cref="UInt32"/></param>
	/// <param name="dwShareMode">The dwShareMode<see cref="UInt32"/></param>
	/// <param name="lpSecurityAttributes">The lpSecurityAttributes<see cref="IntPtr"/></param>
	/// <param name="dwCreationDisposition">The dwCreationDisposition<see cref="UInt32"/></param>
	/// <param name="dwFlagsAndAttributes">The dwFlagsAndAttributes<see cref="UInt32"/></param>
	/// <param name="hTemplateFile">The hTemplateFile<see cref="IntPtr"/></param>
	/// <returns>The <see cref="IntPtr"/></returns>
	[DllImport("kernel32.dll",
		EntryPoint = "CreateFileW",
		SetLastError = true,
		CharSet = CharSet.Auto,
		CallingConvention = CallingConvention.StdCall)]
	private static extern IntPtr CreateFileW(
		  string lpFileName,
		  UInt32 dwDesiredAccess,
		  UInt32 dwShareMode,
		  IntPtr lpSecurityAttributes,
		  UInt32 dwCreationDisposition,
		  UInt32 dwFlagsAndAttributes,
		  IntPtr hTemplateFile
		);

	/// <summary>
	/// Defines the GENERIC_WRITE
	/// </summary>
	private const UInt32 GENERIC_WRITE = 0x40000000;

	/// <summary>
	/// Defines the GENERIC_READ
	/// </summary>
	private const UInt32 GENERIC_READ = 0x80000000;

	/// <summary>
	/// Defines the FILE_SHARE_READ
	/// </summary>
	private const UInt32 FILE_SHARE_READ = 0x00000001;

	/// <summary>
	/// Defines the FILE_SHARE_WRITE
	/// </summary>
	private const UInt32 FILE_SHARE_WRITE = 0x00000002;

	/// <summary>
	/// Defines the OPEN_EXISTING
	/// </summary>
	private const UInt32 OPEN_EXISTING = 0x00000003;

	/// <summary>
	/// Defines the FILE_ATTRIBUTE_NORMAL
	/// </summary>
	private const UInt32 FILE_ATTRIBUTE_NORMAL = 0x80;

	/// <summary>
	/// Defines the ERROR_ACCESS_DENIED
	/// </summary>
	private const UInt32 ERROR_ACCESS_DENIED = 5;

	/// <summary>
	/// Defines the ATTACH_PARRENT
	/// </summary>
	private const UInt32 ATTACH_PARRENT = 0xFFFFFFFF;
}
