using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnTimerMax = 4f;
    private int plateAmount;
    private int plateAmountMax = 4;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnTimerMax) {
            spawnPlateTimer = 0f;

            if(KitchenGameManager.Instance.IsGamePlaying() && plateAmount < plateAmountMax) {
                plateAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if(!player.HasKitchenObject()) {
            //Player is empty handed
            if(plateAmount > 0) {
                //There is at least a plate here
                plateAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}