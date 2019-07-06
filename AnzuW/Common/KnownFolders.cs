using System;
using System.Runtime.InteropServices;

/// <summary>
/// /// Standard folders registered with the system. These folders are installed with Windows Vista
/// and later operating systems, and a computer will have only folders appropriate to it
/// installed.
/// <KnownFolder.Contacts></KnownFolder.Contacts>
/// </summary>
public enum KnownFolder {
	///<summary>Contacts</summary>
	Contacts,
	///<summary>Desktop</summary>
	Desktop,
	///<summary>Documents</summary>
	Documents,
	///<summary>Downloads</summary>
	Downloads,
	///<summary>Favorites</summary>
	Favorites,
	///<summary>Links</summary>
	Links,
	///<summary>Music</summary>
	Music,
	///<summary>Pictures</summary>
	Pictures,
	///<summary>SavedGames</summary>
	SavedGames,
	///<summary>SavedSearches</summary>
	SavedSearches,
	///<summary>Videos</summary>
	Videos
}

/// <summary>
/// Class containing methods to retrieve specific file system paths.
/// </summary>
public static class KnownFolders {
	/// <summary>
	/// Defines the _knownFolderGuids
	/// </summary>
	private static string[] _knownFolderGuids = new string[]
	{
		"{56784854-C6CB-462B-8169-88E350ACB882}", // Contacts
        "{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}", // Desktop
        "{FDD39AD0-238F-46AF-ADB4-6C85480369C7}", // Documents
        "{374DE290-123F-4565-9164-39C4925E467B}", // Downloads
        "{1777F761-68AD-4D8A-87BD-30B759FA33DD}", // Favorites
        "{BFB9D5E0-C6A9-404C-B2B2-AE6DB6AF4968}", // Links
        "{4BD8D571-6D19-48D3-BE97-422220080E43}", // Music
        "{33E28130-4E1E-4676-835A-98395C3BC3BB}", // Pictures
        "{4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4}", // SavedGames
        "{7D1D3A04-DEBB-4115-95CF-2F29DA2920DA}", // SavedSearches
        "{18989B1D-99B5-455B-841C-AB7C74E4DDFC}", // Videos
    };

	/// <summary>
	/// Defines the KnownFolderFlags
	/// </summary>
	[Flags]
	private enum KnownFolderFlags : uint {
		/// <summary>
		/// Defines the SimpleIDList
		/// </summary>
		SimpleIDList = 0x00000100,

		/// <summary>
		/// Defines the NotParentRelative
		/// </summary>
		NotParentRelative = 0x00000200,

		/// <summary>
		/// Defines the DefaultPath
		/// </summary>
		DefaultPath = 0x00000400,

		/// <summary>
		/// Defines the Init
		/// </summary>
		Init = 0x00000800,

		/// <summary>
		/// Defines the NoAlias
		/// </summary>
		NoAlias = 0x00001000,

		/// <summary>
		/// Defines the DontUnexpand
		/// </summary>
		DontUnexpand = 0x00002000,

		/// <summary>
		/// Defines the DontVerify
		/// </summary>
		DontVerify = 0x00004000,

		/// <summary>
		/// Defines the Create
		/// </summary>
		Create = 0x00008000,

		/// <summary>
		/// Defines the NoAppcontainerRedirection
		/// </summary>
		NoAppcontainerRedirection = 0x00010000,

		/// <summary>
		/// Defines the AliasOnly
		/// </summary>
		AliasOnly = 0x80000000
	}

	/// <summary>
	/// Gets the current path to the specified known folder as currently configured. This does
	/// not require the folder to be existent.
	/// </summary>
	/// <param name="knownFolder">The known folder which current path will be returned.</param>
	/// <returns>The default path of the known folder.</returns>
	public static string GetPath(KnownFolder knownFolder) {
		return GetPath(knownFolder, false);
	}

	/// <summary>
	/// Gets the current path to the specified known folder as currently configured. This does
	/// not require the folder to be existent.
	/// </summary>
	/// <param name="knownFolder">The known folder which current path will be returned.</param>
	/// <param name="defaultUser">The defaultUser<see cref="bool"/></param>
	/// <returns>The default path of the known folder.</returns>
	public static string GetPath(KnownFolder knownFolder, bool defaultUser) {
		return GetPath(knownFolder, KnownFolderFlags.DontVerify, defaultUser);
	}

	/// <summary>
	/// The GetPath
	/// </summary>
	/// <param name="knownFolder">The knownFolder<see cref="KnownFolder"/></param>
	/// <param name="flags">The flags<see cref="KnownFolderFlags"/></param>
	/// <param name="defaultUser">The defaultUser<see cref="bool"/></param>
	/// <returns>The <see cref="string"/></returns>
	private static string GetPath(KnownFolder knownFolder, KnownFolderFlags flags,
		bool defaultUser) {
		int result = SHGetKnownFolderPath(new Guid(_knownFolderGuids[(int)knownFolder]),
			(uint)flags, new IntPtr(defaultUser ? -1 : 0), out IntPtr outPath);
		if(result >= 0) {
			string path = Marshal.PtrToStringUni(outPath);
			Marshal.FreeCoTaskMem(outPath);
			return path;
		}
		else {
			throw new ExternalException("Unable to retrieve the known folder path. It may not "
				+ "be available on this system.", result);
		}
	}

	/// <summary>
	/// The SHGetKnownFolderPath
	/// </summary>
	/// <param name="rfid">The rfid<see cref="Guid"/></param>
	/// <param name="dwFlags">The dwFlags<see cref="uint"/></param>
	/// <param name="hToken">The hToken<see cref="IntPtr"/></param>
	/// <param name="ppszPath">The ppszPath<see cref="IntPtr"/></param>
	/// <returns>The <see cref="int"/></returns>
	[DllImport("Shell32.dll")]
	private static extern int SHGetKnownFolderPath(
		[MarshalAs(UnmanagedType.LPStruct)]Guid rfid, uint dwFlags, IntPtr hToken,
		out IntPtr ppszPath);
}
