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

namespace VisualPinball.Engine.VPT.Spinner
{
	[Serializable]
	[MessagePackObject]
	public class SpinnerData : ItemData
	{
		public override string GetName() => Name;
		public override void SetName(string name) { Name = name; }

		[Key(16)]
		[BiffString("NAME", IsWideString = true, Pos = 16)]
		public string Name;

		[Key(1)]
		[BiffVertex("VCEN", Pos = 1)]
		public Vertex2D Center;

		[Key(2)]
		[BiffFloat("ROTA", Pos = 2)]
		public float Rotation = 0f;

		[MaterialReference]
		[Key(13)]
		[BiffString("MATR", Pos = 13)]
		public string Material = string.Empty;

		[Key(12)]
		[BiffBool("SSUP", Pos = 12)]
		public bool ShowBracket = true;

		[Key(5)]
		[BiffFloat("HIGH", Pos = 5)]
		public float Height = 60f;

		[Key(6)]
		[BiffFloat("LGTH", Pos = 6)]
		public float Length = 80f;

		[Key(7)]
		[BiffFloat("AFRC", Pos = 7)]
		public float Damping = 0.9879f;

		[Key(8)]
		[BiffFloat("SMAX", Pos = 8)]
		public float AngleMax = 0f;

		[Key(9)]
		[BiffFloat("SMIN", Pos = 9)]
		public float AngleMin = 0f;

		[Key(10)]
		[BiffFloat("SELA", Pos = 10)]
		public float Elasticity = 0.3f;

		[Key(11)]
		[BiffBool("SVIS", Pos = 11)]
		public bool IsVisible = true;

		[TextureReference]
		[Key(14)]
		[BiffString("IMGF", Pos = 14)]
		public string Image = string.Empty;

		[Key(15)]
		[BiffString("SURF", Pos = 15)]
		public string Surface = string.Empty;

		[Key(3)]
		[BiffBool("TMON", Pos = 3)]
		public bool IsTimerEnabled;

		[Key(4)]
		[BiffInt("TMIN", Pos = 4)]
		public int TimerInterval;

		public SpinnerData(string name, float x, float y) : base(StoragePrefix.GameItem)
		{
			Name = name;
			Center = new Vertex2D(x, y);
		}

		[SerializationConstructor]
		public SpinnerData() : base(StoragePrefix.GameItem)
		{
		}

		#region BIFF

		static SpinnerData()
		{
			Init(typeof(SpinnerData), Attributes);
		}

		public SpinnerData(BinaryReader reader, string storageName) : base(storageName)
		{
			Load(this, reader, Attributes);
		}

		public override void Write(BinaryWriter writer, HashWriter hashWriter)
		{
			writer.Write((int)ItemType.Spinner);
			WriteRecord(writer, Attributes, hashWriter);
			WriteEnd(writer, hashWriter);
		}

		private static readonly Dictionary<string, List<BiffAttribute>> Attributes = new Dictionary<string, List<BiffAttribute>>();

		#endregion
	}
}
