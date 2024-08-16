using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextArchitect
{
    private TextMeshProUGUI tmpro_ui;
    private TextMeshPro tmpro_world;
    public TMP_Text tmpro => tmpro_ui != null ? tmpro_ui : tmpro_world;

    public string currentText => tmpro.text;
    public string targetText { get; private set; } = "";
    public string preText { get; private set; } = "";

    private int preTextLength = 0;
    public string fullTargetText => preText + targetText;
    
    public enum BuildMethod
    {
        instent,
        typewriter,
        fade
    }

    public BuildMethod buildMethod = BuildMethod.typewriter;
    
    public Color textColor
    {
        get { return tmpro.color; }
        set { tmpro.color = value; }
    }

    public float speed
    {
        get { return baseSpeed * speedMultipliter; }
        set { speedMultipliter = value; }
    }
    private const float baseSpeed = 1;
    private float speedMultipliter = 1;

    public int charactersPerCycle { get { return speed <= 2f ? characterMultiplier : speed <= 2.5f ? charactersPerCycle * 2 : charactersPerCycle * 3; } }
    private int characterMultiplier = 1;

    public bool hurryUp = false;

    public TextArchitect(TextMeshProUGUI tmpro_ui)
    {
        this.tmpro_ui = tmpro_ui;
    }
    public TextArchitect(TextMeshPro tmpro_world)
    {
        this.tmpro_world = tmpro_world;
    }

    public Coroutine Build(string text)
    {
        preText = "";
        targetText = text;
        Stop();
        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }    
    public Coroutine Append(string text)
    {
        preText = "";
        targetText = text;
        Stop();
        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }

    private Coroutine buildProcess = null;
    public bool isBuilding => buildProcess != null;

    public void Stop()
    {
        if (!isBuilding)
        {
            return;
        }
        tmpro.StopCoroutine(buildProcess);
        buildProcess = null;
    }

    IEnumerator Building()
    {
        Prepare();
        switch (buildMethod)
        {
            case BuildMethod.typewriter:
                yield return Build_Typewriter();
                break;
            case BuildMethod.fade:
                yield return Build_Fade();
                break;
        }
        OnComplete();
    }
    private void OnComplete()
    {
        buildProcess = null;
        hurryUp = false;
    }

    public void ForceComplete()
    {
        switch (buildMethod)
        {
            case BuildMethod.typewriter:
                break;
            case BuildMethod.fade:
                break;
        }
        Stop();
        OnComplete();
    }
    private void Prepare()
    {
        switch (buildMethod)
        {
            case BuildMethod.instent:
                Prepare_Instant();
                break;
            case BuildMethod.typewriter:
                Prepare_Typewriter();
                break;
            case BuildMethod.fade:
                Prepare_Fade();
                break;
        }
    }   
    private void Prepare_Instant()
    {
        tmpro.color = tmpro.color;
        tmpro.text = fullTargetText;
        tmpro.ForceMeshUpdate();
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
    }    
    private void Prepare_Typewriter()
    {
        tmpro.color = tmpro.color;
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = preText;

        if (preText != "")
        {
            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
        }

        tmpro.text += targetText;
        tmpro.ForceMeshUpdate();
    }    
    private void Prepare_Fade()
    {
        
    }

    private IEnumerator Build_Typewriter()
    {
        while (tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
        {
            tmpro.maxVisibleCharacters += hurryUp ? charactersPerCycle * 5 : charactersPerCycle;
            yield return new WaitForSeconds(0.015f / speed);
        }
    }    
    private IEnumerator Build_Fade()
    {
        yield return null;

    }
}
