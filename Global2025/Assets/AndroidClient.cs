using FMOD.Studio;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

namespace QuickStart
{
    public class AndroidClient : NetworkBehaviour
    {
        [SerializeField]
        private Image _pressingScreenFeedback = null;

        [SerializeField]
        private GameObject _androidCanvas = null;

        [SerializeField]
        private GameObject _cursor = null;

        [SyncVar(hook = nameof(UpdateAttitude))]
        private Vector3 _attitude;

        [SyncVar(hook = nameof(UpdateReference))]
        // no nos importa el valor como tal, solo que cambie
        private bool _updateReferenceToggle = false;

        [SyncVar(hook = nameof(UpdateReference2AhoraConElMovil))]
        Vector3 attitude_reference;

        [SyncVar(hook = nameof(UpdatePressingScreen))]
        bool _pressingScreen = false;

        [SerializeField]
        private Transform _interactionLimits = null;

        private void UpdatePressingScreen(bool _Old, bool _New)
        {
            InputServerManager.Instance.UpdatePressingScreen(_pressingScreen);
        }

        private void UpdateReference2AhoraConElMovil(Vector3 _Old, Vector3 _New)
        {
            InputServerManager.Instance.UpdateReference(_New);
        }

        private void UpdateReference(bool _Old, bool _New)
        {
            // Aquí meter los valores con los que queremos tomar la snapshot de la referencia
            InputServerManager.Instance.UpdateReference(attitude_reference);
        }

        public void SetAsReference()
        {
            if (AttitudeSensor.current != null) 
                attitude_reference = AttitudeSensor.current.attitude.value.eulerAngles;
        }

        void UpdateAttitude(Vector3 _Old, Vector3 _New)
        {
            InputServerManager.Instance.CalculateInput(_New);
        }


        public void UpdateReferenceToggle()
        {
            SetAsReference();
            _updateReferenceToggle = !_updateReferenceToggle;
        }


        private bool HasAttitude()
        {
            return AttitudeSensor.current != null;
        }

        private void OnEnable()
        {
            EnhancedTouch.TouchSimulation.Enable();
            EnhancedTouch.EnhancedTouchSupport.Enable();
        }

        private void OnDisable()
        {
            EnhancedTouch.TouchSimulation.Disable();
            EnhancedTouch.EnhancedTouchSupport.Disable();
        }

        private void Touch(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();
            
            Debug.LogError(value);

            _pressingScreen = value > 0.0f;
        }

        private void Start()
        {
            attitude_reference = _attitude;
            if (HasAttitude())
            {
                InputSystem.EnableDevice(AttitudeSensor.current);
            }
            else
            {
                Debug.LogError("NO TIENES ACELERÓMETRO");
            }

            InputServerManager.Instance.AddClient();
        }

        private void OnDestroy()
        {
            if(HasAttitude())
            {
                InputSystem.DisableDevice(AttitudeSensor.current);
            }

            InputServerManager.Instance.RemoveClient();
        }

        public override void OnStartLocalPlayer()
        {
            //Camera.main.transform.SetParent(transform);
            //Camera.main.transform.localPosition = new Vector3(0, 0, 0);

            //string name = "Player" + Random.Range(100, 999);
            //Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            //CmdSetupPlayer(_accelerometerMovement);

            // aquí tenemos que poner que se instancie el Canvas guapetón este. a ello voy amigo

            _androidCanvas.SetActive(isLocalPlayer);
            _cursor.SetActive(!isLocalPlayer);
        }

        [Command]
        public void CmdSetupPlayer(Vector3 pos)
        {
            this._attitude = pos;
        }

        void Update()
        {
            if(isLocalPlayer)
            {
                if(EnhancedTouch.Touch.activeTouches.Count > 0)
                {
                    EnhancedTouch.Touch touch = EnhancedTouch.Touch.activeTouches[0];
                    if(touch.screenPosition.y < _interactionLimits.position.y)
                    {
                        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
                        {
                            _pressingScreen = true;
                        }
                        else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
                        {
                            _pressingScreen = false;
                        }
                    }
                }
                //if (Input.touchCount > 0)
                //{
                //    Touch touch = Input.GetTouch(0);

                //    Debug.LogError(touch.position.y + " " + _interactionLimits.position.y);
                //    if (touch.position.y < _interactionLimits.position.y)
                //    {
                //        if (touch.phase != UnityEngine.TouchPhase.Began)
                //        {
                //            Debug.LogError("Touch started");
                //            _pressingScreen = true;
                //        }
                //        else if (touch.phase == UnityEngine.TouchPhase.Ended)
                //        {
                //            Debug.LogError("Touch ENDED");
                //            _pressingScreen = false;
                //        }
                //    }
                //}
                // El cliente lanza el acelerómetro y eso
                if (HasAttitude())
                {
                    _attitude = AttitudeSensor.current.attitude.value.eulerAngles;
                }
            }

            _pressingScreenFeedback.enabled = _pressingScreen;
        }
    }
}