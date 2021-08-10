using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    private Enemy enemy;

    [Header("General")] 

    //감지범위
    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    //발사 속도
    public float fireRate = 1f;
    //발사 딜레이
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;


    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;

    public float turnSpeed = 10f;

    public Transform firePoint;

    void Start()
    {
        // 0초후에, 0.5초마다 UpdateTarget 함수 실행시켜라.
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        enemy = FindObjectOfType<Enemy>();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        //적이랑 거리가 가장 가깝다면
        float shortestDistance = Mathf.Infinity;
        //지금까지 발견한 가장 가까운적 디폴트로
        GameObject nearestEnemy = null;

        //적이랑 거리계산
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

    void Update()
    {
        if(target == null)
        {
            //레이저 쏘다가 범위 벗어나면 중지시키게
            if(useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    //임팩트 스탑
                    impactEffect.Stop();
                }
            }

            return;
        }

        LockOnTarget();

        if(useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

    }

    //터렛 회전
    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //y축만 돌아가게
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
        
    //레이저 발사
    void Laser()
    {
        target.GetComponent<Enemy>().Slow();

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            //레이저 닿았을때 임팩트 활성화
            impactEffect.Play();
        }

        //내가 쏘는위치, 타겟위치
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        //레이저 임팩트 만들면서 추가. 6월3일
        impactEffect.transform.position = target.position;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

    }

    void Shoot()
    {
        //GameObject로 형변환 필수 
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }

    }

    //유니티 콜백함수. 차트를 선택한 경우에만 범위가 그려짐.
    private void OnDrawGizmosSelected()
    {
        //공격 범위 컬러 선택
        Gizmos.color = Color.white;
        //공격 범위 그려주기
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
