using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    private UIDocument _doc;
    private VisualElement _root;
    private Button _battleBtn;
    private Button _gongBtn;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _battleBtn = _root.Q<Button>("BattleBtn");
        _gongBtn = _root.Q<Button>("GongBtn");
        _battleBtn.clicked += () => SceneLoad("PVP");
        _gongBtn.clicked += () => SceneLoad("MakeRobot");
    }

    void SceneLoad(string sceneString)
    {
        SceneManager.LoadScene(sceneString);
    }

}
