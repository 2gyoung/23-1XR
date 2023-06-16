using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 먹이 던지기 기능 구현
 */

public class ThrowFoodController : FoodMoveController
{
    private Vector2 _inputPositionPivot;
    public float throwForce = 100f;
    //private bool _isHolding = false;


    protected override void Update()
    {
        //Debug.Log("Please...");
        //Nothing...
    }
    protected override void OnPut(Vector3 pos)
    {
        //Debug.Log("ThrowFoodController::OnPut"); //작동안함
        if (isHolding)
        {
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.useGravity = true;
            var direction = mainCamera.transform.forward;
            var delta = (pos.y - _inputPositionPivot.y) * 100f / Screen.height;
            var throwForce = 2.5f * delta;
            var throwVector = direction + mainCamera.transform.up + mainCamera.transform.forward * throwForce;
            rigidbody.AddForce((direction + Vector3.up) * 4.5f * delta);
            //rigidbody.velocity = (direction + Vector3.up) * 4.5f * delta;
            Debug.Log((direction + Vector3.up) * 4.5f * delta);
            transform.SetParent(null);
            
            isHolding = false;

        }
    }
    protected override void Reset()
    {

        //Nothing...

    }



    protected override void OnHold()
    {
        if (!isHolding && Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _inputPositionPivot = touch.position;
                isHolding = true;
            }
        }
    }

    // 먹이가 화면 밖으로 나가면 삭제
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}