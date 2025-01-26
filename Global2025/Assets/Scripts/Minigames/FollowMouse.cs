using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FollowMouse : GameComponent
{

    private RectTransform _rectTransform;
    private float _posIzq;
    private float _posDer;
    private Transform _transform;

    private float elapsedTime = 60f;
    [SerializeField]
    private float instanceTime = 50f;

    private bool startFinishAnim = false;
    private float animTime = 5f;

    [SerializeField]
    private GameObject[] soap;

    [SerializeField]
    private GameObject brillitos;

    [SerializeField]
    private Transform _canvas;


    private StudioEventEmitter _emitter = null;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _transform = this.transform;
        brillitos.SetActive(false);
        elapsedTime = 0;
    }

    void Update()
    {
        Vector3 pos = new Vector3(Mouse.current.position.x.value, Mouse.current.position.y.value, transform.position.z);
        transform.SetPositionAndRotation(pos, Quaternion.identity);

        elapsedTime += Time.deltaTime;


        if (startFinishAnim)
        {
            if (elapsedTime >= animTime)
            {
                this.gameObject.GetComponentInParent<TimerComponent>().StopTimer();
                elapsedTime = 0;
                startFinishAnim = false;
            }
        }

        if (Mouse.current.leftButton.isPressed)
        {

            RaycastHit2D[] hits = new RaycastHit2D[10];
            Physics2D.Raycast(new Vector3(_rectTransform.position.x, _rectTransform.position.y, -10), Camera.main.transform.forward, new ContactFilter2D(), hits);
            RaycastHit2D hit = new RaycastHit2D();
            for (int i = 0; i < 10; ++i)
            {
                if (hits[i].collider != null && hits[i].collider.gameObject.GetComponent<CleanComponent>())
                {
                    hit = hits[i];
                    break;
                }
                else if (i == 9) return;
            }


            Image img = hit.collider.gameObject.GetComponent<Image>();
            RectTransform rectTransform = hit.collider.gameObject.GetComponent<RectTransform>();
            Vector2 halfBallonSize = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y) / 2;
            Vector2 halfHandSize = new Vector2(_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y) / 2;
            Vector2 relativeLeftBottomPosition = (hit.point - halfHandSize)
                - ((Vector2)rectTransform.position - halfBallonSize);
            Vector2 relativeRightTopPosition = (hit.point + halfHandSize)
                - ((Vector2)rectTransform.position - halfBallonSize);

            float scaleW = (img.sprite.textureRect.width + 2 * img.sprite.textureRectOffset.x) / rectTransform.sizeDelta.x;
            float scaleH = (img.sprite.textureRect.height + 2 * img.sprite.textureRectOffset.y) / rectTransform.sizeDelta.y;

            relativeLeftBottomPosition.x *= scaleW;
            relativeRightTopPosition.x *= scaleW;
            relativeLeftBottomPosition.y *= scaleH;
            relativeRightTopPosition.y *= scaleH;

            Mathf.Clamp(relativeLeftBottomPosition.x, 0, img.sprite.textureRect.width);
            Mathf.Clamp(relativeRightTopPosition.x, 0, img.sprite.textureRect.width);
            Mathf.Clamp(relativeLeftBottomPosition.y, 0, img.sprite.textureRect.height);
            Mathf.Clamp(relativeRightTopPosition.y, 0, img.sprite.textureRect.height);


            CleanComponent comp = hit.collider.gameObject.GetComponent<CleanComponent>();
            comp?.CleanRectangle(relativeLeftBottomPosition, relativeRightTopPosition);

            if (elapsedTime >= instanceTime && !startFinishAnim)
            {
                int dirty = comp.checkDirtyNess();
                if (dirty <= 30) showSoap(0);
                else if (dirty <= 60) showSoap(1);
                else if (dirty <= 85) showSoap(2);
                else
                {
                    brillitos.SetActive(true);
                    startFinishAnim = true;
                    this.gameObject.GetComponentInParent<TimerComponent>().StopRunning();
                    elapsedTime = 0;
                }
               
            }

        }
    }

    private void showSoap(int i)
    {
        GameObject obj = Instantiate(soap[i], _transform.position, Quaternion.identity);
        obj.transform.SetParent(_canvas);
        obj.GetComponent<Shrink>().SetMinigameManager(_manager);
    }
   
}
