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
    public GameObject handForSinkHold;
    public GameObject sink;

    public GameObject fullObjectStack;
    public GameObject cubeParent;

    public int swirlSpeed = 5;
    public int stirSpeed = 5;


    private Vector3 harioRestingPlace = new Vector3(2.1175f, -2.1326f, -10.02f);
    private Vector3 filterRestingPlace = new Vector3(2.1175f, -2.168f, -10.0216f);
    private Vector3 mugRestingPlace = new Vector3(2.117868f, -2.327361f, -10.01884f);
    private Vector3 mugRestingRotation = new Vector3(0, 0, 0);
    private Vector3 spoonSpawningPlace = new Vector3(1.949469f, -2.03336f, -10.01812f);
    private int spoonRotX = -90;
    private int spoonRotZ = -90;
    private bool step5 = false;
    private bool isSwirling = false;
    private bool isStirring = false;
    private bool isDumpingCoffee = false;

    private Animator mugPouring;

    private float offsetX;
    private float offsetY;
    private float offsetZ;
    float swirlTime = 0;
    float stirTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        mugPouring = mug.GetComponent<Animator>();
        mugPouring.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StepOne();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StepTwo();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StepThree();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StepFour();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StepFive();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            StepSix();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            StepSeven();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            StepEight();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            StepNine();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            StepTen();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StepEleven();
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

    public void StepOne() // place hario on mug
    {
        ShowHarioPlacementAnimation();
        GetStackOffsets();
    }
    public void StepTwo() // place filter in hario
    {
        SetHarioPlacement();
        ShowFilterPlacementAnimation();
    }
    public void StepThree() // pour in some primer water
    {
        SetFilterPlacement();
        PourWaterAnimation();
    }
    public void StepFour() // discard primer water
    {
        StopPouringWaterAnimation();
        PourIntoSink();
    }
    public void StepFive() // add coffee grinds
    {
        StopMugPouringAnimation();
        PourCoffeeGroundsAnimation();
    }
    public void StepSix() // First water pour
    {
        FinishAddingCoffeeGroundsStep();
        PourWaterAnimation();
    }
    public void StepSeven() // Swirl entire stack
    {
        StopPouringWaterAnimation();
        ShowHands();
    }

    public void StepEight() // Secondary water pour
    {
        HideHands();
        PourWaterAnimation();
    }

    public void StepNine() // stir with a spoon
    {
        StopPouringWaterAnimation();
        UseSpoonToStirAnimation();
    }

    public void StepTen() // Another stack swirl
    {
        StopStirAnimation();
        ShowHands();
    }

    public void StepEleven() // FINISH
    {
        HideHands();
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

    public void GetStackOffsets()
    {
        offsetX = cubeParent.transform.position.x;
        offsetY = cubeParent.transform.position.y;
        offsetZ = cubeParent.transform.position.z;
        print($"offsetx = {offsetX}, offsety = {offsetY}, offsetz = {offsetZ}");
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

    public void PourIntoSink()
    {
        mugPouring.enabled = true;
        handForSinkHold.SetActive(true);
        sink.SetActive(true);
    }

    public void StopMugPouringAnimation()
    {
        mugPouring.enabled = false;
        handForSinkHold.SetActive(false);
        sink.SetActive(false);
        mug.transform.localPosition = mugRestingPlace;
        mug.transform.eulerAngles = mugRestingRotation;
    }

    public void PourCoffeeGroundsAnimation()
    {
        isDumpingCoffee = true;
        groundsSpoon.SetActive(true);
        coffeeInFilter.SetActive(false);
    }

    public void FinishAddingCoffeeGroundsStep()
    {
        isDumpingCoffee = false;
        groundsSpoon.transform.eulerAngles = new Vector3(-90, 0, -90);
        coffeeGrounds.gameObject.SetActive(false);
        groundsSpoon.SetActive(false);
        coffeeInFilter.SetActive(true);
    }

    public void PourWaterAnimation()
    {
        kettle.SetActive(true);
    }

    public void StopPouringWaterAnimation()
    {
        kettle.SetActive(false);
    }

    public void ShowHands()
    {
        hands.SetActive(true);
        isSwirling = true;
    }

    public void Swirl()
    {
        float timeX = Mathf.Cos(swirlTime)/ 4 + offsetX;
        float timeZ = Mathf.Sin(swirlTime)/ 4 + offsetZ;
        float timeY = 0 + offsetY;

        cubeParent.transform.position = new Vector3(timeX, timeY, timeZ);
        print($"timeX: {timeX}, timeZ: {timeZ}, swirltime: {swirlTime}");
    }

    public void HideHands()
    {
        hands.SetActive(false);
        isSwirling = false;
    }

    public void UseSpoonToStirAnimation()
    {
        stirSpoon.SetActive(true);
        isStirring = true;
    }

    public void StopStirAnimation()
    {
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
