using Funfia.File;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SoftStar
{
	[Serializable]
	public class Langue
	{
		private class ConfigIni
		{
			public static string ConfigFilePath = Path.Combine(Application.dataPath, "Data/Langue/Map.ini");

			public static Regex SectionRegex = new Regex("\\A[ \\t]*\\[(?<SectionName>[^\\]]*)\\][ \\t]*\\Z");

			public static Regex KVRegex = new Regex("\\A(?<K>[^=]*)[=](?<V>.*)\\Z");

			private Dictionary<string, Dictionary<string, string>> Data;

			private static Langue.ConfigIni instance = null;

			private ConfigIni()
			{
				this.ReLoad();
			}

			public static Langue.ConfigIni GetInstance()
			{
				if (Langue.ConfigIni.instance == null)
				{
					Langue.ConfigIni.instance = new Langue.ConfigIni();
				}
				return Langue.ConfigIni.instance;
			}

			public void ReLoad()
			{
				this.Data = new Dictionary<string, Dictionary<string, string>>();
				if (!File.Exists(Langue.ConfigIni.ConfigFilePath))
				{
					return;
				}
				using (StreamReader streamReader = new StreamReader(Langue.ConfigIni.ConfigFilePath, Encoding.UTF8, true))
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					this.Data[string.Empty] = dictionary;
					while (streamReader.Peek() >= 0)
					{
						string input = streamReader.ReadLine();
						Match match = Langue.ConfigIni.SectionRegex.Match(input);
						if (match.Success)
						{
							string key = match.Groups["SectionName"].Value.Trim();
							if (!this.Data.TryGetValue(key, out dictionary))
							{
								dictionary = new Dictionary<string, string>();
								this.Data[key] = dictionary;
							}
						}
						else
						{
							match = Langue.ConfigIni.KVRegex.Match(input);
							if (match.Success)
							{
								string key2 = match.Groups["K"].Value.Trim();
								string text = match.Groups["V"].Value.Trim();
								if (text[0] == '"' && text[text.Length - 1] == '"')
								{
									text = text.Substring(1, text.Length - 2);
								}
								if (!dictionary.ContainsKey(key2))
								{
									dictionary[key2] = text;
								}
								else
								{
									dictionary[key2] = dictionary[key2] + "," + text;
								}
							}
						}
					}
				}
			}

			public void Save()
			{
				using (StreamWriter streamWriter = new StreamWriter(Langue.ConfigIni.ConfigFilePath, false, Encoding.UTF8))
				{
					foreach (KeyValuePair<string, Dictionary<string, string>> current in this.Data)
					{
						if (!string.IsNullOrEmpty(current.Key))
						{
							streamWriter.WriteLine("[" + current.Key + "]");
						}
						foreach (KeyValuePair<string, string> current2 in current.Value)
						{
							streamWriter.WriteLine(current2.Key + "=" + current2.Value);
						}
					}
				}
			}

			public Dictionary<string, string> GetSection(string name)
			{
				Dictionary<string, string> result = null;
				this.Data.TryGetValue(name, out result);
				return result;
			}

			public T ReadConfig<T>(string SectionName, string KeyName, T DefaultValue)
			{
				Dictionary<string, string> section = this.GetSection(SectionName);
				if (section != null)
				{
					return this.ReadConfig<T>(section, KeyName, DefaultValue);
				}
				return DefaultValue;
			}

			public T ReadConfig<T>(Dictionary<string, string> CurSection, string KeyName, T DefaultValue)
			{
				string value = null;
				if (CurSection.TryGetValue(KeyName, out value))
				{
					try
					{
						return (T)((object)Convert.ChangeType(value, typeof(T)));
					}
					catch
					{
					}
					return DefaultValue;
				}
				return DefaultValue;
			}
		}

		private class SoftReferenceDictionary
		{
			private readonly string SavePath;

			public Dictionary<ulong, string> Data;

			public SoftReferenceDictionary(string inSavePath)
			{
				this.SavePath = inSavePath;
				string text = Path.Combine(Application.dataPath, string.Concat(new string[]
				{
					"Data/Langue/",
					Langue.CurLangue.ToString(),
					"/",
					this.SavePath,
					".string"
				}));
				if (!File.Exists(text))
				{
					UnityEngine.Debug.LogError("file no found:" + text);
				}
				using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read))
				{
					if (fileStream.Length <= 14L)
					{
						UnityEngine.Debug.LogError("file error 0:" + text);
						return;
					}
					if (fileStream.ReadByte() != 76)
					{
						UnityEngine.Debug.LogError("file error 1:" + text);
						return;
					}
					if (fileStream.ReadByte() != 83)
					{
						UnityEngine.Debug.LogError("file error 1:" + text);
						return;
					}
					using (BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.Unicode))
					{
						if (binaryReader.ReadUInt32() != 0u)
						{
							UnityEngine.Debug.LogError("file error 2:" + text);
							return;
						}
						ulong num = binaryReader.ReadUInt64();
						ulong num2 = 0uL;
						while (fileStream.Length - fileStream.Position > 8L)
						{
							num ^= binaryReader.ReadUInt64();
						}
						while (fileStream.Length > fileStream.Position)
						{
							num ^= (ulong)binaryReader.ReadByte();
						}
						if (num != num2)
						{
							UnityEngine.Debug.LogError("file error 3:" + text);
							return;
						}
						fileStream.Position = 14L;
						this.Data = new Dictionary<ulong, string>(binaryReader.ReadInt32());
						while (fileStream.Length != fileStream.Position)
						{
							ulong key = binaryReader.ReadUInt64();
							this.Data[key] = binaryReader.ReadString();
						}
					}
				}
				Langue.AllRows[this.SavePath] = this;
			}

			~SoftReferenceDictionary()
			{
				Langue.AllRows.Remove(this.SavePath);
			}
		}

		public delegate bool BoolFunVoid();

		public delegate void VoidFunString(string inText);

		public Langue.BoolFunVoid IsAlive;

		private static Dictionary<string, uint> mMap;

		private static uint PalLangueFromSteam = 4294967295u;

		[NonSerialized]
		private static uint m_curLangue = 4294967295u;

		[SerializeField]
		private int GUIDPart1;

		[SerializeField]
		private int GUIDPart2;

		[NonSerialized]
		private string CurString;

		[SerializeField]
		private string SavePath;

		private static Dictionary<string, Langue.SoftReferenceDictionary> AllRows = new Dictionary<string, Langue.SoftReferenceDictionary>();

        public event Langue.VoidFunString OnTextChanged;

        public static Dictionary<string, uint> Map
		{
			get
			{
				if (Langue.mMap == null)
				{
					Langue.mMap = new Dictionary<string, uint>();
					foreach (KeyValuePair<string, string> current in Langue.ConfigIni.GetInstance().GetSection("Langue"))
					{
						uint value;
						if (uint.TryParse(current.Value, out value))
						{
							Langue.mMap[current.Key] = value;
						}
					}
				}
				return Langue.mMap;
			}
		}

		public static uint CurLangue
		{
			get
			{
				if (Langue.m_curLangue == 4294967295u)
				{
					Langue.m_curLangue = Langue.get_CurLangue_from_config();
				}
				return Langue.m_curLangue;
			}
			set
			{
				if (Langue.m_curLangue != value)
				{
					FileLoader.ResetLanguagePath();
				}
				Langue.m_curLangue = value;
			}
		}

		public static bool IsLanguage2
		{
			get
			{
				return Langue.CurLangue == 2u;
			}
		}

		public ulong GUID
		{
			get
			{
				return (ulong)this.GUIDPart1 | (ulong)this.GUIDPart2 << 32;
			}
		}

		public Langue(ulong newGUID, string newSavePath = "")
		{
			this.Initialize(newGUID, newSavePath);
		}

		public Langue(BinaryReader dataReader)
		{
			this.change_GUID(dataReader.ReadUInt64());
			this.SavePath = dataReader.ReadString();
			this.ReLoad();
		}

		public static uint GetLangueID(string v)
		{
			uint result;
			if (uint.TryParse(v, out result))
			{
				return result;
			}
			if (Langue.Map.TryGetValue(v, out result))
			{
				return result;
			}
			return 0u;
		}

		private static uint get_CurLangue_from_config()
		{
			Langue.PalLangueFromSteam = 0u;
			return Langue.PalLangueFromSteam;
		}

		private void change_GUID(ulong newValue)
		{
			this.GUIDPart1 = (int)((uint)(newValue));
			this.GUIDPart2 = (int)((uint)(newValue >> 32));
		}

		public void Serialize(BinaryWriter dataWriter)
		{
			dataWriter.Write(this.GUID);
			dataWriter.Write(this.SavePath);
		}

		public void Initialize(ulong newGUID, string newSavePath)
		{
			this.change_GUID(newGUID);
			this.SavePath = newSavePath;
			this.ReLoad();
		}

		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.GUID.ToString(),
				",",
				this.SavePath,
				":",
				this.get_string()
			});
		}

		private static Dictionary<ulong, string> GetIniDictionary(string CurSavePath)
		{
			Langue.SoftReferenceDictionary softReferenceDictionary = null;
			if (Langue.AllRows.TryGetValue(CurSavePath, out softReferenceDictionary) && softReferenceDictionary != null)
			{
				return softReferenceDictionary.Data;
			}
			return new Langue.SoftReferenceDictionary(CurSavePath).Data;
		}

		public static void ClearLanguageDictionary()
		{
			Langue.AllRows.Clear();
		}

		public void ReLoad()
		{
			this.CurString = Langue.get_string(this.GUID, this.SavePath);
		}

		public static string get_string(ulong GUID, string SavePath)
		{
			try
			{
				return Langue.GetIniDictionary(SavePath)[GUID];
			}
			catch (Exception ex)
			{
				StackTrace stackTrace = new StackTrace(1, false);
				UnityEngine.Debug.LogError(string.Concat(new string[]
				{
					GUID.ToString(),
					":",
					ex.ToString(),
					Environment.NewLine,
					stackTrace.ToString()
				}));
			}
			return string.Empty;
		}

		public static string get_string_myself(string Text)
		{
			return Text;
		}

		public static char get_char(ulong GUID, string SavePath)
		{
			return Langue.get_string(GUID, SavePath)[0];
		}

		public string get_SavePath()
		{
			return this.SavePath;
		}

		public string get_string()
		{
			return this.CurString;
		}
	}
}
