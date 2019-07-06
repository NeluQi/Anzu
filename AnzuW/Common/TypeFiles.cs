// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

using System.IO;
using System.Linq;

/// <summary>
/// Defines the <see cref="TypeFiles" />
/// </summary>
internal class TypeFiles {
	/// <summary>
	/// Defines the CAD
	/// </summary>
	private static readonly string[] CAD = { ".dwg", ".dxf", ".dgn", ".stl", ".3dm", ".3ds", ".ma", ".mod", ".obj", ".igs" };

	/// <summary>
	/// Defines the Archives
	/// </summary>
	private static readonly string[] Archives = { ".7z", ".zip", ".jar", ".rar", ".gz", ".ace", ".cab", ".cbr", ".deb", ".gzip", ".tar-gz", ".tgz", ".zipx", ".bz2" };

	/// <summary>
	/// Defines the Audio
	/// </summary>
	private static readonly string[] Audio = {".m4a", ".aif", ".aiff", ".aifc", ".aif", ".mov", ".moov", ".qt", ".alaw", ".caf", ".gsm", ".wave", ".wav",
	".mpa", ".mp2v", ".mp2", ".mp3",".mpeg",".mpg",".midi",".mid",".kar",".rmi",".wma",".asf", ".mid"};

	/// <summary>
	/// Defines the VectorGraphics
	/// </summary>
	private static readonly string[] VectorGraphics = { ".ai", ".cdd", ".cdr", ".dem", ".eps", ".max", ".ps", ".svg", ".vsd", ".wmf" };

	/// <summary>
	/// Defines the Video
	/// </summary>
	private static readonly string[] Video = { ".asf", ".avi", ".mp4", ".m4v", ".mov", ".mpg", ".mpeg", ".swf", ".wmv", ".avi", ".3g2", ".webm" };

	/// <summary>
	/// Defines the GeoinformationSystems
	/// </summary>
	private static readonly string[] GeoinformationSystems = { ".dem", ".gpx", ".kml", ".kmz", ".loc", ".mid", ".ov2", ".pps" };

	/// <summary>
	/// Defines the Graphics
	/// </summary>
	private static readonly string[] Graphics = { ".cr2", ".cur", ".icns", ".pict", ".tex" };

	/// <summary>
	/// Defines the Documents
	/// </summary>
	private static readonly string[] Documents = {".doc", ".docx", ".odt", ".pdf", ".dotx", ".epub", ".fb2", ".ibooks", ".key", ".ppsm", ".pptx", ".rtf",
	".wpd", ".wps",".xlr", ".xlsb",".xslx", ".xltx", };

	/// <summary>
	/// Defines the Encrypted
	/// </summary>
	private static readonly string[] Encrypted = { ".crypt", ".crypt5", ".crypt6", ".crypt7", ".crypt8", ".crypt12", ".hqx", ".keychain", ".kwm", ".mim",
	".pub",".uue"};

	/// <summary>
	/// Defines the Web
	/// </summary>
	private static readonly string[] Web = { ".asp", ".aspx", ".cer", ".cfm", ".chm", ".crdownload", ".csr", ".css", ".eml", ".flv", ".htm", ".html", ".jnlp",
	".js", ".jsp", ".magnet", ".mht", ".mhtm", ".msg", ".php", ".rss", ".torrent", ".vcf", ".url", ".webarchive", ".webloc",".xhtml", ".xul" };

	/// <summary>
	/// Defines the Executable
	/// </summary>
	private static readonly string[] Executable = { ".apk", ".bat", ".cgi", ".cmd", ".com", ".hta", ".msi", ".paf.exe", ".pif", ".ps1", ".scr", ".vb", ".vbe", ".vbs", ".wsf", ".exe" };

	/// <summary>
	/// Defines the Configuration
	/// </summary>
	private static readonly string[] Configuration = {".application", ".appref-ms", ".cfg", ".conf", ".config", ".cpl", ".cue", ".deskthemepack", ".inf", ".ini", ".pkg",
	".prf", ".themepack", ".thm", };

	/// <summary>
	/// Defines the DiskImages
	/// </summary>
	private static readonly string[] DiskImages = { ".ccd", ".cdi", ".dmg", ".i00", ".iso", ".isz", ".mdf", ".mds", ".nrg", ".rom", ".sub", ".tib", ".toast", ".vc4", ".vcd" };

	/// <summary>
	/// Defines the ModulesAndPlugins
	/// </summary>
	private static readonly string[] ModulesAndPlugins = { ".8bi", ".ape", ".crx", ".plugin", ".xll", ".xpi", };

	/// <summary>
	/// Defines the RasterGraphics
	/// </summary>
	private static readonly string[] RasterGraphics = { ".apt", ".bmp", ".dds", ".dng", ".iff", ".msp", ".pot", ".psd", ".pspimage", ".tga", ".thm", ".tif", ".tiff", ".xcf",
	".yuv", };

	/// <summary>
	/// Defines the Pictures
	/// </summary>
	private static readonly string[] Pictures = { ".jpg", ".jpeg", ".gif", ".png" };

	/// <summary>
	/// Defines the Scripts
	/// </summary>
	private static readonly string[] Scripts = { ".cs", ".c", ".class", ".cpp", ".dtd", ".fla", ".h", ".java", ".lua", ".sh", ".sln", ".sql", ".vcproj", ".vcxproj", ".wsc",
	".xcodeproj", ".xsd"};

	/// <summary>
	/// Defines the Text
	/// </summary>
	private static readonly string[] Text = { ".err", ".log", ".pwi", ".tex", ".text", ".txt", ".csv" };

	/// <summary>
	/// Defines the Database
	/// </summary>
	private static readonly string[] Database = { ".accdb", ".b2", ".dat", ".db", ".dbf", ".dbx", ".kdc", ".mdb", ".sdf", ".sis" };

	/// <summary>
	/// Defines the Backup
	/// </summary>
	private static readonly string[] Backup = { ".awb", ".bak", ".dmp", ".gho", ".ghs", ".sav", ".v2i" };

	/// <summary>
	/// Defines the Fonts
	/// </summary>
	private static readonly string[] Fonts = { ".fnt", ".fon", ".otf", ".ttf" };

	/// <summary>
	/// Defines the Type
	/// </summary>
	public enum Type {
		/// <summary>
		/// Defines the CAD
		/// </summary>
		CAD,

		/// <summary>
		/// Defines the Archives
		/// </summary>
		Archives,

		/// <summary>
		/// Defines the Audio
		/// </summary>
		Audio,

		/// <summary>
		/// Defines the VectorGraphics
		/// </summary>
		VectorGraphics,

		/// <summary>
		/// Defines the Video
		/// </summary>
		Video,

		/// <summary>
		/// Defines the GeoinformationSystems
		/// </summary>
		GeoinformationSystems,

		/// <summary>
		/// Defines the Graphics
		/// </summary>
		Graphics,

		/// <summary>
		/// Defines the Documents
		/// </summary>
		Documents,

		/// <summary>
		/// Defines the Encrypted
		/// </summary>
		Encrypted,

		/// <summary>
		/// Defines the Executable
		/// </summary>
		Executable,

		/// <summary>
		/// Defines the Configuration
		/// </summary>
		Configuration,

		/// <summary>
		/// Defines the DiskImages
		/// </summary>
		DiskImages,

		/// <summary>
		/// Defines the ModulesAndPlugins
		/// </summary>
		ModulesAndPlugins,

		/// <summary>
		/// Defines the RasterGraphics
		/// </summary>
		RasterGraphics,

		/// <summary>
		/// Defines the Pictures
		/// </summary>
		Pictures,

		/// <summary>
		/// Defines the Scripts
		/// </summary>
		Scripts,

		/// <summary>
		/// Defines the Text
		/// </summary>
		Text,

		/// <summary>
		/// Defines the Database
		/// </summary>
		Database,

		/// <summary>
		/// Defines the Backup
		/// </summary>
		Backup,

		/// <summary>
		/// Defines the Fonts
		/// </summary>
		Fonts,

		/// <summary>
		/// Defines the Web
		/// </summary>
		Web,

		/// <summary>
		/// Defines the Other
		/// </summary>
		Other
	}

	/// <summary>
	/// The GetTypePath
	/// </summary>
	/// <param name="file">The file<see cref="FileInfo"/></param>
	/// <returns>The <see cref="string"/></returns>
	public static string GetTypePath(FileInfo file) {
		string ex = file.Extension;

		if(CAD.Contains(ex))
			return "/" + nameof(CAD) + "/";

		if(Archives.Contains(ex))
			return "/" + nameof(Archives) + "/";

		if(Audio.Contains(ex))
			return "/" + nameof(Audio) + "/";

		if(VectorGraphics.Contains(ex))
			return "/" + nameof(VectorGraphics) + "/";

		if(Video.Contains(ex))
			return "/" + nameof(Video) + "/";

		if(GeoinformationSystems.Contains(ex))
			return "/" + nameof(GeoinformationSystems) + "/";

		if(Graphics.Contains(ex))
			return "/" + nameof(Graphics) + "/";

		if(Documents.Contains(ex))
			return "/" + nameof(Documents) + "/";

		if(Encrypted.Contains(ex))
			return "/" + nameof(Encrypted) + "/";

		if(Web.Contains(ex))
			return "/" + nameof(Web) + "/";

		if(Fonts.Contains(ex))
			return "/" + nameof(Fonts) + "/";

		if(Backup.Contains(ex))
			return "/" + nameof(Backup) + "/";

		if(Database.Contains(ex))
			return "/" + nameof(Database) + "/";

		if(Text.Contains(ex))
			return "/" + nameof(Text) + "/";

		if(Scripts.Contains(ex))
			return "/" + nameof(Scripts) + "/";

		if(Pictures.Contains(ex))
			return "/" + nameof(Pictures) + "/";

		if(RasterGraphics.Contains(ex))
			return "/" + nameof(RasterGraphics) + "/";

		if(ModulesAndPlugins.Contains(ex))
			return "/" + nameof(ModulesAndPlugins) + "/";

		if(DiskImages.Contains(ex))
			return "/" + nameof(DiskImages) + "/";

		if(Configuration.Contains(ex))
			return "/" + nameof(Configuration) + "/";

		if(Executable.Contains(ex))
			return "/" + nameof(Executable) + "/";

		return "/Other/";
	}
}
