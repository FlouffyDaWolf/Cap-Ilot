using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEditor.Rendering;
using UnityEditor.SceneManagement;
using UnityEngine;

[Serializable]
public struct Page
{
    public Themes Theme;
    public Emotions Emotion;
    public string Content;
    public string Date;

    public bool Equals(Page other) => Theme == other.Theme && Emotion == other.Emotion && Content == other.Content && Date == other.Date;
    public override bool Equals(object obj) => obj is Page p && Equals(p);
    public override int GetHashCode() => HashCode.Combine(Theme, Emotion, Content, Date);
    public static bool operator ==(Page a, Page b) => a.Equals(b);
    public static bool operator !=(Page a, Page b) => !a.Equals(b);
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
    [SerializeField] TMP_Text _oldDate;

    [Header("Page type")]
    [SerializeField] GameObject _emptyPage;
    [SerializeField] GameObject _oldPage;

    int _currentPage = 0;
    Page _newPageSave = new();
    bool _newPageFinalized = false;
    string _date = DateTime.Today.ToString("d" ,new CultureInfo("fr-FR"));

    public List<Page> Pages { get; } = new();

    void OnEnable()
    {
        // Pages = Saves.Pages; // un truc du genre

        _newEmotions.AddOptions(new List<string>(Enum.GetNames(typeof(Emotions))));
        _newTheme.AddOptions(new List<string>(Enum.GetNames(typeof(Themes))));

        _currentPage = Pages.Count;

        if (Pages[^1].Date == _date)
        {
            Debug.Log("mais pourtant tu es la");
            _currentPage--;
            _newPageFinalized = true;
            LoadOldPage(_currentPage);
        }
    }

    void SaveNewPage()
    {
        _newPageSave.Theme = (Themes)_newTheme.value;
        _newPageSave.Emotion = (Emotions)_newEmotions.value;
        _newPageSave.Content = _newContent.text;
        _newPageSave.Date = _date;
    }

    void LoadSaveNewPage()
    {
        _newTheme.value = (int)_newPageSave.Theme;
        _newEmotions.value = (int)_newPageSave.Emotion;
        _newContent.text = _newPageSave.Content;
        _emptyPage.SetActive(true);
        _oldPage.SetActive(false);
    }

    void LoadOldPage(int index)
    {
        Page oldPage = Pages[index];
        _oldTheme.text = oldPage.Theme.ToString();
        _oldEmotions.text = oldPage.Emotion.ToString();
        _oldContent.text = oldPage.Content;
        _oldDate.text = oldPage.Date;
        _emptyPage.SetActive(false);
        _oldPage.SetActive(true);
    }

    public void FinalizePage()
    {
        SaveNewPage();
        Pages.Add(_newPageSave);
        _newPageFinalized = true;
    }

    public void ChangePage(int amount)
    {
        _currentPage += amount;

        Debug.Log(_currentPage);

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
            return;
        }

        if (_currentPage == Pages.Count)
        {
            // see if the page has been done
            if (_newPageFinalized)
            {
                _currentPage -= amount;
                Debug.Log("mais est ce que tu passe ici ?");
                return;
            }
            Debug.Log("comment ca mec");
            // show new page
            LoadSaveNewPage();
            return;
        }
    }
}
