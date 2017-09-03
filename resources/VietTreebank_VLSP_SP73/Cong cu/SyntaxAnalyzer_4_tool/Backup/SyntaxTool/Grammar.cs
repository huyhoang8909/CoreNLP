using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SyntaxTool
{
    class Grammar
    {
        public List<Rule> rules; //danh sách các rule tạo nên grammar  
        public List<string> nonTerminals; //danh sách các nonTerminals tồn tại trong grammar
        public List<Rule> nullRules; //danh sách các Rule mà suy ra null
        private List<List<Rule>> leftSortedRules;//danh sách các Rule đc sắp xếp theo vế trái của luật
        private List<List<Rule>> rightSortedRules; //danh sách các Rule đc sắp xếp theo vế phải của luật

        string pathOfGrammarFile;
        //phần khởi tạo
        public Grammar(string pathOfGrammarFile)
        {
            this.pathOfGrammarFile = pathOfGrammarFile;
            //khởi tạo rules và nonTerminals
            this.rules = new List<Rule>();
            this.nonTerminals = new List<string>();
            this.nullRules = new List<Rule>();
            this.leftSortedRules = new List<List<Rule>>();
            this.rightSortedRules = new List<List<Rule>>();

            //đầu tiên phải kiểm tra xem đường dẫn file có tồn tại hay không
            if (File.Exists(pathOfGrammarFile))
            { 
                try
                {
                    FileStream fs = new FileStream(pathOfGrammarFile, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    string s; //biến tạm dùng để đọc một string từ file
                    //bắt đầu đọc từ file nào
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (s != "")
                        {
                            Rule r = new Rule(s);
                            //kiểm tra xem trong list nonterminal đã có nonterminal này hay chưa
                            if (!this.nonTerminals.Contains(r.leftOfRule))
                            {
                                //nếu chưa có thì thêm vào trong đó
                                this.nonTerminals.Add(r.leftOfRule);
                            }
                            //tiếp tục thêm rule vào danh sách rules
                            this.rules.Add(r);
                        }
                    }
                    //đóng file
                    sr.Close();
                    fs.Close();
                    //sắp xếp lại các thuộc tính cho dễ tìm kiếm
                    sort();
                      
                }
                catch(Exception e)
                {
                    //do nothing
                }
            }
        }
        
        #region một số hàm chức năng
        //hàm sắp xếp list
        private void sort()
        {
            #region//sắp xếp tập rules
            //khai báo tạm một tamRules để chuyển đổi Rules về dạng string, dễ dàng cho việc sắp xếp
            List<string> tamRules = new List<string>();
            for (int i = 0; i < this.rules.Count; i++)
            {
                tamRules.Add(this.rules[i].ToString());
            }
            //bắt đầu sắp xếp, lưu kết quả vào tamRules
            tamRules = this.sortStringList(tamRules);
            //chuyển đổi ngược lại, từ string sang Rules và lưu lại chỗ cũ
            this.rules.RemoveRange(0, this.rules.Count);
            for (int i = 0; i < tamRules.Count; i++)
            {
                this.rules.Add(new Rule(tamRules[i]));
            }
            #endregion
            #region tạo danh sách sắp xếp theo vế trái
            //duyệt lần lượt các rule trong grammar
            for (int i = 0; i < this.rules.Count; i++)
            {
                bool isNewList = true; //cờ đánh dấu xem có thêm listRule mới hay không
                for (int j = 0; j < this.leftSortedRules.Count; j++)
                {
                    //kiểm tra xem leftRule này đã có trong danh sách sắp xếp chưa, nếu có rồi
                    //thì thêm vào danh sách đó
                    if (this.leftSortedRules[j][0].leftOfRule.Equals(this.rules[i].leftOfRule))
                    {
                        isNewList = false;
                        this.leftSortedRules[j].Add(this.rules[i]);
                        break;//không cần thiết phải tiếp tục vòng lặp nữa, break ra
                    }
                }
                //nếu vế trái của rule này chưa là một đề mục trong danh sách sắp xếp, 
                //thì ta tạo ra một list mới, thêm vào danh sách sắp xếp
                if (isNewList == true)
                {
                    List<Rule> newListRule = new List<Rule>();
                    newListRule.Add(this.rules[i]);
                    this.leftSortedRules.Add(newListRule);
                }
            }
            #endregion
            #region tạo danh sách sắp xếp theo vế phải
            //duyệt lần lượt các rule trong grammar
            for (int i = 0; i < this.rules.Count; i++)
            {
                bool isNewList = true; //cờ đánh dấu xem có thêm listRule mới hay không
                for (int j = 0; j < this.rightSortedRules.Count; j++)
                {
                    //kiểm tra xem leftRule này đã có trong danh sách sắp xếp chưa, nếu có rồi
                    //thì thêm vào danh sách đó
                    if (this.rightSortedRules[j][0].rightOfRule[0].Equals(this.rules[i].rightOfRule[0]))
                    {
                        isNewList = false;
                        this.rightSortedRules[j].Add(this.rules[i]);
                        break;//không cần thiết phải tiếp tục vòng lặp nữa, break ra
                    }
                }
                //nếu vế trái của rule này chưa là một đề mục trong danh sách sắp xếp, 
                //thì ta tạo ra một list mới, thêm vào danh sách sắp xếp
                if (isNewList == true)
                {
                    List<Rule> newListRule = new List<Rule>();
                    newListRule.Add(this.rules[i]);
                    this.rightSortedRules.Add(newListRule);
                }
            }
            #endregion
            #region sắp xếp lại danh sách sắp xếp theo thứ tự string của vế phải
            List<Rule>[] newRightSortedRules = new List<Rule>[this.rightSortedRules.Count];
            int index = -1;
            for (int i = 0; i < this.rightSortedRules.Count; i++)
            {
                string A = this.rightSortedRules[i][0].rightOfRule[0];
                for (int j = 0; j < this.rightSortedRules.Count; j++)
                {
                    string B = this.rightSortedRules[j][0].rightOfRule[0];
                    if (A.CompareTo(B) >= 0)
                        index++;
                }
                while (newRightSortedRules[index] != null)
                {
                    index--;
                }
                newRightSortedRules[index] = this.rightSortedRules[i];
                index = -1;
            }
            this.rightSortedRules = new List<List<Rule>>();
            this.rightSortedRules.AddRange(newRightSortedRules);
            #endregion
        }
        //hàm sắp xếp một danh sách các string, phục vụ cho hàm sort
        private List<string> sortStringList(List<string> ls)
        {
            if (ls != null)
            {
                List<string> newLs = new List<string>();
                string[] tam = new string[ls.Count];
                int index = -1; //chỉ số, chỉ định xem string này nằm ở đâu trong newList
                for (int i = 0; i < ls.Count; i++)
                {
                    for (int j = 0; j < ls.Count; j++)
                    {
                        if (ls[i].CompareTo(ls[j]) >= 0)
                        {
                            index++;
                        }
                    }
                    //kiểm tra xem tại vị trí newLs đã có phần tử nào chưa, nếu chưa có thì thêm vào
                    //nếu có rồi thì lùi lại đến khi có khoảng trống
                    while (tam[index] == ls[i])
                    {
                        index--;
                    }
                    tam[index] = ls[i];
                    index = -1;
                }
                newLs.AddRange(tam);
                return newLs;
            }
            return null;
        }
        //hàm tìm kiếm trong grammar xem có rule nào xuất phát bên vế trái là nonterminal mà ta cần tìm ko
        public List<Rule> searchInLeftRules(string nonTerminal)
        {
            int left = 0;
            int right = this.leftSortedRules.Count - 1;
            int i = 0;
            while (left < right)
            {
                i = (left + right) / 2;
                
                if (this.leftSortedRules[i][0].leftOfRule.CompareTo(nonTerminal) == 0)
                {
                    return this.leftSortedRules[i];
                }
                if (i == left) break;
                else if (this.leftSortedRules[i][0].leftOfRule.CompareTo(nonTerminal) > 0)
                {
                    right = i;
                }
                else
                {
                    left = i;
                }
            }
            if (this.rightSortedRules[right][0].rightOfRule[0].CompareTo(nonTerminal) == 0)
            {
                return this.rightSortedRules[right];
            }
            return null;
        }
        //hàm tìm kiếm trong grammar xem có rule nào xuất phát ở phần tử đầu tiên bên vế phải có
        //nonterminal mà ta cần tìm không
        public List<Rule> searchInFirstRightRules(string nonTerminal)
        {
            int left = 0;
            int right = this.rightSortedRules.Count - 1;
            int i;
            while (left < right)
            {
                i = (left + right) / 2;
                
                if (this.rightSortedRules[i][0].rightOfRule[0].CompareTo(nonTerminal) == 0)
                {
                    return this.rightSortedRules[i];
                }
                if (i == left) break;
                else if (this.rightSortedRules[i][0].rightOfRule[0].CompareTo(nonTerminal) > 0)
                {
                    right = i;
                }
                else
                {
                    left = i;
                }
            }
            if (this.rightSortedRules[right][0].rightOfRule[0].CompareTo(nonTerminal) == 0)
            {
                return this.rightSortedRules[right];
            }
            return null;
        }
        //hàm lưu Grammar trở lại
        public void saveGrammar()
        {
            //bây giờ ghi ra file
            FileStream fs = new FileStream(this.pathOfGrammarFile, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = this.rules.Count - 1; i >= 0; i--)
            {
                sw.WriteLine(this.rules[i].ToString());
            }
            //đóng file
            sw.Close();
            fs.Close();
        }
        //hàm thêm một rule
        public void addNewRule(Rule newRule)
        {
            if (newRule != null)
            {
                this.rules.Add(newRule);
                sort();
            }
        }
        #endregion
    }
}
