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

namespace VisualPinball.Engine.VPT.Rubber
{
	[Serializable]
	[MessagePackObject]
	public class RubberData : ItemData, IPhysicalData
	{
		public override string GetName() => Name;
		public override void SetName(string name) { Name = name; }

		[Key(8)]
		[BiffString("NAME", IsWideString = true, Pos = 8)]
		public string Name;

		[Key(1)]
		[BiffFloat("HTTP", Pos = 1)]
		public float Height = 25f;

		[Key(2)]
		[BiffFloat("HTHI", Pos = 2)]
		public float HitHeight = 25f;

		[Key(3)]
		[BiffInt("WDTP", Pos = 3)]
		public int Thickness = 8;

		[Key(4)]
		[BiffBool("HTEV", Pos = 4)]
		public bool HitEvent = false;

		[MaterialReference]
		[Key(5)]
		[BiffString("MATR", Pos = 5)]
		public string Material = string.Empty;

		[TextureReference]
		[Key(9)]
		[BiffString("IMAG", Pos = 9)]
		public string Image = string.Empty;

		[Key(10)]
		[BiffFloat("ELAS", Pos = 10)]
		public float Elasticity;

		[Key(11)]
		[BiffFloat("ELFO", Pos = 11)]
		public float ElasticityFalloff;

		[Key(12)]
		[BiffFloat("RFCT", Pos = 12)]
		public float Friction;

		[Key(13)]
		[BiffFloat("RSCT", Pos = 13)]
		public float Scatter;

		[Key(14)]
		[BiffBool("CLDR", Pos = 14)]
		public bool IsCollidable = true;

		[Key(15)]
		[BiffBool("RVIS", Pos = 15)]
		public bool IsVisible = true;

		[Key(21)]
		[BiffBool("REEN", Pos = 21)]
		public bool IsReflectionEnabled = true;

		[Key(16)]
		[BiffBool("ESTR", Pos = 16)]
		public bool StaticRendering = true;

		[Key(17)]
		[BiffBool("ESIE", Pos = 17)]
		public bool ShowInEditor = true;

		[Key(18)]
		[BiffFloat("ROTX", Pos = 18)]
		public float RotX = 0f;

		[Key(19)]
		[BiffFloat("ROTY", Pos = 19)]
		public float RotY = 0f;

		[Key(20)]
		[BiffFloat("ROTZ", Pos = 20)]
		public float RotZ = 0f;

		[MaterialReference]
		[Key(22)]
		[BiffString("MAPH", Pos = 22)]
		public string PhysicsMaterial = string.Empty;

		[Key(23)]
		[BiffBool("OVPH", Pos = 23)]
		public bool OverwritePhysics = false;

		[Key(2000)]
		[BiffDragPoint("DPNT", TagAll = true, Pos = 2000)]
		public DragPointData[] DragPoints;

		[Key(6)]
		[BiffBool("TMON", Pos = 6)]
		public bool IsTimerEnabled;

		[Key(7)]
		[BiffInt("TMIN", Pos = 7)]
		public int TimerInterval;

		[Key(1999)]
		[BiffTag("PNTS", Pos = 1999)]
		public bool Points;

		// IPhysicalData
		public float GetElasticity() => Elasticity;
		public float GetElasticityFalloff() => 0;
		public float GetFriction() => Friction;
		public float GetScatter() => Scatter;
		public bool GetOverwritePhysics() => OverwritePhysics;
		public bool GetIsCollidable() => IsCollidable;
		public string GetPhysicsMaterial() => PhysicsMaterial;

		public RubberData(string name) : base(StoragePrefix.GameItem)
		{
			Name = name;
		}

		#region BIFF

		static RubberData()
		{
			Init(typeof(RubberData), Attributes);
		}

		[SerializationConstructor]
		public RubberData() : base(StoragePrefix.GameItem)
		{
		}

		public RubberData(BinaryReader reader, string storageName) : base(storageName)
		{
			Load(this, reader, Attributes);
		}

		public override void Write(BinaryWriter writer, HashWriter hashWriter)
		{
			writer.Write((int)ItemType.Rubber);
			WriteRecord(writer, Attributes, hashWriter);
			WriteEnd(writer, hashWriter);
		}

		private static readonly Dictionary<string, List<BiffAttribute>> Attributes = new Dictionary<string, List<BiffAttribute>>();

		#endregion
	}
}
