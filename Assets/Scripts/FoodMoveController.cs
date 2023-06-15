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
    protected bool isHolding; // �߰�: ������ ��� �ִ� ���� ����
    protected Vector3 DragDirection; // �߰�: �巡�� ���� ����

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
            if (TouchHelper.IsUp && isHolding) // ����: ������ ��� �ִ� ���¿��� ��ġ�� ������ ��
            {
                OnPut(InputPosition);
                isHolding = false; // �߰�: ������ ��� �ִ� ���� ����
                HoldingObject = null; // ����: ��� �ִ� ���� ��ü �ʱ�ȭ
                return;
            }

            if (isHolding) // �߰�: ������ ��� �ִ� ������ ���� �̵�
            {
                Move(InputPosition);
            }
            return;
        }

        if (TouchHelper.IsDown)
        {
            // ����: ��ġ�� ��ġ�� ���� ����
            var spawnPos = mainCamera.ScreenToWorldPoint(new Vector3(InputPosition.x, InputPosition.y, CameraDistance));
            var index = Random.Range(0, prefab.Length);
            HoldingObject = Instantiate(prefab[index], spawnPos, Quaternion.identity);
            OnHold();
            isHolding = true; // �߰�: ������ ��� �ִ� ���·� ����
        }
    }

    protected virtual void OnPut(Vector3 pos)
    {
        HoldingObject.GetComponent<Rigidbody>().useGravity = true;

        // �巡�� ���� ��ġ�� ���� ��ġ ������ ���� ���� ���
        var direction = DragDirection.normalized;

        // �巡�� �Ÿ��� ���� ������ �� ���
        var delta = DragDirection.magnitude;
        var throwForce = delta * 4f; // �巡�� �Ÿ��� ����Ͽ� �� ����

        // �巡�� ������ y �� ������ �����Ͽ� ���� ������Ŵ
        var throwForceVector = new Vector3(direction.x, direction.y * 4f, direction.z) * throwForce;

        // ������ ����� ���� �����Ͽ� ��ü�� ����
        HoldingObject.GetComponent<Rigidbody>().AddForce(throwForceVector, ForceMode.Impulse);

        HoldingObject.transform.SetParent(null);
    }

    private void Move(Vector3 pos)
    {
        pos.z = mainCamera.nearClipPlane * CameraDistance;
        HoldingObject.transform.position = Vector3.Lerp(HoldingObject.transform.position,
            mainCamera.ScreenToWorldPoint(pos),
            Time.deltaTime * 7f);

        // �߰�: �巡�� ���� ������Ʈ
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

        // �߰�: �巡�� ���� �ʱ�ȭ
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