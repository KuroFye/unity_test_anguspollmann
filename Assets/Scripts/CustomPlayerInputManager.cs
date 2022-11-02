using UnityEngine;
using System.Collections;

public class CustomPlayerInputManager : MonoBehaviour
{
    [SerializeField] private PlayerInputConfiguration _playerInputConfigurationReference;
	public static CustomPlayerInputManager Instance;

    private bool _initialized = false;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        if(_playerInputConfigurationReference == null)
        {
            Debug.LogWarning("Player input config not added to " + gameObject.name);
            return;
        }
        _initialized = true;
    }

    public float GetHorizontalAxis()
    {
        if (!_initialized)
            return 0f;
        var inputValue = 0f;
        if (Input.GetKey(_playerInputConfigurationReference.horizontalPositiveButton))
        {
            inputValue += 1f;
        }
        if (Input.GetKey(_playerInputConfigurationReference.horizontalNegativeButton))
        {
            inputValue -= 1f;
        }
        return inputValue;
    }

    public float GetVerticalAxis()
    {
        if (!_initialized)
            return 0f;
        var inputValue = 0f;
        if (Input.GetKey(_playerInputConfigurationReference.verticalPositiveButton))
        {
            inputValue += 1f;
        }
        if (Input.GetKey(_playerInputConfigurationReference.verticalNegativeButton))
        {
            inputValue -= 1f;
        }
        return inputValue;
    }
}

