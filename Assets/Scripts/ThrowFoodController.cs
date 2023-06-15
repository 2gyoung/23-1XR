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

            // �巡�� ���� ��ġ�� ���� ��ġ ������ ���� ���� ���
            var direction = (new Vector3(pos.x, pos.y) - new Vector3(_inputPositionPivot.x, _inputPositionPivot.y)).normalized;

            // �巡�� �Ÿ��� ���� ������ �� ���
            var delta = (pos.magnitude - _inputPositionPivot.magnitude);
            var throwForce = delta * throwForceMultiplier;
            var throwForceVector = new Vector3(direction.x, direction.y, direction.y * throwForce);

            // ������ ����� ���� �����Ͽ� ��ü�� ����
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

                // ��ġ ���� �� ��ü�� ����
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

    // ���̰� ȭ�� ������ ������ ����
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        // ��ü�� �巡���� �������� �̵��ϸ鼭 �Ʒ��� ���������� ����
        if (_heldRigidbody != null)
        {
            var velocity = _heldRigidbody.velocity;
            velocity.y = -Mathf.Abs(velocity.z * gravityMultiplier);
            _heldRigidbody.velocity = velocity;
        }
    }
}
