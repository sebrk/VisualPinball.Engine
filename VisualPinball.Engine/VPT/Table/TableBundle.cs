using MessagePack;
using VisualPinball.Engine.VPT.Bumper;

namespace VisualPinball.Engine.VPT.Table
{
	[MessagePackObject]
	public class TableBundle
	{
		[Key(0)]
		public TableData Table;

		[Key(1)]
		public BumperData[] Bumpers;
	}
}
