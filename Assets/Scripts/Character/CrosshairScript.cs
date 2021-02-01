using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairScript : MonoBehaviour
{
    public Vector2 currentAimPosition { get; private set; }

    public bool Inverted = false;
    public Vector2 mouseSensitivity = Vector2.zero;

    [SerializeField, Range(0.0f, 1.0f)]
    private float horizontalPercentageConstrain;
    [SerializeField, Range(0.0f, 1.0f)]
    private float verticalPercentageConstrain;

    private float horizontalConstrain;
    private float verticalConstrain;

    private Vector2 crosshairStartingPosition;

    private Vector2 CurrentLookDelta = Vector2.zero;

    private float minHorizontalConstrainValue;
    private float maxHorizontalConstrainValue;
    private float minVerticalConstrainValue;
    private float maxVerticalConstrainValue;

    private GameInputActions inputActions;

    private void Awake()
    {
        inputActions = new GameInputActions();
    }
    private void Start()
    {
        if (GameManager.Instance.CursorActive)
        {
            AppEvents.Invoke_MouseCursorEnable(false);
        }

        crosshairStartingPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);

        horizontalConstrain = (Screen.width * horizontalPercentageConstrain) / 2f;
        minHorizontalConstrainValue = -(Screen.width / 2) + horizontalConstrain;
        maxHorizontalConstrainValue = (Screen.width / 2) - horizontalConstrain;

        verticalConstrain = (Screen.height * verticalPercentageConstrain) * 2f;
        minVerticalConstrainValue = -(Screen.height / 2f) + verticalConstrain;
        maxVerticalConstrainValue = (Screen.height / 2f) - verticalConstrain;
    }

    // Update is called once per frame
    private void Update()
    {
        float crosshairXPosition = crosshairStartingPosition.x + CurrentLookDelta.x;
        float crosshairYPosition = Inverted 
            ? crosshairStartingPosition.y - CurrentLookDelta.y 
            : crosshairStartingPosition.y + CurrentLookDelta.y;

        currentAimPosition = new Vector2(crosshairXPosition, crosshairYPosition);

        transform.position = currentAimPosition;
    }

    private void OnLook(InputAction.CallbackContext delta)
    {
        Vector2 mouseDelta = delta.ReadValue<Vector2>();

        CurrentLookDelta.x += mouseDelta.x * mouseSensitivity.x;
        if (CurrentLookDelta.x >= maxHorizontalConstrainValue || CurrentLookDelta.x <= minHorizontalConstrainValue)
        {
            CurrentLookDelta.x -= mouseDelta.x * mouseSensitivity.x;
        }

        CurrentLookDelta.y += mouseDelta.y * mouseSensitivity.y;
        if (CurrentLookDelta.y >= maxVerticalConstrainValue || CurrentLookDelta.y <= minVerticalConstrainValue)
        {
            CurrentLookDelta.y -= mouseDelta.y * mouseSensitivity.y;
        }

    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.ThirdPerson.Look.performed += OnLook;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.ThirdPerson.Look.performed -= OnLook;
    }
}
