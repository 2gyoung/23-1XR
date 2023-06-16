using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/*
 평면 인식 후 위에 인디케이터 생성
 */

public class DishCreateController : MonoBehaviour
{

    public GameObject indicator;
    public GameObject bowl;
    ARRaycastManager arManager;
    GameObject placeObject;
    
    // Start is called before the first frame update
    void Start()
    {
        //시작시 인디케이터 비활성화
        indicator.SetActive(false);
        //AR Raycast Manager 컴포넌트 가져오기
        arManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectGround();
        // bowl 모델링 생성
        if (indicator.activeInHierarchy)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                if (placeObject == null)
                {
                    placeObject = Instantiate(bowl, indicator.transform.position, indicator.transform.rotation);
                    indicator.SetActive(false);
                }
            }
        }
    }

    //바닥 감지 및 인디케이터 출력
    void DetectGround()
    {
        //스크린의 중앙 지점 찾기
        Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitinfo = new List<ARRaycastHit>();

        //ray 이용해 바닥 감지
        if(arManager.Raycast(screenSize, hitinfo, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            //평면 감지시 인디케이터 활성화
            indicator.SetActive(true);

            //인디케이터의 위치와 회전값을 레이가 닿은 지점에 일치시킴
            indicator.transform.position = hitinfo[0].pose.position;
            indicator.transform.rotation = hitinfo[0].pose.rotation;

        }
        else
        {
            indicator.SetActive(false);
        }
    }
}
