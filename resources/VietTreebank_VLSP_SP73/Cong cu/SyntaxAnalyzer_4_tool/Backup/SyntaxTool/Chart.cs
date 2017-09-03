using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

namespace SyntaxTool
{
    class Chart
    {
        #region//phần khai báo thuộc tính
        Grammar grammar; //bộ luật sinh
        Agenda agenda;

        //List<ActRule> listOfActRules; //danh sách các luật đang xem xét
        List<List<ActRule>>[] rightIndex_listOfActRules; //danh sách các luật đang được xem xét, sắp xếp theo terminal sau vị trí dot
        List<ActRule> listOfRoots; //danh sách các nút có thể là nút gốc của cây
        int lastIndex; //chỉ số cuối cùng của câu mà ta cần xem xét
        #endregion

        #region//phần khởi tạo
        public Chart(string  pathOfGrammar )
        {
            this.grammar = new Grammar(pathOfGrammar);
            //this.listOfActRules = new List<ActRule>();
            this.rightIndex_listOfActRules = new List<List<ActRule>>[0];
        }
        public Chart(Grammar grammar)
        {
            this.grammar = grammar;
            //this.listOfActRules = new List<ActRule>();
            this.rightIndex_listOfActRules = new List<List<ActRule>>[0];
        }
        #endregion

        #region một số hàm chức năng
        //hàm parsing một câu đầu vào
        public List<Tree> ChartParsing(string sentence)
        { 
            //cắt câu đầu vào
            string[] split1 = new string[] {" "};
            string[] s = sentence.Split(split1, StringSplitOptions.RemoveEmptyEntries);
            string[] non_terminalsOfS = new string[s.Length];
            string[] terminalsOfS = new string[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                int indexOfSlash = s[i].IndexOf("//");
                if (indexOfSlash >= 0)
                {
                    non_terminalsOfS[i] = s[i].Substring(indexOfSlash + 2);
                    terminalsOfS[i] = s[i].Substring(0, indexOfSlash);
                }
                else
                {
                    non_terminalsOfS[i] = s[i];
                    terminalsOfS[i] = "";
                }
            }

            return ChartParsing(non_terminalsOfS, terminalsOfS);
        }
        public List<Tree> ChartParsing(string[] non_terminals, string[] terminals)
        {
            if ((non_terminals != null)&&(terminals != null))
            {
                #region phần khởi tạo lại các thuộc tính
                //this.listOfActRules = new List<ActRule>();
                this.rightIndex_listOfActRules = new List<List<ActRule>>[non_terminals.Length + 1];
                for (int i = 0; i <= non_terminals.Length; i++)
                {
                    this.rightIndex_listOfActRules[i] = new List<List<ActRule>>();
                }
                this.listOfRoots = new List<ActRule>();
                this.agenda = new Agenda();
                #endregion
                //đưa partsOfSpeech vào Agenda
                if (PartsOfSpeechToAgenda(non_terminals, terminals) == true)
                { 
                    //lấy lần lượt ActRule Từ Agenda cho đến khi nào hết
                    while (true)
                    {
                        ActRule arInAgenda = GetFromAgendaToChart();
                        if (arInAgenda != null)
                        {
                            ParseInListOfActRules(arInAgenda);
                        }
                        else
                            break;
                    }
                    //gọi hàm dò vết, tìm cây cú pháp
                    List<Tree> lt = this.Trace();
                    //trả về tập hợp các cây cho nơi gọi hàm
                    return lt;
                }
            }
            return null;
        }
        //hàm dò vết, xem sau khi phân tích cú pháp thì thu được bao nhiêu cây, kết quả trả về các cây tìm đc
        public List<Tree> Trace()
        { 
            //kiểm tra xem có cây cú pháp nào ko
            if (this.listOfRoots != null)
            {
                if (this.listOfRoots.Count > 0)
                {
                    List<Tree> lt = new List<Tree>();
                    for (int i = 0; i < this.listOfRoots.Count; i++)
                    {
                        //lần lượt tạo từng cây 1
                        Tree t = new Tree(this.listOfRoots[i]);
                        lt.Add(t);
                    }
                    //return lại tập hợp các cây cú pháp
                    return lt;
                }
            }
            //nếu không thì return null
            return null;
        }
        //hàm thêm các partsOfSpeech từ câu đầu vào, vào trong Agenda
        bool PartsOfSpeechToAgenda(string[] non_terminals, string[] terminals)
        {
            //thêm xen kẽ các nullActRule và Các ActRule mà vế trái là partsOfSpeech[i] vào trong Agenda
            if ((non_terminals != null) && (terminals != null))
            {
                //lấy chỉ số độ dài của câu
                this.lastIndex = non_terminals.Length;
                //thêm lần lượt từng tag vào trong Agenda
                for (int i = 0; i < non_terminals.Length; i++)
                {
                    Rule r = new Rule(non_terminals[i], terminals[i]);
                    ActRule ar;
                    if (terminals[i].Equals(""))
                    {
                        ar = new ActRule(r, 1, null, i, i + 1);
                    }
                    else
                    {
                        Rule childOfR = new Rule(terminals[i], "null");
                        ActRule childOfAr = new ActRule(childOfR, 0, null, i, i + 1);
                        ar = new ActRule(r, 1, childOfAr, i, i + 1);
                        //this.addNullRulesIntoAgenda(i);
                    }
                    this.agenda.SetActRuleToAgenda(ar);
                }
                //this.addNullRulesIntoAgenda(partsOfSpeech.Length);
            }
            return true;
        }
        //hàm lấy một ActRule trong Agenda, thêm vào trong Chart (listOfActRules)
        ActRule GetFromAgendaToChart()
        {
            if (this.agenda != null)
            { 
                //lấy một ActRule trong Agenda
                ActRule arInAgenda = this.agenda.getActRule();
                if (arInAgenda != null)
                { 
                    //Tìm trong Grammar xem có Rule nào mà phần tử đầu tiên, 
                    //bên vế phải chứa vế trái của Rule này hay không
                    List<Rule> lr = grammar.searchInFirstRightRules(arInAgenda.leftOfRule);
                    if (lr != null)
                    {
                        for (int i = 0; i < lr.Count; i++)
                        {
                            //Tạo một actrule mới, bắt nguồn từ rule tìm được trong grammar
                            //ta parse qua một đơn vị
                            ActRule newActRule = new ActRule(lr[i], 1, arInAgenda, arInAgenda.leftIndex, arInAgenda.rightIndex);
                            #region thêm newActRule vào trong Chart
                            List<List<ActRule>> lla = this.rightIndex_listOfActRules[arInAgenda.rightIndex];
                            int j = 0;
                            for (; j < lla.Count; j++)
                            {
                                if (lla[j][0].stringAfterDot().Equals(newActRule.stringAfterDot()) == true)
                                {
                                    lla[j].Add(newActRule);
                                    break;
                                }
                            }
                            if (j == lla.Count)
                            {
                                List<ActRule> newChildOfLla = new List<ActRule>();
                                newChildOfLla.Add(newActRule);
                                lla.Add(newChildOfLla);
                            }
                            #endregion
                            //this.listOfActRules.Add(newActRule);
                            #region kiểm tra xem ActRule này có complete hay không, nếu có thì lại thêm vào Agenda
                            if (newActRule.IsCompleted())
                            {
                                this.agenda.SetActRuleToAgenda(newActRule);
                                //sau đó kiểm tra xem actRule này có khả năng là nút gốc của cây hay không
                                //nếu đúng thì đưa vào mảng các nút gốc của cây, có gì sau này cũng dễ tìm kiếm
                                if (newActRule.leftOfRule == "Cau")
                                {
                                    if (newActRule.leftIndex == 0)
                                    {
                                        if (newActRule.rightIndex == this.lastIndex)
                                            this.listOfRoots.Add(newActRule);
                                    }
                                }
                                //_______________
                            }
                            #endregion
                        }
                    }
                    return arInAgenda;
                }
            }
            return null;
        }
        //hàm tìm trong chart xem có Actrule nào mà đang xem xét tới ActRule vừa thêm vào từ 
        //Agenda không, thì parse qua
        void ParseInListOfActRules(ActRule ar)
        {
            //khai báo list các actrule có thể parse qua
            List<ActRule> la = new List<ActRule>();
            //lấy ra danh sách các rule có rightIndex trùng với leftIndex của luật mới thêm vào
            List<List<ActRule>> lla = this.rightIndex_listOfActRules[ar.leftIndex];
            #region lấy ra trong lla danh sách các rule có non_terminal sau dot trùng với leftOfRule của luật mới thêm vào
            for (int i = 0; i < lla.Count; i++)
            {
                if (lla[i][0].stringAfterDot().Equals(ar.leftOfRule) == true)
                {
                    la = lla[i];
                }
            }
            #endregion
            //bây giờ bắt đầu parse qua
            for (int i = 0; i < la.Count; i++)
            {
                ActRule newAr = la[i].NextDot(ar);
                if (newAr != null)
                {
                    #region thêm newAr vào trong Chart
                    List<List<ActRule>> llaOfNewAr = this.rightIndex_listOfActRules[newAr.rightIndex];
                    int j = 0;
                    for (; j < llaOfNewAr.Count; j++)
                    {
                        if (llaOfNewAr[j][0].stringAfterDot().Equals(newAr.stringAfterDot()) == true)
                        {
                            llaOfNewAr[j].Add(newAr);
                            break;
                        }
                    }
                    if (j == llaOfNewAr.Count)
                    {
                        List<ActRule> newChildOfLla = new List<ActRule>();
                        newChildOfLla.Add(newAr);
                        llaOfNewAr.Add(newChildOfLla);
                    }
                    #endregion
                    #region nếu completed thì thêm vào agenda
                    if (newAr.IsCompleted() == true)
                    {
                        this.agenda.SetActRuleToAgenda(newAr);
                        //this.completedRules.Add(newAr);
                        //sau đó kiểm tra xem actRule này có khả năng là nút gốc của cây hay không
                        //nếu đúng thì đưa vào mảng các nút gốc của cây, có gì sau này cũng dễ tìm kiếm
                        if (newAr.leftOfRule == "Cau")
                        {
                            if (newAr.leftIndex == 0)
                            {
                                if (newAr.rightIndex == this.lastIndex)
                                    this.listOfRoots.Add(newAr);
                            }
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion
    }
}
