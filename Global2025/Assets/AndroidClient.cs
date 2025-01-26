using FMOD.Studio;
using FMODUnity;
using Mirror;
using System;
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

        [SyncVar(hook = nameof(UpdateAttitude))]
        private Vector3 _attitude;

        [SyncVar(hook = nameof(UpdateAcceletometer))]
        private Vector3 _accelerometer;

        [SyncVar(hook = nameof(UpdateReference))]
        // no nos importa el valor como tal, solo que cambie
        private bool _updateReferenceToggle = false;

        [SyncVar(hook = nameof(UpdateReference2AhoraConElMovil))]
        Vector3 attitude_reference;

        [SyncVar(hook = nameof(UpdateAccelReference2AhoraConElMovil))]
        Vector3 accelerometer_reference;

        [SyncVar(hook = nameof(UpdatePressingScreen))]
        bool _pressingScreen = false;

        [SerializeField]
        private Transform _interactionLimits = null;

        private void UpdatePressingScreen(bool _Old, bool _New)
        {
            if(_pressingScreen)
            {
                GetComponent<StudioEventEmitter>().Play();
            }
            InputServerManager.Instance.UpdatePressingScreen(_pressingScreen);
        }

        private void UpdateReference2AhoraConElMovil(Vector3 _Old, Vector3 _New)
        {
            InputServerManager.Instance.UpdateReference(_New);
        }
        private void UpdateAccelReference2AhoraConElMovil(Vector3 _Old, Vector3 _New)
        {
            InputServerManager.Instance.UpdateAccelReference(_New);
        }

        private void UpdateReference(bool _Old, bool _New)
        {
            // Aquí meter los valores con los que queremos tomar la snapshot de la referencia
            InputServerManager.Instance.UpdateReference(attitude_reference);
            InputServerManager.Instance.UpdateAccelReference(accelerometer_reference);
        }

        public void SetAsReference()
        {
            if (AttitudeSensor.current != null) 
                attitude_reference = AttitudeSensor.current.attitude.value.eulerAngles;
            if (Accelerometer.current != null) 
                accelerometer_reference = Accelerometer.current.acceleration.value;
        }

        void UpdateAttitude(Vector3 _Old, Vector3 _New)
        {
            InputServerManager.Instance.CalculateMouseX(_New);
        }

        void UpdateAcceletometer(Vector3 _Old, Vector3 _New) {
            InputServerManager.Instance.CalculateMouseY(_New);
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
        private bool HasAccelerometer()
        {
            return Accelerometer.current != null;
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
            accelerometer_reference = _accelerometer;
            if (HasAttitude())
            {
                InputSystem.EnableDevice(AttitudeSensor.current);
            }
            else
            {
                Debug.LogError("NO TIENES ATTITUDE");
            }
            if (HasAccelerometer())
            {
                InputSystem.EnableDevice(Accelerometer.current);
            }
            else
            {
                Debug.LogError("NO TIENES ACELERÓMETRO");
            }

            InputServerManager.Instance.AddClient();
        }

        private void OnDestroy()
        {
            if (HasAttitude()) {
                InputSystem.DisableDevice(AttitudeSensor.current);
            }
            if (HasAccelerometer()) {
                InputSystem.DisableDevice(Accelerometer.current);
            }
            InputServerManager.Instance.RemoveClient();
        }

        public override void OnStartLocalPlayer()
        {
            _androidCanvas.SetActive(isLocalPlayer);
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
                            GetComponent<StudioEventEmitter>().Play();
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
                if (HasAttitude()) {
                    _attitude = AttitudeSensor.current.attitude.value.eulerAngles;
                }
                if (HasAccelerometer()) {
                    _accelerometer = Accelerometer.current.acceleration.value;
                }
            }

            _pressingScreenFeedback.enabled = _pressingScreen;
        }
    }
}