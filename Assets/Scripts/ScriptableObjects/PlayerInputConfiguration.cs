using UnityEngine;

[CreateAssetMenu(fileName = "CustomInputManager", menuName = "ScriptableObjects/PlayerInputManager", order = 1)]
public class PlayerInputConfiguration : ScriptableObject
{
    public KeyCode horizontalPositiveButton = KeyCode.D;
    public KeyCode horizontalNegativeButton = KeyCode.A;
    public KeyCode verticalPositiveButton = KeyCode.W;
    public KeyCode verticalNegativeButton = KeyCode.S;
}
