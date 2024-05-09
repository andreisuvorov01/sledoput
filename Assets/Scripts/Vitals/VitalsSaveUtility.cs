using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Vitals
{
	public static class VitalsSaveUtility
	{
		[Obsolete("This method is obsolete starting V2")]
		public static void Save(float value, float maxValue, string fileName)
		{
			VitalSaveData data = new VitalSaveData(value, maxValue);
			string path = Application.persistentDataPath + "/" + fileName + ".data";
			SaveData(data, path);
		}

		[Obsolete("This method is obsolete starting V2")]
		public static VitalSaveData Load(string fileName)
		{
			string text = Application.persistentDataPath + "/" + fileName + ".data";
			if (File.Exists(text))
			{
				FileStream fileStream = new FileStream(text, FileMode.Open);
				VitalSaveData result = new BinaryFormatter().Deserialize(fileStream) as VitalSaveData;
				fileStream.Close();
				return result;
			}
			Debug.LogError("Save file not found in " + text);
			return null;
		}

		public static void Save(VitalsBase vitalsBase)
		{
			float value = vitalsBase.Value;
			float maxValue = vitalsBase.MaxValue;
			VitalSaveData data = new VitalSaveData(value, maxValue);
			string path = Application.persistentDataPath + "/" + vitalsBase.gameObject.name + "_" + vitalsBase.GetType().Name + ".data";
			SaveData(data, path);
		}

		public static bool Load(VitalsBase vitalsBase)
		{
			string path = Application.persistentDataPath + "/" + vitalsBase.gameObject.name + "_" + vitalsBase.GetType().Name + ".data";
			if (File.Exists(path))
			{
				FileStream fileStream = new FileStream(path, FileMode.Open);
				VitalSaveData vitalSaveData = new BinaryFormatter().Deserialize(fileStream) as VitalSaveData;
				fileStream.Close();
				if (vitalSaveData != null)
				{
					vitalsBase.SetMax(vitalSaveData.maxValue);
					vitalsBase.Set(vitalSaveData.value);
				}
				return true;
			}
			vitalsBase.Reload();
			return false;
		}

		public static void ClearSave(VitalsBase vitalsBase)
		{
			string text = Application.persistentDataPath + "/" + vitalsBase.gameObject.name + "_" + vitalsBase.GetType().Name + ".data";
			if (File.Exists(text))
			{
				File.Delete(text);
				Debug.Log("Save file for " + vitalsBase.gameObject.name + " in " + text + " has been deleted.");
			}
			else
			{
				Debug.Log("Save file for " + vitalsBase.gameObject.name + " in " + text + " does not exist.");
			}
		}

		private static void SaveData<T>(T data, string path)
		{
			FileStream fileStream = new FileStream(path, FileMode.Create);
			new BinaryFormatter().Serialize(fileStream, data);
			fileStream.Close();
		}
	}
}
