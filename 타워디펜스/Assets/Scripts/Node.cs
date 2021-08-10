using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private Gold gold;
    private Shop shop;
    [HideInInspector]
    public NodeUI nodeUI;

    public Color hoverColor;
    public Color noMoneyColor;
    public Vector3 positionOffset;

    public GameObject turret;

    public TurretButtonUI turretButtonUI;
   
    public bool isUpgraded = false;

    private Renderer rend;

    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        //게임 시작했을때 컬러 넣어주기
        startColor = rend.material.color;

        buildManager = BuildManager.instance;

        gold = FindObjectOfType<Gold>();
        shop = FindObjectOfType<Shop>();

    }

    //업그레이드, 판매 UI 위치 잡아주는 함수
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    //마우스 클릭했을때
    void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild) return;

        BuildTurret(buildManager.GetTurretToBuild());

        if (turret != null)
        {
            Debug.Log("이미 배치되어 있습니다.");

            return;
        }
        
    }

    void BuildTurret(TurretButtonUI buttonUI)
    {
        if(PlayerState.Money < buttonUI.cost)
        {
            Debug.Log("돈부족함. 노드에 빌드터렛까지 들어옴");
            return;
        }

        PlayerState.Money -= buttonUI.cost;

        GameObject _turret = (GameObject)Instantiate(buttonUI.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretButtonUI = buttonUI;
    }

    public void UpgradeTurret()
    {
        if(PlayerState.Money < turretButtonUI.upgradeCost)
        {
            Debug.Log("업그레이드 비용이 부족합니다");
            return;
        }

        //업그레이드 비용 빼주기
        PlayerState.Money -= turretButtonUI.upgradeCost;
        //업그레이드하기위해 기존 터렛 제거
        Destroy(turret);

        //업그레이드 타워 생성
        GameObject _turret = (GameObject)Instantiate(turretButtonUI.upgradeedPrefab, GetBuildPosition(), Quaternion.identity);
        //업그레이드 터렛 생성했으니 터렛에 넣어주기
        turret = _turret;

        isUpgraded = true;

        Debug.Log("터렛 업그레이드 완료");
    }

    public void SellTurret()
    {
        PlayerState.Money += turretButtonUI.GetSellAmount();

        Destroy(turret);
        turretButtonUI = null;
    }

    //마우스 갔다댓을때 (호버링)
    void OnMouseEnter()
    {
        //시야 변경해해서 마우스가 UI랑 NODE랑 겹칠때 호버링 막아주는거
        if (EventSystem.current.IsPointerOverGameObject()) return;

        //이미 터렛이 지어져있다면 반응하지 마라
        if (!buildManager.CanBuild) return;

        if(buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            Debug.Log("nomoney");
            rend.material.color = noMoneyColor;
        }
       
    }
    //마우스 땟을때 컬러 원래대로
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
