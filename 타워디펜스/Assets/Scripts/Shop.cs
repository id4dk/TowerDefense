using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretButtonUI firstTurretPrefab;
    public TurretButtonUI missileLauncherPrefab;
    public TurretButtonUI laserTurretPrefab;

    BuildManager buildManager;
    private Gold gold;


    void Start()
    {
        buildManager = BuildManager.instance;

        gold = FindObjectOfType<Gold>();
    }

    public void SelectFirstTurret()
    {
        Debug.Log("첫번째 타워 클릭함");
        buildManager.SelectTurretToBuild(firstTurretPrefab);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("두번째 타워 클릭함");
        buildManager.SelectTurretToBuild(missileLauncherPrefab);
    }
    public void SelectLaserTurret()
    {
        Debug.Log("세번째 타워 클릭함");
        buildManager.SelectTurretToBuild(laserTurretPrefab);
    }
}
