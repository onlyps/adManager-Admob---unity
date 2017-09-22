using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
public class demo : MonoBehaviour {

	// Create new object in the hierarchy and add the adManager script to it & and then  steup the ad units and ad options
	// add this script (demo) to any acive game objec for example Camera game objcvt and select the adManager gameobject
	public adManager ad;
	bool IsGameOver = true;
	void Awake ()
	{
		
		

	}


	// Use this for initialization
	void Start () {
		// if Interstitial ad is not Ready request a new one
		ad.checkifInterstitial_isLoadedAndPrepareIt ();


	}
	
	// Update is called once per frame
	void Update () {
		if (IsGameOver) {
			//GameOver.SetActive(true);
			if (CanHndelinrstialAd){
				StartCoroutine(DelayAd(2.5F));
				StartCoroutine(DelayMenu(4.0F));

				CanHndelinrstialAd = false;
			}











		}
	}


	
	
		public void NewGame(){

		SceneManager.LoadScene (2);
		ad.InterstialHandler ();
		ad.DestroyBnrAd ();
	}
	
	
	
	


	IEnumerator DelayAd(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		//print("WaitAndPrint " + Time.time);
		ad.ShowInterstitial ();

	}



	IEnumerator DelayMenu(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		//print("WaitAndPrint " + Time.time);
		//NewGame.SetActive(true);
		//MainMenu.SetActive(true);

	}





}
