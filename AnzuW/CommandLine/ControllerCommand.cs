// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

using System;

/// <summary>
/// TODO:
/// </summary>
internal class ControllerCommand {
	/// <summary>
	/// Gets the Commands
	/// </summary>
	public Command[] Commands { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ControllerCommand"/> class.
	/// </summary>
	/// <param name="args">The args<see cref="string[]"/></param>
	public ControllerCommand(string[] args) {
		Commands = new Command[] {
			new Command(new string[]{"--consolemode", "-con"}, ConsoleMode)
		};
	}

	//TODO: Parse com line
	/// <summary>
	/// The ConsoleMode
	/// </summary>
	public void ConsoleMode() {
		ConsoleHelper.Initialize();
		Console.WriteLine("Run console mode");
	}
}
