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

namespace VisualPinball.Engine.VPT.Surface
{
	[Serializable]
	[MessagePackObject]
	public class SurfaceData : ItemData, IPhysicalData
	{
		public override string GetName() => Name;
		public override void SetName(string name) { Name = name; }

		[Key(16)]
		[BiffString("NAME", IsWideString = true, Pos = 16)]
		public string Name;

		[Key(1)]
		[BiffBool("HTEV", Pos = 1)]
		public bool HitEvent = false;

		[Key(2)]
		[BiffBool("DROP", Pos = 2)]
		public bool IsDroppable = false;

		[Key(3)]
		[BiffBool("FLIP", Pos = 3)]
		public bool IsFlipbook = false;

		[Key(4)]
		[BiffBool("ISBS", Pos = 4)]
		public bool IsBottomSolid = false;

		[Key(5)]
		[BiffBool("CLDW", Pos = 5)]
		public bool IsCollidable = true;

		[Key(8)]
		[BiffFloat("THRS", Pos = 8)]
		public float Threshold = 2.0f;

		[Key(9)]
		[TextureReference]
		[BiffString("IMAG", Pos = 9)]
		public string Image = string.Empty;

		[Key(10)]
		[TextureReference]
		[BiffString("SIMG", Pos = 10)]
		public string SideImage = string.Empty;

		[Key(11)]
		[MaterialReference]
		[BiffString("SIMA", Pos = 11)]
		public string SideMaterial = string.Empty;

		[Key(12)]
		[MaterialReference]
		[BiffString("TOMA", Pos = 12)]
		public string TopMaterial = string.Empty;

		[Key(29)]
		[MaterialReference]
		[BiffString("MAPH", Pos = 29)]
		public string PhysicsMaterial = string.Empty;

		[Key(13)]
		[MaterialReference]
		[BiffString("SLMA", Pos = 13)]
		public string SlingShotMaterial = string.Empty;

		[Key(14)]
		[BiffFloat("HTBT", Pos = 14)]
		public float HeightBottom = 0f;

		[Key(15)]
		[BiffFloat("HTTP", Pos = 15)]
		public float HeightTop = 50f;

		[Key(50)]
		[BiffBool("INNR", SkipWrite = true)]
		public bool Inner = true;

		[Key(17)]
		[BiffBool("DSPT", Pos = 17)]
		public bool DisplayTexture = false;

		[Key(18)]
		[BiffFloat("SLGF", Pos = 18)]
		public float SlingshotForce = 80f;

		[Key(19)]
		[BiffFloat("SLTH", Pos = 19)]
		public float SlingshotThreshold = 0f;

		[Key(24)]
		[BiffBool("SLGA", Pos = 24)]
		public bool SlingshotAnimation = true;

		[Key(20)]
		[BiffFloat("ELAS", Pos = 20)]
		public float Elasticity;

		[Key(21)]
		[BiffFloat("WFCT", Pos = 21)]
		public float Friction;

		[Key(22)]
		[BiffFloat("WSCT", Pos = 22)]
		public float Scatter;

		[Key(23)]
		[BiffBool("VSBL", Pos = 23)]
		public bool IsTopBottomVisible = true;

		[Key(30)]
		[BiffBool("OVPH", Pos = 30)]
		public bool OverwritePhysics = true;

		[Key(26)]
		[BiffFloat("DILI", QuantizedUnsignedBits = 8, Pos = 26)]
		public float DisableLightingTop;

		[Key(27)]
		[BiffFloat("DILB", Pos = 27)]
		public float DisableLightingBelow;

		[Key(25)]
		[BiffBool("SVBL", Pos = 25)]
		public bool IsSideVisible = true;

		[Key(28)]
		[BiffBool("REEN", Pos = 28)]
		public bool IsReflectionEnabled = true;

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

		// non-persisted
		[IgnoreMember]
		public bool IsDisabled;

		public SurfaceData(string name, DragPointData[] dragPoints) : base(StoragePrefix.GameItem)
		{
			Name = name;
			DragPoints = dragPoints;
		}

		[SerializationConstructor]
		public SurfaceData() : base(StoragePrefix.GameItem)
		{
		}

		#region BIFF

		static SurfaceData()
		{
			Init(typeof(SurfaceData), Attributes);
		}

		public SurfaceData(BinaryReader reader, string storageName) : base(storageName)
		{
			Load(this, reader, Attributes);
		}

		public override void Write(BinaryWriter writer, HashWriter hashWriter)
		{
			writer.Write((int)ItemType.Surface);
			WriteRecord(writer, Attributes, hashWriter);
			WriteEnd(writer, hashWriter);
		}

		private static readonly Dictionary<string, List<BiffAttribute>> Attributes = new Dictionary<string, List<BiffAttribute>>();

		#endregion
	}
}
