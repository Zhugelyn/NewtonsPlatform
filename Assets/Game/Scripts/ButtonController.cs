using TMPro;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private GameObject _object2;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _videoBoard;
    [SerializeField] private GameObject _formula;
    [SerializeField] private TMP_Text _statusCreationCourse;
    [SerializeField] private GameObject _materialLab;
    [SerializeField] private GameObject _Line;
    [SerializeField] private GameObject _createTask;
    [SerializeField] private GameObject _downloadResource;
    public void ImageObj()
    {
        if (_object.gameObject.active)
        {
            _object.gameObject.SetActive(false);
        }
        else
        {
            _object.gameObject.SetActive(true);
        }
    }

    public void ImageObj2()
    {
        if (_object2.gameObject.active)
        {
            _object2.gameObject.SetActive(false);
        }
        else
        {
            _object2.gameObject.SetActive(true);
        }
    }

    public void ShowMiniMenu()
    {
        if (_menu.gameObject.active)
            _menu.SetActive(false);
        else
            _menu.SetActive(true);
    }

    public void ReturnMainMenu()
    {
        var sceneLoader = new SceneLoader();
        sceneLoader.LoadMainMenuScene();
    }

    public void ShowVideoBoard()
    {
        if (_videoBoard.gameObject.active)
            _videoBoard.SetActive(false);
        else
            _videoBoard.SetActive(true);
    }
    public void ShowFormul()
    {
        if (_formula.gameObject.active)
            _formula.SetActive(false);
        else
            _formula.SetActive(true);
    }

    public void ShowStatusCreationCourse()
    {
        _statusCreationCourse.text = "Курс успешно создан";
    }

    public void ShowPanelMaterialLab()
    {
        if (_materialLab.gameObject.active)
            _materialLab.SetActive(false);
        else
            _materialLab.SetActive(true);
    }

    public void ShowLine()
    {
        if (_Line.gameObject.active)
            _Line.SetActive(false);
        else
            _Line.SetActive(true);
    }

    public void ShowCreateTask()
    {
        if (_createTask.gameObject.active)
            _createTask.SetActive(false);
        else
            _createTask.SetActive(true);
    }

    public void ShowDownLoadResource()
    {
        if (_downloadResource.gameObject.active)
            _downloadResource.SetActive(false);
        else
            _downloadResource.SetActive(true);
    }
}
