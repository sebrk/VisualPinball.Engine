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

namespace VisualPinball.Engine.VPT.HitTarget
{
	[Serializable]
	[MessagePackObject]
	public class HitTargetData : ItemData, IPhysicalData
	{
		public override string GetName() => Name;
		public override void SetName(string name) { Name = name; }

		[Key(6)]
		[BiffString("NAME", IsWideString = true, Pos = 6)]
		public string Name;

		[Key(20)]
		[BiffFloat("PIDB", Pos = 20)]
		public float DepthBias;

		[Key(18)]
		[BiffFloat("DILB", Pos = 18)]
		public float DisableLightingBelow;

		[Key(17)]
		[BiffFloat("DILI", QuantizedUnsignedBits = 8, Pos = 17)]
		public float DisableLightingTop;

		[Key(22)]
		[BiffFloat("DRSP", Pos = 22)]
		public float DropSpeed =  0.5f;

		[Key(19)]
		[BiffBool("REEN", Pos = 19)]
		public bool IsReflectionEnabled = true;

		[Key(25)]
		[BiffInt("RADE", Pos = 25)]
		public int RaiseDelay = 100;

		[Key(12)]
		[BiffFloat("ELAS", Pos = 12)]
		public float Elasticity;

		[Key(13)]
		[BiffFloat("ELFO", Pos = 13)]
		public float ElasticityFalloff;

		[Key(14)]
		[BiffFloat("RFCT", Pos = 14)]
		public float Friction;

		[Key(16)]
		[BiffBool("CLDR", Pos = 16)]
		public bool IsCollidable = true;

		[Key(21)]
		[BiffBool("ISDR", Pos = 21)]
		public bool IsDropped = false;

		[Key(8)]
		[BiffBool("TVIS", Pos = 8)]
		public bool IsVisible = true;

		[Key(9)]
		[BiffBool("LEMO", Pos = 9)]
		public bool IsLegacy = false;

		[Key(27)]
		[BiffBool("OVPH", Pos = 27)]
		public bool OverwritePhysics = false;

		[Key(3)]
		[BiffFloat("ROTZ", Pos = 3)]
		public float RotZ = 0f;

		[Key(15)]
		[BiffFloat("RSCT", Pos = 15)]
		public float Scatter;

		[Key(4)]
		[TextureReference]
		[BiffString("IMAG", Pos = 4)]
		public string Image = string.Empty;

		[Key(7)]
		[MaterialReference]
		[BiffString("MATR", Pos = 7)]
		public string Material = string.Empty;

		[Key(26)]
		[MaterialReference]
		[BiffString("MAPH", Pos = 26)]
		public string PhysicsMaterial = string.Empty;

		[Key(5)]
		[BiffInt("TRTY", Pos = 5)]
		public int TargetType = VisualPinball.Engine.VPT.TargetType.DropTargetSimple;

		[Key(11)]
		[BiffFloat("THRS", Pos = 11)]
		public float Threshold = 2.0f;

		[Key(10)]
		[BiffBool("HTEV", Pos = 10)]
		public bool UseHitEvent = true;

		[Key(1)]
		[BiffVertex("VPOS", IsPadded = true, Pos = 1)]
		public Vertex3D Position = new Vertex3D();

		[Key(2)]
		[BiffVertex("VSIZ", IsPadded = true, Pos = 2)]
		public Vertex3D Size = new Vertex3D(32, 32, 32);

		[Key(23)]
		[BiffBool("TMON", Pos = 23)]
		public bool IsTimerEnabled;

		[Key(24)]
		[BiffInt("TMIN", Pos = 24)]
		public int TimerInterval;

		[IgnoreMember]
		public bool IsDropTarget =>
			   TargetType == VisualPinball.Engine.VPT.TargetType.DropTargetBeveled
			|| TargetType == VisualPinball.Engine.VPT.TargetType.DropTargetFlatSimple
			|| TargetType == VisualPinball.Engine.VPT.TargetType.DropTargetSimple;

		public HitTargetData(string name, float x, float y) : base(StoragePrefix.GameItem)
		{
			Name = name;
			Position = new Vertex3D(x, y, 0f);
		}

		#region BIFF

		static HitTargetData()
		{
			Init(typeof(HitTargetData), Attributes);
		}

		[SerializationConstructor]
		public HitTargetData() : base(StoragePrefix.GameItem)
		{
		}

		public HitTargetData(BinaryReader reader, string storageName) : base(storageName)
		{
			Load(this, reader, Attributes);
		}

		public override void Write(BinaryWriter writer, HashWriter hashWriter)
		{
			writer.Write((int)ItemType.HitTarget);
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
