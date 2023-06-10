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
        /* 사용자가 버튼을 누를 시 선택한 버튼 타입에 맞게 파츠 카드들을 정렬하는 코드 입니다.
         * "All", "Head", "Body", "LA", "RA", "Leg" 들어오니까 스위치로 케이스로
         * 데이터에서 파츠 타입 구별하는 코드 작성해 봅시다.
         * 참고로 LA는 LeftArm, RA는 RightArm입니다.
         */
        SetInfo(); // for문 돌리기
    }

    private void SetInfo(/*데이터*/)
    {
        GameObject _partsCard = Instantiate(_PartsCardPrefab, _content.transform);

        TextMeshProUGUI _cardName = _partsCard.transform.Find("PartsImageBack/Image/NamePanel/NameText").GetComponent<TextMeshProUGUI>();
        Image _partsTypeImage = _partsCard.transform.Find("PartsImageBack/Image/NamePanel/PartsTypeImage").GetComponent<Image>();
        Image _partsImage = _partsCard.transform.Find("PartsImageBack/PartsImage").GetComponent<Image>();
        Image _partsRateImage = _partsCard.transform.Find("PartsImageBack").GetComponent<Image>();

        //순서대로 이름, 파츠 종류 이미지, 파츠 이미지, 파츠 등급 색(나중에 텍스쳐 받아서 수정 예정)
        //파츠 등급 색은 등급을 받아서 등급에 맞는 색갈로 바꿔주새요 위에 선언해 놨습니다.
        GaragePartsCard gpc = _partsCard.GetComponent<GaragePartsCard>();
        // 정보 셋팅 해주세용 gpc.InfoSet();
        _cardList.Add(_partsCard);
    }

    private void ResetSkillCard()
    {
        // 현재 보이는 카드들을 밀어버리는 코드입니다.
        foreach(GameObject card in _cardList)
        {
            Destroy(card);
        }
    }
}
