// Visual Pinball Engine
// Copyright (C) 2020 freezy and VPE Team
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.

#region ReSharper
// ReSharper disable UnassignedField.Global
// ReSharper disable StringLiteralTypo
// ReSharper disable FieldCanBeMadeReadOnly.Global
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MessagePack;
using VisualPinball.Engine.IO;
using VisualPinball.Engine.Math;

namespace VisualPinball.Engine.VPT.Table
{
	[Serializable]
	[MessagePackObject]
	public class TableData : ItemData
	{
		public override string GetName() => Name;
		public override void SetName(string name) { Name = name; }

		[Key(112)]
		[BiffString("NAME", IsWideString = true, Pos = 112)]
		public string Name;

		[Key(1)]
		[BiffFloat("LEFT", Pos = 1)]
		public float Left;

		[Key(3)]
		[BiffFloat("RGHT", Pos = 3)]
		public float Right = 952f;

		[Key(2)]
		[BiffFloat("TOPX", Pos = 2)]
		public float Top;

		[Key(4)]
		[BiffFloat("BOTM", Pos = 4)]
		public float Bottom = 2162f;

		[Key(15)]
		[BiffBool("EFSS", Pos = 15)]
		public bool BgEnableFss;

		[Key(36)]
		[BiffInt("ORRP", Pos = 36)]
		public int OverridePhysics;

		[Key(37)]
		[BiffBool("ORPF", Pos = 37)]
		public bool OverridePhysicsFlipper;

		[Key(38)]
		[BiffFloat("GAVT", Pos = 38)]
		public float Gravity = 1.762985f;

		[Key(39)]
		[BiffFloat("FRCT", Pos = 39)]
		public float Friction = 0.075f;

		[Key(40)]
		[BiffFloat("ELAS", Pos = 40)]
		public float Elasticity = 0.25f;

		[Key(41)]
		[BiffFloat("ELFA", Pos = 41)]
		public float ElasticityFalloff;

		[Key(42)]
		[BiffFloat("PFSC", Pos = 42)]
		public float Scatter;

		[Key(43)]
		[BiffFloat("SCAT", Pos = 43)]
		public float DefaultScatter;

		[Key(44)]
		[BiffFloat("NDGT", Pos = 44)]
		public float NudgeTime = 5f;

		[Key(45)]
		[BiffInt("MPGC", Pos = 45)]
		public int PlungerNormalize = 100;

		[Key(46)]
		[BiffBool("MPDF", Pos = 46)]
		public bool PlungerFilter;

		[Key(47)]
		[BiffInt("PHML", Pos = 47)]
		public int PhysicsMaxLoops;

		[Key(49)]
		[BiffBool("DECL", Pos = 49)]
		public bool RenderDecals;

		[Key(48)]
		[BiffBool("REEL", Pos = 48)]
		public bool RenderEmReels;

		[Key(50)]
		[BiffFloat("OFFX", Index = 0, Pos = 50)]
		[BiffFloat("OFFY", Index = 1, Pos = 51)]
		public float[] Offset = { 476f, 1081f };

		[Key(52)]
		[BiffFloat("ZOOM", Pos = 52)]
		public float Zoom = 0.5f;

		[Key(55)]
		[BiffFloat("MAXS", Pos = 55)]
		public float StereoMaxSeparation = 0.015f;

		[Key(56)]
		[BiffFloat("ZPD", Pos = 56)]
		public float StereoZeroParallaxDisplacement = 0.1f;

		[Key(57)]
		[BiffFloat("STO", Pos = 57)]
		public float StereoOffset;

		[Key(58)]
		[BiffBool("OGST", Pos = 58)]
		public bool OverwriteGlobalStereo3D;

		[Key(53)]
		[BiffFloat("SLPX", Pos = 53)]
		public float AngleTiltMax = 6f;

		[Key(54)]
		[BiffFloat("SLOP", Pos = 54)]
		public float AngleTiltMin = 6f;

		[Key(70)]
		[BiffFloat("GLAS", Pos = 70)]
		public float GlassHeight = 400f;

		[Key(71)]
		[BiffFloat("TBLH", Pos = 71)]
		public float TableHeight = 0f;

		[Key(59)]
		[BiffString("IMAG", Pos = 59)]
		public string Image;

		[Key(65)]
		[BiffString("BLIM", Pos = 65)]
		public string BallImage;

		[Key(66)]
		[BiffString("BLIF", Pos = 66)]
		public string BallImageFront;

		[Key(68)]
		[BiffString("SSHT", Pos = 68)]
		public string ScreenShot;

		[Key(69)]
		[BiffBool("FBCK", Pos = 69)]
		public bool DisplayBackdrop;

		[Key(107)]
		[BiffInt("SEDT", Pos = 107)]
		public int NumGameItems;

		[Key(108)]
		[BiffInt("SSND", Pos = 108)]
		public int NumSounds;

		[Key(109)]
		[BiffInt("SIMG", Pos = 109)]
		public int NumTextures;

		[Key(110)]
		[BiffInt("SFNT", Pos = 110)]
		public int NumFonts;

		[Key(111)]
		[BiffInt("SCOL", Pos = 111)]
		public int NumCollections;

		[Key(63)]
		[BiffBool("BIMN", Pos = 63)]
		public bool ImageBackdropNightDay;

		[Key(64)]
		[BiffString("IMCG", Pos = 64)]
		public string ImageColorGrade;

		[Key(67)]
		[BiffString("EIMG", Pos = 67)]
		public string EnvImage;

		[MaterialReference]
		[Key(72)]
		[BiffString("PLMA", Pos = 72)]
		public string PlayfieldMaterial;

		[Key(75)]
		[BiffColor("LZAM", Pos = 75)]
		public Color LightAmbient = new Color(0, 0, 0, 255);

		[IgnoreMember]
		[BiffInt("LZDI", Pos = 76)]
		public int Light0Emission {
			set => Light[0].Emission = new Color(value, ColorFormat.Bgr);
			get => Light[0].Emission.Bgr;
		}
		[Key(76)]
		public LightSource[] Light = { new LightSource { Emission = new Color(255, 255, 240, 255), Pos = Vertex3D.Zero } };

		[Key(77)]
		[BiffFloat("LZHI", Pos = 77)]
		public float LightHeight = 5000f;

		[Key(78)]
		[BiffFloat("LZRA", Pos = 78)]
		public float LightRange = 4000000f;

		[Key(79)]
		[BiffFloat("LIES", Pos = 79)]
		public float LightEmissionScale = 4000000f;

		[Key(80)]
		[BiffFloat("ENES", Pos = 80)]
		public float EnvEmissionScale = 2f;

		[Key(81)]
		[BiffFloat("GLES", Pos = 81)]
		public float GlobalEmissionScale = 0.52f;

		[Key(82)]
		[BiffFloat("AOSC", Pos = 82)]
		public float AoScale = 1.75f;

		[Key(83)]
		[BiffFloat("SSSC", Pos = 83)]
		public float SsrScale = 1f;

		[Key(87)]
		[BiffInt("BREF", Pos = 87)]
		public int UseReflectionForBalls = -1;

		[Key(88)]
		[BiffFloat("PLST", QuantizedUnsignedBits = 8, Pos = 88)]
		public float PlayfieldReflectionStrength = 0.2941177f;

		[Key(89)]
		[BiffInt("BTRA", Pos = 89)]
		public int UseTrailForBalls = -1;

		[Key(93)]
		[BiffFloat("BTST", QuantizedUnsignedBits = 8, Pos = 93)]
		public float BallTrailStrength = 0.4901961f;

		[Key(91)]
		[BiffFloat("BPRS", Pos = 91)]
		public float BallPlayfieldReflectionStrength = 1f;

		[Key(92)]
		[BiffFloat("DBIS", Pos = 92)]
		public float DefaultBulbIntensityScaleOnBall = 1f;

		[Key(99)]
		[BiffInt("UAAL", Pos = 99)]
		public int UseAA = -1;

		[Key(101)]
		[BiffInt("UAOC", Pos = 101)]
		public int UseAO = -1;

		[Key(102)]
		[BiffInt("USSR", Pos = 102)]
		public int UseSSR = -1;

		[Key(100)]
		[BiffInt("UFXA", Pos = 100)]
		public int UseFXAA = -1;

		[Key(103)]
		[BiffFloat("BLST", Pos = 103)]
		public float BloomStrength = 1.8f;

		[Key(73)]
		[BiffColor("BCLR", ColorFormat = ColorFormat.Bgr, Pos = 73)]
		public Color ColorBackdrop = new Color(35, 35, 35, 255);

		[Key(113)]
		[BiffColor("CCUS", Count = 16, Pos = 113)]
		public Color[] CustomColors = new Color[16];

		[Key(74)]
		[BiffFloat("TDFT", Pos = 74)]
		public float GlobalDifficulty = 0.2f;

		[Key(84)]
		[BiffFloat("SVOL", Pos = 84)]
		public float TableSoundVolume = 1f;

		[Key(90)]
		[BiffBool("BDMO", Pos = 90)]
		public bool BallDecalMode;

		[Key(85)]
		[BiffFloat("MVOL", Pos = 85)]
		public float TableMusicVolume = 1f;

		[Key(86)]
		[BiffInt("AVSY", Pos = 86)]
		public int TableAdaptiveVSync = -1;

		[Key(95)]
		[BiffBool("OGAC", Pos = 95)]
		public bool OverwriteGlobalDetailLevel;

		[Key(96)]
		[BiffBool("OGDN", Pos = 96)]
		public bool OverwriteGlobalDayNight;

		[Key(97)]
		[BiffBool("GDAC", Pos = 97)]
		public bool ShowGrid = true;

		[Key(98)]
		[BiffBool("REOP", Pos = 98)]
		public bool ReflectElementsOnPlayfield = true;

		[Key(94)]
		[BiffInt("ARAC", Pos = 94)]
		public int UserDetailLevel = 5;

		[Key(104)]
		[BiffInt("MASI", Pos = 104)]
		public int NumMaterials;

		[Key(114)]
		[BiffString("CODE", LengthAfterTag = true, Pos = 114)]
		public string Code;

		[Key(5)]
		[BiffFloat("ROTA", Index = BackglassIndex.Desktop, Pos = 5)]
		[BiffFloat("ROTF", Index = BackglassIndex.Fullscreen, Pos = 16)]
		[BiffFloat("ROFS", Index = BackglassIndex.FullSingleScreen, Pos = 26)]
		public float[] BgRotation = { 0, 270f, 0 };

		[Key(7)]
		[BiffFloat("LAYB", Index = BackglassIndex.Desktop, Pos = 7)]
		[BiffFloat("LAYF", Index = BackglassIndex.Fullscreen, Pos = 18)]
		[BiffFloat("LAFS", Index = BackglassIndex.FullSingleScreen, Pos = 28)]
		public float[] BgLayback = { 0, 36f, 0 };

		[Key(6)]
		[BiffFloat("INCL", Index = BackglassIndex.Desktop, Pos = 6)]
		[BiffFloat("INCF", Index = BackglassIndex.Fullscreen, Pos = 17)]
		[BiffFloat("INFS", Index = BackglassIndex.FullSingleScreen, Pos = 27)]
		public float[] BgInclination = { 45f, 15f, 52f };

		[Key(8)]
		[BiffFloat("FOVX", Index = BackglassIndex.Desktop, Pos = 8)]
		[BiffFloat("FOVF", Index = BackglassIndex.Fullscreen, Pos = 19)]
		[BiffFloat("FOFS", Index = BackglassIndex.FullSingleScreen, Pos = 29)]
		public float[] BgFov = { 45f, 17f, 45f };

		[Key(12)]
		[BiffFloat("SCLX", Index = BackglassIndex.Desktop, Pos = 12)]
		[BiffFloat("SCFX", Index = BackglassIndex.Fullscreen, Pos = 23)]
		[BiffFloat("SCXS", Index = BackglassIndex.FullSingleScreen, Pos = 33)]
		public float[] BgScaleX = { 1f, 1.3f, 1.2f };

		[Key(13)]
		[BiffFloat("SCLY", Index = BackglassIndex.Desktop, Pos = 13)]
		[BiffFloat("SCFY", Index = BackglassIndex.Fullscreen, Pos = 24)]
		[BiffFloat("SCYS", Index = BackglassIndex.FullSingleScreen, Pos = 34)]
		public float[] BgScaleY = { 1f, 1.41f, 1.1f };

		[Key(14)]
		[BiffFloat("SCLZ", Index = BackglassIndex.Desktop, Pos = 14)]
		[BiffFloat("SCFZ", Index = BackglassIndex.Fullscreen, Pos = 25)]
		[BiffFloat("SCZS", Index = BackglassIndex.FullSingleScreen, Pos = 35)]
		public float[] BgScaleZ = { 1f, 1f, 1f };

		[Key(9)]
		[BiffFloat("XLTX", Index = BackglassIndex.Desktop, Pos = 9)]
		[BiffFloat("XLFX", Index = BackglassIndex.Fullscreen, Pos = 20)]
		[BiffFloat("XLXS", Index = BackglassIndex.FullSingleScreen, Pos = 30)]
		public float[] BgOffsetX = { 0, 110f, 0 };

		[Key(10)]
		[BiffFloat("XLTY", Index = BackglassIndex.Desktop, Pos = 10)]
		[BiffFloat("XLFY", Index = BackglassIndex.Fullscreen, Pos = 21)]
		[BiffFloat("XLYS", Index = BackglassIndex.FullSingleScreen, Pos = 31)]
		public float[] BgOffsetY = { 30f, -86f, 30f };

		[Key(11)]
		[BiffFloat("XLTZ", Index = BackglassIndex.Desktop, Pos = 11)]
		[BiffFloat("XLFZ", Index = BackglassIndex.Fullscreen, Pos = 22)]
		[BiffFloat("XLZS", Index = BackglassIndex.FullSingleScreen, Pos = 32)]
		public float[] BgOffsetZ = { -200f, 400f, -50f };

		[Key(60)]
		[BiffString("BIMG", Index = BackglassIndex.Desktop, Pos = 60)]
		[BiffString("BIMF", Index = BackglassIndex.Fullscreen, Pos = 61)]
		[BiffString("BIMS", Index = BackglassIndex.FullSingleScreen, Pos = 62)]
		public string[] BgImage = new string[3];

		[Key(105)]
		[BiffMaterials("MATE", Pos = 105)]
		[BiffMaterials("PHMA", IsPhysics = true, Pos = 106)]
		public Material[] Materials;

		// other stuff
		[IgnoreMember] public int BgCurrentSet = BackglassIndex.Desktop;

		[IgnoreMember] public const float OverrideContactFriction = 0.075f;
		[IgnoreMember] public const float OverrideElasticity = 0.25f;
		[IgnoreMember] public const float OverrideElasticityFalloff = 0f;
		[IgnoreMember] public const float OverrideScatterAngle = 0f;

		public float GetFriction() => OverridePhysics != 0 ? OverrideContactFriction : Friction;
		public float GetElasticity() => OverridePhysics != 0 ? OverrideElasticity : Elasticity;
		public float GetElasticityFalloff() => OverridePhysics != 0 ? OverrideElasticityFalloff : ElasticityFalloff;
		public float GetScatter() => OverridePhysics != 0 ? OverrideScatterAngle : Scatter;

		protected override bool SkipWrite(BiffAttribute attr)
		{
			switch (attr.Name) {
				case "LOCK":
				case "LAYR":
				case "LANR":
				case "LVIS":
					return true;
			}
			return false;
		}

		#region BIFF

		static TableData()
		{
			Init(typeof(TableData), Attributes);
		}

		public TableData() : base("GameData")
		{
		}

		public TableData(BinaryReader reader) : this()
		{
			Load(this, reader, Attributes);
		}

		public override void Write(BinaryWriter writer, HashWriter hashWriter)
		{
			WriteRecord(writer, Attributes, hashWriter);
			WriteEnd(writer, hashWriter);
		}

		private static readonly Dictionary<string, List<BiffAttribute>> Attributes = new Dictionary<string, List<BiffAttribute>>();

		#endregion
	}

	/// <summary>
	/// Parses material data.<p/>
	///
	/// Since we additionally need <see cref="TableData.NumMaterials"/> in
	/// order to know how many materials to parse, we can't use the standard
	/// BiffAttribute.
	/// </summary>
	public class BiffMaterialsAttribute : BiffAttribute
	{
		public bool IsPhysics;

		public BiffMaterialsAttribute(string name) : base(name) { }

		public override void Parse<T>(T obj, BinaryReader reader, int len)
		{
			if (obj is TableData tableData) {
				if (IsPhysics) {
					ParsePhysicsMaterial(tableData, reader, len);
				} else {
					ParseMaterial(tableData, reader, len);
				}
			} else {
				throw new InvalidOperationException();
			}
		}

		public override void Write<TItem>(TItem obj, BinaryWriter writer, HashWriter hashWriter)
		{
			if (!(GetValue(obj) is Material[] materials)) {
				return;
			}
			using (var stream = new MemoryStream())
			using (var dataWriter = new BinaryWriter(stream)) {
				foreach (var material in materials) {
					if (IsPhysics) {
						material.PhysicsMaterialData.Write(dataWriter);
					} else {
						material.UpdateData();
						material.MaterialData.Write(dataWriter);
					}
				}

				var data = stream.ToArray();
				WriteStart(writer, data.Length, hashWriter);
				writer.Write(data);
				hashWriter?.Write(data);
			}
		}

		private void ParseMaterial(TableData tableData, BinaryReader reader, int len)
		{
			if (len < tableData.NumMaterials * MaterialData.Size) {
				throw new ArgumentOutOfRangeException($"Cannot parse {tableData.NumMaterials} of {tableData.NumMaterials * MaterialData.Size} bytes from a {len} bytes buffer.");
			}
			var materials = new Material[tableData.NumMaterials];
			for (var i = 0; i < tableData.NumMaterials; i++) {
				materials[i] = new Material(reader);
			}
			SetValue(tableData, materials);
		}

		private void ParsePhysicsMaterial(TableData tableData, BinaryReader reader, int len)
		{
			if (len < tableData.NumMaterials * PhysicsMaterialData.Size) {
				throw new ArgumentOutOfRangeException($"Cannot parse {tableData.NumMaterials} physics materials of {tableData.NumMaterials * PhysicsMaterialData.Size} bytes from a {len} bytes buffer.");
			}

			if (!(GetValue(tableData) is Material[] materials)) {
				throw new ArgumentException("Materials must be loaded before physics properties!");
			}
			for (var i = 0; i < tableData.NumMaterials; i++) {
				var savePhysMat = new PhysicsMaterialData(reader);
				var material = materials.First(m => m.Name == savePhysMat.Name);
				if (material == null) {
					throw new Exception($"Cannot find material \"{savePhysMat.Name}\" in [{ string.Join(", ", tableData.Materials.Select(m => m.Name))} ] for updating physics.");
				}
				material.UpdatePhysics(savePhysMat);
			}
		}
	}

	[Serializable]
	[MessagePackObject]
	public class LightSource {
		[Key(0)] public Color Emission;
		[Key(1)] public Vertex3D Pos;
	}
}
