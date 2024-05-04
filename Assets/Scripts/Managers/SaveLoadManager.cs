using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Data;
using UnityEngine;

namespace Managers
{
    public static class SaveLoadManager
    {
        public static void SaveGameDataInJson(GameData data, string path)
        {
            string jsonData = JsonUtility.ToJson(data);
            
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(jsonData);
                }
            }
        }

        public static GameData LoadJsonGameData(string path)
        {
            string jsonData = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(jsonData);
            return data;
        }

        public static void SaveGameDataInBinary(GameData data, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.Create(path))
            {
                formatter.Serialize(stream, data);
            }
        }

        public static GameData LoadBinaryGameData(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.OpenRead(path))
            {
                return (GameData)formatter.Deserialize(stream);
            }
        }
    }
}