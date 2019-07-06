// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

using System;

/// <summary>
/// Defines the <see cref="Command" />
/// </summary>
public class Command {
	/// <summary>
	/// The Execute
	/// </summary>
	public delegate void Execute();

	/// <summary>
	/// Defines the MyExecute
	/// </summary>
	private event Execute MyExecute;

	/// <summary>
	/// Gets the Alias
	/// </summary>
	private string[] Alias { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Command"/> class.
	/// </summary>
	/// <param name="Alias">The Alias<see cref="string[]"/></param>
	/// <param name="execute">The execute<see cref="Execute"/></param>
	public Command(string[] Alias, Execute execute) //TODO:
	{
		this.Alias = Alias ?? throw new ArgumentNullException("Error", nameof(Alias));
		MyExecute = execute ?? throw new ArgumentNullException("Error", nameof(execute));
		for(int i = 0; i <= this.Alias.Length; i++)
			this.Alias[i].ToLower();
	}

	/// <summary>
	/// The Check
	/// </summary>
	/// <param name="cmd">The cmd<see cref="string"/></param>
	public void Check(string cmd) {
	}
}
