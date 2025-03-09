//using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResistorController : MonoBehaviour
{
    [SerializeField] private GameObject R1parallel;
    [SerializeField] private GameObject R2parallel;
    [SerializeField] private GameObject R1series;
    [SerializeField] private GameObject R2series;

    [SerializeField] private Button ResistorModeButton;
    [SerializeField] private TMP_Text CurrentModeTEXT;
    private const string parallel = "Parallel";
    private const string series = "Series";

    enum ResistorMode
    { 
        Parallel,
        Series
    }

    private ResistorMode currentmode = ResistorMode.Parallel;

    private void Start()
    {
        ResistorModeButton.onClick.AddListener(ToggleResistorMode);
        CurrentModeTEXT.text = parallel;
    }

    void ToggleResistorMode()
    {
        if(currentmode == ResistorMode.Series)
        {
            R1parallel.SetActive(true);
            R2parallel.SetActive(true);
            R1series.SetActive(false);
            R2series.SetActive(false);
            CurrentModeTEXT.text = parallel;
            currentmode = ResistorMode.Parallel;
        }
        else
        {
            R1parallel.SetActive(false);
            R2parallel.SetActive(false);
            R1series.SetActive(true);
            R2series.SetActive(true);
            CurrentModeTEXT.text = series;
            currentmode = ResistorMode.Series;
        }
    }
}