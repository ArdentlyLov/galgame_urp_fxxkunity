using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class DL_SPEAKER_DATA
{
    public string name, castName;
    public string displayName => (castName != string.Empty ? castName : name);
    //=>：这是 lambda 表达式的引入符号，在这里用于定义属性的 getter。
    // (castName != string.Empty ? castName : name)：这是一个条件运算符（也称为三元运算符），它检查 castName 是否不等于 string.Empty（也就是确保 castName 有值）。
    // 如果 castName 不是空字符串，那么 displayName 属性将返回 castName 的值。
    // 如果 castName 是空字符串，那么 displayName 属性将返回 name 的值。
    
    public Vector2 castPosition;//用vector2来储存配音演员在界面上的位置
    
    //定义了一个泛型列表属性，用于存储配音演员的表情和层级信息。
    public List<(int layer, string expression)> CastExpressions { get; set; }

    //正则表达式的特殊字符：
    // |：逻辑“或”操作符，表示匹配NAMECAST_ID、POSITIONCAST_ID或EXPRESSIONCAST_ID中的任何一个。
    // @：在字符串前面加上@符号，表示后面的字符串是逐字字符串，即不会对字符串中的反斜杠\进行转义处理。
    private const string NAMECAST_ID = " as ";
    private const string POSITIONCAST_ID = " at ";
    private const string EXPRESSIONCAST_ID = " [";
    private const char AXISDELIMITER = ':';
    private const char EXPRESSIONLAYER_JOINER = ',';
    private const char EXPRESSIONLAYER_DELIMITER = ':';
    
    public DL_SPEAKER_DATA(string rawSpeaker)
    {
        string pattern = @$"{NAMECAST_ID}|{POSITIONCAST_ID}|{EXPRESSIONCAST_ID.Insert(EXPRESSIONCAST_ID.Length - 1, @"\")}";
        MatchCollection matches = Regex.Matches(rawSpeaker, pattern);
        // 使用正则表达式Regex.Matches来匹配和提取rawSpeaker中的相关信息。
        
        //populate this data to avoid null references to values
        castName = "";
        castPosition = Vector2.zero;
        CastExpressions = new List<(int layer, string expression)>();
        
        //if there are no matches, then this entire line is the speaker name
        if (matches.Count == 0)
        {
            name = rawSpeaker;
            return;
        }
        
        //otherwise, issolate the speakername from the casting data
        int index = matches[0].Index;
        name = rawSpeaker.Substring(0, index);
        
        for (int i = 0; i < matches.Count; i++)
        {
            Match match = matches[i];
            int startIndex = 0, endIndex = 0;
            
            if (match.Value == NAMECAST_ID)
            {
                startIndex = match.Index + NAMECAST_ID.Length;//还不够细心，容易出bug
                endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;
                castName = rawSpeaker.Substring(startIndex, endIndex - startIndex);
            }else if (match.Value == POSITIONCAST_ID)
            {
                startIndex = match.Index + POSITIONCAST_ID.Length;//还不够细心，容易出bug
                endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;
                string castPos = rawSpeaker.Substring(startIndex, endIndex - startIndex);

                string[] axis = castPos.Split(AXISDELIMITER, System.StringSplitOptions.RemoveEmptyEntries);

                float.TryParse(axis[0], out castPosition.x);
                if (axis.Length > 1)
                    float.TryParse(axis[1], out castPosition.y);
            }else if (match.Value == EXPRESSIONCAST_ID)
            {
                startIndex = match.Index + EXPRESSIONCAST_ID.Length;//还不够细心，容易出bug
                endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;
                string castExp = rawSpeaker.Substring(startIndex, endIndex - (startIndex + 1));

                CastExpressions = castExp.Split(EXPRESSIONLAYER_JOINER)
                    .Select(x =>
                    {
                        var parts = x.Trim().Split(EXPRESSIONLAYER_DELIMITER);
                        return (int.Parse(parts[0]), parts[1]);
                    }).ToList();
            }
        }
    }
}
