using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    MetalBattleGround = 0,
    PVP = 1,
    LivePVP = 2,
    Store = 3,
    MakeRobot = 4,
    Garage = 5,
    Gacha = 6,
    StartScene = 7
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
    private GameObject _eCurrentScene, _eBeforeScene;
    [SerializeField] private GameObject[] _sceneGroup;
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

    public void SceneLoading(SceneType type)
    {
        if(_eBeforeScene != null)
        {
            Destroy(_eBeforeScene);
        }
        _eBeforeScene = _eCurrentScene;
        _eCurrentScene = Instantiate(_sceneGroup[(int)type]);

    }
}
