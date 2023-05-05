using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    [SerializeField] Canvas canvas;

    public Transform _oldParent;

    private void Awake()
    {
        canvas = GameObject.Find("UIManager").GetComponent<Canvas>();

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if(transform.parent.name == "Selected")
        {
            StartCoroutine(DelayOBJ());
        }

        transform.parent = canvas.transform;
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        rectTransform.sizeDelta = new Vector2(120, 120);
    }

    IEnumerator DelayOBJ()
    {
        yield return new WaitUntil(()=>UISlotManager.Instance.SoltUI);
        UISlotManager.Instance.SoltUI.GetComponent<PartUIInfo>().Seleted(null, GetComponent<UnitPart>().PartSO.Type);
        UISlotManager.Instance.SoltUI.obj = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 이전 이동과 비교해서 얼마나 이동했는지를 보여줌
        // 캔버스의 스케일과 맞춰야 하기 때문에
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (UISlotManager.Instance.SoltUI)
        {
            transform.parent = UISlotManager.Instance.SoltUI.transform;
            transform.position = UISlotManager.Instance.SoltUI.transform.position;

            if(UISlotManager.Instance.SoltUI.obj != null)
                UISlotManager.Instance.SoltUI.obj.ReturnParent();


            UISlotManager.Instance.SoltUI.GetComponent<PartUIInfo>().Seleted(GetComponent<UnitPart>().PartSO);

            UISlotManager.Instance.SoltUI.obj = this;

            UISlotManager.Instance.NullUIS();

        }
        else
        {
            ReturnParent();
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }

    public void ReturnParent()
    {
        rectTransform.sizeDelta = new Vector2(100, 100);
        transform.parent = _oldParent;
    }
}