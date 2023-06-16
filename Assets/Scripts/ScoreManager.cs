using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/*
 ���� ������ ���� �� ���� �� ����
 */

public class ScoreManager : MonoBehaviour
{

    // ���� ������ ���� UI
    public GameObject FoodGageUI;
    public GameObject foodGage;
    public GameObject SucceessImage;
    // ���� �𵨸� ����
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
            //���� �� ������ ���߰� ���� �̹��� ���
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
            // 3�� �� �ʱ� ���·� ����
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

    // ���� �����տ��� ���� ����
    GameObject RandomAnimal()
    {
        GameObject prefab = null;
        int index = Random.Range(0, AnimalPrefabs.Length);
        prefab = AnimalPrefabs[index];

        return prefab;
    }

    // ���� ���� ����
    public void ShowAnimal()
    {
        GameObject animal = (GameObject)Instantiate(RandomAnimal(), Camera.main.WorldToViewportPoint(AnimalPos.position), Quaternion.Euler(0,180,0));
        GameObject successEffect = Instantiate(SucceessEffect);
        successEffect.transform.position = EffectPos.position;
    }
}
