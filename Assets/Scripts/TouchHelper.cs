using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TouchHelper 
{
#if UNITY_EDITOR

    /*우클릭 시 오브젝트 생성
    좌클릭 후 누른 상태로 드래그 시 오브젝트 이동
    */
    public static bool Touch2 => Input.GetMouseButtonDown(1);
    public static bool IsDown => Input.GetMouseButtonDown(0);
    public static bool IsUp => Input.GetMouseButtonUp(0);
    public static Vector2 TouchPosition => Input.mousePosition;

#else

    //손가락 2개 동시에 터치(TouchCount == 2) 오브젝트 생성, 드래그 시 이동
    public static bool Touch2 => Input.touchCount == 2 && (Input.GetTouch(1).phase == TouchPhase.Began);
    public static bool IsDown => Input.GetTouch(0).phase == TouchPhase.Began;
    public static bool IsUp => Input.GetTouch(0).phase == TouchPhase.Ended;
    public static Vector2 TouchPosition => Input.GetTouch(0).position;
#endif
}