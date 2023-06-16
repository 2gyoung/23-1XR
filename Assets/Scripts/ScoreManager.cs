using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

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
    public GameObject DestroyAnimal;
    private float ResetTime = 3f;
    private float Timer;

    // Start is called before the first frame update
    void Start()
    {
        this.foodGage = GameObject.Find("FoodGage");
        this.foodGage.GetComponent<Image>().fillAmount = 0f;
        SucceessImage.SetActive(false);
        //test
        //ShowAnimal();
        Timer = 0;
    }

    public void IncreaseFood()
    {
        this.foodGage.GetComponent<Image>().fillAmount += 0.5f;
        if(this.foodGage.GetComponent<Image>().fillAmount == 1.0f)
        {
            //성공 시 게이지 감추고 성공 이미지 출력
            foodGage.SetActive(false);
            FoodGageUI.SetActive(false);
            SucceessImage.SetActive(true);
            ShowAnimal();
            
        }
    }

    void Update()
    {
        if (this.foodGage.GetComponent<Image>().fillAmount == 1.0f)
        {
            // 3초 후 초기 상태로 리셋
            Timer += Time.deltaTime;
            //Debug.Log("TIme");
            if (Timer >= ResetTime)
            {
                //Debug.Log("TimeOver");
                DestroyAnimal = GameObject.Find("Animal");
                Destroy(DestroyAnimal);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Timer = 0;
            }
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
        GameObject animal = (GameObject)Instantiate(RandomAnimal(), Camera.main.WorldToViewportPoint(AnimalPos.position), Quaternion.Euler(0,180,0));
        GameObject successEffect = Instantiate(SucceessEffect);
        successEffect.transform.position = EffectPos.position;
    }
}
