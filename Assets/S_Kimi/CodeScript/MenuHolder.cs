using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHolder : MonoBehaviour
{
    [Header("Menu")]
    //public ListGameObjectVariable menuOptionPrefab; // List of menu options prefabs   
    //public VoidEventChannel RedrawMenuEvent; // Event to redraw the menu
    public Transform menuContainer; // Container for menu options
    public float radius = 100f; // Radius for menu option positioning
    public float animationDuration = 0.3f; // Duration of the menu animation
    private bool isMenuOpen = false; // Is the menu currently open?
    private List<GameObject> instantiatedOptions = new List<GameObject>(); // List to keep track of instantiated menu options
    public List<GameObject> menuOptionPrefab;
    
    
    
    // private void OnEnable() {
    //     RedrawMenuEvent.OnEventRaised += RedrawMenu;
    // }
    // private void OnDisable() {
    //     RedrawMenuEvent.OnEventRaised -= RedrawMenu;
    // }
    private void Start()
    {
        InitializeMenu();
    }

    public void RedrawMenu()
    {
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        if (menuContainer == null)
        {
            return;
        }

        // Instantiate and store menu options in the container
        foreach (GameObject prefab in menuOptionPrefab)
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
            StartCoroutine(SmoothMove(option.GetComponent<RectTransform>(), targetPosition));
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
            StartCoroutine(SmoothMove(option.GetComponent<RectTransform>(), Vector2.zero));
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
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
    }
}




