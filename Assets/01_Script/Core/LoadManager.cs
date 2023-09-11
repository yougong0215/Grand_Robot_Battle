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
    Size,
    GameEnd
}



public static class LoadManager
{
    public static Stack<int> _loadStack = new Stack<int>();

    static int nowScene = 1;
    public static void LoadScene(SceneEnum enums, bool SceneReturn = false)
    {
        // ?????±ÑŠëƒ¼???ºëˆ???ºí€?taskï§£ì„????ãˆƒ ??êº¼äºŒì‡¨ë¦?

        if(enums == SceneEnum.Menu || enums == SceneEnum.StartScene)
        {
            _loadStack.Clear();
            nowScene = 1;
        }
        else{
            if(SceneReturn== false)
            {
                _loadStack.Push(nowScene);
            }
        }

        nowScene = (int)enums;
        SceneManager.LoadScene((int)enums);
    }

    public static void ReturnBack()
    {
        if(_loadStack.Count == 0)
        {
            Debug.LogError("??¿êµ… ?ºëˆ?ï§???ˆë¦º?ë¶¾ëœ² ?ºëˆ????ë€?: _loadStack(SceneManager) is Empty");
            return;
        }

        int temp =  _loadStack.Pop();
        LoadScene((SceneEnum)temp, true);
    }
}
