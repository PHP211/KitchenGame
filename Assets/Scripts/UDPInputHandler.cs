using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPInputHandler : MonoBehaviour {

    public static UDPInputHandler Instance { get; private set; }

    private UDPConnector connector;

    public int direction = 0;
    public int interaction = 0;
    public int isMovement = 0;

    private int previousInteraction = -1;

    private void Awake() {
        Instance = this;
    }

    private void Update() {

        direction = UDPConnector.Instance.direction;
        interaction = UDPConnector.Instance.interaction;
        isMovement = UDPConnector.Instance.isMovement;

        int currentInteraction = interaction;

        if (interaction != previousInteraction) {
            previousInteraction = interaction;

            if (interaction == 1) {
                // Gọi hành động InteractAlternate
                Player.Instance.HandleInteractAlternateAction();
            } else if (interaction == 2) {
                // Gọi hành động Interact
                Player.Instance.HandleInteractAction();
            }
        }


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
