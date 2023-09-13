using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLoadResource : Singleton<StoryLoadResource>
{
    private StoryUISO _info;
    //public BattleEnemySO _enemy;

    public bool clear = false;

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
        
        
}