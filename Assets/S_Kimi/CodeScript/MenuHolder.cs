using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuHolder : MonoBehaviour
{
    [Header("PlaceTurret")]
    public GameObject[] towerPrefab;
    public Transform parentRoot;
    private GameObject currentTower;
    private bool isOccupied;
    
    [Header("Menu")]
    public GameObject menuOptionPrefab;
    public Transform menuContainer;
    public float radius = 100f;
    public float animationDuration = 0.3f;
    private bool isMenuOpen = false;
    

    
    private void Start()
    {
        isOccupied = false;
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        if (menuContainer == null)
        {
            return;
        }
        
        for (int i = 0; i < menuContainer.childCount; i++)
        {
            Transform option = menuContainer.GetChild(i);
            option.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            option.gameObject.SetActive(false);
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
        int optionCount = menuContainer.childCount;
        float angleStep = 90f / optionCount;

        for (int i = 0; i < optionCount; i++)
        {
            Transform option = menuContainer.GetChild(i);
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

    public void AddMenuOption()
    {
        GameObject newOption = Instantiate(menuOptionPrefab, menuContainer);
        newOption.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        newOption.gameObject.SetActive(false);
        StartCoroutine(OpenMenu()); // Ensure the menu is repositioned if needed
    }
    
    public void PlaceTower(int index)
    {
        if (isOccupied)
        {
            return;
        }
        
        currentTower = Instantiate(towerPrefab[index], parentRoot.transform, false);
        currentTower.SetActive(true);
        isOccupied = true;
        
        StartCoroutine(CloseMenu());
    }

    public void DestroyTower()
    {
        if (currentTower != null)
        {
            Destroy(currentTower);
            currentTower = null;
            isOccupied = false;
        }
    }
}