using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using FMODUnity;


public class AgitameEstaComponent : GameComponent
{
    [SerializeField]
    private GameObject rotationObj;
    [SerializeField]
    private GameObject spawnPos;
    [SerializeField]
    private GameObject bubble;
    private ScoreComponent scoreComponent;

    //moviendo la botella
    private bool isMoving;
    private bool shoot;
    [SerializeField]
    private float intensityFactor;
    [SerializeField]
    private float timeCount;
    [SerializeField]
    private float totalTime;
    [SerializeField]
    private float intensityIncrease;
    private Vector3 initialRotation;
    private Vector3 offsetRotation;
    private Quaternion targetRotation;


    //generate bubbles
    private RectTransform spawnPosRect;
    [SerializeField] 
    private float bubbleAnimationTime;
    private float currentAnimationTime;

    StudioEventEmitter _emitter;

    InputManager _input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _input = transform.root.GetComponent<InputManager>();
        isMoving = true;
        shoot = false;
        spawnPosRect = spawnPos.GetComponent<RectTransform>();
        scoreComponent = this.GetComponent<ScoreComponent>();
        timeCount = 0;
        totalTime = 2;
        intensityFactor = 0.3f;
        initialRotation = new Vector3(0, 0, 60f);
        offsetRotation = new Vector3(0, 0, 30f);
        targetRotation = Quaternion.Euler(offsetRotation+ initialRotation);
        intensityIncrease = 0.022f;
        _emitter = GetComponent<StudioEventEmitter>();
    }

    //funcion callback timer finish time agitando
    //funcion callback timer finish time representacion puntuacion
    public void startShooting()
    {
        isMoving = false;
        shoot = true;
        currentAnimationTime = 0;
        _emitter.Play();
    }
   
    void Shoot()
    {
        //little offsets
        Vector3 posOffset = new Vector3(Random.Range(-25f, 25f), Random.Range(-25f, 25f), 0);
        Vector3 velOffset = new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0);
        float randomScaleOff = Random.Range(-0.6f, 0.3f);
        Vector3 scaleOffset = new Vector3(randomScaleOff, randomScaleOff, 0);

        //instancia burbuja chorro
        GameObject bubbleInstance = GameObject.Instantiate(bubble, spawnPosRect.position + posOffset, Quaternion.identity, this.GetComponent<RectTransform>().transform);

        //alcance depende de lo que haya agitado el jugador (+offsets)
        Vector3 vel = new Vector3(200*intensityFactor, 150 + (intensityFactor*50), 0);
        bubbleInstance.transform.localScale += scaleOffset;
        bubbleInstance.GetComponent<Rigidbody2D>().linearVelocity = vel + velOffset;
        bubbleInstance.GetComponent<BubbleDrinkComponent>().SetMinigameManager(_manager);

        //check animation time
        currentAnimationTime += Time.deltaTime;
        if(currentAnimationTime > bubbleAnimationTime)
        {
            shoot = false;
            scoreComponent.changeScore(Mathf.Floor((intensityFactor-0.3f)* 100));
            //Call end function end game bla
            _emitter.Stop();
        }

    }
    void RotateArm() //parameter: input cuanto estamos agitando 
    {
        Vector3 currTarget = targetRotation.eulerAngles;
        Vector3 currRot = rotationObj.transform.rotation.eulerAngles;
        if (Mathf.Abs(currTarget.z - currRot.z) <= 0.5f)
        {
            offsetRotation = -offsetRotation;
            targetRotation = Quaternion.Euler(offsetRotation + initialRotation);
            timeCount = 0;
        }
        rotationObj.transform.rotation = Quaternion.Lerp(rotationObj.transform.rotation, targetRotation, (timeCount * intensityFactor) / totalTime);
        timeCount = timeCount + Time.deltaTime;
        Debug.Log(timeCount);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(intensityFactor);
        if (isMoving)
        {
            RotateArm();
            if (_input.GetPop())
            {
                intensityFactor += intensityIncrease;
            }
        }
        else if (shoot) Shoot();
    }
}
