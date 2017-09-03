using System;
using System.Collections.Generic;
using System.Text;

namespace SyntaxTool
{
    class Agenda
    {
        //phần khai báo thuộc tính
        public List<ActRule> listOfCompleteRules; //danh sách các rule đã đc thêm vào or đc complete bên Chart
        //hàm khởi tạo, đầu vào là string chứa câu cần phân tích
        public Agenda(string[] partsOfSpeech)
        {
            //đầu tiên là khởi tạo list
            this.listOfCompleteRules = new List<ActRule>();
            //để chắc chắn, đầu tiên phải kiểm tra đầu vào có null không
            if (partsOfSpeech != null)
            {
                //thêm lần lượt từng tag vào trong Agenda
                for (int i = 0; i < partsOfSpeech.Length; i++)
                {
                    Rule r = new Rule(partsOfSpeech[i] + " -> ");
                    ActRule ar = new ActRule(r, 0, null, i, i + 1);
                    this.listOfCompleteRules.Add(ar);
                }
            }
        }
        //hàm khởi tạo này đơn thuần chỉ tạo Queue mới để sau này sử dụng
        public Agenda()
        {
            this.listOfCompleteRules = new List<ActRule>();
        }
        #region một số hàm chức năng
        //hàm lấy một actRule ra khỏi agenda
        public ActRule getActRule()
        {
            if (this.listOfCompleteRules.Count > 0)
            {
                List<ActRule> newLa = new List<ActRule>();
                for (int i = 1; i < this.listOfCompleteRules.Count; i++)
                {
                    newLa.Add(this.listOfCompleteRules[i]);
                }
                ActRule arToReturn = this.listOfCompleteRules[0];
                this.listOfCompleteRules = newLa;
                return arToReturn;
            }
            else return null;
        }
        //hàm đưa một ActRule vào Agenda
        public bool SetActRuleToAgenda(ActRule actRule)
        {
            if (actRule != null)
            {
                this.listOfCompleteRules.Add(actRule);
                sort();
                return true;
            }
            return false;
        }
        //hàm sắp xếp các phần tử trong Agenda
        private void sort()
        {
            if (this.listOfCompleteRules != null)
            {
                //taom một mảng mới chứa tất cả các phần tử trong List cũ
                ActRule[] ar = new ActRule[this.listOfCompleteRules.Count];
                int count = -1;
                for (int i = 0; i < this.listOfCompleteRules.Count; i++)
                {
                    for (int j = 0; j < this.listOfCompleteRules.Count; j++)
                    {
                        if (this.listOfCompleteRules[i].leftIndex >= this.listOfCompleteRules[j].leftIndex)
                            count++;
                    }
                    //kiểm tra xem tại vị trí count đã có phần tử nào chưa, nếu chưa có thì thêm vào
                    //nếu có rồi thì lùi lại đến khi có khoảng trống
                    while (ar[count] != null)
                    {
                        count--;
                    }
                    if (ar[count] == null)
                        ar[count] = this.listOfCompleteRules[i];
                    count = -1;
                }
                //sau đó gán lại list
                List<ActRule> tam = new List<ActRule>();
                tam.AddRange(ar);
                this.listOfCompleteRules = tam;
            }

        }
        #endregion
    }
}
