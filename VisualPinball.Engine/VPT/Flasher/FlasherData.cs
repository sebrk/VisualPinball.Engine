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
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ConvertToConstant.Global
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using MessagePack;
using VisualPinball.Engine.IO;
using VisualPinball.Engine.Math;
using VisualPinball.Engine.VPT.Table;

namespace VisualPinball.Engine.VPT.Flasher
{
	[Serializable]
	[MessagePackObject]
	public class FlasherData : ItemData
	{
		public override string GetName() => Name;
		public override void SetName(string name) { Name = name; }

		[Key(10)]
		[BiffString("NAME", IsWideString = true, Pos = 10)]
		public string Name;

		[Key(1)]
		[BiffFloat("FHEI", Pos = 1)]
		public float Height = 50.0f;

		[IgnoreMember]
		[BiffFloat("FLAX", Pos = 2)]
		public float PosX { set => Center.X = value; get => Center.X; }

		[IgnoreMember]
		public float PosY { set => Center.Y = value; get => Center.Y; }

		[Key(3)]
		[BiffFloat("FLAY", Pos = 3)]
		public Vertex2D Center = new Vertex2D();

		[Key(4)]
		[BiffFloat("FROX", Pos = 4)]
		public float RotX = 0.0f;

		[Key(5)]
		[BiffFloat("FROY", Pos = 5)]
		public float RotY = 0.0f;

		[Key(6)]
		[BiffFloat("FROZ", Pos = 6)]
		public float RotZ = 0.0f;

		[Key(7)]
		[BiffColor("COLR", Pos = 7)]
		public Color Color = new Color(0xfffffff, ColorFormat.Bgr);

		[Key(11)]
		[BiffString("IMAG", Pos = 11)]
		public string ImageA;

		[Key(12)]
		[BiffString("IMAB", Pos = 12)]
		public string ImageB;

		[Key(13)]
		[BiffInt("FALP", Min = 0, Pos = 13)]
		public int Alpha = 100;

		[Key(14)]
		[BiffFloat("MOVA", Pos = 14)]
		public float ModulateVsAdd = 0.9f;

		[Key(15)]
		[BiffBool("FVIS", Pos = 15)]
		public bool IsVisible = true;

		[Key(17)]
		[BiffBool("ADDB", Pos = 17)]
		public bool AddBlend = false;

		[Key(18)]
		[BiffBool("IDMD", Pos = 18)]
		public bool IsDmd = false;

		[Key(16)]
		[BiffBool("DSPT", Pos = 16)]
		public bool DisplayTexture = false;

		[Key(19)]
		[BiffFloat("FLDB", Pos = 19)]
		public float DepthBias = 0.0f;

		[Key(20)]
		[BiffInt("ALGN", Pos = 20)]
		public int ImageAlignment = VisualPinball.Engine.VPT.ImageAlignment.ImageAlignTopLeft;

		[Key(21)]
		[BiffInt("FILT", Pos = 21)]
		public int Filter = Filters.Filter_Overlay;

		[Key(22)]
		[BiffInt("FIAM", Pos = 22)]
		public int FilterAmount = 100;

		[Key(2000)]
		[BiffDragPoint("DPNT", TagAll = true, Pos = 2000)]
		public DragPointData[] DragPoints;

		[Key(8)]
		[BiffBool("TMON", Pos = 8)]
		public bool IsTimerEnabled;

		[Key(9)]
		[BiffInt("TMIN", Pos = 9)]
		public int TimerInterval;

		#region BIFF

		static FlasherData()
		{
			Init(typeof(FlasherData), Attributes);
		}


		[SerializationConstructor]
		public FlasherData() : base(StoragePrefix.GameItem)
		{
		}

		public FlasherData(BinaryReader reader, string storageName) : base(storageName)
		{
			Load(this, reader, Attributes);
		}

		public override void Write(BinaryWriter writer, HashWriter hashWriter)
		{
			writer.Write((int)ItemType.Flasher);
			WriteRecord(writer, Attributes, hashWriter);
			WriteEnd(writer, hashWriter);
		}

		private static readonly Dictionary<string, List<BiffAttribute>> Attributes = new Dictionary<string, List<BiffAttribute>>();

		#endregion
	}
}
