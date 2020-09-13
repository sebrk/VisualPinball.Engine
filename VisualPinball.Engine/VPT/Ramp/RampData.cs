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

namespace VisualPinball.Engine.VPT.Ramp
{
	[Serializable]
	[MessagePackObject]
	public class RampData : ItemData, IPhysicalData
	{
		public override string GetName() => Name;
		public override void SetName(string name) { Name = name; }

		[Key(9)]
		[BiffString("NAME", IsWideString = true, Pos = 9)]
		public string Name;

		[Key(24)]
		[BiffFloat("RADB", Pos = 24)]
		public float DepthBias = 0f;

		[Key(2000)]
		[BiffDragPoint("DPNT", TagAll = true, Pos = 2000)]
		public DragPointData[] DragPoints;

		[Key(19)]
		[BiffFloat("ELAS", Pos = 19)]
		public float Elasticity;

		[Key(20)]
		[BiffFloat("RFCT", Pos = 20)]
		public float Friction;

		[Key(17)]
		[BiffBool("HTEV", Pos = 17)]
		public bool HitEvent = false;

		[Key(1)]
		[BiffFloat("HTBT", Pos = 1)]
		public float HeightBottom = 0f;

		[Key(2)]
		[BiffFloat("HTTP", Pos = 2)]
		public float HeightTop = 50f;

		[Key(11)]
		[BiffInt("ALGN", Pos = 11)]
		public int ImageAlignment = RampImageAlignment.ImageModeWorld;

		[Key(12)]
		[BiffBool("IMGW", Pos = 12)]
		public bool ImageWalls = true;

		[Key(22)]
		[BiffBool("CLDR", Pos = 22)]
		public bool IsCollidable = true;

		[Key(28)]
		[BiffBool("REEN", Pos = 28)]
		public bool IsReflectionEnabled = true;

		[Key(23)]
		[BiffBool("RVIS", Pos = 23)]
		public bool IsVisible = true;

		[Key(13)]
		[BiffFloat("WLHL", Pos = 13)]
		public float LeftWallHeight = 62f;

		[Key(15)]
		[BiffFloat("WVHL", Pos = 15)]
		public float LeftWallHeightVisible = 30f;

		[Key(30)]
		[BiffBool("OVPH", Pos = 30)]
		public bool OverwritePhysics = true;

		[Key(8)]
		[BiffInt("TYPE", Pos = 8)]
		public int RampType = VisualPinball.Engine.VPT.RampType.RampTypeFlat;

		[Key(14)]
		[BiffFloat("WLHR", Pos = 14)]
		public float RightWallHeight = 62f;

		[Key(16)]
		[BiffFloat("WVHR", Pos = 16)]
		public float RightWallHeightVisible = 30f;

		[Key(21)]
		[BiffFloat("RSCT", Pos = 21)]
		public float Scatter;

		[TextureReference]
		[Key(10)]
		[BiffString("IMAG", Pos = 10)]
		public string Image = string.Empty;

		[MaterialReference]
		[Key(5)]
		[BiffString("MATR", Pos = 5)]
		public string Material = string.Empty;

		[MaterialReference]
		[Key(29)]
		[BiffString("MAPH", Pos = 29)]
		public string PhysicsMaterial = string.Empty;

		[Key(18)]
		[BiffFloat("THRS", Pos = 18)]
		public float Threshold;

		[Key(3)]
		[BiffFloat("WDBT", Pos = 3)]
		public float WidthBottom = 75f;

		[Key(4)]
		[BiffFloat("WDTP", Pos = 4)]
		public float WidthTop = 60f;

		[Key(25)]
		[BiffFloat("RADI", Pos = 25)]
		public float WireDiameter = 8f;

		[Key(26)]
		[BiffFloat("RADX", Pos = 26)]
		public float WireDistanceX = 38f;

		[Key(27)]
		[BiffFloat("RADY", Pos = 27)]
		public float WireDistanceY = 88f;

		[Key(6)]
		[BiffBool("TMON", Pos = 6)]
		public bool IsTimerEnabled;

		[Key(7)]
		[BiffInt("TMIN", Pos = 7)]
		public int TimerInterval;

		[Key(1999)]
		[BiffTag("PNTS", Pos = 1999)]
		public bool Points;

		public RampData(string name, DragPointData[] dragPoints) : base(StoragePrefix.GameItem)
		{
			Name = name;
			DragPoints = dragPoints;
		}

		#region BIFF

		static RampData()
		{
			Init(typeof(RampData), Attributes);
		}

		[SerializationConstructor]
		public RampData() : base(StoragePrefix.GameItem)
		{
		}

		public RampData(BinaryReader reader, string storageName) : base(storageName)
		{
			Load(this, reader, Attributes);
		}

		public override void Write(BinaryWriter writer, HashWriter hashWriter)
		{
			writer.Write((int)ItemType.Ramp);
			WriteRecord(writer, Attributes, hashWriter);
			WriteEnd(writer, hashWriter);
		}

		private static readonly Dictionary<string, List<BiffAttribute>> Attributes = new Dictionary<string, List<BiffAttribute>>();

		#endregion

		// IPhysicalData
		public float GetElasticity() => Elasticity;
		public float GetElasticityFalloff() => 0;
		public float GetFriction() => Friction;
		public float GetScatter() => Scatter;
		public bool GetOverwritePhysics() => OverwritePhysics;
		public bool GetIsCollidable() => IsCollidable;
		public string GetPhysicsMaterial() => PhysicsMaterial;
	}
}
