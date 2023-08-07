using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eSceneName
{
    None = -1,
    Loading,
    Logo,
    Title,
    Game
}

public enum SceneType
{

}

public class SceneMG : MonoBehaviour
{
    private static SceneMG _instance;
    public static SceneMG Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(SceneMG)) as SceneMG;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<SceneMG>();
                }
            }

            return _instance;
        }
    }

    private eSceneName _currentScene, _beforeScene;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SceneLoading()
    {

    }
}
