using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;



public class Console : MonoBehaviour
{

    [SerializeField] private TMP_Text ConsoleText;
    [SerializeField] private TMP_Text HelpText;
    [SerializeField] private TMP_InputField CommandInputField;
    [SerializeField] private Button SendButton;

    [SerializeField] private GameObject HelpPanel;
    [SerializeField] private GameObject ContentText;

    private List<Command> commands = new List<Command>();
    private List<GameObject> HelpPanelCommands = new();
    public static Console console;
    public int trig = 0;


    private void OnDisable()
    {
        CommandInputField.text = string.Empty;
        HelpPanel.SetActive(false);
        ClearConsole();
    }


    private void Awake()
    {
        console = this;
        FindCommands();
        AddListener();
    }

    private void AddListener()
    {
        FindCommands();

        SendButton.onClick.AddListener(Send);
        CommandInputField.onValueChanged.AddListener(InputValueChanged);
        CommandInputField.onSelect.AddListener(e =>
        {
            Debug.Log("jjj1");
            CommandInputField.text = String.Empty;
            HelpPanel.SetActive(true);
            InputValueChanged(string.Empty);
        });

        CommandInputField.onDeselect.AddListener(e =>
        {
            Debug.Log("jjj2");
            HelpPanel.SetActive(false);
            HelpText.text = string.Empty;
        }
            );
    }

    private void FindCommands()
    {
        var MonoBehaviors = FindObjectsOfType<MonoBehaviour>();

        foreach (var MonoBehavior in MonoBehaviors)
        {
                var methods = MonoBehavior.GetType()
                    .GetMethods()
                    .Where(method => Attribute.IsDefined(method, typeof(CommandAttribute)));

                foreach (var method in methods)
                {
                    var attribute = (CommandAttribute)Attribute.GetCustomAttribute(method, typeof(CommandAttribute));
                    commands.Add(new Command(MonoBehavior, method, attribute.ParamsTypes));
                }
        }
    
    }

    private object[] GetParameters(List<Type> Types, List<String> Strings)
    {
        object[] Parameters = new object[Types.Count];

        if(Types.Count != Strings.Count)
        {
            return null;
        }
        else
        {
            for (int i = 0; i < Types.Count; i++)
            {
                try
                {
                    if (Types[i] == typeof(string)) Parameters[i] = Strings[i];
                    else if (Strings[i] == "null") Parameters[i] = null;
                    else if (Types[i] == typeof(int)) Parameters[i] = int.Parse(Strings[i]);
                    else if (Types[i] == typeof(float)) Parameters[i] = float.Parse(Strings[i]);
                    else if (Types[i] == typeof(double)) Parameters[i] = double.Parse(Strings[i]);
                    else if (Types[i] == typeof(GameObject)) Parameters[i] = Resources.Load($"Prefabs/{Strings[i]}") as GameObject;
                    else if (Types[i] == typeof(Transform)) Parameters[i] = GameObject.Find(Strings[i]).transform;
                }
                catch
                {
                    
                    return null;
                }
                return Parameters;
            }
        }
        return null;
    }

    private void Send() => Send(CommandInputField.text);


    public static int iter=0;
    private float RND(int q)
    {

        return UnityEngine.Random.value;
    }
    private int SGN(float q)
    {
        if (q > 0){ return 1; }
        if (q < 0){ return -1; }
        return 0;
    }
    private string RI(string rtt,int nn)
    {
        string str = "";
        for (int i = 0; i < nn; i++)
        {

            str += rtt[i];
        }
        return str;
    }
    private string RIG(string rtt, int nn)
    {
        string str = "";
        for (int i = rtt.Length- nn; i < rtt.Length; i++)
        {

            str += rtt[i];
        }
        return str;
    }


        float T=0;
        float V1=0;
        float V2=0;
        float X1=0;
        float Y1=0;
        float X2=0;
        float Y2=0;
        float C=0;
        float P1 =0;
        float H=0;
        float D1=0;
        float Z=0;
    float D2=0;
    string trrr ="                             RABBIT CHASE\n               CREATIVE COMPUTING  MORRISTOWN NEW JERSEY\n\n\n\nSPEEDS (UNITS/HOP):\nRABBIT - 80   YOU - 240\n\n\n\nHOP#:     1 DISTANCE TO RABBIT:   165   CLOSEST APPROACH:   165\nRABBIT ---     POSITION: ( -114, -119)   AND DIRECTION:  166\nYOU ------     POSITION: (    0,    0)   AND DIRECTION:? 0\n\n\nHOP#:     2 DISTANCE TO RABBIT:   443   CLOSEST APPROACH:   165\nRABBIT ---     POSITION: ( -192, -100)   AND DIRECTION:  136\nYOU ------     POSITION: (  240,    0)   AND DIRECTION:? 0\n\n\nHOP#:     3 DISTANCE TO RABBIT:   731   CLOSEST APPROACH:   443\nRABBIT ---     POSITION: ( -249,  -44)   AND DIRECTION:  203\nYOU ------     POSITION: (  480,    0)   AND DIRECTION:? 0\n\n\nHOP#:     4 DISTANCE TO RABBIT:  1046   CLOSEST APPROACH:   731\nRABBIT ---     POSITION: ( -323,  -75)   AND DIRECTION:   67\nYOU ------     POSITION: (  720,    0)   AND DIRECTION:?";


    private void Send(string Text)
    {
        List<String> Strings = new();
        Debug.Log(Text);
        Strings.AddRange(Text.Split(' '));
        CommandInputField.text = String.Empty;





        if (trig == 0)
        {
            ConsoleText.text += $"                             RABBIT CHASE\n";
            ConsoleText.text += $"               CREATIVE COMPUTING  MORRISTOWN NEW JERSEY\n";
            ConsoleText.text += $"\n\n\n";

             T = 400;
             V1 = (int)(RND(1) * 10 + 0.5) * 10 + 50;
             V2 = ((int)(RND(1) * 2 + 0.5) + 1) * V1;




             X1 = ((int)(RND(1) * 400) + 100) * SGN(RND(1) - 0.5f);
             Y1 = ((int)(RND(1) * 400) + 100) * SGN(RND(1) - 0.5f);
            while ((Y1 == 0) | (X1 == 0))
            {
                X1 = ((int)(RND(1) * 400) + 100) * SGN(RND(1) - 0.5f);
                Y1 = ((int)(RND(1) * 400) + 100) * SGN(RND(1) - 0.5f);

            }
             X2 = 0;
             Y2 = 0;
            ConsoleText.text += $"SPEEDS (UNITS/HOP):\n";
            //ConsoleText.text ;
            ConsoleText.text += $"RABBIT - " + RI(V1 + "     ", 5) + $"YOU - " + V2;//+ RIGHT("     " + V1,5)
            ConsoleText.text += $"\n\n\n\n";

             C = (X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1);
             P1 = (float)(3.141592653589 / 180);//
             H = 1;
            trig = 1;


            D1 = (int)(RND(1) * 359);
            ConsoleText.text += $"HOP#: ";
            Z = H;
            Z = (int)(Z + 0.5);//510
            ConsoleText.text += RIG("     " + Z, 5);
            ConsoleText.text += $" DISTANCE TO RABBIT: ";
            Z = MathF.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1));
            Z = (int)(Z + 0.5);//510
            ConsoleText.text += RIG("     " + Z, 5);
            ConsoleText.text += $"   CLOSEST APPROACH: ";
            Z = MathF.Sqrt(C);
            Z = (int)(Z + 0.5);//510
            ConsoleText.text += RIG("     " + Z, 5);
            ConsoleText.text += $"\n";
            ConsoleText.text += $"RABBIT ---     POSITION: (";
            Z = X1;
            Z = (int)(Z + 0.5);//510
            ConsoleText.text += RIG("     " + Z, 5);
            ConsoleText.text += $",";
            Z = Y1;
            Z = (int)(Z + 0.5);//510
            ConsoleText.text += RIG("     " + Z, 5);
            ConsoleText.text += $")   AND DIRECTION:";
            Z = D1;
            Z = (int)(Z + 0.5);//510
            ConsoleText.text += RIG("     " + Z, 5);
            ConsoleText.text += $"\n";
            ConsoleText.text += $"YOU ------     POSITION: (";
            Z = X2;
            Z = (int)(Z + 0.5);//510
            ConsoleText.text += RIG("     " + Z, 5);
            ConsoleText.text += $",";
            Z = Y2;
            Z = (int)(Z + 0.5);//510
            ConsoleText.text += RIG("     " + Z, 5);
            ConsoleText.text += $")   AND DIRECTION:? ";


        }
        else
        {
            string strInNumbers = Text;
            int output;
            if (int.TryParse(strInNumbers, out output) == true)
            {
                D2 = output;  //Print 123434
                ConsoleText.text += $""+D2;
                ConsoleText.text += $"\n";
                ConsoleText.text += $"\n";
                ConsoleText.text += $"\n";
                float X3 = V1 * (float)Math.Cos(D1 * P1) / 100;
                float Y3 = V1 * (float)Math.Sin(D1 * P1) / 100;
                float X4 = V2 * (float)Math.Cos(D2 * P1) / 100;
                float Y4 = V2 * (float)Math.Sin(D2 * P1) / 100;
                C = (X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1);

                for (int i = 1; i < 101; i++)
                {
                    X1 = X1 + X3;
                    Y1 = Y1 + Y3;
                    X2 = X2 + X4;
                    Y2 = Y2 + Y4;
                    if(C < (X2 - X1)*(X2 - X1) + (Y2 - Y1)*(Y2 - Y1)) { } else {
                        C = (X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1);
                    }
                }
                H = H + 1;
                if (C > T) {        D1 = (int)(RND(1) * 359);
        ConsoleText.text += $"HOP#: ";
        Z = H;
        Z = (int)(Z + 0.5);//510
        ConsoleText.text += RIG("     "+Z,5);
        ConsoleText.text += $" DISTANCE TO RABBIT: ";
        Z = MathF.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1));
        Z = (int)(Z + 0.5);//510
        ConsoleText.text += RIG("     " + Z, 5);
        ConsoleText.text += $"   CLOSEST APPROACH: ";
        Z = MathF.Sqrt(C);
        Z = (int)(Z + 0.5);//510
        ConsoleText.text += RIG("     " + Z, 5);
        ConsoleText.text += $"\n";
        ConsoleText.text += $"RABBIT ---     POSITION: (";
        Z = X1;
        Z = (int)(Z + 0.5);//510
        ConsoleText.text += RIG("     " + Z, 5);
        ConsoleText.text += $",";
        Z = Y1;
        Z = (int)(Z + 0.5);//510
        ConsoleText.text += RIG("     " + Z, 5);
        ConsoleText.text += $")   AND DIRECTION:";
        Z = D1;
        Z = (int)(Z + 0.5);//510
        ConsoleText.text += RIG("     " + Z, 5);
        ConsoleText.text += $"\n";
        ConsoleText.text += $"YOU ------     POSITION: (";
        Z = X2;
        Z = (int)(Z + 0.5);//510
        ConsoleText.text += RIG("     " + Z, 5);
        ConsoleText.text += $",";
        Z = Y2;
        Z = (int)(Z + 0.5);//510
        ConsoleText.text += RIG("     " + Z, 5);
        ConsoleText.text += $")   AND DIRECTION:? "; } 
                else
                {
                    ConsoleText.text += $"\n";
                    ConsoleText.text += $"\n";
                    ConsoleText.text += $"**********";
                    ConsoleText.text += $"\n";
                    ConsoleText.text += $"* GOT YA *";
                    ConsoleText.text += $"\n";
                    ConsoleText.text += $"**********";
                    ConsoleText.text += $"\n";
                    ConsoleText.text += $"\n";
                }
                if (H==3)
                {
                    for (int i = 1; i < 500; i++)
                    {
                        if (ConsoleText.text[i] != trrr[i])
                        {
                            Debug.Log(ConsoleText.text[i]);
                            //Debug.Log(trrr[i]);
                        }
                    }
                }

            }
            else
            {
                ConsoleText.text += $"\n!NUMBER EXPECTED - RETRY INPUT LINE\n? ";
               
            }
        }

        

        //רעחדו













        IEnumerable <Command> SendCommands = commands.OfType<Command>().Where(i => i.Method.Name == Strings[0]);
        if (SendCommands.Count() == 0)
        {
            return;
        }
        List<String> tempList = new();
        tempList.AddRange(Strings.Skip(1));
        object[] param = GetParameters(SendCommands.First().ParamTypes, tempList);
        if(param == null)
        {
            return;
        }
        SendCommands.First().Method.Invoke(SendCommands.First().Target, param);
    }

    private void InputValueChanged(string value)
    {
        for(int i = 0; i < HelpPanelCommands.Count; i++)
        {
            Destroy(HelpPanelCommands[i]);
        }
        HelpPanelCommands.Clear();

        List<Command> SendCommands = new();
        SendCommands.AddRange(commands.OfType<Command>().Where(i => i.Method.Name.StartsWith(value)));
        for (int i = 0; i < SendCommands.Count; i++)
        {
            TMP_Text TempText = Instantiate(ContentText, HelpPanel.transform).GetComponent<TMP_Text>();
            HelpPanelCommands.Add(TempText.gameObject);
            TempText.text = SendCommands[i].Method.Name;
        }

        if(SendCommands.Count == 0 || string.IsNullOrEmpty(CommandInputField.text))
        {
            HelpText.text = string.Empty;
            return;
        }
        HelpText.text = SendCommands.First().Method.Name;
    }

    public void WriteConsole(string Text)
    {
        ConsoleText.text +=$"\n{Text}";
    }

    public void WriteConsole(string Text,ConsoleColor color)
    {
        //ConsoleText.text +=$"\n<color=#{color.ToH}>{Text}</color>";
        ConsoleText.text += $"\n{Text}";
    }
    

    public void ClearConsole()
    {
        ConsoleText.text = string.Empty;
    }

    public List<Command> GetCommands()
    {
        List<Command> temp = new();
        temp.AddRange(commands.ToArray());
        return temp;
    }
}

public struct Command
{
    public Command(object _Target, MethodInfo _Method, List<Type> _ParamTypes)
    {
        Target = _Target;
        Method = _Method;
        ParamTypes = _ParamTypes;
    }

    public object Target;
    public MethodInfo Method;
    public List<Type> ParamTypes;
}