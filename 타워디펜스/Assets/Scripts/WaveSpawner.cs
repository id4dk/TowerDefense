using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    //나중에 스테이지 올라갈때마다 난이도 업되야 하니까 timecount 안에 있는 변수 조절해주든가 하기

    public Transform enemyPrefabs;
    public Transform enemyPrefabs2;
    public Transform bossPrefabs;

    public Transform spawnPoint;

    public float timeCount = 5f;

    //다음 에너리 스폰 시간
    private float countdown = 2f;

    public Text waveCountdownText;

    public static int waveIndex = 1;

    //1단계 에너미 다 죽으면 2단계 에너미 나오게 만들 변수
    public static int EnemiesAlive = 0;

    public static bool isBoss = false;

    void Update()
    {
        if(countdown <= 0)
        {
            //SpawnWave(); 처음에 이렇게 하다가 코루틴 사용해서 바꿔줌
            StartCoroutine(SpawnWave());
            countdown = timeCount;
        }

        countdown -= Time.deltaTime;

        //text형인 waveCountdownText.text를 ToString으로 형변환 해줌.
        //mathf.Floor는 소숫점 첫째자리 뒤에꺼 다 잘라내줌
        //mathf.Round 로 바꿔줌. 반올림해주는 함수
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        Debug.Log("다음에너미 생성 대기중");

        waveIndex++;
        
        PlayerState.round++;
        
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            //void 대신 IEnumerator 사용하여 함수 만들고 WaitForSeconds(원하는 시간) 정지시켜줘서 에너미 나오는 텀 설정함.
            yield return new WaitForSeconds(0.5f);
        }

    }
    //에너미 몇마리 생성할지
    void SpawnEnemy()
    {
        //게임매니저에 에너미프리팹, 웨이포인트 넣어주기
        if (waveIndex < 5 && isBoss == false)
        {
            Instantiate(enemyPrefabs, spawnPoint.position, spawnPoint.rotation);
        }

        //두번째 에너미
        if ( (waveIndex > 5 && waveIndex < 9) && isBoss == false)
        {
            Instantiate(enemyPrefabs2, spawnPoint.position, spawnPoint.rotation);
        }

        //보스
        if (waveIndex > 10)
        {
            isBoss = true;
        }

        if(isBoss)
        {
            waveIndex = -50;
            //isBoss = false;
            Instantiate(bossPrefabs, spawnPoint.position, spawnPoint.rotation);
        }
    }
   
}
