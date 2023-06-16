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

    // 먹이 게이지 관리 UI
    public GameObject FoodGageUI;
    public GameObject foodGage;
    public GameObject SucceessImage;
    // 동물 모델링 변수
    public GameObject[] AnimalPrefabs;
    public Transform AnimalPos;
    public GameObject SucceessEffect;
    public Transform EffectPos;

    // Start is called before the first frame update
    void Start()
    {
        this.foodGage = GameObject.Find("FoodGage");
        this.foodGage.GetComponent<Image>().fillAmount = 0f;
        SucceessImage.SetActive(false);
        ShowAnimal();
    }

    public void IncreaseFood()
    {
        this.foodGage.GetComponent<Image>().fillAmount += 0.5f;
        if(this.foodGage.GetComponent<Image>().fillAmount == 1.0f)
        {
            //고양이 애니메이션 등장
            //playableDirector.gameObject.SetActive(true);
            //playableDirector.Play();
            //성공 시 게이지 감추고 성공 출력
            foodGage.SetActive(false);
            FoodGageUI.SetActive(false);
            SucceessImage.SetActive(true);
            ShowAnimal();
        }
    }

    // 동물 프리팹에서 임의 선택
    GameObject RandomAnimal()
    {
        GameObject prefab = null;
        int index = Random.Range(0, AnimalPrefabs.Length);
        prefab = AnimalPrefabs[index];

        return prefab;
    }

    // 동물 등장 연출
    public void ShowAnimal()
    {
        GameObject animal = (GameObject)Instantiate(RandomAnimal(), AnimalPos.position, Quaternion.Euler(0,180,0));
        GameObject successEffect = Instantiate(SucceessEffect);
        successEffect.transform.position = EffectPos.position;
    }
}
