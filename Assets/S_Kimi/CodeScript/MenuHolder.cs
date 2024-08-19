using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHolder : MonoBehaviour
{
    public GameObject[] turretPrefabs;
    public GameObject currentTurret;
    public Transform turretPlacementArea;
    public GameObject menuOptionPrefab;
    public Transform menuContainer;
    public float radius = 100f;
    public float animationDuration = 0.3f;
    private bool isMenuOepn = false;


    private void Start()
    {
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        for (int i = 0; i < menuContainer.childCount; i++)
        {
            int index = i;
            Transform option = menuContainer.GetChild(i);
            option.GetComponent<Button>().onClick.AddListener(() => SelectTurret(index));
            option.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            option.gameObject.SetActive(false);
        }
    }

    public void OnIconClick()
    {
        if (isMenuOepn)
        {
            StartCoroutine(CloseMenu());
        }
        else
        {
            StartCoroutine(OpenMenu());
        }
    }

    void SelectTurret(int index)
    {
        if (currentTurret != null)
        {
            Destroy(currentTurret);
        }

        GameObject prefab = turretPrefabs[index];
        currentTurret = Instantiate(prefab, turretPlacementArea.position, Quaternion.identity);
        StartCoroutine(CloseMenu());
    }

    private IEnumerator OpenMenu()
    {
        isMenuOepn = true;

        int optionCount = menuContainer.childCount;
        float angleStep = 90f / (optionCount-1);

        for (int i = 0; i < optionCount; i++)
        {
            Transform option = menuContainer.GetChild(i);
            float angle = -30f + i * angleStep;
            float angleRad = angle * Mathf.Rad2Deg;

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
        isMenuOepn = false;

        foreach (Transform option in menuContainer)
        {
            StartCoroutine(SmoothMove(
                option.GetComponent<RectTransform>(), Vector2.zero));
        }

        yield return new WaitForSeconds(animationDuration);
        foreach (Transform option in menuContainer)
        {
            option.gameObject.SetActive(false);
        }
    }

    public void CancelCurrentTurret()
    {
        if (currentTurret != null)
        {
            Destroy(currentTurret);
            currentTurret = null;
        }
    }

    private IEnumerator SmoothMove(RectTransform rectTransform, Vector2 targetPosition)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            rectTransform.anchoredPosition =
                Vector2.Lerp(
                    startPosition, 
                    targetPosition, 
                    elapsedTime / animationDuration);
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
        StartCoroutine(OpenMenu());
    }
}
