using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;
public class ConfigHero : IDataRow
{
    public int Id { get; protected set; }
    public string Name { get; private set; }
    public int Hp { get; private set; }
    public void ParseDataRow(string dataRowText)
    {
        string[] text = dataRowText.Split(',');
        int index = 0;
        index++; // 跳过#注释列
        Id = int.Parse(text[index++]);
        Name = text[index++];
        Hp = int.Parse(text[index++]);
    }
}