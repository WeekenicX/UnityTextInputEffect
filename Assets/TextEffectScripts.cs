using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffectScripts : MonoBehaviour
{
    private Text targetText;
    private string thisText;
    private int textNum;
    private int textIndex;
    private int textStaus;
    private char textChar;
    private bool textHaveFinished;
    private bool isNeedInputLine;
    private int textInputLine;
    IEnumerator textEffectIEnumerator;
    [Header("InputLineStatus/�Ƿ���Ҫ����������")]
    public bool InputLineStatus;
    [Header("InputLine/�������ı�����Ҫ����")]
    public string InputLine;
    [Header("InputLinePerTime/��������˸ʱ��")]
    public float InputLineTime;
    [Header("RandomTimes/ÿ���������������")]
    [Range(0,20)]
    public int randomTimes;
    [Header("RandomTextPerTime/ÿ�����������ʱ��")]
    [Range(0, 1)]
    public float randomTextPerTime;
    private string thisTextBefore;

    // Start is called before the first frame update
    void Start()
    {
        textEffectIEnumerator = TimerTextEffect();
        if (InputLine == string.Empty)
        {
            isNeedInputLine = false;
        }
        else if(InputLine.Trim() != string.Empty)
        {
            isNeedInputLine = true;
        }
        targetText = this.GetComponent<Text>();
        thisText = targetText.text;
        textNum = thisText.Length;
        targetText.text = string.Empty;
        thisTextBefore = string.Empty;
        StartCoroutine(textEffectIEnumerator);
    }
    IEnumerator TextInputLineIEnumerator()
    {
        while (true)
        {
            textInputLine++;
            if (textInputLine == 1)
            {
                targetText.text += InputLine;
            }
            else if(textInputLine == 2)
            {
                targetText.text = thisTextBefore;
            }
            else
            {
                textInputLine = 0;
            }
            yield return new WaitForSeconds(InputLineTime);
        }
    }

    IEnumerator TimerTextEffect()
    {
        while (!textHaveFinished)
        {
            
            //�ж�ѭ������
            textStaus++;
            if (textStaus != randomTimes+1)
            {
                targetText.text = thisTextBefore;
            }
            targetText.text = thisTextBefore;

            if (textStaus <= randomTimes)
            {
                //��������ַ�������Ϊԭ�����ַ�
                targetText.text += thisText[Random.Range(0, textNum)];
                if (isNeedInputLine)
                {
                    targetText.text += InputLine;
                }
                yield return new WaitForSeconds(randomTextPerTime);
            }
            else
            {
                if (textIndex != textNum)
                {
                    targetText.text += thisText[textIndex];
                    textStaus = 0;
                    thisTextBefore = targetText.text;
                    textIndex++;
                    if (randomTimes == 0)
                    {
                        yield return new WaitForSeconds(randomTextPerTime);
                    }   
                }
                else
                {
                    if (isNeedInputLine)
                    {
                        if (InputLineStatus)
                        {
                            StartCoroutine(TextInputLineIEnumerator());
                        }
                    }
                    textHaveFinished=true;
                    yield break;
                    //��������Ч��

                }
            }
           

        }
    }

}
