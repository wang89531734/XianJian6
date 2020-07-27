using System;
using System.IO;

public interface ISaveInterface
{
	void Save(BinaryWriter writer);

	void Load(BinaryReader reader);
}
