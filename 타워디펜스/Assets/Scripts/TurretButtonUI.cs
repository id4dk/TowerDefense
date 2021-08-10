using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Shop클래스에서 지금 보는 클래스 가져와서 프리팹들 선언 해주니 컴포넌트에서 부착가능하게됌.
[System.Serializable]
public class TurretButtonUI 
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradeedPrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }

}
