using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
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
    public VisualTreeAsset _getItem;
    VisualElement _resultVs;

    [SerializeField] public List<VisualElement> _result = new();


    void OnEnable()
    {
        _doc = GetComponent<UIDocument>();
        _root = _doc.rootVisualElement;

        exitbtn = _root.Q<Button>("ExitBtn");
        againbtn = _root.Q<Button>("AgainBtn");
        _resultVs = _root.Q<VisualElement>("ItemPanel");
        

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
        

        exitbtn.clicked += () => StoryLoadResource.Instance.Save(null);
        againbtn.clicked += () => StoryLoadResource.Instance.Save(null);
        

        if (PVPUI.cache_reward != null) {
            int i = 0;
            foreach(string puzzelID in PVPUI.cache_reward) {
                _getItem.CloneTree(_resultVs);

                var element = _resultVs.ElementAt(i);

                Texture2D tex = new Texture2D(0, 0);
                string path = $"Assets/MAESTRO/PartsPuzzleItem/{puzzelID}.png";
                byte[] byteTex = File.ReadAllBytes(path);
                tex.LoadImage(byteTex);

                element.Q("ItemImg").style.backgroundImage = new StyleBackground(tex);

                i ++;
            }

            PVPUI.cache_reward = null;
        }


        
    }
}
