using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject m_tutorialScreen;
    void Start()
    {
        m_tutorialScreen.SetActive(true);
        StartCoroutine(DisableTutorial());
    }

    IEnumerator DisableTutorial()
    {
        yield return new WaitForSeconds(4f);
        m_tutorialScreen.SetActive(false);
    }
}
