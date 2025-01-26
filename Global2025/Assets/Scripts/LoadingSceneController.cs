using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] float screenDuration = 5f;
    [SerializeField] float fadeInSpeed = 1f;
    [SerializeField] float fadeInDelay = 1f;

    [SerializeField] Image image;

    Color color;
    bool fadein = false;
    float currAlpha = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(StartFadeIn), fadeInDelay);
        Invoke(nameof(FinishScene), screenDuration);
    }

    private void StartFadeIn()
    {
        fadein = true;
    }

    private void FinishScene()
    {
        SceneManager.LoadScene("Inicio");
    }

    // Update is called once per frame
    void Update()
    {
        if (fadein)
        {
            currAlpha += Time.deltaTime / fadeInSpeed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, currAlpha);
        }
    }
}
