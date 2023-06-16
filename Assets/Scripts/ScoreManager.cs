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

    // ���� ������ ���� UI
    public GameObject FoodGageUI;
    public GameObject foodGage;
    public GameObject SucceessImage;
    // ���� �𵨸� ����
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
            //����� �ִϸ��̼� ����
            //playableDirector.gameObject.SetActive(true);
            //playableDirector.Play();
            //���� �� ������ ���߰� ���� ���
            foodGage.SetActive(false);
            FoodGageUI.SetActive(false);
            SucceessImage.SetActive(true);
            ShowAnimal();
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
        GameObject animal = (GameObject)Instantiate(RandomAnimal(), AnimalPos.position, Quaternion.Euler(0,180,0));
        GameObject successEffect = Instantiate(SucceessEffect);
        successEffect.transform.position = EffectPos.position;
    }
}
