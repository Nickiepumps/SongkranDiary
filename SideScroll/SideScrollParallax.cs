using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollParallax : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Renderer paralaxMat;
    [SerializeField] private float paralaxXValue, paralaxYValue;
    private void Update()
    {
        paralaxMat.material.mainTextureOffset = new Vector2(player.position.x * paralaxXValue, player.position.y * paralaxYValue);
    }
}
