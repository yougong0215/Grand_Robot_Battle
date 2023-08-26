using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Size
}



public static class LoadManager
{
    public static Stack<int> _loadStack = new Stack<int>();

    static int nowScene = 0;
    public static void LoadScene(SceneEnum enums, bool SceneReturn = false)
    {
        // 대충 리소스 불러오고 task처리 되면 넘겨주기

        if(enums == SceneEnum.Menu || enums == SceneEnum.StartScene)
        {
            _loadStack.Clear();
        }
        else{
            if(SceneReturn== false)
            {
                _loadStack.Push(nowScene);
            }
        }

        nowScene = (int)enums;
    }

    public static void ReturnBack()
    {
        if(_loadStack.Count == 0)
        {
            Debug.LogError("이거 불리면 안되는데 불림 ㅇㅇ : _loadStack(SceneManager) is Empty");
            return;
        }

        int temp = _loadStack.Pop();

        LoadScene((SceneEnum)temp, true);
    }
}
