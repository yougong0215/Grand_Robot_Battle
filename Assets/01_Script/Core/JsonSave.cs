using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;


public static class JsonSave<T>
{
    public static void Save(T data, string fileName)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/" + fileName, json);
    }

    public static T Load(T data, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log(json);
            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);

            Save(data, fileName);

            return Load(data, fileName);
        }
    }
}