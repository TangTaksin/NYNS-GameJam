using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredGate : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Collider2D collider;

    [SerializeField] Sprite ActiveSprite;
    [SerializeField] Sprite InactiveSprite;

    [SerializeField] bool defaultState = true;
    bool isActive;

    [SerializeField] MissionCondition condition;

    private void Start()
    {
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
        TryGetComponent<Collider2D>(out collider);

        isActive = defaultState;
    }

    private void Update()
    {
        SetActive(condition.GetMissionState());
    }

    private void SetActive(bool state)
    {
        isActive = state ? !defaultState : defaultState;
        collider.enabled = isActive;
        if (ActiveSprite && InactiveSprite)
            spriteRenderer.sprite = isActive ? ActiveSprite : InactiveSprite;
    }

}
