using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[CreateAssetMenu(fileName = "new TutorialMessage", menuName = "Tutorials/TutorialMessage", order = 0)]
public class TutorialMessage : ScriptableObject
{
    [Multiline] public string Message;
    public List<InputActionReference> Binding;
}
