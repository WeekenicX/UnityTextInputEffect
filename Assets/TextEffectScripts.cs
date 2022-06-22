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
    [Header("InputLineStatus/是否需要输入线滞留")]
    public bool InputLineStatus;
    [Header("InputLine/输入线文本不需要留空")]
    public string InputLine;
    [Header("InputLinePerTime/输入线闪烁时间")]
    public float InputLineTime;
    [Header("RandomTimes/每字体所需随机次数")]
    [Range(0,20)]
    public int randomTimes;
    [Header("RandomTextPerTime/每字体所需随机时间")]
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
            
            //判断循环次数
            textStaus++;
            if (textStaus != randomTimes+1)
            {
                targetText.text = thisTextBefore;
            }
            targetText.text = thisTextBefore;

            if (textStaus <= randomTimes)
            {
                //加入随机字符后重置为原来的字符
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
                    //结束字体效果

                }
            }
           

        }
    }

}
