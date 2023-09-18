using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WareManager : ObjectManager
{
    public static WareManager Instance;
    public WareBase SelectWare;
    public List<GameObject> WarePrefabs = new List<GameObject>();
    public List<Sprite> WareSprites = new List<Sprite>();

    public override void SetInstance()
    {
        Instance = this;
    }

    public void CreateWare(WareType wType, bool isBlack, string ID)
    {
        Transform trm = MapManager.Instance.MapDatas[ID];
        SelectWare = Instantiate(isBlack ? WarePrefabs[(int)wType] : WarePrefabs[(int)wType + 6]).GetComponent<WareBase>();
        SelectWare.transform.position = new Vector3(trm.position.x, 25, trm.position.z);
        SelectWare.CurrentPos = ID;

        Sequence seq = DOTween.Sequence();
        seq.Join(SelectWare.transform.DOMoveY(10, 0.8f).SetEase(Ease.InExpo));
        seq.Join(SelectWare.transform.DOScale(new Vector3(5, 5, 5), 0.8f).SetEase(Ease.InExpo));
        EffectManager.Instance.CreateFX(trm, FXType.dust, 0.7f);

    }
}
