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

namespace VisualPinball.Engine.VPT.Gate
{
	[Serializable]
	[MessagePackObject]
	public class GateData : ItemData
	{
		public override string GetName() => Name;
		public override void SetName(string name) { Name = name; }

		[Key(18)]
		[BiffString("NAME", IsWideString = true, Pos = 18)]
		public string Name;

		[Key(12)]
		[BiffFloat("GAMA", Pos = 12)]
		public float AngleMax = MathF.PI / 2.0f;

		[Key(13)]
		[BiffFloat("GAMI", Pos = 13)]
		public float AngleMin = 0f;

		[Key(15)]
		[BiffFloat("AFRC", Pos = 15)]
		public float Damping = 0.985f;

		[Key(11)]
		[BiffFloat("ELAS", Pos = 11)]
		public float Elasticity = 0.3f;

		[Key(14)]
		[BiffFloat("GFRC", Pos = 14)]
		public float Friction = 0.02f;

		[Key(21)]
		[BiffInt("GATY", Min = VisualPinball.Engine.VPT.GateType.GateWireW, Max = VisualPinball.Engine.VPT.GateType.GateLongPlate, Pos = 21)]
		public int GateType = VisualPinball.Engine.VPT.GateType.GateWireW;

		[Key(16)]
		[BiffFloat("GGFC", Pos = 16)]
		public float GravityFactor = 0.25f;

		[Key(3)]
		[BiffFloat("HGTH", Pos = 3)]
		public float Height = 50f;

		[Key(8)]
		[BiffBool("GCOL", Pos = 8)]
		public bool IsCollidable = true;

		[Key(20)]
		[BiffBool("REEN", Pos = 20)]
		public bool IsReflectionEnabled = true;

		[Key(17)]
		[BiffBool("GVSB", Pos = 17)]
		public bool IsVisible = true;

		[Key(2)]
		[BiffFloat("LGTH", Pos = 2)]
		public float Length = 100f;

		[Key(4)]
		[BiffFloat("ROTA", Pos = 4)]
		public float Rotation = -90f;

		[Key(7)]
		[BiffBool("GSUP", Pos = 7)]
		public bool ShowBracket = true;

		[MaterialReference]
		[Key(5)]
		[BiffString("MATR", Pos = 5)]
		public string Material;

		[Key(10)]
		[BiffString("SURF", Pos = 10)]
		public string Surface;

		[Key(19)]
		[BiffBool("TWWA", Pos = 19)]
		public bool TwoWay = false;

		[Key(1)]
		[BiffVertex("VCEN", Pos = 1)]
		public Vertex2D Center;

		[Key(6)]
		[BiffBool("TMON", Pos = 6)]
		public bool IsTimerEnabled;

		[Key(9)]
		[BiffInt("TMIN", Pos = 9)]
		public int TimerInterval;

		public GateData(string name, float x, float y) : base(StoragePrefix.GameItem)
		{
			Name = name;
			Center = new Vertex2D(x, y);
			Rotation = 180f;
		}

		#region BIFF

		static GateData()
		{
			Init(typeof(GateData), Attributes);
		}

		[SerializationConstructor]
		public GateData() : base(StoragePrefix.GameItem)
		{
		}

		public GateData(BinaryReader reader, string storageName) : base(storageName)
		{
			Load(this, reader, Attributes);
		}

		public override void Write(BinaryWriter writer, HashWriter hashWriter)
		{
			writer.Write((int)ItemType.Gate);
			WriteRecord(writer, Attributes, hashWriter);
			WriteEnd(writer, hashWriter);
		}

		private static readonly Dictionary<string, List<BiffAttribute>> Attributes = new Dictionary<string, List<BiffAttribute>>();

		#endregion
	}
}
