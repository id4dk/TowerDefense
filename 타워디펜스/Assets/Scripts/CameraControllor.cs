using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllor : MonoBehaviour
{
    //평평한 면에서 움직일때
    public float panSpeed = 30f;
    //마우스로 카메라 컨트롤 할때 마우스가 화면에서 얼마만큼 떨어져있을때 체크할 변수
    public float mouseToSide = 10f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;
    public bool toggle = false;
     
    void Update()
    {
        //카메라 락 토글키
        if (Input.GetKeyDown(KeyCode.Tab))
            toggle = !toggle;
        if (!toggle) return;

        //w키(소문자로 해야함) 마우스의 y축이 화면 끝으로 부터 mouseToSide 만큼 떨어져있을때
        //유니티 좌표는 화면 좌하단이 0,0임
        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - mouseToSide)
        {
            //만들때 local로 만들었는데 space.World로 하면 글로벌로 축 이동되서 내가 원하던 z축이동 가능
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= mouseToSide)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= mouseToSide)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - mouseToSide)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        //스크롤 컨트롤하려고. edit -> projectsetting -> mouse scrollwheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        //스크롤 휠 값이 작아서 1000정도 줬음
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        //스크롤 범위 컨트롤
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
