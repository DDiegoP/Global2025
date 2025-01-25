using FMODUnity;
using UnityEngine;

public class RompemeEstaComponent : GameComponent
{
    [SerializeField]
    private GameObject _typeABubble;

    [SerializeField]
    private GameObject _typeBBubble;

    [SerializeField]
    private double _timeBetweenBubbles = 0.75;

    private double _elapsedTime = 0.0;

    private bool _canInstantiate;

    private RectTransform _canvas;

    private StudioEventEmitter _emitter;

    private void createBubble()
    {
        int type = Random.Range(0, 10);

        // Calcular la posición de la burbuja
        Vector2 randomPos = new Vector2(Random.Range(-_canvas.sizeDelta.x / 2, _canvas.sizeDelta.x / 2), 
            Random.Range(-_canvas.sizeDelta.y / 2, _canvas.sizeDelta.y / 2));

        // Intanciar segun el tipo
        GameObject newBubble;
        if (type < 7) newBubble = Instantiate(_typeABubble, gameObject.transform, false);
        else newBubble = GameObject.Instantiate(_typeBBubble, gameObject.transform, false);
        BubbleComponent bubbleComponent = newBubble.GetComponent<BubbleComponent>();
        bubbleComponent.SetStartPosition(randomPos);
        bubbleComponent.SetMinigameManager(_manager);
            
        // Reproducir sonido
        //_emitter.Play();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canInstantiate = true;
        _canvas = GetComponent<RectTransform>();
        //_emitter = GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canInstantiate) {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _timeBetweenBubbles)
            {
                createBubble();
                _elapsedTime = 0;
            }
        }
    }

    public void gameFinished()
    {
        _canInstantiate = false;
        _elapsedTime = 0.0;
    }
}
