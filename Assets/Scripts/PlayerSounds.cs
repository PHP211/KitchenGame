using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    private float footstepTimer;
    private float footstepTimerMax = .1f;
    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        footstepTimer -= Time.deltaTime;

        if (player.IsWalking()) {
            if (footstepTimer < 0f) {
                footstepTimer = footstepTimerMax;

                SoundManager.Instance.PlayFootstep(player.transform.position);
            }
        }
    }
}