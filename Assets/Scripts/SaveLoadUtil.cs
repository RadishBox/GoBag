using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using System;
using System.Xml.Serialization;

/// <summary>
/// Util class to load and save a GameSave in a binary or xml file
/// </summary>
public static class SaveLoadUtil {
    
    public static List<GameSave> savedGames = new List<GameSave>();
    private static string fileName = "PonteTruchaSave.gd";

    public static void Save()
    {
        savedGames.Add(GameSave.Instance);
        /*
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "savedGames.gd"));
        bf.Serialize(file, SaveLoadUtil.savedGames);
        file.Close();
        */
        using (MemoryStream stream = new MemoryStream())
        {
            //XmlSerializer progressSerializer = new XmlSerializer(typeof(List<GameSave>));
            XmlSerializer progressSerializer = new XmlSerializer(typeof(GameSave));
            //progressSerializer.Serialize(stream, SaveLoadUtil.savedGames);
            progressSerializer.Serialize(stream, GameSave.Instance);
            using (FileStream fileWriter = File.Create(Path.Combine(Application.persistentDataPath, fileName)))
            {
                byte[] originalData = stream.GetBuffer();
                fileWriter.Write(originalData, 0, originalData.Length);
                fileWriter.Close();
            }
        }
    }

    /*
    public static List<GameSave> Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Path.Combine(Application.persistentDataPath, "savedGames.gd"), FileMode.Open);
            //SaveLoadUtil.savedGames = (List<GameSave>)bf.Deserialize(file);
            GameSave.Instance = (GameSave)bf.Deserialize(file);
            file.Close();
            return SaveLoadUtil.savedGames;
        }
        
        return null;
    }
     * */

    public static GameSave LoadGameSave()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, fileName)))
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(List<GameSave>));
            XmlSerializer serializer = new XmlSerializer(typeof(GameSave));
            StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, fileName));
            //SaveLoadUtil.savedGames = (List<GameSave>)serializer.Deserialize(reader);
            GameSave.Instance = (GameSave)serializer.Deserialize(reader);
            reader.Close();

            /*
            Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Path.Combine(Application.persistentDataPath, "savedGames.gd"), FileMode.Open);
            SaveLoadUtil.savedGames = (List<GameSave>)bf.Deserialize(file);
            file.Close();
            */
            //return SaveLoadUtil.savedGames[0];
            return GameSave.Instance;
        }
        return null;
    }

    public static void DeleteGameSave()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, fileName)))
        {
            File.Delete(Path.Combine(Application.persistentDataPath, fileName));
        }
    }
}
