using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHolder : MonoBehaviour
{
    [Header("Menu")]
    public GameObject[] menuOptionPrefab; // List of menu options
    public Transform menuContainer; // Container for menu options
    public float radius = 100f; // Radius for menu option positioning
    public float animationDuration = 0.3f; 
    private bool isMenuOpen = false;

    private void Start()
    {
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        if (menuContainer == null)
        {
            return;
        }
        
        // Initially set all menu options inactive
        for (int i = 0; i < menuOptionPrefab.Length; i++)
        {
            menuOptionPrefab[i].SetActive(false);
        }
    }

    public void OnIconClick()
    {
        if (isMenuOpen)
        {
            StartCoroutine(CloseMenu());
        }
        else
        {
            StartCoroutine(OpenMenu());
        }
    }

    private IEnumerator OpenMenu()
    {
        isMenuOpen = true;
        int purchasedCount = 0;
        
        if (purchasedCount == 0) yield break;

        float angleStep = 90f / purchasedCount;
        int displayIndex = 0;

        for (int i = 0; i < menuOptionPrefab.Length; i++)
        {
            
                Transform option = menuOptionPrefab[i].transform;
                float angle = -30f + displayIndex * angleStep;
                float angleRad = angle * Mathf.Deg2Rad;

                Vector2 targetPosition = new Vector2(
                    Mathf.Cos(angleRad),
                    Mathf.Sin(angleRad)) * radius;

                StartCoroutine(SmoothMove(option.GetComponent<RectTransform>(), targetPosition));
                option.gameObject.SetActive(true);
                displayIndex++;
            
        }

        yield return null;
    }

    private IEnumerator CloseMenu()
    {
        if (!isMenuOpen)
        {
            yield break;
        }
        
        isMenuOpen = false;

        foreach (Transform option in menuContainer)
        {
            StartCoroutine(SmoothMove(option.GetComponent<RectTransform>(), Vector2.zero));
        }

        yield return new WaitForSeconds(animationDuration);

        foreach (Transform option in menuContainer)
        {
            option.gameObject.SetActive(false);
        }
    }

    private IEnumerator SmoothMove(RectTransform rectTransform, Vector2 targetPosition)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
    }
}


