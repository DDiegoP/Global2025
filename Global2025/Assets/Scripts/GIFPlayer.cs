using UnityEngine;
using UnityEngine.UI;
public class GIFPlayer : MonoBehaviour
{
    [SerializeField] Sprite[] frames;
    public int framesCount;
    [SerializeField] Image imageReference;
    float frameDelay = 0.08f;
    public int currFrame = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()    
    {
        InvokeRepeating(nameof(changeFrame), 0.0f,  frameDelay);
        framesCount = frames.Length;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void changeFrame(){
        currFrame++;
        currFrame %= framesCount;
        imageReference.sprite = frames[currFrame];
    }

    void OnDestroy(){

    }
}
