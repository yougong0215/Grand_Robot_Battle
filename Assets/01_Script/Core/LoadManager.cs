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
        // ?異?由ъ냼??遺덈윭?ㅺ퀬 task泥섎━ ?섎㈃ ?섍꺼二쇨린

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
            Debug.LogError("?닿굅 遺덈━硫??덈릺?붾뜲 遺덈┝ ?뉎뀋 : _loadStack(SceneManager) is Empty");
            return;
        }

        int temp =  _loadStack.Pop();
        LoadScene((SceneEnum)temp, true);
    }
}
