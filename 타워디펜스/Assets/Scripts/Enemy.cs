using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    private Bullet bullet;
    private Life life;
    private Gold gold;
    private GameManager gameManager;
    public float speed = 10f;
    public float startSpeed = 12f;

    public float HP = 100f;
    public float damage = 10f;

    public static float bossHP = 1000f;
    public Image HPBar;

    private Transform target;
    private int wavePointIndex = 0;

    private float timer = 0.0f;
    private int waitingTime = 3;


    void Start()
    {
        target = WayPoints.points[0];

        life = FindObjectOfType<Life>();
        gold = FindObjectOfType<Gold>();
        bullet = FindObjectOfType<Bullet>();
        gameManager = FindObjectOfType<GameManager>();


    }

    public void TakeDamage()
    {
            HP -= damage;
            HPBar.fillAmount = HP / 100f;

            if (HP < 0)
            {
                Die();
            
            }

        if (WaveSpawner.isBoss)
        {
            bossHP -= damage;
            HPBar.fillAmount = HP / 10f;
            Debug.Log(bossHP);
            if (bossHP <= 0)
            {
                Die();
                gameManager.NextRound();
                Time.timeScale = 1f;
                WaveSpawner.isBoss = false;
                bossHP = 1000;
                
            }
        }

    }

    public void Slow()
    {
        if (speed >= 5)
        {
            //레이저빔 맞으면 속도 5까지 줄여줌
            speed--;
            //다음 5초동안 회복하고
            if (timer >= 3) speed = 10;
        }
        
        timer += Time.deltaTime;


        if (timer > waitingTime )
        {

            speed = 10;
            Debug.Log(speed);
            
            timer = 0;
        }

    }

    public void Die()
    {
        if(target != null)
        Destroy(gameObject);
        //에너미 죽이면 보여지는 골드 텍스트 올려주기
        PlayerState.Money += 10;

        //현재 살아있는 적의 수 에서 빼주기
        WaveSpawner.EnemiesAlive--;
    }

    void Update()
    {
        //웨이포인트에서 방향전환
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); //정규화. 항상 같은 속도 가지도록

        //웨이포인트 바라보게 하기
        transform.LookAt(target.position);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        //마지막 웨이포인트 도달시 에너미 삭제
        if (wavePointIndex >= WayPoints.points.Length -1)
        {
            Destroy(gameObject);

            if(PlayerState.Life > 0)
            PlayerState.Life -= 1;
            
            return;
        }
        wavePointIndex++;
        target = WayPoints.points[wavePointIndex];
    }
   
}
