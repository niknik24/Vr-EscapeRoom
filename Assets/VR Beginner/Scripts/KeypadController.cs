using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Linq;

public class KeypadController : MonoBehaviour
{
    public List<int> correct = new List<int>();
    private List<int> input = new List<int>();
    [SerializeField] private TMP_InputField display;
    [SerializeField] private float reset = 1f;
    [SerializeField] private string successText;
    [Space(5f)]
    [Header("Entry Events")]
    public UnityEvent onCorrect;
    public UnityEvent onInCorrect;
    public GameObject light;

    public bool allowMult = false;
    private bool pass = false;

    public void userNumber(int number)
    {

        if (pass)
            return;

        if (input.Count >= 3)
        {
            return;
        }
        input.Add(number);
        UpdateDisplay();

        if (input.Count >= 3)
        {
            if (correct.SequenceEqual(input))
            {
                Correct();
            }
            else
            {
                Incorrect();
                return;
            }
        }
    }

    private void Correct()
    {
        onCorrect.Invoke();
        pass = true;
        Light l = light.GetComponent<Light>();
        l.color = Color.green;
        display.text = "Success";
    }

    private void Incorrect()
    {
        onInCorrect.Invoke();
        StartCoroutine(ResetCode());
    }

    IEnumerator ResetCode()
    {
        display.text = "Wrong!";
        Light l = light.GetComponent<Light>();
        l.color = Color.red;
        yield return new WaitForSeconds(reset);

        input.Clear();
        display.text = "Enter text...";
        Color newCol;
        bool e = ColorUtility.TryParseHtmlString("#FFF88F", out newCol);
        l.color = newCol;
    }

    public void UpdateDisplay()
    {
        display.text = null;
        for (int i = 0; i < input.Count; i++)
        {
            display.text += input[i];
        }
    }

    public void Delete()
    {
        if (input.Count <= 0)
        {
            return;
        }

        int pos = input.Count - 1;
        input.RemoveAt(pos);
        UpdateDisplay();
    }
}