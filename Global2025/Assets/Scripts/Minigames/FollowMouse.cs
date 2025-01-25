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


    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _transform = this.transform;
    }

    void Update()
    {
        //RaycastHit2D hit = Physics2D.Raycast(new Vector3(_rectTransform.position.x, _rectTransform.position.y, -10), Camera.main.transform.forward);

        //Vector3 pos = new Vector3(Mouse.current.position.x.value, Mouse.current.position.y.value, transform.position.z);
        //transform.SetPositionAndRotation(pos, Quaternion.identity);

        //if (Mouse.current.leftButton.isPressed && hit.collider != null && hit.collider.gameObject.GetComponent<CleanComponent>())
        //{
        //    Image img = hit.collider.gameObject.GetComponent<Image>();
        //    RectTransform rectTransform = hit.collider.gameObject.GetComponent<RectTransform>();
        //    Vector2 halfBallonSize = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y) / 2;
        //    Vector2 halfHandSize = new Vector2(_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y) / 2;
        //    Vector2 relativeLeftBottomPosition = (hit.point - halfHandSize)
        //        - ((Vector2)rectTransform.position - halfBallonSize);
        //    Vector2 relativeRightTopPosition = (hit.point + halfHandSize)
        //        - ((Vector2)rectTransform.position - halfBallonSize);

        //    float scaleW = (img.sprite.textureRect.width + 2 * img.sprite.textureRectOffset.x) / rectTransform.sizeDelta.x; 
        //    float scaleH = (img.sprite.textureRect.height + 2 * img.sprite.textureRectOffset.y) / rectTransform.sizeDelta.y;

        //    relativeLeftBottomPosition.x *= scaleW;
        //    relativeRightTopPosition.x *= scaleW;
        //    relativeLeftBottomPosition.y *= scaleH;
        //    relativeRightTopPosition.y *= scaleH;

        //    Mathf.Clamp(relativeLeftBottomPosition.x, 0, img.sprite.textureRect.width);
        //    Mathf.Clamp(relativeRightTopPosition.x, 0, img.sprite.textureRect.width);
        //    Mathf.Clamp(relativeLeftBottomPosition.y, 0, img.sprite.textureRect.height);
        //    Mathf.Clamp(relativeRightTopPosition.y, 0, img.sprite.textureRect.height);

        //    hit.collider.gameObject.GetComponent<CleanComponent>()?.CleanRectangle(relativeLeftBottomPosition, relativeRightTopPosition);
        //    //Debug.Log("Pos: " + textCoord.x + " " + textCoord.y);
        //}

        Vector3 pos = new Vector3(Mouse.current.position.x.value, Mouse.current.position.y.value, transform.position.z);
        transform.SetPositionAndRotation(pos, Quaternion.identity);

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

        if (Mouse.current.leftButton.isPressed)
        {
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

            hit.collider.gameObject.GetComponent<CleanComponent>()?.CleanRectangle(relativeLeftBottomPosition, relativeRightTopPosition);
            //Debug.Log("Pos: " + textCoord.x + " " + textCoord.y);
        }
    }

    private Vector2 PointerDataToRelativePos()
    {
        Vector2 result;
        Vector2 clickPosition = new Vector2(Mouse.current.position.x.magnitude, Mouse.current.position.y.magnitude);
        RectTransform thisRect = GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(thisRect, clickPosition, null, out result);
        result += thisRect.sizeDelta / 2;

        return result;
    }
}
