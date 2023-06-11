using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolBox : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _content;

    private void Awake()
    {
        _canvas = GameObject.Find("ScrollCanvas");
        _content = GameObject.Find("ScrollCanvas/EnforecePaZe/Scroll View/Viewport/Content");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.parent = _canvas.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;
        this.transform.position = currentPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.parent = _content.transform;
    }
}
