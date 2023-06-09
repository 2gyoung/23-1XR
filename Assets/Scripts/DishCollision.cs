using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 ¹ä±×¸©°ú ¸ÔÀÌ Ãæµ¹ ÀÎ½Ä
 */

public class DishCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        
        //¹ä±×¸©°ú Á¢ÃË
        if(other.gameObject.tag == "Food")
        {
            GameObject smObject = GameObject.Find("ScoreManager");
            smObject.GetComponent<ScoreManager>().IncreaseFood();
        }
    }
}
