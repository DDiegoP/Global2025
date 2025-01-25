using UnityEngine;

public class FoamComponent : GameComponent
{
    enum FoamState { DIRTY, HALF_DIRTY, CLEAN, ERASED}

    private FoamState st;

    private void changeState()
    {
        // Actualizamos el FoamState
        if (st < FoamState.ERASED) st++;

        //switch (st)
        //{
        //    case FoamState.HALF_DIRTY: break;
        //    case FoamState.CLEAN: break;
        //    case FoamState.ERASED: break;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        st = FoamState.DIRTY;
    }
}
