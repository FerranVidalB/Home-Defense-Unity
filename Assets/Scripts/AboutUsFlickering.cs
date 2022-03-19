using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutUsFlickering : MonoBehaviour
{
    public Image image;
    public Sprite defaultSprite;
    public Sprite highlightedSprite;
    public float startOffset;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFlickering()) ;   
    }

    private IEnumerator StartFlickering()
    {
        yield return new WaitForSeconds(startOffset);
        while (true)
        {
            image.sprite = highlightedSprite;
            yield return new WaitForSeconds(1f);
            image.sprite = defaultSprite;
            yield return new WaitForSeconds(2f);
        }
    }

    public void GoToLinkedin(string username)
    {
        Application.OpenURL("https://www.linkedin.com/in/" + username);
    }

}
