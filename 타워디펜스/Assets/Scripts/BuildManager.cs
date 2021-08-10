using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //노드 하나하나에 추가해주면 힘드니까 싱글톤화 시킬거임
    public static BuildManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("빌드 매니저 두개 돌아가는듯");
            return;
        }

        //재귀함수로 만들어주고 매번 빌드관리자가 되어 게임실행
        instance = this;    
    }

    public GameObject firstTurretPrefab;
    public GameObject missileLauncherPrefab;
    public GameObject laserTurretPrefab;
   
    public TurretButtonUI turretToBuild;

    //프로퍼티 생성. 빌드 할수 있는지 없는지 체크용
    public bool CanBuild
    {
        get { return turretToBuild != null; }
    }
    //비용보다 머니가 많을때만 체크해주게 프로퍼티 생성
    public bool HasMoney
    {
        get { return PlayerState.Money >= turretToBuild.cost; }
    }

    //업그레이드, 팔기 기능 담당.
    private Node selectNode;
    public NodeUI nodeUI;

    public TurretButtonUI GetTurretToBuild()  
    {
        return turretToBuild;
    }

    //업그레이드,판매 만들때 추가
    public void SelectNode(Node node)
    {
        if(selectNode == node)
        {
            DeselectNode();
            return;
        }

        selectNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretButtonUI turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }
}
