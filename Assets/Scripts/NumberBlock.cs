using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NumberBlock : MonoBehaviour
{
    [SerializeField] private Sprite[] numberSprites;
    [SerializeField] private GameObject lockButton;
    [Range(0, 9)] private int value;
    private bool isLocked;
    [SerializeField] private float timeToDisappear;

    void Start()
    {
        //animation 
        StartCoroutine(DisappearAtTime());
    }

    public void SetValue(int v)
    {
        value = v;
        GetComponent<Image>().sprite = numberSprites[value];
    }

    public int GetValue()
    {
        return value;
    }

    public void SetIsLocked(bool il)
    {
        isLocked = il;
        lockButton.SetActive(il);
    }

    public bool GetIsLocked()
    {
        return isLocked;
    }

    public void Unlock()
    {
        isLocked = false;
        lockButton.SetActive(false);
    }

    public void DestroyGO()
    {
        //destroy animation
        Destroy(gameObject);
    }

    IEnumerator DisappearAtTime()
    {
        yield return new WaitForSecondsRealtime(timeToDisappear);
        Destroy(gameObject);
    }
}
