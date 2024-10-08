using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHolder : MonoBehaviour
{
    [Header("Menu")] public Transform menuContainer; // Container for menu options
    public float radius = 100f; // Radius for menu option positioning
    public float animationDuration = 0.3f; // Duration of the menu animation
    private bool isMenuOpen = false; // Is the menu currently open?

    private List<GameObject>
        instantiatedOptions = new List<GameObject>(); // List to keep track of instantiated menu options

    // public List<GameObject> menuOptionPrefab; // List of menu option prefabs

    public ListGameObjectVariable purchasedTurrets;
    public VoidEventChannel RedrawMenu;

    private void Start()
    {
        InitializeMenu();
    }
    private void OnEnable() {
        RedrawMenu.OnEventRaised += InitializeMenu;
    }
    private void OnDisable() {
        RedrawMenu.OnEventRaised -= InitializeMenu;
    }



    private void InitializeMenu()
    {
        if (menuContainer == null)
        {
            return;
        }

        // Clear existing instantiated options
        foreach (var option in instantiatedOptions)
        {
            Destroy(option);
        }

        instantiatedOptions.Clear();

        // Instantiate and store menu options in the container
        foreach (GameObject prefab in purchasedTurrets.listGameObject)
        {
            GameObject option = Instantiate(prefab, menuContainer);
            option.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            option.SetActive(false);
            instantiatedOptions.Add(option); // Store the instantiated option
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
        int optionCount = instantiatedOptions.Count;
        float angleStep = 90f / optionCount;

        for (int i = 0; i < optionCount; i++)
        {
            Transform option = instantiatedOptions[i].transform;
            float angle = -30f + i * angleStep;
            float angleRad = angle * Mathf.Deg2Rad;

            Vector2 targetPosition = new Vector2(
                Mathf.Cos(angleRad),
                Mathf.Sin(angleRad)) * radius;
            StartCoroutine(SmoothMove(
                option.GetComponent<RectTransform>(), targetPosition));
            option.gameObject.SetActive(true);
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

        foreach (GameObject option in instantiatedOptions)
        {
            StartCoroutine(SmoothMove(
                option.GetComponent<RectTransform>(), Vector2.zero));
        }

        yield return new WaitForSeconds(animationDuration);

        foreach (GameObject option in instantiatedOptions)
        {
            option.SetActive(false);
        }
    }

    private IEnumerator SmoothMove(RectTransform rectTransform, Vector2 targetPosition)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(
                startPosition,
                targetPosition,
                elapsedTime / animationDuration
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
    }

    public void ActivateMenuOption(int optionIndex)
    {
        if (optionIndex >= 0 && optionIndex < instantiatedOptions.Count)
        {
            GameObject option = instantiatedOptions[optionIndex];
            option.SetActive(true);

            // Animate the option to its position
            StartCoroutine(AnimateNewOption(option));
        }
        else
        {
            Debug.LogWarning("Invalid option index.");
        }
    }

    private IEnumerator AnimateNewOption(GameObject newOption)
    {
        int optionCount = instantiatedOptions.Count;
        float angleStep = 90f / optionCount;
        int index = instantiatedOptions.IndexOf(newOption);
        float angle = -30f + index * angleStep;
        float angleRad = angle * Mathf.Deg2Rad;

        Vector2 targetPosition = new Vector2(
            Mathf.Cos(angleRad),
            Mathf.Sin(angleRad)) * radius;

        newOption.SetActive(true); // Activate the option before animating
        yield return SmoothMove(newOption.GetComponent<RectTransform>(), targetPosition);
    }

    public void DeactivateMenuOption(int optionIndex)
    {
        if (optionIndex >= 0 && optionIndex < instantiatedOptions.Count)
        {
            GameObject option = instantiatedOptions[optionIndex];
            option.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Invalid option index.");
        }
    }
}





