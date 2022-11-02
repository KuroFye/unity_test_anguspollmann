using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerControllerReference;

    private bool _initialized = false;

    // Start is called before the first frame update
    void Awake()
    {
        if(_playerControllerReference == null)
        {
            Debug.LogError("Target player controller not set on " + gameObject.name);
            return;
        }
        _initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_initialized)
        {
            return;
        }
        transform.position = new Vector3(_playerControllerReference.position.x, transform.position.y, _playerControllerReference.position.z);
    }
}
