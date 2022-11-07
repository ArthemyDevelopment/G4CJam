using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace ArthemyDevelopment.Localization
{

	[DefaultExecutionOrder(-5)]
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager current;

		

		[Header("Languages")][Tooltip("The list of the language files the game will use, to add a new file drag and drop it from the StreamingAssets folder or add it manually.")]
		public List<LanguageFile> currentsLanguages;

		private Dictionary<string, string> LocalizedText = new Dictionary<string, string>(); 
		private string S_MissingTextString = "<String Not Found>";


		private void Awake()
		{
			if(current == null)
			{
				current = this;
			}
			else if(current != this)
			{
				Destroy(this);
			}
			if(PlayerPrefs.HasKey("ADLocalizationIndex"))
			{
				LoadTextFile(currentsLanguages[PlayerPrefs.GetInt("ADLocalizationIndex")].S_FileName);
			}
			DontDestroyOnLoad(gameObject);
			
			BetterStreamingAssets.Initialize();
		}

		#region Localization File


		public void LoadTextFile(string fileName)
		{
			if(fileName.EndsWith(".json"))
			{
				LoadLocalizationTextJson(fileName);
			}
			else if(fileName.EndsWith(".strings"))
			{
				LoadLocalizationTextStrings(fileName);
			}
			else if (fileName.EndsWith(".csv"))
			{
				LoadLocalizationTextCSV(fileName);
			}
		}

		public void LoadLocalizationTextJson(string fileName)
		{
			LocalizedText = new Dictionary<string, string>();
			
			if(BetterStreamingAssets.FileExists("/"+fileName))
			{
				
				string JsonData = BetterStreamingAssets.ReadAllText("/" + fileName);
				LocalizationData LoadedData = JsonUtility.FromJson<LocalizationData>(JsonData);

				for (int i = 0; i < LoadedData.LI_Items.Length; i++)
				{
					LocalizedText.Add(LoadedData.LI_Items[i].key, LoadedData.LI_Items[i].value);
				} 

			}
			else
			{
				Debug.LogError("LocalizationData file not found: no Json file with the given name exist in the correct folder");
			}
		}


		public void LoadLocalizationTextStrings(string fileName)
		{
			LocalizedText = new Dictionary<string, string>();
			Debug.Log(BetterStreamingAssets.FileExists("/"+fileName));
			if(BetterStreamingAssets.FileExists("/"+fileName))
			{
				Debug.Log(BetterStreamingAssets.OpenText("/"+fileName));
				StreamReader reader = BetterStreamingAssets.OpenText("/"+fileName);
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					if (line.StartsWith("\""))
					{
						string[] data = line.Split('"');
						LocalizedText.Add(data[1], data[3]);
					}
				}
			}
			else
			{
				Debug.LogError("LocalizationData file not found: no .String file with the given name exist in the correct folder");
			}
		}

		public void LoadLocalizationTextCSV(string fileName)
		{
			LocalizedText = new Dictionary<string, string>();

			BetterStreamingAssets.Initialize();

			
			if(BetterStreamingAssets.FileExists(fileName))
			{
				//Debug.Log(BetterStreamingAssets.OpenText(fileName));
				StreamReader reader = BetterStreamingAssets.OpenText(fileName);
				string file;

				while ((file = reader.ReadLine()) != null)
				{
					string[] lines = file.Split('\n');
					for (int i = 0; i < lines.Length; i++)
					{
						string[] data = lines[i].Split(';');
						LocalizedText.Add(data[0], data[1]);
					}


				}
			}
			else
			{
				Debug.LogError("LocalizationData file not found: no .CSV file with the given name exist in the correct folder");
			}

			

		}

		#endregion


		#region Localization Value

		public string GetLocalizationValue(string key)
		{
			if(LocalizedText == null || LocalizedText.Count == 0 )
			{
				LoadDefault();
			}
			string result = S_MissingTextString;
			if(LocalizedText.ContainsKey(key))
			{
				result = LocalizedText[key];
			}
			return result;
		}

		public void LoadDefault()
		{
			if(PlayerPrefs.HasKey("ADLocalizationIndex"))
			{
				LoadTextFile(currentsLanguages[PlayerPrefs.GetInt("ADLocalizationIndex")].S_FileName);
			}
			else
			{
				LoadTextFile(currentsLanguages[0].S_FileName);
			}
		}

		public void SaveDefault(int pref)
		{
			PlayerPrefs.SetInt("ADLocalizationIndex", pref);
			PlayerPrefs.SetString("ADLanguage", currentsLanguages[pref].S_Name);
		}

		public void CustomEventTrigger(int i)
		{
			PlayerPrefs.SetInt("CustomEventTrigger" + i, 1);
		}

		#endregion
	}

	#region Serilized Classses

	[System.Serializable]
	public class LocalizationData
	{
		public LocalizationItem[] LI_Items;
	}

	[System.Serializable]
	public class LocalizationItem
	{
		public string key;
		public string value;
	}

	[Serializable]
	public class LanguageFile
	{
		public string S_Name;
		public string S_FileName;
	}
	#endregion
}
