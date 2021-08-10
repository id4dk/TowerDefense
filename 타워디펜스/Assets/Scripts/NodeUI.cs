using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;

    public GameObject ui;

    //업그레이드 버튼 ui 텍스트
    public Text upgradeCost;
    //판매 버튼 ui 텍스트
    public Text sellAmount;
    //업그레이드 버튼
    public Button upgradeButton;

    public void SetTarget(Node _target)
    {
        target = _target;
        //업그레이드, 판매 UI 위치 잡아주는 함수.
        transform.position = target.GetBuildPosition();

        //업그레이드 전 기본상태
        if(!target.isUpgraded)
        {
            upgradeCost.text = "Upgrade\n -" + target.turretButtonUI.upgradeCost + "G";
            //interactable = true // 버튼 활성화
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "업그레이드 완료";
            //interactable = false //버튼 비활성화. 눌러도 응답없게
            upgradeButton.interactable = false;
        }

        sellAmount.text = "Sell\n +" + target.turretButtonUI.GetSellAmount() + "G";


        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}
