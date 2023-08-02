using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FindManager : Singleton<FindManager>
{
    Dictionary<string, GameObject> FindDirectory = new();
    
    void Awake()
    {
        SceneManager.sceneLoaded += ClearDic;
    }

    private void ClearDic(Scene arg0, LoadSceneMode arg1)
    {
        FindDirectory.Clear();
    }

    public GameObject FindObject(string key, string name = "Not_Select")
    {



        if (FindDirectory.ContainsKey(key))
        {
            return FindDirectory[key];
        }

        if (name == "Not_Select")
            name = key;

        FindDirectory[key] = GameObject.Find(name);
        return FindDirectory[key];
    }

    public GameObject FindByTransformObject(string key, Transform transforms, string name = "Not_Select")
    {


        if (FindDirectory[key] != null)
        {
            return FindDirectory[key];
        }

        if (name == "Not_Select")
            name = key;

        FindDirectory[key] = transforms.Find(name).gameObject;
        return FindDirectory[key];
    }


    
}
