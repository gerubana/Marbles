﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Facebook.MiniJSON;
using System;


public class member_plugs : MonoBehaviour {
    /*
    // Awake function from Unity's MonoBehavior
    void Awake ()
    {
        if (!FB.IsInitialized) {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        } else {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback ()
    {
        if (FB.IsInitialized) {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        } else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity (bool isGameShown)
    {
        if (!isGameShown) {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        } else {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void AuthCallback (ILoginResult result) {
        if (FB.IsLoggedIn) {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions) {
                Debug.Log(perm);
            }
        } else {
            Debug.Log("User cancelled login");
        }
    }

    public void CallFBLogin()
    {
        FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, AuthCallback);
    }

    */
    void Awake ()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        Debug.Log("InitCallback");
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            Debug.Log("Continue with Facebook SDK");
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        Debug.Log("OnHideUnity");
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    public void CallFBLogin()
    {
        /*var perms = new List<string>() { "email","public_profile", "user_friends"};
        Debug.Log("FB login");
        FB.LogInWithReadPermissions(perms, AuthCallback);*/

        FB.LogInWithReadPermissions (
            new List<string>(){"public_profile", "email", "user_friends"},
            AuthCallback
        );


    }

    private void AuthCallback(ILoginResult result)
    {
        Debug.Log("AuthCallback");
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);

            FetchFBProfile();

            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
        }
        else
        {
            Debug.Log("User cancelled login");
            this.GetComponent<member>().callbackWait("取 消 串 接");
        }
    }

    private void FetchFBProfile()
    {
        FB.API("/me", HttpMethod.GET, FetchFBProfile_callback);
    }

    private void FetchFBProfile_callback(IGraphResult result)
    {
        IDictionary dict = Json.Deserialize(result.RawResult) as IDictionary;
        Debug.Log(result.RawResult);
        /*Debug.Log(dict["name"]);
        Debug.Log(dict["id"]);*/

        string id = dict["id"].ToString();
        string name = dict["name"].ToString();

        this.GetComponent<member>().outside_callback("FB", id, name);
    }

    public void Share()
    {
        Debug.Log("FB Share");
        FB.ShareLink(
            new Uri("https://developers.facebook.com/"),
            callback: ShareCallback
        );
    }

    private void ShareCallback(IShareResult result)
    {
        Debug.Log("ShareCallback");
        if (result.Cancelled || !String.IsNullOrEmpty(result.Error))
        {
            Debug.Log("ShareLink Error: " + result.Error);
        }
        else if (!String.IsNullOrEmpty(result.PostId))
        {
            // Print post identifier of the shared content
            Debug.Log(result.PostId);
        }
        else
        {
            // Share succeeded without postID
            Debug.Log("ShareLink success!");
        }
    }

}
