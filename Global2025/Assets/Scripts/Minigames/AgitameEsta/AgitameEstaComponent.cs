using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using UnityEditorInternal;
using UnityEngine.InputSystem;


public class AgitameEstaComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject rotationObj;
    [SerializeField]
    private GameObject spawnPos;
    [SerializeField]
    private GameObject bubble;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isMoving = true;
        shoot = false;
        spawnPosRect = spawnPos.GetComponent<RectTransform>();
        timeCount = 0;
        totalTime = 2;
        intensityFactor = 0.3f;
        initialRotation = new Vector3(0, 0, 60f);
        offsetRotation = new Vector3(0, 0, 30f);
        targetRotation = Quaternion.Euler(offsetRotation+ initialRotation);
        intensityIncrease = 0.022f;
    }

    //funcion callback timer finish time agitando
    //funcion callback timer finish time representacion puntuacion
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
        Vector3 vel = new Vector3(200, 150, 0) * intensityFactor;
        bubbleInstance.transform.localScale += scaleOffset;
        bubbleInstance.GetComponent<Rigidbody2D>().linearVelocity = vel + velOffset;

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
            if (Mouse.current.leftButton.isPressed)
            {
                intensityFactor += intensityIncrease;
            }
        }
        else if (shoot) Shoot();
    }
}
