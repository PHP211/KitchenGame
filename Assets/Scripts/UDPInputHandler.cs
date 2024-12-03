using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPInputHandler : MonoBehaviour {

    public static UDPInputHandler Instance { get; private set; }

    //private PlayerInputActions playerInputActions;

    private UDPConnector connector;

    public int direction = 0;
    public int isMovement = 0;

    public string prediction = "";

    private void Awake() {
        Instance = this;
        //connector = new UDPConnector();
    }

    private void Update() {

        //prediction = UDPConnector.Instance.receivedData;

        //Debug.Log("prediction: " + prediction);

        direction = UDPConnector.Instance.direction;
        isMovement = UDPConnector.Instance.isMovement;

        //Debug.Log("Received from InputHandler: " + direction + isMovement);
    }

    public Vector2 GetMovementVector() {
        if (isMovement == 0) {
            return Vector2.zero;
        } else {
            Vector2 inputVector = Vector2.zero;

            if (direction == 0) inputVector = Vector2.zero;
            if (direction == 1) inputVector.y += 1;
            if (direction == 2) inputVector.y -= 1;
            if (direction == 3) inputVector.x -= 1;
            if (direction == 4) inputVector.x += 1;

            inputVector = inputVector.normalized;
            //Debug.Log("Received vector: " + inputVector);
            return inputVector;
        }
    }


}
