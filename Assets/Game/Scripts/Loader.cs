using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;


public class Loader : MonoBehaviour
{
    RawImage rawImage;
    FirebaseStorage storage;
    StorageReference storageReference;

    IEnumerator LoadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl); 
        yield return request.SendWebRequest(); 
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }

    void Start()
    {
        rawImage = gameObject.GetComponent<RawImage>();
        // StartCoroutine(LoadImage("https://firebasestorage.googleapis.com/v0/b/vikings-login.appspot.com/o/VikingsLogo.jpeg?alt=media&token=44b39032-34fe-489e-bd69-536f0ed7c251"));

        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://newton-s-platform.appspot.com");

        StorageReference image = storageReference.Child("VikingsLogo.jpeg");

        image.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                StartCoroutine(LoadImage(Convert.ToString(task.Result))); //Fetch file from the link
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });
    }

}