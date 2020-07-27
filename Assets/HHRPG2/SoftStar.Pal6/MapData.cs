using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace SoftStar.Pal6
{
	public class MapData
	{
		public static readonly string DefaultDataPath = Path.Combine(Application.dataPath, "Data/MapData.dat");

		public readonly int ID;

		public readonly int SceneID;

		public readonly int LowSceneID;

		public readonly Langue Name;

		public readonly string SceneName;

		public readonly int CanClickMidMapBtn;

		public readonly int MusicId;

		public readonly int nameSE;

		public readonly int CanShowMidMapBtn;

		public readonly string LoadingImage0;

		public readonly string LoadingImage1;

		public readonly string MapInfoPath;

		public readonly string SoleEffect;

		public readonly int mapFlag;

		public readonly int MiniMapFlagValue;

		public readonly float ShadowDistance;

		private static string[] mDataPaths = null;

		private static MapData[] Datas = null;

		public static string[] DataPaths
		{
			get
			{
				if (MapData.mDataPaths == null)
				{
					List<string> list = new List<string>();
					list.Add(MapData.DefaultDataPath);
					string path = MapData.DefaultDataPath + ".list";
					if (File.Exists(path))
					{
						using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8, true))
						{
							do
							{
								string text = streamReader.ReadLine();
								if (File.Exists(text))
								{
									list.Add(text);
								}
							}
							while (streamReader.EndOfStream);
						}
					}
					MapData.mDataPaths = list.ToArray();
				}
				return MapData.mDataPaths;
			}
		}

		public MapData(BinaryReader DataReader)
		{
			this.ID = DataReader.ReadInt32();
			this.SceneID = DataReader.ReadInt32();
			this.LowSceneID = DataReader.ReadInt32();
			this.Name = new Langue(DataReader);
			this.SceneName = DataReader.ReadString();
			this.CanClickMidMapBtn = DataReader.ReadInt32();
			this.MusicId = DataReader.ReadInt32();
			this.nameSE = DataReader.ReadInt32();
			this.CanShowMidMapBtn = DataReader.ReadInt32();
			this.LoadingImage0 = DataReader.ReadString();
			this.LoadingImage1 = DataReader.ReadString();
			this.MapInfoPath = DataReader.ReadString();
			this.SoleEffect = DataReader.ReadString();
			this.mapFlag = DataReader.ReadInt32();
			this.MiniMapFlagValue = DataReader.ReadInt32();
			this.ShadowDistance = DataReader.ReadSingle();
		}

		public static MapData[] GetDatasFromFile()
		{
			if (MapData.Datas == null)
			{
				List<MapData> list = new List<MapData>(512);
				string[] dataPaths = MapData.DataPaths;
				for (int i = 0; i < dataPaths.Length; i++)
				{
					string text = dataPaths[i];
					if (text.ExistFile())
					{
						using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
						{
							BinaryReader dataReader = new BinaryReader(fileStream);
							while (fileStream.Position < fileStream.Length)
							{
								MapData mapData = new MapData(dataReader);
								if (mapData.ID >= list.Count)
								{
									if (list.Capacity < mapData.ID)
									{
										list.Capacity = mapData.ID;
									}
									while (list.Count < mapData.ID)
									{
										list.Add(null);
									}
									list.Add(mapData);
								}
								else if (list[mapData.ID] == null)
								{
									list[mapData.ID] = mapData;
								}
								else
								{
									Debug.Log(text + " id conflict : " + mapData.ID.ToString());
								}
							}
						}
					}
				}
				MapData.Datas = list.ToArray();
				return MapData.Datas;
			}
			return MapData.Datas;
		}

		public static void Reset()
		{
			MapData.Datas = null;
		}

		public static MapData GetData(int id)
		{
			id = UtilFun.GetPalMapLevel(id);
			MapData[] datasFromFile = MapData.GetDatasFromFile();
			if (id >= 0 && id < datasFromFile.Length)
			{
				return datasFromFile[id];
			}
			return null;
		}
	}
}
