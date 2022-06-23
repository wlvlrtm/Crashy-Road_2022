using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGet : MonoBehaviour {



    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.name) {
            case "Item_Repair" :
                RepairItem();
                break;
            case "Item_Star" :
                StarItem();
                break;
            case "Item_Speed" :
                SpeedItem();
                break;
            case "Item_Coin" :
                CoinItem();
                break;
        }
    }

    private void RepairItem() {

    }

    private void StarItem() {
        
    }

    private void SpeedItem() {

    }

    private void CoinItem() {

    }

}
