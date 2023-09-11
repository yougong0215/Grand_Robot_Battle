using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/StorySO")]
public class StoryUISO : ScriptableObject
{
    [Header("Title")]
    public string TitleName;
    public Color TitlePanelColor;
    public Texture2D StageSprite;
    [TextArea] public string StageExample;

    [Header("Info")]
    public string EnemyName;
    public Texture2D EnemySprite;
    [TextArea] public string EnemyInfo;


}
