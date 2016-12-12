using System;
using System.Collections.Generic;

namespace XamGame.Themes
{
	public class Theme
	{
		public int Id { get; set; }
		public String Name { get; set; }
		public String BackgroundImageUrl { get; set; }
		public List<String> TileImageUrls { get; set; }
		public List<String> AdKeywords { get; set; }
		public String BackgroundSoundUrl { get; set; }
		public String ClickSoundUrl{ get; set; }
	}
}