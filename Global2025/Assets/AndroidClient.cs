using FMOD.Studio;
using Mirror;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuickStart
{
    public class AndroidClient : NetworkBehaviour
    {
        [SerializeField]
        private GameObject _androidCanvas = null;

        [SerializeField]
        private GameObject _cursor = null;

        [SyncVar(hook = nameof(UpdateAttitude))]
        private Vector3 _attitude;

        [SyncVar(hook = nameof(UpdateReference))]
        // no nos importa el valor como tal, solo que cambie
        private bool _updateReferenceToggle = false;

        Vector3 attitude_reference;

        private void UpdateReference(bool _Old, bool _New)
        {
            // Aquí meter los valores con los que queremos tomar la snapshot de la referencia
            InputServerManager.Instance.UpdateReference(attitude_reference);
        }

        public void SetAsReference()
        {
            if (AttitudeSensor.current != null) attitude_reference = AttitudeSensor.current.attitude.value.eulerAngles;
        }

        void UpdateAttitude(Vector3 _Old, Vector3 _New)
        {
            Debug.LogError("New value from accelerometer: " + _New);
            InputServerManager.Instance.CalculateInput(_New);
        }


        public void UpdateReferenceToggle()
        {
            _updateReferenceToggle = !_updateReferenceToggle;
            SetAsReference();
        }

        Accelerometer _accel;

        private bool HasAttitude()
        {
            return AttitudeSensor.current != null;
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
        }

        private void OnDestroy()
        {
            if(HasAttitude())
            {
                InputSystem.DisableDevice(AttitudeSensor.current);
            }
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
            // El cliente lanza el acelerómetro y eso
            if(HasAttitude())
            {
                Debug.LogError("Tengo attitude");
                _attitude = AttitudeSensor.current.attitude.value.eulerAngles;
                Debug.LogError("Este es mi valor de attitude: " + _attitude.ToString());
            }
        }
    }
}