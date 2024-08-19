using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Outliner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Hover-over effect
    private Outline outlineComponent;
    private bool isHover = false;
    private float transitionSpeed = 5f;
    private float minOutlineSize = 0.5f;
    private float maxOutlineSize = 2f;
    private Vector2 targetEffectDistance;

    private void Start()
    {
        outlineComponent = GetComponent<Outline>();
        if (outlineComponent != null)
        {
            outlineComponent.enabled = false;
            outlineComponent.effectDistance = new Vector2(minOutlineSize, minOutlineSize);
        }
    }

    private void Update()
    {
        if (outlineComponent != null && outlineComponent.enabled)
        {
            Vector2 currentEffectDistance = outlineComponent.effectDistance;
            outlineComponent.effectDistance = Vector2.Lerp(
                    currentEffectDistance,
                    targetEffectDistance, 
                    Time.deltaTime * transitionSpeed
                    );
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (outlineComponent != null)
        {
            outlineComponent.enabled = true;
            targetEffectDistance = new Vector2(maxOutlineSize, maxOutlineSize);
            isHover = true;
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (outlineComponent != null)
        {
            targetEffectDistance = new Vector2(minOutlineSize, minOutlineSize);
            isHover = false;

            StartCoroutine(DisableOutlineAfterTransition());
        }
    }

    private System.Collections.IEnumerator DisableOutlineAfterTransition()
    {
        yield return new WaitForSeconds(0.25f);
        if (!isHover)
        {
            outlineComponent.enabled = false;
        }
    }
}
