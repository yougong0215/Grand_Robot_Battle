using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ContentSelectUI : MonoBehaviour
{
    UIDocument _doc;
    VisualElement _root;

    Button _metalGroundBtn;
    Button _fastAtkBtn;
    Button _metalHellBtn;
    Button _metalMineBtn;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        _metalGroundBtn = _root.Q<Button>("MetalGroundBtn");
        _fastAtkBtn = _root.Q<Button>("FastAttackBtn");
        _metalHellBtn = _root.Q<Button>("MetalHellBtn");
        _metalMineBtn = _root.Q<Button>("MetalMineBtn");

        _metalGroundBtn.clicked += () => SceneManager.LoadScene("MetalBattleGround");
        _fastAtkBtn.clicked += () => SceneManager.LoadScene("FastAttack");
        _metalHellBtn.clicked += () => SceneManager.LoadScene("ScrapHell");
        _metalMineBtn.clicked += () => SceneManager.LoadScene("SteelMine");
    }
}
