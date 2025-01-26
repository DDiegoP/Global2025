using UnityEngine;
using UnityEngine.UI;
public class GIFPlayer : MonoBehaviour
{
    [SerializeField] Sprite[] frames;
    Image imageReference;
    float frameDelay = 0.08f;
    int currFrame = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(changeFrame), frameDelay);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void changeFrame(){
        currFrame++;
        currFrame %= frames.Lenght;
        imageReference.sprite = frames[currFrame];
    }

    void OnDestroy(){

    }
}
