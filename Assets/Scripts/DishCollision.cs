using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 ��׸��� ���� �浹 �ν�
 */

public class DishCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        
        //��׸��� ����
        if(other.gameObject.tag == "Food")
        {
            GameObject director = GameObject.Find("DishDirector");
            director.GetComponent<DishDirector>().IncreaseFood();
        }
    }
}
