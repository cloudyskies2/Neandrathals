using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Sprite dim, lit;
    Image progressImage;
    // Start is called before the first frame update
    void Awake()
    {
        progressImage = GetComponent<Image>();
    }

    public void SetProgressImage(ProgressStatus status)
    {
        switch(status)
        {
            case ProgressStatus.Dim:
            progressImage.sprite = dim;
            break;
            case ProgressStatus.Lit:
            progressImage.sprite = lit;
            break;
        }
    }
}
public enum ProgressStatus
{
    Dim = 0,
    Lit = 1
}
