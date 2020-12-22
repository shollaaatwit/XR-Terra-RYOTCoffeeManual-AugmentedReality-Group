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

    public enum Direction { Forward, Backward }; // onCLick to 1 if press next, or onClick to -1 if they're going back

    Direction currentDirection = Direction.Forward;

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
    private Animator harioAnimation;
    private Animator filterAnimation;
    private Animator groundsSpoonAnimation;
    private Animator kettlePouringAnimation;


    private float offsetX;
    private float offsetY;
    private float offsetZ;
    float swirlTime = 0;
    float stirTime = 0;

    Dictionary<int, bool> stepComplete = new Dictionary<int, bool>();

    // Start is called before the first frame update
    void Start()
    {
        mugPouring = mug.GetComponent<Animator>();
        harioAnimation = hario.GetComponent<Animator>();
        filterAnimation = filter.GetComponent<Animator>();
        groundsSpoonAnimation = groundsSpoon.GetComponent<Animator>();
        kettlePouringAnimation = kettle.GetComponent<Animator>();

        mugPouring.enabled = false;
        harioAnimation.enabled = false;
        filterAnimation.enabled = false;
        groundsSpoonAnimation.enabled = false;
        kettlePouringAnimation.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
/*        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaceHarioOnMug();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaceFilterInHario();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PourInPrimerWater();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DiscardPrimerWater();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AddCoffeeGrinds();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            FirstWaterPour();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SwirlEntireStack();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SecondaryWaterPour();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            StirWithSpoon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            FinalStackSwirl(); 
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Finish();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            StepForward();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StepBackward();
        }*/

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

    public void StepForward()
    {
        currentDirection = Direction.Forward;
        print($"StepForward Fn fired {currentDirection}");
    }
    public void StepBackward()
    {
        currentDirection = Direction.Backward;
        print($"StepBackward Fn fired {currentDirection}");
    }

    public void PlaceHarioOnMug() // place hario on mug
    {
        print("PlaceHarioOnMug Fn fired");
        if (currentDirection == Direction.Forward)
        {
            ToggleHarioActive();
            ToggleHarioAnimation();
            GetStackOffsets();
            print("PlaceHarioOnMug Direction is Forward");
        }
        else if (currentDirection == Direction.Backward)
        {
            ToggleFilterActive();
            ToggleFilterAnimation();
            ToggleHarioAnimation();
            print("PlaceHarioOnMug Direction is Backward");
        }
    }

    public void PlaceFilterInHario() // place filter in hario
    {
        print("PlaceFilterInHario Fn fired");
        if (currentDirection == Direction.Forward)
        {
            ToggleHarioAnimation();
            SetHarioPlacement();
            ToggleFilterActive();
            ToggleFilterAnimation();
            print("PlaceFilterInHario Direction is Forward");
        }
        else
        {
            TogglePouringWater();
            ToggleFilterAnimation();
            print("PlaceFilterInHario Direction is Backward");

        }
    }

    public void PourInPrimerWater() // pour in some primer water
    {
        if (currentDirection == Direction.Forward)
        {
            SetFilterPlacement();
            TogglePouringWater();
        }
        else
        {
            TogglePouringWater();
            ToggleMugIntoSinkAnimation();
            SetMugPlacement();
        }

    }
    public void DiscardPrimerWater() // discard primer water
    {
        if (currentDirection == Direction.Forward)
        {
            TogglePouringWater();
            ToggleMugIntoSinkAnimation();
        }
        else
        {
            ToggleMugIntoSinkAnimation();
            ToggleAddingCoffeeGrounds();
        }

    }
    public void AddCoffeeGrinds() // add coffee grinds
    {
        if (currentDirection == Direction.Forward)
        {
            ToggleMugIntoSinkAnimation();
            SetMugPlacement();
            ToggleAddingCoffeeGrounds();
        }
        else
        {
            TogglePouringWater();
            ToggleAddingCoffeeGrounds();
            ToggleCoffeeInFilter();
        }

    }

    public void StopPouringCoffeeGrinds()
    {
        if (currentDirection == Direction.Forward)
        {
            ToggleCoffeeInFilter();
            ToggleAddingCoffeeGrounds();
            SetCoffeeScoopPosition();
        }
        else
        {

        }
    }

    public void FirstWaterPour() // First water pour
    {
        if (currentDirection == Direction.Forward)
        {
            TogglePouringWater();
        }
        else
        {
            ToggleSwirlingStack();
            TogglePouringWater();
        }

    }
    public void SwirlEntireStack() // Swirl entire stack
    {
        if (currentDirection == Direction.Forward)
        {
            TogglePouringWater();
            ToggleSwirlingStack();
        }
        else
        {
            TogglePouringWater();
            ToggleSwirlingStack();
        }

    }

    public void StopSwirlingStack()
    {
        ToggleSwirlingStack();
    }

    public void SecondaryWaterPour() // Secondary water pour
    {
        if (currentDirection == Direction.Forward)
        {
            TogglePouringWater();
        }
        else
        {
            TogglePouringWater();
            ToggleStirringSpoon();
        }

    }

    public void StirWithSpoon() // stir with a spoon
    {
        if (currentDirection == Direction.Forward)
        {
            TogglePouringWater();
            ToggleStirringSpoon();
        }
        else
        {
            ToggleStirringSpoon();
            ToggleSwirlingStack();
        }

    }

    public void FinalStackSwirl() // Another stack swirl
    {
        if (currentDirection == Direction.Forward)
        {
            ToggleStirringSpoon();
            ToggleSwirlingStack();
        }
        else
        {
            ToggleSwirlingStack();
        }

    }

    public void Finish() // FINISH
    {
        if (currentDirection == Direction.Forward)
        {
            ToggleSwirlingStack();
        }
        else
        {
            ToggleSwirlingStack();
        }
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
    }


    // PlaceHarioOnMug
    public void ToggleHarioActive()
    {
        hario.SetActive(!hario.activeSelf);
    }
    public void ToggleHarioAnimation()
    {
        harioAnimation.enabled = !harioAnimation.enabled;
    }
    public void SetHarioPlacement()
    {
        hario.transform.localPosition = harioRestingPlace;
    }



    // PlaceFilterInHario
    public void ToggleFilterActive()
    {
        filter.SetActive(!filter.activeSelf);
    }
    public void ToggleFilterAnimation()
    {
        filterAnimation.enabled = !filterAnimation.enabled;
    }
    public void SetFilterPlacement()
    {
        // print("SetFilterPlacement fn fired");
        ToggleFilterAnimation();
        filter.transform.localPosition = filterRestingPlace;
    }



    // PourInPrimerWater
    public void TogglePouringWater()
    {
        kettle.SetActive(!kettle.activeSelf);
        kettlePouringAnimation.enabled = !kettlePouringAnimation.enabled;
    }



    // DiscardPrimerWater
    // same as above
    public void SetMugPlacement()
    {
        mug.transform.localPosition = mugRestingPlace;
        mug.transform.eulerAngles = mugRestingRotation;
    }

    public void ToggleMugIntoSinkAnimation()
    {
        handForSinkHold.SetActive(!handForSinkHold.activeSelf);
        sink.SetActive(!sink.activeSelf);
        mugPouring.enabled = !mugPouring.enabled;
    }




    // AddCoffeeGrinds
    public void ToggleAddingCoffeeGrounds()
    {
        isDumpingCoffee = !isDumpingCoffee;
        groundsSpoon.SetActive(!groundsSpoon.activeSelf);
        groundsSpoonAnimation.enabled = !groundsSpoonAnimation.enabled;
        coffeeGrounds.gameObject.SetActive(!coffeeGrounds.gameObject.activeSelf);
    }

    // FirstWaterPour
    public void SetCoffeeScoopPosition()
    {
        groundsSpoon.transform.eulerAngles = new Vector3(-90, 0, -90);
    }

    public void ToggleCoffeeInFilter()
    {
        coffeeInFilter.SetActive(!coffeeInFilter.activeSelf);
    }


/*    public void ShowHands()
    {
        hands.SetActive(true);
        isSwirling = true;
    }

    public void HideHands()
    {
        hands.SetActive(false);
        isSwirling = false;
    }*/

    public void ToggleSwirlingStack()
    {
        hands.SetActive(!hands.activeSelf);
        isSwirling = !isSwirling;
    }

    public void Swirl()
    {
        float timeX = Mathf.Cos(swirlTime)/ 25 + offsetX;
        float timeZ = Mathf.Sin(swirlTime)/ 25 + offsetZ;
        float timeY = 0 + offsetY;

        cubeParent.transform.position = new Vector3(timeX, timeY, timeZ);
    }

/*    public void UseSpoonToStirAnimation()
    {
        stirSpoon.SetActive(true);
        isStirring = true;
    }

    public void StopStirAnimation()
    {
        stirSpoon.SetActive(false);
        isStirring = false;
    }*/

    public void ToggleStirringSpoon()
    {
        stirSpoon.SetActive(!stirSpoon.activeSelf);
        isStirring = !isStirring;
    }

    public void Stir()
    {
        float timeX = Mathf.Cos(stirTime) / 50 + 2.1185f + 0.005f;
        float timeZ = Mathf.Sin(stirTime) / 50 + -10.0372f + 0.01f;
        float timeY = -2.006f;

        stirSpoon.transform.localPosition = new Vector3(timeX, timeY, timeZ);
    }
}
