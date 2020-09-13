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
// ReSharper disable ConvertToConstant.Global
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using MessagePack;
using VisualPinball.Engine.IO;
using VisualPinball.Engine.Math;
using VisualPinball.Engine.VPT.Table;

namespace VisualPinball.Engine.VPT.Light
{
	[Serializable]
	[BiffIgnore("PNTS")]
	[MessagePackObject]
	public class LightData : ItemData
	{
		public override string GetName() => Name;
		public override void SetName(string name) { Name = name; }

		[Key(15)]
		[BiffString("NAME", IsWideString = true, Pos = 15)]
		public string Name;

		[Key(1)]
		[BiffVertex("VCEN", Pos = 1)]
		public Vertex2D Center;

		[Key(2)]
		[BiffFloat("RADI", Pos = 2)]
		public float Falloff = 50f;

		[Key(3)]
		[BiffFloat("FAPO", Pos = 3)]
		public float FalloffPower = 2f;

		[Key(4)]
		[BiffInt("STAT", Pos = 4)]
		public int State = LightStatus.LightStateOff;

		[Key(5)]
		[BiffColor("COLR", Pos = 5)]
		public Color Color = new Color(0xffff00, ColorFormat.Argb);

		[Key(6)]
		[BiffColor("COL2", Pos = 6)]
		public Color Color2 = new Color(0xffffff, ColorFormat.Argb);

		[Key(10)]
		[BiffString("IMG1", Pos = 10)]
		public string OffImage = string.Empty;

		[Key(100)]
		[BiffBool("SHAP", SkipWrite = true)]
		public bool IsRoundLight = false;

		[Key(9)]
		[BiffString("BPAT", Pos = 9)]
		public string BlinkPattern = "10";

		[Key(11)]
		[BiffInt("BINT", Pos = 11)]
		public int BlinkInterval = 125;

		[Key(12)]
		[BiffFloat("BWTH", Pos = 12)]
		public float Intensity = 1f;

		[Key(13)]
		[BiffFloat("TRMS", Pos = 13)]
		public float TransmissionScale = 0.5f;

		[Key(14)]
		[BiffString("SURF", Pos = 14)]
		public string Surface = string.Empty;

		[Key(16)]
		[BiffBool("BGLS", Pos = 16)]
		public bool IsBackglass = false;

		[Key(17)]
		[BiffFloat("LIDB", Pos = 17)]
		public float DepthBias;

		[Key(18)]
		[BiffFloat("FASP", Pos = 18)]
		public float FadeSpeedUp = 0.2f;

		[Key(19)]
		[BiffFloat("FASD", Pos = 19)]
		public float FadeSpeedDown = 0.2f;

		[Key(20)]
		[BiffBool("BULT", Pos = 20)]
		public bool IsBulbLight = false;

		[Key(21)]
		[BiffBool("IMMO", Pos = 21)]
		public bool IsImageMode = false;

		[Key(22)]
		[BiffBool("SHBM", Pos = 22)]
		public bool ShowBulbMesh = false;

		[Key(27)]
		[BiffBool("STBM", Pos = 22)]
		public bool HasStaticBulbMesh = true;

		[Key(23)]
		[BiffBool("SHRB", Pos = 23)]
		public bool ShowReflectionOnBall = true;

		[Key(24)]
		[BiffFloat("BMSC", Pos = 24)]
		public float MeshRadius = 20f;

		[Key(25)]
		[BiffFloat("BMVA", Pos = 25)]
		public float BulbModulateVsAdd = 0.9f;

		[Key(26)]
		[BiffFloat("BHHI", Pos = 26)]
		public float BulbHaloHeight = 28f;

		[Key(2000)]
		[BiffDragPoint("DPNT", TagAll = true, Pos = 2000)]
		public DragPointData[] DragPoints;

		[Key(7)]
		[BiffBool("TMON", Pos = 7)]
		public bool IsTimerEnabled;

		[Key(8)]
		[BiffInt("TMIN", Pos = 8)]
		public int TimerInterval;

		#region BIFF

		static LightData()
		{
			Init(typeof(LightData), Attributes);
		}

		[SerializationConstructor]
		public LightData() : base(StoragePrefix.GameItem)
		{
		}

		public LightData(BinaryReader reader, string storageName) : base(storageName)
		{
			Load(this, reader, Attributes);
		}

		public LightData(string name, float x, float y) : base(StoragePrefix.GameItem)
		{
			Name = name;
			Center = new Vertex2D(x, y);
		}

		public override void Write(BinaryWriter writer, HashWriter hashWriter)
		{
			writer.Write((int)ItemType.Light);
			WriteRecord(writer, Attributes, hashWriter);
			WriteEnd(writer, hashWriter);
		}

		private static readonly Dictionary<string, List<BiffAttribute>> Attributes = new Dictionary<string, List<BiffAttribute>>();

		#endregion
	}
}
