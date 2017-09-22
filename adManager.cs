using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Diagnostics;

public class adManager : MonoBehaviour {


	public int ADCounter;
	// Test ad units from admob
	public String app_ad_Bnr = "ca-app-pub-3940256099942544/6300978111";
	public String app_ad_Interstitial = "ca-app-pub-3940256099942544/1033173712";
	public bool ADPosition_isTop;
	public bool is_BnrAdActive;
	private BannerView bannerView;
	private InterstitialAd interstitial;
	public static bool NewGameButtonIsActive;
	public static adManager InstanceData;
	bool isDebug = true;
	void Awake() {
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
		//print ("App Is Ready");
		if (is_BnrAdActive == true && bannerView == null) {
			//print("Admob is Active");
			#if !UNITY_EDITOR
			print("Admob RequestBanner");
			RequestBanner ();
			#endif
		}
	}


	void Start(){
		InstanceData = this;
		/*
		 * 
		 * IOS IDS
		 ***/ 

		//print ("BNR->"+IosBnr+" INTREST->"+ IosInterstitial);
		if(isDebug){
			print ("app_ad_Bnr-> "+app_ad_Bnr);
			print ("app_ad_Interstitial->"+app_ad_Interstitial);

		}


		NewGameButtonIsActive = false;

		if (is_BnrAdActive == true && bannerView != null) {
			print("View bnr");
			#if !UNITY_EDITOR
			print("View bnr");
			bannerView.Show ();
			#endif





		}




	}








	void Update () {

	


		if (Input.GetKeyDown(KeyCode.Escape)){

/*
			if(bannerView != null){
				bannerView.Destroy ();
			}

*/

		}












			//StartCoroutine(WaitAndPrint(1.0F));
			//print("ShowInterstitial");







	}





	IEnumerator DelayExit(float waitTime) {
		//print ("GoToMainMenu Called !");
		yield return new WaitForSeconds(waitTime);

		Application.Quit();


	}






	IEnumerator WaitAndPrint(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		//print("WaitAndPrint " + Time.time);
		#if !UNITY_EDITOR
		print("Admob ShowInterstitial");
		ShowInterstitial ();
		#endif

	}











	public void DestroyBnrAd(){
		if(bannerView != null){
			bannerView.Destroy ();
		}
	}




	private void RequestBanner()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = app_ad_Bnr;
		#elif UNITY_IPHONE
		string adUnitId = app_ad_Bnr;
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create a 320x50 banner at the top of the screen.
		if (ADPosition_isTop) {
			bannerView = new BannerView (adUnitId, AdSize.Banner, AdPosition.Top);
		} else {
		bannerView = new BannerView (adUnitId, AdSize.Banner, AdPosition.Bottom);
		}
		// Register for ad events.
		bannerView.OnAdLoaded += HandleAdLoaded;
		bannerView.OnAdFailedToLoad  += HandleAdFailedToLoad;
		bannerView.OnAdOpening   += HandleAdOpened;
		bannerView.OnAdClosed  += HandleAdClosing;
		bannerView.OnAdClosed  += HandleAdClosed;
		bannerView.OnAdLeavingApplication  += HandleAdLeftApplication;
		// Load a banner ad.
		bannerView.LoadAd(createAdRequest());
		}

		private void RequestInterstitial()
		{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = app_ad_Interstitial;
		#elif UNITY_IPHONE
		string adUnitId = app_ad_Interstitial;
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create an interstitial.
		interstitial = new InterstitialAd(adUnitId);
		// Register for ad events.
		interstitial.OnAdLoaded  += HandleInterstitialLoaded;
		interstitial.OnAdFailedToLoad  += HandleInterstitialFailedToLoad;
		interstitial.OnAdOpening  += HandleInterstitialOpened;
		interstitial.OnAdClosed  += HandleInterstitialClosing;
		interstitial.OnAdClosed  += HandleInterstitialClosed;
		interstitial.OnAdLeavingApplication  += HandleInterstitialLeftApplication;
		// Load an interstitial ad.
		interstitial.LoadAd(createAdRequest());
		}


		/******************************************************************/

		// Returns an ad request with custom ad targeting.
		private AdRequest createAdRequest()
		{
		return new AdRequest.Builder()
		.AddTestDevice(AdRequest.TestDeviceSimulator)
		.AddTestDevice("ccc")
		.Build();

		}



		public void checkifInterstitial_isLoadedAndPrepareIt()
		{
		print ("checkifInterstitial_isLoadedAndPrepareIt");
		if (interstitial == null || !interstitial.IsLoaded())
		{
		RequestInterstitial ();
		print("Interstitial is not ready yet.");
			
		}
		else
		{
		print ("Interstitial is ready");

		}
		}




		public void ShowInterstitial()
		{
		print ("ShowInterstitial");
		if (interstitial != null && interstitial.IsLoaded())
		{
		interstitial.Show();
		}
		else
		{
		print("Interstitial is not ready yet.");
			RequestInterstitial ();
		}
		}





		public void InterstialHandler(){

		if (ADCounter < 2) {
		ADCounter++;
		} else {
		ADCounter = 0;
		}


		if (ADCounter < 0) {
		ShowInterstitial();	
		}

		if (ADCounter < 2) {
		ShowInterstitial();	
		}

		}



		#region Banner callback handlers

		public void HandleAdLoaded(object sender, EventArgs args)
		{
		print("HandleAdLoaded event received.");
		}

		public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
		{
		print("HandleFailedToReceiveAd event received with message: " + args.Message);
		}

		public void HandleAdOpened(object sender, EventArgs args)
		{
		print("HandleAdOpened event received");
		}

		void HandleAdClosing(object sender, EventArgs args)
		{
		print("HandleAdClosing event received");
		}

		public void HandleAdClosed(object sender, EventArgs args)
		{
		print("HandleAdClosed event received");
		}

		public void HandleAdLeftApplication(object sender, EventArgs args)
		{
		print("HandleAdLeftApplication event received");
		}

		#endregion

		#region Interstitial callback handlers

		public void HandleInterstitialLoaded(object sender, EventArgs args)
		{
		print("HandleInterstitialLoaded event received.");
		}

		public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
		{
		print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
		}

		public void HandleInterstitialOpened(object sender, EventArgs args)
		{
		print("HandleInterstitialOpened event received");
		}

		void HandleInterstitialClosing(object sender, EventArgs args)
		{
		print("HandleInterstitialClosing event received");
		}

		public void HandleInterstitialClosed(object sender, EventArgs args)
		{

		print("HandleInterstitialClosed event received");
		}

		public void HandleInterstitialLeftApplication(object sender, EventArgs args)
		{
		print("HandleInterstitialLeftApplication event received");
		}

		#endregion












}
