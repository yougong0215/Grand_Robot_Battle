using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLoadResource : Singleton<StoryLoadResource>
{
    public StoryScriptSO Init;
    private StoryUISO _info;
    public StoryScriptSO Out;
    //public BattleEnemySO _enemy;

    public bool isWin = false;

    public bool isBattle = false;

    public bool clear = false;
    public int stageInfo;

    public bool isIthave()
    {
        return _info != null;
    }
    
    public void Save(StoryUISO _so)
    {
        _info = _so;
    }
    
    public StoryUISO Loading()
    {

        return _info;
    }

    public StoryScriptSO ReturnStorySO()
    {
        if(isBattle==false)
        {
            return Init;
        }
        else
        {
            return Out;
        }
    }

    public void RemoveStory()
    {
        if (isBattle == false)
        {
            Init = null;
        }
        else
        {
            Out = null;
        }
    }

    public SceneEnum NextScene()
    {
        if(isBattle==false)
        {

            
            if(Init== null)
            {
                isBattle = true;
                return SceneEnum.PVP;
            }
            else
            {
                return SceneEnum.StoryScript;
            }
        }
        else
        {
            if(Out == null)
            {
                isBattle = false;
                return SceneEnum.GameEnd;
            }
            else
            {
                return SceneEnum.StoryScript;
            }
        }
    }
        
        
}