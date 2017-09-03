using System;
using System.Collections.Generic;
using System.Text;

namespace SyntaxTool
{
    class Rule //class chứa cấu trúc của một luật văn phạm, và một số chức năng tìm kiếm luật
    {
        public String leftOfRule;//vế trái của luật
        public List<String> rightOfRule;//vế phải của luật
        //phần hàm khởi tạo
        public Rule(string stringRule)
        {
            if (stringRule != "")
            {
                if(stringRule.IndexOf("->") != -1)
                {
                    //một luật bao gồm 2 phần: phần bên trái dấu "->" và phần bên phải dấu "->"
                    //đầu tiên là tìm vế trái của dấu ->
                    string ls = stringRule.Substring(0, stringRule.IndexOf("->"));
                    //tìm vế trái của dấu ->
                    string rs = stringRule.Substring(stringRule.IndexOf("->") + 2);
                    //chuẩn hóa vế trái
                    this.leftOfRule = ls.Trim();
                    //chuẩn hóa vế phải
                    string[] split = new string[1] { " " };
                    string[] splitedRight = rs.Split(split, StringSplitOptions.RemoveEmptyEntries);
                    if (splitedRight.Length > 0)
                        this.rightOfRule = new List<string>(splitedRight);
                    else
                    {
                        this.rightOfRule = new List<string>();
                        this.rightOfRule.Add("null");
                    }
                }
            }
        }
        public Rule(Rule rule)
        {
            //đơn giản, ta gán left và right từ luật cũ sang luật mới
            this.leftOfRule = rule.leftOfRule;
            this.rightOfRule = new List<string>();
            this.rightOfRule.AddRange(rule.rightOfRule);
        }
        public Rule(string leftOfRule, List<String> rightsOfRule)
        {
            this.leftOfRule = leftOfRule;
            this.rightOfRule = new List<string>();
            if (rightsOfRule != null)
                this.rightOfRule.AddRange(rightsOfRule);
            else
            {
                this.rightOfRule.Add("null");
            }
        }
        public Rule(string leftOfRule, string rightOfRule)
        {
            this.leftOfRule = leftOfRule;
            this.rightOfRule = new List<string>();
            if (rightOfRule != null)
                this.rightOfRule.Add(rightOfRule);
            else
            {
                this.rightOfRule.Add("null");
            }
        }
        //hàm tìm kiếm trong rule xem có một ký tự ở bên vế trái hay không
        public bool isInLeftRule(string nonTerminal)
        {
            if (this.leftOfRule == nonTerminal)
                return true;
            else
                return false;
        }
        //hàm tìm kiếm trong rule xem có một ký tự ở đầu tiên, bên vế phải hay không
        public bool isFirstOfRightRule(string nonTerminal)
        {
            if (this.rightOfRule != null)
            {
                if (this.rightOfRule[0] == nonTerminal)
                    return true;
            }
            return false;
        }
        //hàm kiểm tra xem luật này có thể suy ra null hay không
        public bool canNull()
        {
            if (this.rightOfRule == null)
                return true;
            else
                return false;
        }
        //hàm chuyển đổi rule về dạng string
        public override string ToString() 
        {
            string stringToReturn;
            stringToReturn = this.leftOfRule + " ->";
            if (this.rightOfRule != null)
            {
                for (int i = 0; i < this.rightOfRule.Count; i++)
                {
                    stringToReturn += " " + this.rightOfRule[i];
                }
            }
            return stringToReturn;
        }
    }

}