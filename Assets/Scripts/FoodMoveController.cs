using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 터치 시 먹이 생성 기능 구현
 */

public class FoodMoveController : MonoBehaviour
{
    private const float CameraDistance = 7.5f;
    public float positionY = 0.4f;
    public GameObject[] prefab;

    protected Camera mainCamera;
    protected GameObject HoldingObject;
    protected Vector3 InputPosition;
    protected bool isHolding; // 추가: 음식을 들고 있는 상태 여부

    void Start()
    {
        mainCamera = Camera.main;
        Reset();
    }

    void Update()
    {
#if !UNITY_EDITOR
        if (Input.touchCount == 0) return;
#endif
        InputPosition = TouchHelper.TouchPosition;

        if (HoldingObject)
        {
            if (TouchHelper.IsUp && isHolding) // 수정: 음식을 들고 있는 상태에서 터치를 놓았을 때
            {
                OnPut(InputPosition);
                isHolding = false; // 추가: 음식을 들고 있는 상태 해제
                HoldingObject = null; // 수정: 들고 있던 음식 객체 초기화
                return;
            }

            if (isHolding) // 추가: 음식을 들고 있는 상태일 때만 이동
            {
                Move(InputPosition);
            }
            return;
        }

        if (TouchHelper.IsDown)
        {
            // 수정: 터치한 위치에 음식 생성
            var spawnPos = mainCamera.ScreenToWorldPoint(new Vector3(InputPosition.x, InputPosition.y, CameraDistance));
            var index = Random.Range(0, prefab.Length);
            HoldingObject = Instantiate(prefab[index], spawnPos, Quaternion.identity);
            OnHold();
            isHolding = true; // 추가: 음식을 들고 있는 상태로 변경
        }
    }

    protected virtual void OnPut(Vector3 pos)
    {
        /*        var rigidbody = HoldingObject.GetComponent<Rigidbody>();
                rigidbody.useGravity = true;
                var direction = mainCamera.transform.TransformDirection(Vector3.forward).normalized;
                var delta = (pos.y - InputPosition.y) * 100f / Screen.height;
                rigidbody.AddForce((direction + Vector3.up) * 2.5f * delta, ForceMode.Impulse); // 수정: 던지는 힘을 impulse로 변경
                HoldingObject.transform.SetParent(null);*/
        HoldingObject.GetComponent<Rigidbody>().useGravity = true;
        HoldingObject.transform.SetParent(null);
    }

    private void Move(Vector3 pos)
    {
        pos.z = mainCamera.nearClipPlane * CameraDistance;
        HoldingObject.transform.position = Vector3.Lerp(HoldingObject.transform.position,
            mainCamera.ScreenToWorldPoint(pos),
            Time.deltaTime * 7f);
    }

    protected virtual void OnHold()
    {
        HoldingObject.GetComponent<Rigidbody>().useGravity = false;

        HoldingObject.transform.SetParent(mainCamera.transform);
        HoldingObject.transform.rotation = Quaternion.identity;
        HoldingObject.transform.position =
            mainCamera.ViewportToWorldPoint(
                new Vector3(0.5f, positionY, mainCamera.nearClipPlane * CameraDistance));
    }

    private void Reset()
    {
        var pos = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, positionY, mainCamera.nearClipPlane * CameraDistance));
        var index = Random.Range(0, prefab.Length);
        var obj = Instantiate(prefab[0], pos, Quaternion.identity, mainCamera.transform);
        var rigidbody = obj.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
