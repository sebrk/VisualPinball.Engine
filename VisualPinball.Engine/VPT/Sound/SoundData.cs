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

using System;
using System.IO;
using System.Text;
using MessagePack;
using VisualPinball.Engine.Common;
using VisualPinball.Engine.IO;
using VisualPinball.Engine.VPT.Table;

namespace VisualPinball.Engine.VPT.Sound
{
	[Serializable]
	[MessagePackObject]
	public class SoundData : ItemData
	{
		public override string GetName() => Name;
		public override void SetName(string name) { Name = name; }

		[Key(0)] public string Name;
		[Key(1)] public string Path;
		[Key(2)] public string InternalName;
		[Key(3)] public WaveFormat Wfx;
		[Key(4)] public byte[] Data;

		[Key(5)] public byte OutputTarget = SoundOutTypes.Table;
		[Key(6)] public int Volume;
		[Key(7)] public int Balance;
		[Key(8)] public int Fade;

		public SoundData(string name) : base(IO.StoragePrefix.Sound)
		{
			Name = name;
			Path = string.Empty;
			InternalName = name;
			Wfx = new WaveFormat();
			Data = new byte[0];
		}

		[SerializationConstructor]
		public SoundData() : base(StoragePrefix.Sound)
		{
		}

		public SoundData(BinaryReader reader, string storageName, int fileVersion) : base(storageName)
		{
			Load(reader, fileVersion);
		}

		public byte[] GetHeader() {
			using (var stream = new MemoryStream())
			using (var writer = new BinaryWriter(stream)) {
				writer.Write(Encoding.Default.GetBytes("RIFF"));
				writer.Write(Data.Length + 36);
				writer.Write(Encoding.Default.GetBytes("WAVE"));
				writer.Write(Encoding.Default.GetBytes("fmt "));
				writer.Write(16);
				writer.Write((short)Wfx.FormatTag);
				writer.Write((short)Wfx.Channels);
				writer.Write((int)Wfx.SamplesPerSec);
				writer.Write((int)(Wfx.SamplesPerSec * Wfx.BitsPerSample * Wfx.Channels / 8));
				writer.Write((short)Wfx.BlockAlign);
				writer.Write((short)Wfx.BitsPerSample);
				writer.Write(Encoding.Default.GetBytes("data"));
				writer.Write(Data.Length);
				return stream.ToArray();
			}
		}

		private void Load(BinaryReader reader, int fileVersion)
		{
			var numValues = fileVersion < Constants.NewSoundFormatVersion ? 5 : 10;
			for (var i = 0; i < numValues; i++)
			{
				int len;
				switch (i) {
					case 0:
						len = reader.ReadInt32();
						Name = Encoding.Default.GetString(reader.ReadBytes(len));
						break;
					case 1:
						len = reader.ReadInt32();
						Path = Encoding.Default.GetString(reader.ReadBytes(len));
						break;
					case 2:
						len = reader.ReadInt32();
						InternalName = Encoding.Default.GetString(reader.ReadBytes(len));
						break;
					case 3: Wfx = new WaveFormat(reader); break;
					case 4:
						len = reader.ReadInt32();
						Data = reader.ReadBytes(len);
						break;
					case 5: OutputTarget = reader.ReadByte(); break;
					case 6: Volume = reader.ReadInt32(); break;
					case 7: Balance = reader.ReadInt32(); break;
					case 8: Fade = reader.ReadInt32(); break;
					case 9: Volume = reader.ReadInt32(); break;
				}
			}
		}

		public override void Write(BinaryWriter writer, HashWriter hashWriter)
		{
			writer.Write(Encoding.Default.GetBytes(Name).Length);
			writer.Write(Encoding.Default.GetBytes(Name));

			writer.Write(Encoding.Default.GetBytes(Path).Length);
			writer.Write(Encoding.Default.GetBytes(Path));

			writer.Write(Encoding.Default.GetBytes(InternalName).Length);
			writer.Write(Encoding.Default.GetBytes(InternalName));

			Wfx.Write(writer);

			writer.Write(Data.Length);
			writer.Write(Data);

			writer.Write(OutputTarget);
			writer.Write(Volume);
			writer.Write(Balance);
			writer.Write(Fade);
			writer.Write(Volume);
		}
	}
}
