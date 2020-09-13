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

namespace VisualPinball.Engine.VPT.DispReel
{
	[Serializable]
	[MessagePackObject]
	public class DispReelData : ItemData
	{
		public override string GetName() => Name;
		public override void SetName(string name) { Name = name; }

		[Key(9)]
		[BiffString("NAME", IsWideString = true, Pos = 9)]
		public string Name;

		[Key(1)]
		[BiffVertex("VER1", Pos = 1)]
		public Vertex2D V1;

		[Key(2)]
		[BiffVertex("VER2", Pos = 2)]
		public Vertex2D V2;

		[Key(10)]
		[BiffFloat("WDTH", Pos = 10)]
		public float Width = 30.0f;

		[Key(11)]
		[BiffFloat("HIGH", Pos = 11)]
		public float Height = 40.0f;

		[Key(3)]
		[BiffColor("CLRB", Pos = 3)]
		public Color BackColor = new Color(0x404040f, ColorFormat.Bgr);

		[Key(6)]
		[BiffBool("TRNS", Pos = 6)]
		public bool IsTransparent = false;

		[Key(18)]
		[BiffBool("VISI", Pos = 18)]
		public bool IsVisible = true;

		[Key(7)]
		[BiffString("IMAG", Pos = 7)]
		public string Image;

		[Key(12)]
		[BiffFloat("RCNT", AsInt = true, Pos = 12)]
		public int ReelCount = 5;

		[Key(13)]
		[BiffFloat("RSPC", Pos = 13)]
		public float ReelSpacing = 4.0f;

		[Key(14)]
		[BiffFloat("MSTP", AsInt = true, Pos = 14)]
		public int MotorSteps = 2;

		[Key(8)]
		[BiffString("SOUN", Pos = 8)]
		public string Sound;

		[Key(17)]
		[BiffBool("UGRD", Pos = 17)]
		public bool UseImageGrid = false;

		[Key(19)]
		[BiffInt("GIPR", Pos = 19)]
		public int ImagesPerGridRow = 1;

		[Key(15)]
		[BiffFloat("RANG", AsInt = true, Pos = 15)]
		public int DigitRange = 9;

		[Key(16)]
		[BiffInt("UPTM", Pos = 16)]
		public int UpdateInterval = 50;

		[Key(99)]
		[BiffFont("FONT", SkipWrite = true)]
		public Font Font;

		[Key(4)]
		[BiffBool("TMON", Pos = 4)]
		public bool IsTimerEnabled;

		[Key(5)]
		[BiffInt("TMIN", Pos = 5)]
		public int TimerInterval;

		[IgnoreMember]
		public float BoxWidth => ReelCount * Width + ReelCount * ReelSpacing + ReelSpacing; // spacing also includes edges

		[IgnoreMember]
		public float BoxHeight => Height + ReelSpacing + ReelSpacing; // spacing also includes edges

		#region BIFF

		static DispReelData()
		{
			Init(typeof(DispReelData), Attributes);
		}

		[SerializationConstructor]
		public DispReelData() : base(StoragePrefix.GameItem)
		{
		}

		public DispReelData(BinaryReader reader, string storageName) : base(storageName)
		{
			Load(this, reader, Attributes);
		}

		public override void Write(BinaryWriter writer, HashWriter hashWriter)
		{
			writer.Write((int)ItemType.DispReel);
			WriteRecord(writer, Attributes, hashWriter);
			WriteEnd(writer, hashWriter);
		}

		private static readonly Dictionary<string, List<BiffAttribute>> Attributes = new Dictionary<string, List<BiffAttribute>>();

		#endregion
	}
}
