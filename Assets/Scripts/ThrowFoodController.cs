using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFoodController : FoodMoveController
{
    private Vector2 _inputPositionPivot;
    private bool _isHolding = false;
    private Rigidbody _heldRigidbody;
    public float throwForceMultiplier = 1f;
    public float gravityMultiplier = 1f;

    protected override void OnPut(Vector3 pos)
    {
        if (_isHolding && _heldRigidbody != null)
        {
            _heldRigidbody.useGravity = true;

            // 드래그 시작 위치와 현재 위치 사이의 방향 벡터 계산
            var direction = (new Vector3(pos.x, pos.y) - new Vector3(_inputPositionPivot.x, _inputPositionPivot.y)).normalized;

            // 드래그 거리에 따라 던지는 힘 계산
            var delta = (pos.magnitude - _inputPositionPivot.magnitude);
            var throwForce = delta * throwForceMultiplier;
            var throwForceVector = new Vector3(direction.x, direction.y, direction.y * throwForce);

            // 던지는 방향과 힘을 적용하여 물체를 던짐
            _heldRigidbody.AddForce(throwForceVector, ForceMode.Impulse);

            _heldRigidbody.transform.SetParent(null);
            _isHolding = false;
            _heldRigidbody = null;
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

                // 터치 시작 시 물체를 잡음
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit))
                {
                    _heldRigidbody = hit.collider.GetComponent<Rigidbody>();
                    if (_heldRigidbody != null)
                    {
                        _heldRigidbody.useGravity = false;
                        _heldRigidbody.transform.SetParent(transform);
                    }
                }
            }
        }
    }

    // 먹이가 화면 밖으로 나가면 삭제
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        // 물체가 드래그한 방향으로 이동하면서 아래로 떨어지도록 설정
        if (_heldRigidbody != null)
        {
            var velocity = _heldRigidbody.velocity;
            velocity.y = -Mathf.Abs(velocity.z * gravityMultiplier);
            _heldRigidbody.velocity = velocity;
        }
    }
}
