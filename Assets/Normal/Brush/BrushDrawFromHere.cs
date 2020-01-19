using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Normal.Realtime;
using UnityEngine.XR.MagicLeap;


public class BrushDrawFromHere : MonoBehaviour {
    // Reference to Realtime to use to instantiate brush strokes
    [SerializeField] private Realtime _realtime = null;
    private RealtimeTransform _rt = null;

    // Prefab to instantiate when we draw a new brush stroke
    [SerializeField] private GameObject _brushStrokePrefab = null;

    // Which hand should this brush instance track?
    private enum Hand { LeftHand, RightHand };
    [SerializeField] private Hand _hand = Hand.RightHand;

    // Used to keep track of the current brush tip position and the actively drawing brush stroke
    private Vector3     _handPosition;
    private Quaternion  _handRotation;
    private BrushStroke _activeBrushStroke;
    public Transform _brushPos;
    //public Material brushMat;

    public Material[] brushMats;

    public float _speed = 0.1f;

    public int action = 0;
    public GameObject[] Actions;


    public bool didOnceX;
    public bool didOnceY;
    public bool didOnceTrig;

    private bool _enabled = true;
    public bool _bumper = false;
    public bool _trigger = false;

    private bool vibration;
    private float vibTime;

    private Transform _controlObject;
    private Transform _player;

    public bool setUp;

    public Vector3 controllerStart;
    public Vector3 controllerEnd;

    public GameObject startPt;
    public GameObject endPt;

    private MLInputController _controller;

    private void Start()
    {
        _realtime = GameObject.FindObjectOfType<Realtime>();
        _rt = this.gameObject.GetComponent<RealtimeTransform>();
        //_rv.RequestOwnership();
        //_realtime.clientID;
        _player = this.gameObject.transform.root;
        _controlObject = GameObject.FindGameObjectWithTag("env").transform;
        //Player = GameObject.FindGameObjectWithTag("Player");
        //laser.SetActive(false);
        //InputTracking.Recenter();

        MLInput.Start();
       //MLInput.OnControllerButtonDown += OnButtonDown;
        //MLInput.OnControllerButtonUp += OnButtonUp;

        Swap();

        if (_realtime.clientID == _rt.ownerID)
        {
            _controller = MLInput.GetController(MLInput.Hand.Left);
        }

        startPt = GameObject.FindGameObjectWithTag("startPt");
        endPt = GameObject.FindGameObjectWithTag("endPt");
    }

    public void VibrateControllers(float frequency, float amplitude, float time)
    {
        vibration = true;
        vibTime = time;
        OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.LTouch);
        /*
        foreach (var device in devices)
        {
            HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    uint channel = 0;
                    //float amplitude = 0.5f;
                    //float duration = 1.0f;
                    device.SendHapticImpulse(channel, amplitude, time);
                }
            }
        }
        */
    }

    public void NextAction()
    {
        if (action < Actions.Length - 1) action += 1;
        else action = 0;
        if (Actions[action] == null) action += 1;
        Swap();
        Debug.Log("Next Action!");
        VibrateControllers(.7f, .7f, .15f);
    }

    public void Swap()
    {
        foreach (GameObject go in Actions)
        {
            if (go != null) go.SetActive(false);
        }
        if (Actions[action] != null) Actions[action].SetActive(true);

        switch (action)
        {
            case 0:
                print("Movement");
                break;
            case 1:
                print("Drawing");
                break;
            default:
                print("Not a mode.");
                break;
        }
    }


    private void Update() {
        if (!_realtime.connected)
            return;

        if (_realtime.clientID == _rt.ownerID)
        {
#if !PLATFORM_LUMIN
            XRNode node = _hand == Hand.LeftHand ? XRNode.LeftHand : XRNode.RightHand;
            string trigger = _hand == Hand.LeftHand ? "Left Trigger" : "Right Trigger";
            string joyclick = _hand == Hand.LeftHand ? "Left Joyclick" : "Right Joyclick";
            string axisY = _hand == Hand.LeftHand ? "Left AxisY" : "Right AxisY";
            string axisX = _hand == Hand.LeftHand ? "Left AxisX" : "Right AxisX";
#endif


#if PLATFORM_LUMIN
            //string trigger = "Right Trigger";
            //string joyclick = "Right Joyclick";
            //string axisY = "Right AxisY";
            //string axisX = "Right AxisX";
            bool triggerPressed = _controller.TriggerValue > 0.2f;
#endif     // Get the position & rotation of the hand
            ///bool handIsTracking = true; _handPosition = transform.position; _handRotation = transform.rotation;

            // Figure out if the trigger is pressed or not


            //Figure out if the joystick is clicked or not
            ///bool joyclickPressed = _controller.OnButtonDown(;

            // Figure out if the joystick / touchpad is pressed or not
            //bool axisYMoved = _controller.TouchpadGesture.Distance > 0.3f && (_controller.TouchpadGesture.Direction == MLInputControllerTouchpadGestureDirection.Up || _controller.TouchpadGesture.Direction == MLInputControllerTouchpadGestureDirection.Down);
            bool axisYMoved = _controller.Touch1PosAndForce.z > 0.0f && Mathf.Abs(_controller.Touch1PosAndForce.y) > 0.2f;
            bool axisXMoved = _controller.Touch1PosAndForce.z > 0.0f && Mathf.Abs(_controller.Touch1PosAndForce.x) > 0.2f;

            //Debug.Log(Input.GetAxisRaw(axisY));
            //bool axisXMoved = _controller.TouchpadGesture.Distance > 0.3f && (_controller.TouchpadGesture.Direction == MLInputControllerTouchpadGestureDirection.Left || _controller.TouchpadGesture.Direction == MLInputControllerTouchpadGestureDirection.Right);

            /*
            if (!axisYMoved)
            {
                didOnceY = false;
            }
            */

            if (!axisXMoved)
            {
                didOnceX = false;
            }

            //actions if moving stuff should be handled by GazeXR
            if (action == 1)
            {
                if (triggerPressed && !didOnceTrig)
                {
                    startPt.transform.position = this.gameObject.transform.position;
                    controllerStart = this.gameObject.transform.position;

                    setUp = true;
                    didOnceTrig = true;
                    Debug.Log("ARE YOU CHANGING THE WORLD?");
                }

                if (!triggerPressed && didOnceTrig)
                {
                    endPt.transform.position = this.gameObject.transform.position;
                    controllerEnd = this.gameObject.transform.position;
                    _player.transform.position += controllerStart - controllerEnd;

                    setUp = false;
                    didOnceTrig = false;
                    Debug.Log("DID YOU CHANGE THE WORLD?");
                }
            }
            
            //actions if drawing
            if (action == 0)
            {

                if (triggerPressed)
                {
                    ColorSync cs = FindObjectOfType<ColorSync>();
                    cs.TransferObject();
                    Debug.Log("I tried to sync color");
                }

                    // If we lose tracking, stop drawing
                    /*
                    if (!handIsTracking)
                    {
                        triggerPressed = false;
                        axisYMoved = false;
                    }
                    */

                    /*
                    if (!axisYMoved)
                    {
                        //Debug.Log("The Y Axis has NOT Moved");
                        //_brushPos.GetComponent<MeshRenderer>().enabled = false;
                    }

                    //If you move the joystick up or down, you're chaning the brush tip location
                    if (axisYMoved && _brushPos.localPosition.z > 0f)
                    {
                        //Debug.Log("The Y Axis has Moved");
                        //_brushPos.GetComponent<MeshRenderer>().enabled = true;
                        _brushPos.localPosition = new Vector3(_brushPos.localPosition.x, _brushPos.localPosition.y, _brushPos.localPosition.z - (Input.GetAxisRaw(axisY) * _speed));
                        VibrateControllers(.1f, .1f, .15f);
                    }
                    if (axisYMoved && _brushPos.localPosition.z <= 0.05f)
                    {
                        _brushPos.localPosition = new Vector3(_brushPos.localPosition.x, _brushPos.localPosition.y, 0.05f);
                    }



                    //if you move the joystick left or right, you're changing the color
                    if (axisXMoved)
                    {
                        //_brushStrokePrefab.GetComponent<BrushStroke>().ResizeWidth(Input.GetAxisRaw(axisY) * (_speed/2));
                        /*
                        Color newColor = new Color(brushMat.color.r - (Input.GetAxisRaw(axisY) * _speed), Random.value, Random.value, 1.0f);
                        // apply it on  material
                        brushMat.color = newColor;

                    }


                    // If the trigger is pressed and we haven't created a new brush stroke to draw, create one!
                    if (triggerPressed && _activeBrushStroke == null)
                    {
                        // Instantiate a copy of the Brush Stroke prefab, set it to be owned by us.
                        GameObject brushStrokeGameObject = Realtime.Instantiate(_brushStrokePrefab.name, ownedByClient: true, useInstance: _realtime);

                        // Make that brush stroke a child of the current state
                        //brushStrokeGameObject.transform.parent = currentOption.transform;

                        // Grab the BrushStroke component from it
                        _activeBrushStroke = brushStrokeGameObject.GetComponent<BrushStroke>();

                        // Tell the BrushStroke to begin drawing at the current brush position
                        _activeBrushStroke.BeginBrushStrokeWithBrushTipPoint(_brushPos.position, _handRotation);
                        VibrateControllers(.12f, .12f, .2f);
                    }

                    // If the trigger is pressed, and we have a brush stroke, move the brush stroke to the new brush tip position
                    if (triggerPressed)
                    {
                        _activeBrushStroke.MoveBrushTipToPoint(_brushPos.position, _handRotation);
                        VibrateControllers(.12f, .12f, .2f);
                        //_brushPos.GetComponent<MeshRenderer>().enabled = true;
                    }


                    // If the trigger is no longer pressed, and we still have an active brush stroke, mark it as finished and clear it.
                    if (!triggerPressed && _activeBrushStroke != null)
                    {
                        _activeBrushStroke.EndBrushStrokeWithBrushTipPoint(_brushPos.position, _handRotation);
                        _activeBrushStroke = null;
                        //_ot.gr.VibrateControllers(.24f, .12f, .4f);
                    } */
                }



            //actions if 
            if (action == 2)
            {

            }

            //actions if 
            if (action == 3)
            {

            }

            //actions if just squeezing your hands
            /*
            if (action == 4)
            {
                if (Input.GetAxisRaw(trigger) > 0.5f)
                {
                    _ps.SetHandState(1);
                }
                else
                {
                    _ps.SetHandState(0);
                }
            } */

        }
    }

    //// Utility

    // Given an XRNode, get the current position & rotation. If it's not tracking, don't modify the position & rotation.
    private void UpdatePose(ref Vector3 position, ref Quaternion rotation)
    {
        position = this.gameObject.transform.position;
        rotation = this.gameObject.transform.rotation;
    }
}
