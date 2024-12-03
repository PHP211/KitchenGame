using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour {

    [Serializable] public struct KitcheObjectSO_GameObject{
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitcheObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitcheObjectSO_GameObject kitcheObjectSO_GameObject in kitchenObjectSOGameObjectList) {
            kitcheObjectSO_GameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach(KitcheObjectSO_GameObject kitcheObjectSO_GameObject in kitchenObjectSOGameObjectList) {
            if(kitcheObjectSO_GameObject.kitchenObjectSO == e.kitchenObjectSO) { 
                kitcheObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
