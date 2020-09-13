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
// ReSharper disable UnusedAutoPropertyAccessor.Global
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using MessagePack;
using VisualPinball.Engine.IO;
using VisualPinball.Engine.Math;
using VisualPinball.Engine.VPT.Table;

namespace VisualPinball.Engine.VPT.Kicker
{
	[Serializable]
	[MessagePackObject]
	public class KickerData : ItemData
	{
		public override string GetName() => Name;
		public override void SetName(string name) { Name = name; }

		[Key(8)]
		[BiffString("NAME", IsWideString = true, Pos = 8)]
		public string Name;

		[Key(9)]
		[BiffInt("TYPE", Pos = 9)]
		public int KickerType = VisualPinball.Engine.VPT.KickerType.KickerHole;

		[Key(1)]
		[BiffVertex("VCEN", Pos = 1)]
		public Vertex2D Center;

		[Key(2)]
		[BiffFloat("RADI", Pos = 2)]
		public float Radius = 25f;

		[Key(10)]
		[BiffFloat("KSCT", Pos = 10)]
		public float Scatter = 0.0f;

		[Key(11)]
		[BiffFloat("KHAC", Pos = 11)]
		public float HitAccuracy = 0.7f;

		[Key(12)]
		[BiffFloat("KHHI", Pos = 12)]
		public float HitHeight = 40.0f;

		[Key(13)]
		[BiffFloat("KORI", Pos = 13)]
		public float Orientation = 0.0f;

		[MaterialReference]
		[Key(5)]
		[BiffString("MATR", Pos = 5)]
		public string Material = string.Empty;

		[Key(6)]
		[BiffString("SURF", Pos = 6)]
		public string Surface = string.Empty;

		[Key(14)]
		[BiffBool("FATH", Pos = 14)]
		public bool FallThrough = false;

		[Key(7)]
		[BiffBool("EBLD", Pos = 7)]
		public bool IsEnabled = true;

		[Key(15)]
		[BiffBool("LEMO", Pos = 15)]
		public bool LegacyMode = true;

		[Key(3)]
		[BiffBool("TMON", Pos = 3)]
		public bool IsTimerEnabled;

		[Key(4)]
		[BiffInt("TMIN", Pos = 4)]
		public int TimerInterval;

		public KickerData(string name, float x, float y) : base(StoragePrefix.GameItem)
		{
			Name = name;
			Center = new Vertex2D(x, y);
		}

		#region BIFF

		static KickerData()
		{
			Init(typeof(KickerData), Attributes);
		}

		[SerializationConstructor]
		public KickerData() : base(StoragePrefix.GameItem)
		{
		}

		public KickerData(BinaryReader reader, string storageName) : base(storageName)
		{
			Load(this, reader, Attributes);
		}

		public override void Write(BinaryWriter writer, HashWriter hashWriter)
		{
			writer.Write((int)ItemType.Kicker);
			WriteRecord(writer, Attributes, hashWriter);
			WriteEnd(writer, hashWriter);
		}

		private static readonly Dictionary<string, List<BiffAttribute>> Attributes = new Dictionary<string, List<BiffAttribute>>();

		#endregion
	}
}
