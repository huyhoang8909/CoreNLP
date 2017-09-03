using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SyntaxTool
{
    class StandardizeRule
    {
        public static List<String> Standardize(String input)
        { 
            //Cắt string ra làm nhiều phần khác nhau
            String[] separator = {" "};
            List<String> listInInput = new List<string>();
            listInInput.AddRange(input.Split(separator, StringSplitOptions.RemoveEmptyEntries));
            List<TagToStandardize> lmt = new List<TagToStandardize>();

            if (listInInput != null)
            {
                if (listInInput.Count > 0)
                {
                    //tạo ra các TagToStandardize
                    foreach (String s in listInInput)
                    {
                        if ((s.Contains("[")) && (s.Contains("]")))
                        {
                            lmt.Add(new TagToStandardize(true, s.Substring(1, s.Length - 2)));
                        }
                        else
                            lmt.Add(new TagToStandardize(false, s));
                    }

                    //bay gio thi hoan vi ket qua
                    List<String> newls = new List<string>();
                    List<String> result = new List<string>();

                    result.AddRange(lmt[0].nameOfTag);
                    for (int i = 0; i < result.Count; i++)
                    {
                        result[i] += " ";
                    }

                    for (int i = 1; i < lmt.Count; i++)
                    {
                        
                        TagToStandardize tag = lmt[i];
                        if (tag.nameOfTag.Count == 1)
                        {
                            for (int j = 0; j < result.Count; j++)
                            {
                                result[j] += tag.nameOfTag[0] + " ";
                            }
                        }
                        if (tag.nameOfTag.Count > 1)
                        {
                            List<String> result1 = new List<string>();
                            result1.AddRange(result);
                            for (int j = 1; j < tag.nameOfTag.Count; j++)
                            {
                                result.AddRange(result1);
                            }
                            for (int j = 0; j < tag.nameOfTag.Count; j++)
                            {
                                for (int k = 0; k < result1.Count; k++)
                                {
                                    result[k + j*result1.Count] += tag.nameOfTag[j] + " ";
                                }
                            }
                        }
                        
                    }
                    //xoa ket qua ma = "";
                    List<string> newResult = new List<string>();
                    foreach (string oneRs in result)
                    {
                        if ( (oneRs.Split(separator, StringSplitOptions.RemoveEmptyEntries)).Length != 0)
                            newResult.Add(oneRs);
                    }
                    return newResult;
                }
            }
            return null;
        }

        public static List<String> StandardizeFile(String pathOfInput, String pathOfOutput)
        {
            if (File.Exists(pathOfInput) == true)
            {
                FileStream fs = new FileStream(pathOfInput, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                List<String> stringToReturn = new List<string>();
                String s;
                while ((s = sr.ReadLine()) != null)
                {
                    List<String> newS1 = Standardize(s);
                    if (newS1 != null)
                    { 
                        stringToReturn.AddRange(newS1);
                    }
                }
                sr.Close();
                fs.Close();
                //loại bỏ bớt những rule trùng lặp:
                List<Rule> lr = new List<Rule>();
                foreach (string sIn in stringToReturn)
                {
                    bool b = true;
                    Rule r = new Rule(sIn);
                    if (r.rightOfRule.Count == 0) continue;
                    else if (r.rightOfRule[0].Equals("null")) continue;
                    foreach (Rule ruleInList in lr)
                    {
                        if (ruleInList.ToString().Equals(r.ToString()))
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b == true)
                        lr.Add(r);
                }
                //ghi ra file
                if (lr.Count > 0)
                {
                    FileStream nfs = new FileStream(pathOfOutput, FileMode.OpenOrCreate);
                    StreamWriter nsw = new StreamWriter(nfs, Encoding.Unicode);
                    foreach (Rule r in lr)
                    {
                        nsw.WriteLine(r.ToString());
                    }
                    nsw.Close();
                    nfs.Close();
                }
                return stringToReturn;
            }
            return null;
        }
    }
    
}
