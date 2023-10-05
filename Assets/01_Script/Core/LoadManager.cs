using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneEnum
{
    StartScene = 0,
    Menu,
    PVP,
    MakeRobot,
    Gacha,
    Store,
    Garage,
    GameMatching,
    SelectStoreScene,
    Story,
    GameEnd,
    InvenTory,
    StoryScript,
    Size,
}



public static class LoadManager
{
    public static Stack<int> _loadStack = new Stack<int>();

    static int nowScene = 1;
    public static void LoadScene(SceneEnum enums, bool SceneReturn = false)
    {
        // ?????�ъ냼???�덈???��?task泥섎????�㈃ ??�꺼二쇨�?



        if(SceneReturn== false)
        {
            _loadStack.Push(nowScene);
        }
        nowScene = (int)enums;
        
        if(enums == SceneEnum.Menu || enums == SceneEnum.StartScene)
        {
            _loadStack.Clear();
            nowScene = 1;
            //_loadStack.Push(nowScene);
            SceneManager.LoadScene((int)enums);
            return;
        }
        
        
        SoundManager.Instance.SceneLoadDestory();
        
        SceneManager.LoadScene(nowScene);
        Debug.LogError($"{_loadStack.Count} | {enums}");
    }

    public static void ReturnBack()
    {
        int temp =  _loadStack.Pop();
        if(_loadStack.Count == 0)
        {
            LoadManager.LoadScene(SceneEnum.Menu, true);
            return;
        }
        LoadScene((SceneEnum)temp, true);
    }
}
