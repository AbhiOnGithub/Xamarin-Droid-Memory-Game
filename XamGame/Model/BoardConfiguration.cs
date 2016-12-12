using System;
using Java.Lang;

namespace XamGame.Models
{
	public class BoardConfiguration
	{
		private static int _6 = 6;
		private static int _12 = 12;
		private static int _18 = 18;
		private static int _28 = 28;
		private static int _32 = 32;
		private static int _50 = 50;

		public int Difficulty { get; set; }
		public int NumTiles { get; set; }
		public int NumTilesInRow { get; set; }
		public int NumRows { get; set; }
		public int Time { get; set; }

		public BoardConfiguration(int difficulty)
		{
			this.Difficulty = difficulty;
			switch (difficulty)
			{
				case 1:
					NumTiles = _6;
					NumTilesInRow = 3;
					NumRows = 2;
					Time = 60;
					break;
				case 2:
					NumTiles = _12;
					NumTilesInRow = 4;
					NumRows = 3;
					Time = 90;
					break;
				case 3:
					NumTiles = _18;
					NumTilesInRow = 6;
					NumRows = 3;
					Time = 120;
					break;
				case 4:
					NumTiles = _28;
					NumTilesInRow = 7;
					NumRows = 4;
					Time = 150;
					break;
				case 5:
					NumTiles = _32;
					NumTilesInRow = 8;
					NumRows = 4;
					Time = 180;
					break;
				case 6:
					NumTiles = _50;
					NumTilesInRow = 10;
					NumRows = 5;
					Time = 210;
					break;
				default:
					throw new IllegalArgumentException("Select one of predefined sizes");
			}
		}
	}
}