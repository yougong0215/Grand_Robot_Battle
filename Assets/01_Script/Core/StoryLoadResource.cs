using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLoadResource : Singleton<StoryLoadResource>
{
    public StoryUISO _info;
    //public BattleEnemySO _enemy;

    public void Save(StoryUISO _so)
    {
        _info = _so;
    }
    
    public StoryUISO Loading()
    {
        return _info;
    }
        
        
}