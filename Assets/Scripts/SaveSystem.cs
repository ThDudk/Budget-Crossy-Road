using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(int score) {
        BinaryFormatter formatter = new BinaryFormatter();
        
        string path = $"{Application.persistentDataPath}/player.dat";
        using FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, score);
    }

    public static int Load() {
        string path = $"{Application.persistentDataPath}/player.dat";

        if (!File.Exists(path)) {
            Debug.LogWarning("Save file not found at " + path);
            return 0;
        }
        
        BinaryFormatter formatter = new BinaryFormatter();
        
        using FileStream stream = new FileStream(path, FileMode.Open);
        return (int) formatter.Deserialize(stream);
    }
}
