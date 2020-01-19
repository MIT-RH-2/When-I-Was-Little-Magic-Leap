using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Player : MonoBehaviour
{
    private Realtime _realtime;
    private RealtimeTransform _rt;
    private WorldMover _wm;

    // Start is called before the first frame update
    void Start()
    {
        _realtime = GameObject.FindObjectOfType<Realtime>();
        _rt = this.gameObject.GetComponent<RealtimeTransform>();

        _wm = GameObject.FindObjectOfType<WorldMover>();

        if (!_realtime.connected)
            return;

        if (_realtime.clientID == _rt.ownerID)
        {
            _wm._player = this.gameObject.transform;
        }
    }

}
