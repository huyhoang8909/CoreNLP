using System;
using System.Collections.Generic;
using System.Text;

namespace SyntaxTool
{
    class ActRule //Class chứa rule trong chart, là các rule ta đang xem xét
    {
        #region//phần khai báo thuộc tính
        public string leftOfRule; //vế trái của luật
        public List<string> rightOfRule; //vế phải của luật
        public int dot;//dấu chấm, đứng trước nonTerminal ta đang xem xét
        public List<ActRule> childs; //ActRule là con của nó trong cây cú pháp
        public int leftIndex; //vị trí trái nhất của nó trong câu
        public int rightIndex; //vị trí phải nhất của nó trong câu
        #endregion

        #region phần khởi tạo
        //hàm này được dùng khi cần tạo một actRule mới từ Agenda để thêm vào trong Chart
        //nên mặc định dot = 0
        public ActRule(Rule rule, int leftIndex, int rightIndex)
        {
            this.leftOfRule = rule.leftOfRule;
            if (rule.rightOfRule != null)
            {
                this.rightOfRule = new List<string>();
                this.rightOfRule.AddRange(rule.rightOfRule);
            }
            this.dot = 0;
            this.childs = new List<ActRule>();
            this.leftIndex = leftIndex;
            this.rightIndex = rightIndex;
        }
        //tiếp theo là hàm khởi tạo mà có fix giá trị dot
        public ActRule(Rule rule, int dot, int leftIndex, int rightIndex)
        {
            this.leftOfRule = rule.leftOfRule;
            if (rule.rightOfRule != null)
            {
                this.rightOfRule = new List<string>();
                this.rightOfRule.AddRange(rule.rightOfRule);
            }
            this.dot = dot;
            this.childs = new List<ActRule>();
            this.leftIndex = leftIndex;
            this.rightIndex = rightIndex;
        }
        public ActRule(Rule rule, int dot, ActRule child, int leftIndex, int rightIndex)
        {
            this.leftOfRule = rule.leftOfRule;
            if (rule.rightOfRule != null)
            {
                this.rightOfRule = new List<string>();
                this.rightOfRule.AddRange(rule.rightOfRule);
            }
            this.dot = dot;
            this.childs = new List<ActRule>();
            if (child != null)
                this.childs.Add(child);
            this.leftIndex = leftIndex;
            this.rightIndex = rightIndex;
        }
        public ActRule(ActRule rule, int dot, ActRule child)
        {
            this.leftOfRule = rule.leftOfRule;
            if (rule.rightOfRule != null)
            {
                this.rightOfRule = new List<string>();
                this.rightOfRule.AddRange(rule.rightOfRule);
            }
            this.dot = dot;
            this.childs = new List<ActRule>();
            if (rule.childs != null)
                this.childs.AddRange(rule.childs);
            if (child != null)
                this.childs.Add(child);
            this.leftIndex = rule.leftIndex;
            this.rightIndex = rule.rightIndex;
        }
        public ActRule(ActRule rule, int dot, ActRule child, int leftIndex, int rightIndex)
        {
            this.leftOfRule = rule.leftOfRule;
            if (rule.rightOfRule != null)
            {
                this.rightOfRule = new List<string>();
                this.rightOfRule.AddRange(rule.rightOfRule);
            }
            this.dot = dot;
            this.childs = new List<ActRule>();
            if(rule.childs != null)
                this.childs.AddRange(rule.childs);
            if(child != null)
                this.childs.Add(child);
            this.leftIndex = leftIndex;
            this.rightIndex = rightIndex;
        }
        #endregion
        #region một số hàm ứng dụng
        //hàm kiểm tra xem một nonterminal có phải ở vị trí dot ko
        private bool isAct(string nonTerminal)
        {
            if(this.rightOfRule != null)
            {
                if (this.dot < this.rightOfRule.Count)
                    if (this.rightOfRule[this.dot] == nonTerminal)
                        return true;
            }
            return false;
        }
        //hàm kiểm tra xem một ActRule có phải ở vị trí dot không
        public bool IsAct(ActRule ar)
        {
            bool i = isAct(ar.leftOfRule);
            bool j = (this.rightIndex == ar.leftIndex);
            if (i && j)
                return true;
            else
                return false;
        }
        //hàm tạo ra một ActRule mới, có giá trị rule như cũ, chỉ nextDot
        public ActRule NextDot(ActRule ar)
        {
            if ((this.dot >= 0) && (this.dot < this.rightOfRule.Count))
            {
                if (this.rightIndex == ar.leftIndex)
                {
                    ActRule newAr = new ActRule(this, this.dot + 1, ar,this.leftIndex, ar.rightIndex);
                    return newAr;
                }
            }
            return null;
        }
        public override string ToString()
        {
            //đầu tiên là chỉ số index
            string stringToReturn = "[" + this.leftIndex.ToString() + "] ";
            //vế trái của luật và dấu suy ra
            stringToReturn += this.leftOfRule + " ->";
            //vế phải của luật, nếu có
            if (this.rightOfRule != null)
            {
                for (int i = 0; i < dot; i++)
                {
                    stringToReturn += " " + this.rightOfRule[i];
                }
                stringToReturn += ".";
                for (int i = dot; i < this.rightOfRule.Count; i++)
                {
                    stringToReturn += " " + this.rightOfRule[i];
                }
            }
            //index kết thúc luật
            stringToReturn += "[" + this.rightIndex.ToString() + " ]";
            return stringToReturn;
        }
        //hàm kiểm tra xem actRule có phải là complete hay không
        public bool IsCompleted()
        {
            if (this.dot == this.rightOfRule.Count)
                return true;
            else
                return false;
        }
        public string stringAfterDot()
        { 
            if(IsCompleted() == true) return "";
            return this.rightOfRule[this.dot];
        }
        #endregion
    }
}
