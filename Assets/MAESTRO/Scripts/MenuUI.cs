using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    private UIDocument _doc;
    public VisualTreeAsset _storyView;
    private VisualElement _root;
    private Button _battleBtn;
    private Button _gongBtn;
    private Button _makeBtn;
    private Button _storeBtn;
    private Button _garageBtn;


    VisualElement _storyElem;
    private Button _storyBtn;
    Button _storyExitBtn;
    //private StorySelectUI _stdUI;



    bool isStroyPanelON = false;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void Start()
    {
        // TEST CODE
        // SceneLoad("MetalBattleGround");
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _battleBtn = _root.Q<Button>("BattleBtn");
        _gongBtn = _root.Q<Button>("GongBtn");
        _makeBtn = _root.Q<Button>("MakeBtn");
        _storeBtn = _root.Q<Button>("StoreBtn");
        _garageBtn = _root.Q<Button>("GarageBtn");
        _storyBtn = _root.Q<Button>("StoryBtn");

        _makeBtn.clicked += () => SceneLoad("Gacha");
        // _battleBtn.clicked += () => SceneLoad("PVP");
        _battleBtn.clicked += () => SceneLoad("MetalBattleGround");
        _storyBtn.clicked += () => LoadStroyView();
        _gongBtn.clicked += () => SceneLoad("MakeRobot");
        _storeBtn.clicked += () => SceneLoad("Store");
        _garageBtn.clicked += () => SceneLoad("Garage");

        _storyView.CloneTree(_root);
        _storyElem = _root.Q<VisualElement>("StoryView");
        _storyExitBtn = _storyElem.Q<Button>("ExitBtn");

        _storyExitBtn.clicked += () => LoadStroyView();
        _storyElem.Blur();
        _storyElem.AddToClassList("off");
    }

    void SceneLoad(string sceneString)
    {
        //�ε�â ���弼��!
        //SceneSave.Instance.SceneSaveLogic(sceneString);
        SceneManager.LoadScene(sceneString);
    }


    void LoadStroyView()
    {

        if (_storyElem.ClassListContains("off") == false)
        {
            _storyElem.AddToClassList("off");
            Debug.Log("1");
        }
        else
        {
            _storyElem.RemoveFromClassList("off");
            Debug.Log("1");
        }

    }
}
