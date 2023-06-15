using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMoveController : MonoBehaviour
{
    private const float CameraDistance = 7.5f;
    public float positionY = 0.4f;
    public GameObject[] prefab;

    protected Camera mainCamera;
    protected GameObject HoldingObject;
    protected Vector3 InputPosition;
    protected bool isHolding; // 추가: 음식을 들고 있는 상태 여부
    protected Vector3 DragDirection; // 추가: 드래그 방향 벡터

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
        HoldingObject.GetComponent<Rigidbody>().useGravity = true;

        // 드래그 시작 위치와 현재 위치 사이의 방향 벡터 계산
        var direction = DragDirection.normalized;

        // 드래그 거리에 따라 던지는 힘 계산
        var delta = DragDirection.magnitude;
        var throwForce = delta * 4f; // 드래그 거리에 비례하여 힘 증가

        // 드래그 방향의 y 축 성분을 강조하여 힘을 증가시킴
        var throwForceVector = new Vector3(direction.x, direction.y * 4f, direction.z) * throwForce;

        // 던지는 방향과 힘을 적용하여 물체를 던짐
        HoldingObject.GetComponent<Rigidbody>().AddForce(throwForceVector, ForceMode.Impulse);

        HoldingObject.transform.SetParent(null);
    }

    private void Move(Vector3 pos)
    {
        pos.z = mainCamera.nearClipPlane * CameraDistance;
        HoldingObject.transform.position = Vector3.Lerp(HoldingObject.transform.position,
            mainCamera.ScreenToWorldPoint(pos),
            Time.deltaTime * 7f);

        // 추가: 드래그 방향 업데이트
        DragDirection = pos - InputPosition;
    }

    protected virtual void OnHold()
    {
        HoldingObject.GetComponent<Rigidbody>().useGravity = false;

        HoldingObject.transform.SetParent(mainCamera.transform);


        HoldingObject.transform.rotation = Quaternion.identity;
        HoldingObject.transform.position =
        mainCamera.ViewportToWorldPoint(
        new Vector3(0.5f, positionY, mainCamera.nearClipPlane * CameraDistance));

        // 추가: 드래그 방향 초기화
        DragDirection = Vector3.zero;
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