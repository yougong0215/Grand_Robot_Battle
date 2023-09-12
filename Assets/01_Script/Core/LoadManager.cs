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
    Size,
}



public static class LoadManager
{
    public static Stack<int> _loadStack = new Stack<int>();

    static int nowScene = 1;
    public static void LoadScene(SceneEnum enums, bool SceneReturn = false)
    {
        // ?????�ъ냼???�덈???��?task泥섎????�㈃ ??�꺼二쇨�?

        if(enums == SceneEnum.Menu || enums == SceneEnum.StartScene)
        {
            _loadStack.Clear();
            nowScene = 1;
            _loadStack.Push(nowScene);
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
            Debug.LogError("??�굅 ?�덈?�硫???�릺?붾뜲 ?�덈????��?: _loadStack(SceneManager) is Empty");
            return;
        }

        int temp =  _loadStack.Pop();
                Debug.LogError(temp);
        LoadScene((SceneEnum)temp, true);
    }
}
