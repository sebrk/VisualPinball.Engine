using MessagePack;
using VisualPinball.Engine.VPT.Bumper;
using VisualPinball.Engine.VPT.Collection;
using VisualPinball.Engine.VPT.Decal;
using VisualPinball.Engine.VPT.DispReel;
using VisualPinball.Engine.VPT.Flasher;
using VisualPinball.Engine.VPT.Flipper;
using VisualPinball.Engine.VPT.Gate;
using VisualPinball.Engine.VPT.HitTarget;
using VisualPinball.Engine.VPT.Kicker;
using VisualPinball.Engine.VPT.Light;
using VisualPinball.Engine.VPT.LightSeq;
using VisualPinball.Engine.VPT.Plunger;
using VisualPinball.Engine.VPT.Primitive;
using VisualPinball.Engine.VPT.Ramp;
using VisualPinball.Engine.VPT.Rubber;
using VisualPinball.Engine.VPT.Sound;
using VisualPinball.Engine.VPT.Spinner;
using VisualPinball.Engine.VPT.Surface;
using VisualPinball.Engine.VPT.TextBox;
using VisualPinball.Engine.VPT.Timer;
using VisualPinball.Engine.VPT.Trigger;

namespace VisualPinball.Engine.VPT.Table
{
	[MessagePackObject]

	public class TableBundle
	{
		[Key(0)] public TableData Table;
		[Key(1)] public BumperData[] Bumpers;
		[Key(2)] public CollectionData[] Collections;
		[Key(3)] public DecalData[] Decals;
		[Key(4)] public DispReelData[] DispReels;
		[Key(5)] public FlasherData[] Flashers;
		[Key(6)] public FlipperData[] Flippers;
		[Key(7)] public GateData[] Gates;
		[Key(8)] public HitTargetData[] HitTargets;
		[Key(9)] public KickerData[] Kickers;
		[Key(10)] public LightData[] Lights;
		[Key(11)] public LightSeqData[] LightSeqs;
		[Key(12)] public PlungerData[] Plungers;
		[Key(13)] public PrimitiveData[] Primitives;
		[Key(14)] public RampData[] Ramps;
		[Key(15)] public RubberData[] Rubbers;
		[Key(16)] public SoundData[] Sounds;
		[Key(17)] public SpinnerData[] Spinners;
		[Key(18)] public SurfaceData[] Surfaces;
		[Key(19)] public TextBoxData[] TextBoxes;
		[Key(20)] public TimerData[] Timers;
		[Key(21)] public TextureData[] Textures;
		[Key(22)] public TriggerData[] Triggers;
	}
}
