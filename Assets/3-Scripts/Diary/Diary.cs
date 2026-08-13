using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public struct Page
{
    public Themes Theme; // for now it is just a string
    public Emotions Emotion;
    public string Content;
    public string Date;
}

[Serializable]
public enum Emotions
{
    Joyeux,
    Triste,
    Effrayé,
}

public class Diary : MonoBehaviour
{
    [Header("New page content")]
    [SerializeField] TMP_Dropdown _newTheme;
    [SerializeField] TMP_Dropdown _newEmotions;
    [SerializeField] TMP_InputField _newContent;

    [Header("Old Page Content")]
    [SerializeField] TMP_Text _oldTheme;
    [SerializeField] TMP_Text _oldEmotions;
    [SerializeField] TMP_Text _oldContent;

    [Header("Page type")]
    [SerializeField] GameObject _emptyPage;
    [SerializeField] GameObject _oldPage;

    int _currentPage = 0;
    Page _newPageSave = new();

    public List<Page> Pages { get; } = new();

    void OnEnable()
    {
        _newEmotions.AddOptions(new List<string>(Enum.GetNames(typeof(Emotions))));
        _newTheme.AddOptions(new List<string>(Enum.GetNames(typeof(Themes))));
        Pages.Add(new(){Content = "2x feur", Emotion = Emotions.Effrayé});
        Pages.Add(new(){Content = "2x skoualala", Emotion = Emotions.Joyeux});
        _currentPage = Pages.Count;
    }

    void SaveNewPage()
    {
        _newPageSave.Theme = (Themes)_newTheme.value;
        _newPageSave.Emotion = (Emotions)_newEmotions.value;
        _newPageSave.Content = _newContent.text;
        _newPageSave.Date = DateTime.Today.ToString();
    }

    void LoadSaveNewPage()
    {
        _newTheme.value = (int)_newPageSave.Theme;
        _newEmotions.value = (int)_newPageSave.Emotion;
        _newContent.text = _newPageSave.Content;
    }

    void LoadOldPage(int index)
    {
        Page oldPage = Pages[index];
        _oldTheme.text = oldPage.Theme.ToString();
        _oldEmotions.text = oldPage.Emotion.ToString();
        _oldContent.text = oldPage.Content;
    }

    public void ChangePage(int amount)
    {
        _currentPage += amount;

        if (_currentPage > Pages.Count || _currentPage < 0)
        {
            _currentPage -= amount;
            return;
        }

        if (_currentPage < Pages.Count)
        {
            // show old page
            SaveNewPage();
            LoadOldPage(_currentPage);
            _emptyPage.SetActive(false);
            _oldPage.SetActive(true);
            return;
        }

        if (_currentPage == Pages.Count)
        {
            // show new page
            LoadSaveNewPage();
            _emptyPage.SetActive(true);
            _oldPage.SetActive(false);
            return;
        }
    }
}
