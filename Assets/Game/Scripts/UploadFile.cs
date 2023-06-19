using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleFileBrowser;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
public class UploadFile : MonoBehaviour
{
    FirebaseStorage storage;
    StorageReference storageReference;
    void Start()
    {

        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png", ".mp4"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"));

        FileBrowser.SetDefaultFilter(".jpg");


        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://newton-s-platform.appspot.com");


    }

    public void OnButtonClick()
    {
        StartCoroutine(ShowLoadDialogCoroutine());

    }

    IEnumerator ShowLoadDialogCoroutine()
    {

        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);

            Debug.Log("File Selected");
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
     
            var newMetadata = new MetadataChange();
            newMetadata.ContentType = "image/jpeg/png";
         
            StorageReference uploadRef = storageReference.Child("uploads");
            Debug.Log("File upload started");
            uploadRef.PutBytesAsync(bytes, newMetadata).ContinueWithOnMainThread((task) => {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log(task.Exception.ToString());
                }
                else
                {
                    Debug.Log("File Uploaded Successfully!");
                }
            });


        }
    }

}