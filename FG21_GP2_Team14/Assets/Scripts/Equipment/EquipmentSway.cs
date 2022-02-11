using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipmentSway : MonoBehaviour
{
    public InputActionAsset inputMap;

    //private AnimationCurve SwayCurve = new AnimationCurve(
    //   new Keyframe(0f, 0f, 0.1173548f, 0.1173548f, 0f, 0.3333333f),
    //   new Keyframe(0.1684321f, 0.01847385f, 0.6746619f, 0.6746619f, 0.3333333f, 1f),
    //   new Keyframe(0.248862f, 0.1105121f, -0.1880226f, -0.1880226f, 0.3333333f, 0.3333333f),
    //   new Keyframe(0.3578651f, -0.05417087f, -0.02397894f, -0.02397894f, 1f, 0.7495057f),
    //   new Keyframe(0.6142033f, 1.061987f, -0.02259465f, -0.02259465f, 0.598191f, 0.3333333f),
    //   new Keyframe(0.7038531f, 0.9012923f, 0.006937847f, 0.006937847f, 0.3333333f, 1f),
    //   new Keyframe(0.777689f, 0.9753183f, 1.048398f, 1.048398f, 0.3333333f, 0.4267399f),
    //   new Keyframe(1f, 1f, 0.1110233f, 0.1110233f, 0.3333333f, 0f));


    [Range(1f, 10f)]
    public float SwayIntensity = 1f;


    [Range(1f, 10f)]
    public float SmoothMultiplier = 1f;

    [Range(-1f, 1f)]
    public float MaxJumpOffsetRot = 0.3f;

    [Range(-1f, 1f)]
    public float MaxJumpOffsetPos = 0.15f;

    private InputAction lookAction;

    private InputAction moveAction;

    private InputAction jumpAction;

    private Vector3 startPos;
    private float deltaOffsetRot = 0f;
    private float deltaOffsetPos = 0f;
    private bool jumping = false;

    void Start()
    {
        startPos = transform.localPosition;
        AssignInputAction("Look", out lookAction);
        AssignInputAction("Move", out moveAction);
        AssignInputAction("Jump", out jumpAction);

    }

    private void AssignInputAction(string inputName, out InputAction inputAction)
    {
        var found = true;
        var actionMove = inputMap.FindAction(inputName, found);
        if (found)
        {
            inputAction = actionMove;
        }
        else
        {
            inputAction = null;
        }
    }

    void Update()
    {

        if (lookAction == null) { return; }
        if (moveAction == null) { return; }

        if (jumpAction != null)
        {
            HandleJumpOffset();
        }

        var axis = lookAction.ReadValue<Vector2>();
        var moveAxisX = moveAction.ReadValue<Vector2>().x;

        var quatx = Quaternion.AngleAxis(-(axis.x + moveAxisX) * SwayIntensity, Vector3.up);
        var quaty = Quaternion.AngleAxis(-axis.y * SwayIntensity, Vector3.right);

        var swayQuat = quatx * quaty;
        swayQuat = new Quaternion(
            Mathf.Clamp(swayQuat.x + deltaOffsetRot, -0.2f, 0.1f),
            Mathf.Clamp(swayQuat.y, -0.2f, 0.2f),
            swayQuat.z,
            1);

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            swayQuat,
            Time.deltaTime * SmoothMultiplier);
    }

    private void HandleJumpOffset()
    {
        transform.localPosition = Vector3.Lerp(
            startPos + new Vector3(0f, -deltaOffsetPos, 0f),
            startPos,
            Time.deltaTime);

        if (deltaOffsetPos != 0f)
        { deltaOffsetPos = Mathf.Clamp(deltaOffsetPos - Time.deltaTime, 0f, 1f); }

        if (deltaOffsetRot != 0f)
        { deltaOffsetRot = Mathf.Clamp(deltaOffsetRot - Time.deltaTime, 0f, 1f); }
    }

    public void SetJumpOffsetToMax()
    {
        if (jumping) { return; }
        jumping = true;
        deltaOffsetPos = MaxJumpOffsetPos;
        deltaOffsetRot = MaxJumpOffsetRot;
    }

    public void ResetJumping()
    {
        jumping = false;
    }
}
