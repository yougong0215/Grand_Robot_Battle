using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RatingType
{
    common,
    unique,
    masterpiece
}

public class PartsPieceItem
{
    public PartSO PartSo;
    public int count;
    public string Info;
}

public class HavingItemList : MonoBehaviour
{
    public PartsPieceItem SelectPPI;
    public List<PartsPieceItem> HavingPPIList = new List<PartsPieceItem>();

    private int[] _needToRate = { 20, 50, 100 };

    public void MakeParts(RatingType rt)
    {
        if (SelectPPI.count > _needToRate[(int)rt])
        {
            SelectPPI.count -= _needToRate[(int)rt];
            // 파츠 리스트에 넣기
            if(SelectPPI.count == 0)
            {
                HavingPPIList.Remove(SelectPPI);
            }
        }
        else
        {
            // 파츠 제작 실패 UI 제작
        }
    }
}
