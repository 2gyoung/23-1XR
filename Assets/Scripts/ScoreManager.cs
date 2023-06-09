using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

/*
 먹이 게이지 관리 및 성공 시 연출
 */

public class ScoreManager : MonoBehaviour
{

    public GameObject FoodGageUI;
    //public GameObject destroy;
    //고양이 애니메이터 변수
    //public PlayableDirector playableDirector;
    public GameObject foodGage;
    public GameObject SucceessImage;
    
    // Start is called before the first frame update
    void Start()
    {
        this.foodGage = GameObject.Find("FoodGage");
        this.foodGage.GetComponent<Image>().fillAmount = 0f;
        SucceessImage.SetActive(false);
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
            FoodGageUI.SetActive(false);
            SucceessImage.SetActive(true);
        }
    }
}
