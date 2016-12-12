using System;
using XamGame.Themes;

namespace XamGame.Models
{
	public class Game
	{
		/// <summary>
		/// The board configuration
		/// </summary>
		/// <value>The board configuration.</value>
		public BoardConfiguration BoardConfiguration { get; set; }

		/// <summary>
		/// The board arrangment
		/// </summary>
		/// <value>The board arrangment.</value>
		public BoardArrangment BoardArrangment { get; set; }

		/// <summary>
		/// The selected theme
		/// </summary>
		/// <value>The theme.</value>
		public Theme Theme { get; set; }

		/// <summary>
		/// Gets or sets the state of the game.
		/// </summary>
		/// <value>The state of the game.</value>
		public GameState GameState { get; set; }

	}
}