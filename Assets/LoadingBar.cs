using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{

    public Slider loadingSlider;
    public float maxLoadingTime = 4f;
    private float currentLoadingTime = 0f;
	void Start() {
	
		maxLoadingTime = 4f;
		currentLoadingTime = 0f;
	}
    private void Update()
    {
		

		currentLoadingTime += Time.deltaTime;
        loadingSlider.value = currentLoadingTime / maxLoadingTime;
    }
}
