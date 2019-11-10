using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class CommandInput : MonoBehaviour
{
    private string _inputCommands = "";
    [SerializeField]
    private bool _commandEnable = true;

    private int _recCommandLength = 100;

    // Start is called before the first frame update
    void Start()
    {
        _inputCommands.PadLeft(100);
        StartCoroutine("commandInputControl");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator commandInputControl()
    {
        while (true)
        {
            if (_commandEnable)
            {
                getAxis();
            }
            else
            {
                _inputCommands += " ";
            }

            confirmCommand();

            yield return null;
        }
    }

    private void getAxis()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            _inputCommands += "a";
        }

        if(Input.GetAxisRaw("Horizontal") <= -0.9)
        {
            _inputCommands += "2";
        }
        else if(Input.GetAxisRaw("Horizontal") >= 0.9)
        {
            _inputCommands += "0";
        }
        else
        {
            _inputCommands += "9";
        }

        if(Input.GetAxisRaw("Vertical") <= -0.9)
        {
            _inputCommands += "1";
        }
        else if(Input.GetAxisRaw("Vertical")>= 0.9)
        {
            _inputCommands += "3";
        }
        else
        {
            _inputCommands += "8";
        }

        if (_inputCommands.Length > _recCommandLength)
        {
            _inputCommands = _inputCommands.Remove(0, 1);
        }
    }

    private void confirmCommand()
    {
        string test = "0.*1.*2.*3";
        int comLength = 30;
        string checkFrame = _inputCommands.Remove(0, _recCommandLength - comLength);
        if(Regex.IsMatch(checkFrame,test))
        {
            Debug.Log("YES");
            return;
        }

        string test2 = "a.*";
        comLength = 50;
        checkFrame = _inputCommands.Remove(0, _recCommandLength - comLength);
        if(Regex.IsMatch(checkFrame,test2))
        {
            Debug.Log("You Pressed A a lot");
            return;
        }
    }
}
