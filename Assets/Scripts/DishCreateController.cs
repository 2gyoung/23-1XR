using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/*
 ��� �ν� �� ���� �ε������� ����
 */

public class DishCreateController : MonoBehaviour
{

    public GameObject indicator;
    public GameObject bowl;
    ARRaycastManager arManager;
    GameObject placeObject;
    
    // Start is called before the first frame update
    void Start()
    {
        //���۽� �ε������� ��Ȱ��ȭ
        indicator.SetActive(false);
        //AR Raycast Manager ������Ʈ ��������
        arManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectGround();
        // bowl �𵨸� ����
        if (indicator.activeInHierarchy)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                if (placeObject == null)
                {
                    placeObject = Instantiate(bowl, indicator.transform.position, indicator.transform.rotation);
                    indicator.SetActive(false);
                }
            }
        }
    }

    //�ٴ� ���� �� �ε������� ���
    void DetectGround()
    {
        //��ũ���� �߾� ���� ã��
        Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitinfo = new List<ARRaycastHit>();

        //ray �̿��� �ٴ� ����
        if(arManager.Raycast(screenSize, hitinfo, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            //��� ������ �ε������� Ȱ��ȭ
            indicator.SetActive(true);

            //�ε��������� ��ġ�� ȸ������ ���̰� ���� ������ ��ġ��Ŵ
            indicator.transform.position = hitinfo[0].pose.position;
            indicator.transform.rotation = hitinfo[0].pose.rotation;

        }
        else
        {
            indicator.SetActive(false);
        }
    }
}
