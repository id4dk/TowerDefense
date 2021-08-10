using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image image;

    //페이드인 페이드아웃 좀더 드라마틱하게
    public AnimationCurve curve;

    public GameObject active;

    public static bool isCheck = false;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    //다른 클래스에서 페이드 아웃 할수 있게 함수 생성
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        //애니메이션 적용. 1에서 0으로 가면 페이드인 되게
        float time = 1f;

        while(time > 0f)
        {
            // * 원하는 시간1f,2f, 이런식 1초,2초
            time -= Time.deltaTime * 0.4f;

            //커브 설정
            float alpha = curve.Evaluate(time);

            //커브 설정 안하고 시간으로만 해도됌.
            //image.color = new Color(0f, 0f, 0f, time);

            image.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;
        }
        //페이드 인 한번 하고 이미지 꺼줘서 메인으로, 다시하기 버튼 눌리게
        if (isCheck == false)
        {
            active.SetActive(false);
        }

        if(isCheck)
        {
            active.SetActive(true);
            image.enabled = false;
        }

    }

    IEnumerator FadeOut(string scene)
    {
        //페이드 인이랑 반대로
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * 0.4f;

            //커브 설정
            float alpha = curve.Evaluate(time);

            //커브 설정 안하고 시간으로만 해도됌.
            //image.color = new Color(0f, 0f, 0f, time);

            image.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;
        }
        //씬을 로드하고 방금 지정한 씬 가져와서 페이드 아웃
        SceneManager.LoadScene(scene);
    }

}
