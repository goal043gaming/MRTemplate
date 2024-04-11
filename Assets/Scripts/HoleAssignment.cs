using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class HoleAssignment : MonoBehaviour
{
    [SerializeField] public List<string> multipleChoice;
    private string selectedChoice;
    private TMP_Text textField;

    // Update is called once per frame
    void OnEnable()
    {
        textField.GetComponentInChildren<TMP_Text>();
    }

    public void AssignNumber()
    {
        int index = Random.Range(0, multipleChoice.Count -1);
        selectedChoice = multipleChoice[index];
        textField.text = selectedChoice;

        multipleChoice.Remove(selectedChoice);
    }
}
