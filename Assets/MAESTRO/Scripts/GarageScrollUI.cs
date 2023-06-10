using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GarageScrollUI : MonoBehaviour
{
    private List<GameObject> _cardList = new List<GameObject>();
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _PartsCardPrefab;

    [SerializeField] private Color _commonColor;
    [SerializeField] private Color _uniqueColor;
    [SerializeField] private Color _masterPiece;

    

    public void SortingScroll(string partsType)
    {
        /* ����ڰ� ��ư�� ���� �� ������ ��ư Ÿ�Կ� �°� ���� ī����� �����ϴ� �ڵ� �Դϴ�.
         * "All", "Head", "Body", "LA", "RA", "Leg" �����ϱ� ����ġ�� ���̽���
         * �����Ϳ��� ���� Ÿ�� �����ϴ� �ڵ� �ۼ��� ���ô�.
         * ����� LA�� LeftArm, RA�� RightArm�Դϴ�.
         */
        SetInfo(); // for�� ������
    }

    private void SetInfo(/*������*/)
    {
        GameObject _partsCard = Instantiate(_PartsCardPrefab, _content.transform);

        TextMeshProUGUI _cardName = _partsCard.transform.Find("PartsImageBack/Image/NamePanel/NameText").GetComponent<TextMeshProUGUI>();
        Image _partsTypeImage = _partsCard.transform.Find("PartsImageBack/Image/NamePanel/PartsTypeImage").GetComponent<Image>();
        Image _partsImage = _partsCard.transform.Find("PartsImageBack/PartsImage").GetComponent<Image>();
        Image _partsRateImage = _partsCard.transform.Find("PartsImageBack").GetComponent<Image>();

        //������� �̸�, ���� ���� �̹���, ���� �̹���, ���� ��� ��(���߿� �ؽ��� �޾Ƽ� ���� ����)
        //���� ��� ���� ����� �޾Ƽ� ��޿� �´� ������ �ٲ��ֻ��� ���� ������ �����ϴ�.
        GaragePartsCard gpc = _partsCard.GetComponent<GaragePartsCard>();
        // ���� ���� ���ּ��� gpc.InfoSet();
        _cardList.Add(_partsCard);
    }

    private void ResetSkillCard()
    {
        // ���� ���̴� ī����� �о������ �ڵ��Դϴ�.
        foreach(GameObject card in _cardList)
        {
            Destroy(card);
        }
    }
}
