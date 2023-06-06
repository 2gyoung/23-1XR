using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

/*
 먹이 게이지 관리 및 성공 시 연출
 */

public class DishDirector : MonoBehaviour
{

    public GameObject succeess;
    //public GameObject destroy;
    //고양이 애니메이터 변수
    //public PlayableDirector playableDirector;
    public GameObject foodGage;
    
    // Start is called before the first frame update
    void Start()
    {
        this.foodGage = GameObject.Find("FoodGage");
        succeess.SetActive(false);
        this.foodGage.GetComponent<Image>().fillAmount = 0f;
    }

    public void IncreaseFood()
    {
        this.foodGage.GetComponent<Image>().fillAmount += 0.5f;
        if(this.foodGage.GetComponent<Image>().fillAmount == 1.0f)
        {
            //destroy.SetActive(false);
            //고양이 애니메이션 등장
            //playableDirector.gameObject.SetActive(true);
            //playableDirector.Play();
            foodGage.SetActive(false);
            succeess.SetActive(true);
        }
    }
}
