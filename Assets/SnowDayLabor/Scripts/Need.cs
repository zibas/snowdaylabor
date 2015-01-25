using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Need : MonoBehaviour {
	
	public Need (){}
	
	public float decay;
	public Slider mySlider;

	// Use this for initialization
	void Start () {}

	public void Reset(){ mySlider.value = 100; }

	// Update is called once per frame
	void Update () { mySlider.value -= Time.deltaTime * decay; }
}
