using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System;

public class SceneSave : MonoBehaviour
{
    GameObject UITOOLKIT;
    Button back;
    [SerializeField] private VisualTreeAsset _backBtn;
    public static SceneSave Instance;
    string beforeScene;
    Scene current;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("!!!!!");
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
        current = SceneManager.GetActiveScene();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnAddBack;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnAddBack;
    }

    private void OnAddBack(Scene scene, LoadSceneMode mode)
    {
        UITOOLKIT = GameObject.Find("UITOOLKIT");
        UIDocument _doc = UITOOLKIT.GetComponent<UIDocument>();
        VisualElement _root = _doc.rootVisualElement;
        if(_root.Contains(_root.Q<Button>("BackBtn")) && scene.name != "Menu")
        {
            return;
        }
        VisualElement _back = _backBtn.Instantiate();
        _root.Add(_back);
        _back.style.left = 40;
        _back.style.top = 40;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(beforeScene);
    }

    public void SceneSaveLogic(string scene)
    {
        beforeScene = scene;
    }
}
