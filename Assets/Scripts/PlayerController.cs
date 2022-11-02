using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody _rigidBodyReference;
    [Header("Player movement configuration")]//These player configurations could also be a scriptable object like the PlayerInputConfiguration
    [SerializeField] private float _maxSpeed = 3f, _rotationSpeed = 500f;
    [SerializeField] private float _acceleration = 1f, _deceleration = 1f;
    private Vector3 _input;
    private float _currentSpeed;
    private CustomPlayerInputManager _playerInput;

    private IEnumerator rotationCoroutine;

    private void Start()
    {
        if(CustomPlayerInputManager.Instance == null)
        {
            Debug.LogError("No input manager was set, can't start PlayerController at " + gameObject.name);
            return;
        }
        _playerInput = CustomPlayerInputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        Rotate();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void ProcessInput()
    {
        _input = new Vector3(_playerInput.GetHorizontalAxis(), 0, _playerInput.GetVerticalAxis());
        if (_input.magnitude > 1f)
        {
            _input = _input.normalized;
        }
    }

    private void Rotate()
    {
        if (_input != Vector3.zero)
        {
            var relative = (transform.position + _input) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            //Here I could use a coroutine to just rotate the model independently from the movement as the character does in Hades but didn't managed to fit it in.
            //  I do know that this rotation means strifing from left to right causes some vertical movement as well, which would be fixed by moving where the character
            //  is facing independently from the character model and "interrupting" the rotation if another action like an attack overrides it e.g. in Hades if you tap
            //  from left to right the character will complete the rotation even if you're no longer pressing the button and maintain the same horizontal axis.
            //I also allow the character to fall by adding rigidBody which isn't necesary, could've also used custom physics to make a more precise movement instead of
            //  relying on the rigidBody.
            transform.rotation = Quaternion.RotateTowards(transform.rotation,rot, _rotationSpeed * Time.deltaTime);
        }
    }

    private void MoveCharacter()
    {
       
        var inputMagnitude = _input.magnitude;
        if(inputMagnitude > 0f)
        {
            _currentSpeed = _currentSpeed + Time.deltaTime * _acceleration * inputMagnitude;
            if(_currentSpeed > _maxSpeed)
            {
                _currentSpeed = _maxSpeed;
            }
        }
        else
        {
            _currentSpeed = _currentSpeed - Time.deltaTime * _deceleration;
            if (_currentSpeed < 0f)
            {
                _currentSpeed = 0f;
            }
        }
        _rigidBodyReference.MovePosition(transform.position + _currentSpeed * Time.deltaTime * _input);
    }
}
