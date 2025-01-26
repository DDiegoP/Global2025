using UnityEngine;
using UnityEngine.UI;
public class GIFPlayer : MonoBehaviour
{
    [SerializeField] Sprite[] frames;
    [SerializeField] Image imageReference;
    float frameDelay = 0.08f;
    int currFrame = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()    
    {
        InvokeRepeating(nameof(changeFrame), 0.0f,  frameDelay);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void changeFrame(){
        imageReference.sprite = frames[currFrame];
        currFrame++;
        currFrame %= frames.Length;
    }

    void OnDestroy(){

    }
}
