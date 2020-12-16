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
    public GameObject coffeeInFilter;
    public ParticleSystem coffeeGrounds;
    public ParticleSystem steam;
    public GameObject hands;

    public GameObject cubeParent;

    public int swirlSpeed = 5;
    public int stirSpeed = 5;


    private Vector3 harioRestingPlace = new Vector3(2.1175f, -2.1326f, -10.02f);
    private Vector3 filterRestingPlace = new Vector3(2.1175f, -2.168f, -10.0216f);
    private Vector3 spoonSpawningPlace = new Vector3(1.949469f, -2.03336f, -10.01812f);
    private int spoonRotX = -90;
    private int spoonRotZ = -90;
    private bool step5 = false;
    private bool isSwirling = false;
    private bool isStirring = false;
    private bool isDumpingCoffee = false;

    private float offsetX;
    private float offsetY;
    private float offsetZ;
    float swirlTime = 0;
    float stirTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowHarioPlacementAnimation();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShowFilterPlacementAnimation();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PourCoffeeGroundsAnimation();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            FinishAddingCoffeeGroundsStep();
            FirstWaterPour();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StopPouringWaterAnimation();
            ShowHands();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SecondPour();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            StopPouringWaterAnimation();
            UseSpoonToStirAnimation();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            StopStirAnimation();
            ShowHands();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            HideHands();
        }

#endif
        if (isSwirling)
        {
            swirlTime += Time.deltaTime * swirlSpeed;
            Swirl();
        }

        if (isStirring)
        {
            stirTime += Time.deltaTime * stirSpeed;
            Stir();
        }

        PouringGrounds();
    }

    private void PouringGrounds()
    {
        if ((coffeeGrounds.gameObject.activeInHierarchy == false))
        {
            if (isSpoonTipped())
            {
                // print("PouringGrounds function is firing");
                coffeeGrounds.gameObject.SetActive(true);
            }
        }
        else if (!isSpoonTipped())
        {
            coffeeGrounds.gameObject.SetActive(false);
        }
    }

    private bool isSpoonTipped()
    {
        return ((groundsSpoon.gameObject.transform.rotation.x * Mathf.Rad2Deg) < -36f); // && coffeeGrounds.gameObject.activeSelf
    }

    public void InvokeAnimation(string functionName)
    {
        Invoke(functionName, 0);
    }

    public void ShowHarioPlacementAnimation()
    {
        print("ShowHarioPlacementAnimation fn fired");
        hario.SetActive(true);
    }

    public void SetHarioPlacement()
    {
        print("SetHarioPlacement fn fired");
        Animator harioAnimation = hario.GetComponent<Animator>();
        harioAnimation.enabled = false;
        hario.transform.localPosition = harioRestingPlace;
    }

    public void ShowFilterPlacementAnimation()
    {
        SetHarioPlacement();
        print("ShowFilterPlacementAnimation fn fired");
        filter.SetActive(true);
    }

    public void SetFilterPlacement()
    {
        print("SetFilterPlacement fn fired");
        Animator filterAnimation = filter.GetComponent<Animator>();
        filterAnimation.enabled = false;
        filter.transform.localPosition = filterRestingPlace;
    }

    public void PourCoffeeGroundsAnimation()
    {
        SetFilterPlacement();
        isDumpingCoffee = true;
        print("PourCoffeeGroundsAnimation fn fired");
        groundsSpoon.SetActive(true);
        coffeeInFilter.SetActive(false);
        // print(groundsSpoon.gameObject.transform.rotation.x * Mathf.Rad2Deg);
    }

    public void FinishAddingCoffeeGroundsStep()
    {
        isDumpingCoffee = false;
        print("FinishAddingCoffeeGroundsStep fn fired");
        // coffeeGrounds.Stop();
        // coffeeGrounds.Pause();
        groundsSpoon.transform.eulerAngles = new Vector3(-90, 0, -90);
        coffeeGrounds.gameObject.SetActive(false);
        groundsSpoon.SetActive(false);
        coffeeInFilter.SetActive(true);
    }

    public void FirstWaterPour()
    {
        // SetFilterPlacement();
        PourWaterAnimation();
    }
  
    public void PourWaterAnimation()
    {
     
        kettle.SetActive(true);
        print("PourWaterAnimation fn fired");
        
    }

    public void StopPouringWaterAnimation()
    {
        kettle.SetActive(false);
            print("StopPouringWaterAnimation fn fired");
        
    }

    public void ShowHands()
    {
        print("ShowHands fn fired");
        hands.SetActive(true);
        isSwirling = true;

    }

    public void Swirl()
    {
        float timeX = Mathf.Cos(swirlTime);
        float timeZ = Mathf.Sin(swirlTime);
        float timeY = 0;

        cubeParent.transform.position = new Vector3(timeX, timeY, timeZ) / 14;
    }

    public void HideHands()
    {
        print("HideHands fn fired");
        hands.SetActive(false);
        isSwirling = false;
    }

    public void SecondPour()
    {
        HideHands();
        PourWaterAnimation();
    }

    public void UseSpoonToStirAnimation()
    {
        print("UseSpoonToStirAnimation fn fired");
        stirSpoon.SetActive(true);
        isStirring = true;
    }

    public void StopStirAnimation()
    {
        print("StopStirAnimation fn fired");
        stirSpoon.SetActive(false);
        isStirring = false;
    }

    public void Stir()
    {
        float timeX = Mathf.Cos(stirTime) / 50 + 2.1185f + 0.005f;
        float timeZ = Mathf.Sin(stirTime) / 50 + -10.0372f + 0.01f;
        float timeY = -2.006f;

        stirSpoon.transform.localPosition = new Vector3(timeX, timeY, timeZ);
    }

}
