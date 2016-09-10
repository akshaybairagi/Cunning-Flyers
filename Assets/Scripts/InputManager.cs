using UnityEngine;

// Game States
public enum InputState
{
    Tap,
    SwipeDown,
    None
}

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    //Current Game State
    public InputState currentInput;

    // Use this for initialization
    void Start()
    {
        //Intialize
        currentInput = InputState.None;
    }

    public void SetInputCurrentState(InputState state)
    {
        currentInput = state;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetMouseButtonDown(0))
        {
            currentInput = InputState.Tap;
        }

        if (SwipeManager.IsSwipingDown())
        {
            currentInput = InputState.SwipeDown;
        }
    }
}
