using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    public GameObject mug;
    public GameObject hario;
    public GameObject filter;
    public GameObject groundsSpoon;
    public GameObject stirSpoon;
    public GameObject kettle;
    public ParticleSystem coffeeGrounds;
    public ParticleSystem steam;

    private Vector3 harioRestingPlace = new Vector3(2.1175f, -2.1326f, -10.02f);
    private Vector3 filterRestingPlace;
    private Vector3 spoonSpawningPlace = new Vector3(1.949469f, -2.03336f, -10.01812f);
    private int spoonRotX = -90;
    private int spoonRotZ = -90;
    private bool step5 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowHarioPlacementAnimation();
        SetHarioPlacement();
        ShowFilterPlacementAnimation();
        SetFilterPlacement();
        PourCoffeeGroundsAnimation();
        FinishAddingCoffeeGroundsStep();
        PourWaterAnimation();
        StopPouringWaterAnimation();
        UseSpoonToStirAnimation();
    }

    public void ShowHarioPlacementAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("ShowHarioPlacementAnimation fn fired");
            hario.SetActive(true);
        }
        
    }

    public void SetHarioPlacement()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("SetHarioPlacement fn fired");
            Animator harioAnimation = hario.GetComponent<Animator>();
            harioAnimation.enabled = false;
            hario.transform.localPosition = harioRestingPlace;
        }
    }

    public void ShowFilterPlacementAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            print("ShowFilterPlacementAnimation fn fired");
            // filter.SetActive(true);
        }
    }

    public void SetFilterPlacement()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            step5 = false;
            print("SetFilterPlacement fn fired");
            filter.transform.position = filterRestingPlace;
        }
    }

    public void PourCoffeeGroundsAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            step5 = true;
            print("PourCoffeeGroundsAnimation fn fired");
            groundsSpoon.SetActive(true);            
        }
        if ((groundsSpoon.gameObject.transform.rotation.x * Mathf.Rad2Deg) < -36f && step5)
        {
            coffeeGrounds.gameObject.SetActive(true);
        }
        else
        {
            coffeeGrounds.gameObject.SetActive(false);

        }
        // print(spoon.gameObject.transform.rotation.x * Mathf.Rad2Deg);
    }

    public void FinishAddingCoffeeGroundsStep()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            step5 = false;
            print("FinishAddingCoffeeGroundsStep fn fired");
            // coffeeGrounds.Stop();
            coffeeGrounds.gameObject.SetActive(false);
            groundsSpoon.SetActive(false);
            coffeeGrounds.gameObject.SetActive(false);
        }
    }

    public void PourWaterAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            kettle.SetActive(true);
            print("PourWaterAnimation fn fired");
        }
    }

    public void StopPouringWaterAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            kettle.SetActive(false);
            print("StopPouringWaterAnimation fn fired");
        }
    }

    public void UseSpoonToStirAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            print("UseSpoonToStirAnimation fn fired");
        }
    }
}
