using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Need : MonoBehaviour {
	
	public Need (){}

	/*public Need (string name = "nullName", float max = 100, float decay = 1, Slider mySlider = null){
			this.name = name;
			this.max = max;
			this.decay = decay;
			this.mySlider = mySlider;
	}*/

	public string sliderName;
	public float max;
	public float decay;
	public Slider mySlider;

	// Use this for initialization
	void Start () {
	}

	public void Reset(){

		mySlider.value = 0;

	}

	// Update is called once per frame
	void Update () {

		mySlider.value += Time.deltaTime * decay;

	}
}
