using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItemScroll : MonoBehaviour
{
    [SerializeField] private List<RectTransform> items;
    [SerializeField] private float _speedCloser;                 // скорость доводчика ряда
    private float itemHeight;
    private int itemCount;
    private int firstItemIndex = 0;
    private float scrollSpeed;
    /*public float ScrollSpeed
    {
        get { return scrollSpeed; }
        set { scrollSpeed = value; }
    }*/

    private void Start()
    {
        if (items.Count == 0)
        {
            Debug.LogError("Items list is empty!");
            return;
        }

        itemHeight = items[0].rect.height;
        itemCount = items.Count;
    }

    private void Update()
    {
        ScrollItems();
        CheckForReset();
    }

    private void ScrollItems()
    {
        foreach (RectTransform item in items)
        {
            item.anchoredPosition -= new Vector2(0, scrollSpeed * Time.deltaTime);
        }
    }

    private void CheckForReset()
    {
        RectTransform firstItem = items[firstItemIndex];
        RectTransform lastItem = items[(firstItemIndex + itemCount - 1) % itemCount];

        if (firstItem.anchoredPosition.y <= -itemHeight)
        {
            firstItem.anchoredPosition = new Vector2(firstItem.anchoredPosition.x, lastItem.anchoredPosition.y + itemHeight);
            firstItemIndex = (firstItemIndex + 1) % itemCount;
        }
    }

    public void StopScrollSmoothly(float duration)
    {
        StartCoroutine(SmoothStopItem(duration));
    }

    private IEnumerator SmoothStopItem(float duration)
    {
        float initialSpeed = scrollSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            scrollSpeed = Mathf.Lerp(initialSpeed, _speedCloser, elapsedTime / duration);
            yield return null;
        }
        scrollSpeed = _speedCloser;
        
        while (true)
        {   
            RectTransform lastItem = items[(firstItemIndex + itemCount - 1) % itemCount];
            if (Mathf.Abs(lastItem.anchoredPosition.y - 1050f) < 0.1f)
            {
                scrollSpeed = 0f; 
                yield break;
            }
            yield return null;
        }
    }

    public void StartScrollAcceleration(float targetSpeed, float duration)
    {
        StartCoroutine(ScrollAccelerationCoroutine(targetSpeed, duration));
    }

    private IEnumerator ScrollAccelerationCoroutine(float targetSpeed, float duration)
    {
        float initialSpeed = scrollSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            scrollSpeed = Mathf.Lerp(initialSpeed, targetSpeed, elapsedTime / duration);
            yield return null;
        }

        scrollSpeed = targetSpeed;
    }
}