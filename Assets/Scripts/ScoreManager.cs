using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

/*
 ���� ������ ���� �� ���� �� ����
 */

public class ScoreManager : MonoBehaviour
{

    public GameObject FoodGageUI;
    //public GameObject destroy;
    //����� �ִϸ����� ����
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
            //����� �ִϸ��̼� ����
            //playableDirector.gameObject.SetActive(true);
            //playableDirector.Play();
            foodGage.SetActive(false);
            FoodGageUI.SetActive(false);
            SucceessImage.SetActive(true);
        }
    }
}
