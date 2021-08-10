using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Transform target;

    public GameObject BulletEffect;

    public float speed = 70;
    public float explosionRadius = 0;  //폭발 반경

    public static int score;

    //프로퍼티 만들기
    public float ExplosionRadius { get { return explosionRadius; } set { explosionRadius = value; } }


    //목표 탐색
    public void Seek(Transform _target)
    {
        ExplosionRadius = 0;
        target = _target;
        
    }

    void Update()
    {
        if(target == null)
        {
            //처리시간때문에 해줘야할듯
            Destroy(gameObject);
            return;
        }

        //날아가는 방향 바라보게
        Vector3 dir = target.position - transform.position;

        float distanceFrame = speed * Time.deltaTime;

        //목표물에 도달할때마다
        if(dir.magnitude <= distanceFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceFrame, Space.World);

        transform.LookAt(target);

        //부딪힌다면 총알 파괴
        void HitTarget()
        {
            GameObject effectIns =  (GameObject)Instantiate(BulletEffect, transform.position, transform.rotation);
            Destroy(effectIns, 5f);     //이펙트

            //폭발반경이 0보다 크다면
            if(explosionRadius > 0)
            {
                //스플래쉬대미지
                Explode();
            }
            else
            {
                //정통으로 맞은 대미지
                Damage(target);
            }

            // Destroy(target.gameObject); //맞자마자 에너미 파괴
            Destroy(gameObject);

                score += 127;
        }
        
        //스플레쉬 데미지
        void Explode()
        {
            //콜라이더 배열에 
            //Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            //foreach (Collider collider in colliders)
            //{
            //    if(collider.tag == "Enemy")
            //    {
            //        Damage(collider.transform);
            //    }
            //}
        }

        //불렛과 에너미 충돌하면 체력바 깎기.
        void Damage(Transform enemy)
        {
           //중요.. 
           enemy.GetComponent<Enemy>().TakeDamage();
        }

        //미사일 폭발반경 그려주기
        //void OnDrawGizmoSelected()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawSphere(transform.position, explosionRadius); 
        //}

    }
}
