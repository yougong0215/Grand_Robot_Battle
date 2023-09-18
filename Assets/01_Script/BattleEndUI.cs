using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BattleEndUI : MonoBehaviour
{
    UIDocument _doc;
    VisualElement _root;

    StoryUISO _so;
    Button exitbtn;
    Button againbtn;
    private Label _stageName;
    void OnEnable()
    {
        _doc = GetComponent<UIDocument>();
        _root = _doc.rootVisualElement;

        exitbtn = _root.Q<Button>("ExitBtn");
        againbtn = _root.Q<Button>("AgainBtn");
        
        

        _so = StoryLoadResource.Instance.Loading();

        string stageStd = "";
        if (_so == null)
        {
            stageStd += "배틀 - ";
           
            if (StoryLoadResource.Instance.isWin)
            {
                stageStd += "승리";
            }
            else
            {
                stageStd += "패배";
            }
             againbtn.clicked += () => SceneManager.LoadScene((int)SceneEnum.GameMatching);
        }
        else
        {
            stageStd += _so.TitleName;
            if (StoryLoadResource.Instance.isWin)
            {
                stageStd += $" - 클리어!";
            }
            else
            {
                stageStd += $" - 패배";
            }
            againbtn.clicked += () => SceneManager.LoadScene((int)SceneEnum.Story);
        }

        _stageName = _root.Q<Label>("Stage");
        _stageName.text = stageStd;
        
        
        exitbtn.clicked += () => LoadManager.LoadScene(SceneEnum.Menu);
        
        if(_so != null)
        {
            exitbtn.clicked += () => StoryLoadResource.Instance.Save(null);
            againbtn.clicked += () => StoryLoadResource.Instance.Save(null);
        }




        
    }
}
