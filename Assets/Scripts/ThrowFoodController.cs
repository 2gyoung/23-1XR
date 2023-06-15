using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 ���� ������ ��� ����
 */

public class ThrowFoodController : FoodMoveController
{
    private Vector2 _inputPositionPivot;
    public float throwForce = 100f;
    private bool _isHolding = false;



    protected override void OnPut(Vector3 pos)
    {
        if (_isHolding)
        {
            var rigidbody = HoldingObject.GetComponent<Rigidbody>();
            rigidbody.useGravity = true;
            var direction = mainCamera.transform.forward;
            var delta = (pos.y - _inputPositionPivot.y) * 100f / Screen.height;
            var throwForce = 1000f * delta;
            var throwVector = direction + mainCamera.transform.up + mainCamera.transform.forward * throwForce;
            rigidbody.AddForce((direction + Vector3.up) * 4.5f * delta);
            HoldingObject.transform.SetParent(null);
            _isHolding = false;

        }
    }



    protected override void OnHold()
    {
        if (!_isHolding && Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _inputPositionPivot = touch.position;
                _isHolding = true;
            }
        }
    }

    // ���̰� ȭ�� ������ ������ ����
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
