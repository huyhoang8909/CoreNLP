using System;
using System.Collections.Generic;
using System.Text;

namespace SyntaxTool
{
    class NodeComparStatistics
    {
        //public int total = 0;  // tong so them/sua/xoa
        public int newNodeCount = 0;  // so nut moi
        public int modNodeCount = 0;  // so nut duoc sua
        public int delNodeCount = 0;  // so nut bi xoa
    }

    public enum NodeType
    {
        Terminal,
        Nonterminal,
        Preterminal
    }

    //Class chứa Node của cây
    class Node
    {
        //phần khai báo thuộc tính
        public string name; //tên của node (nut la thi name la word)
        public List<Node> childs; //chứa các Childs của nó
        public int pos1=-1, pos2=-1;

        // 0: nut moi, 1: nut bi sua, 2: nut binh thuong 
        public int modType=2;  // khi so sanh cay nay voi phien ban cu --> modType
        
        public NodeType type;  // phan loai nut theo van pham hinh thuc
        public bool isNullElem=false;  // cho biet nut rong hay khong
        public bool isMapped=false;  // cho biet nut da duoc anh xa sang nut cay khac chua

        private int hight = 0;  // chieu cao cua nut

        // ham khoi tao mac dinh
        // Thai bo xung ngay 8/3/2009
        public Node()
        { 
        }
/*
        //phần khởi tạo
        public Node(ActRule ar)
        {
            if (ar != null)
            {
                this.name = ar.leftOfRule;
                if (ar.childs != null)
                {
                    this.childs = new List<Node>();
                    for (int i = 0; i < ar.childs.Count; i++)
                    {
                        if (ar.childs[i] != null)
                        {
                            Node newChild = new Node(ar.childs[i]);
                            this.childs.Add(newChild);
                        }
                    }
                }
            }
        }
 */ 
        public Node(List<String> stringsOfTree)
        {
            if (stringsOfTree != null)
            {
                if (stringsOfTree.Count == 0) return;
                if ((stringsOfTree[0].Equals("(")) && 
                    (stringsOfTree[stringsOfTree.Count - 1].Equals(")")) &&
                    (stringsOfTree.Count > 2))
                {
                    //khởi tạo một số phần tử
                    this.name = stringsOfTree[1];
                    List<String> tempList = new List<string>();
                    List<List<String>> childs = new List<List<string>>();
                    #region tìm các string là node con của node đang xét
                    int count = 0;
                    for (int i = 2; i < stringsOfTree.Count - 1; i++)
                    {
                        if (stringsOfTree[i].Equals("("))
                        {
                            if ((tempList.Count > 0) &&(count == 0))
                            {
                                childs.Add(tempList);
                                tempList = new List<string>();
                            }
                            count++;
                            tempList.Add(stringsOfTree[i]);
                        }
                        else if (stringsOfTree[i].Equals(")"))
                        {
                            count--;
                            tempList.Add(stringsOfTree[i]);
                            if (count == 0)
                            {
                                childs.Add(tempList);
                                tempList = new List<string>();
                            }
                        }
                        else
                        {
                            tempList.Add(stringsOfTree[i]);
                        }
                    }
                    if (stringsOfTree[stringsOfTree.Count - 2].Equals(")") == false)
                    {
                        childs.Add(tempList);
                    }
                    #endregion
                    //gọi đệ quy tạo các node con
                    this.childs = new List<Node>();
                    foreach (List<string> ls in childs)
                    {
                        Node n = new Node(ls);
                        this.childs.Add(n);
                    }
                }

                else if (stringsOfTree.Count >= 0)
                {
                    string aChild = "";
                    for (int i = 0; i < stringsOfTree.Count; i++)
                    {
                        aChild += stringsOfTree[i] + " ";
                    }
                    this.name = aChild;
                  }

            }
            else this.name = null;
        }
        #region một số hàm chức năng
        public String ToString(string space) 
        {
            String sToReturn = "( ";
            sToReturn += this.name + "\n";
            if (this.childs != null)
            {
                foreach (Node aChild in this.childs)
                {
                    sToReturn += space + aChild.ToString(space + "\t") + "\n";
                }
            }
            sToReturn = sToReturn.Substring(0, sToReturn.LastIndexOf("\n"));
            sToReturn += " )";
            return sToReturn;
        }
        
        #endregion

        public void SetPos()
        {
            if (isNullElem) return;

            if (childs!=null && childs.Count > 0)  // chi xet nut trong vi nut la da xet truoc roi
            {
                pos1 = 500; pos2 = -1;
                for (int i = 0; i < childs.Count; ++i)
                    if (!childs[i].isNullElem)  // chi xet cac nut khac null
                    {
                        childs[i].SetPos();
                        if (childs[i].pos1 < pos1) pos1 = childs[i].pos1;
                        if (childs[i].pos2 > pos2) pos2 = childs[i].pos2;
                    }
                
                //pos1 = childs[0].pos1;
                //pos2 = childs[childs.Count - 1].pos2;
            }
        }

        public void SetPosForLeaves(ref int n)
        {
            if (isNullElem) return;

            if (childs==null || childs.Count == 0)
            {
/*
                int syllCount = 1;
                name = name.Trim();
                for (int i = 1; i < name.Length; ++i)
                    if (name[i] == ' ' && name[i-1]!=' ')
                        syllCount++;
                pos1 = n;
                pos2 = n + syllCount - 1;
                n += syllCount;
 */
                int charCount = 0;
                name = name.Trim();
                for (int i = 0; i < name.Length; ++i)
                    if (name[i] != ' ')
                        ++charCount;
                pos1 = n;
                pos2 = n + charCount - 1;
                n += charCount;
            }
            else
            {
                for (int i = 0; i < childs.Count; ++i)
                    childs[i].SetPosForLeaves(ref n);
            }
        }

        public int FindNode(string inTag, NodeType inType, int inPos1, int inPos2)
        {
            if (inPos1 == pos1 && inPos2 == pos2 && inType==type )
            {
                if (inTag == name && !isMapped)
                {
                    isMapped = true;
                    return 2;
                }
                else
                {
                    if (!isNullElem && childs != null)  // di xuong tiep
                    {
                        // di xuong
                        for (int i = 0; i < childs.Count; ++i)
                            if (!childs[i].isNullElem)
                                if (childs[i].pos1 <= inPos1 && inPos2 <= childs[i].pos2)  // ko quan trong lam
                                {
                                    int t = childs[i].FindNode(inTag, inType, inPos1, inPos2);
                                    if (t != 0) return t;  // dam bao co kiem tra isMapped roi
                                }
                    }

                    if (!isMapped)  // nut hien thoi chua duoc map voi nut nao khac
                    {
                        isMapped = true;
                        return 1;
                    }

                    return 0;  // ko di xuong nua
                }
            }
            else
            {
                if (childs != null && childs.Count > 0)
                {
                    for (int i = 0; i < childs.Count; ++i)
                        if (childs[i].pos1 <= inPos1 && inPos2 <= childs[i].pos2)  // ko quan trong lam
                        {
                            int t = childs[i].FindNode(inTag, inType, inPos1, inPos2);
                            if (t != 0) return t;
                        }
                }

                return 0;
            }
        }

        // bottom-up
        public void ComputeModType( Node root )
        {
         // khong kiem tra terminal
            //if (type==NodeType.Terminal)
            //    return ;
            
            //name += "-" + type.ToString();

         // Cho nut con
            if (childs != null && childs.Count > 0)
            {
                for (int i = 0; i < childs.Count; ++i)
                    childs[i].ComputeModType(root);
            }

         // Cho nut hien thoi, bo qua nut rong
            if( !isNullElem )
                modType = root.FindNode(name, type, pos1, pos2);
        }

        public void SetType()
        {
            // Terminal (nut la)
            if (childs == null || childs.Count == 0)
            {
                type = NodeType.Terminal;
            }
            else
            {
             // Tinh cho nut con
                for (int i = 0; i < childs.Count; ++i)
                    childs[i].SetType();

             // Tinh cho nut hien thoi
                if (childs.Count == 1 && childs[0].type == NodeType.Terminal)
                    type = NodeType.Preterminal;
                else
                    type = NodeType.Nonterminal;
            } 
        }

        public void SetNullElem()
        {
            if (childs==null || childs.Count == 0)  // terminal (word)
            {
                if (name.Length > 0)
                    if (name[0] == '*')
                        isNullElem = true;
            }
            else  // nonterminal hoac preterminal
            {
                isNullElem = true;
                for (int i = 0; i < childs.Count; ++i)
                {
                    childs[i].SetNullElem();
                    if (!childs[i].isNullElem)
                    {
                        isNullElem = false;
                        //break;
                    }
                }
            }
        }

        public void GetSearchKeyList(ref List<String> skList)
        {
            if (childs == null || childs.Count == 0)  // terminal (word)
            {
                String key = name.Trim();
                if (key.Length > 0)
                {
                    if (!skList.Contains(key))
                        skList.Add(key);
                }
            }
            else  // nonterminal hoac preterminal
            {
                String key1 = "t=" + name.Trim();
                if (!skList.Contains(key1))
                    skList.Add(key1);

                String hWord = GetHeadWord();
                char[] hWordSep = { '+' };
                String[] hWordArr = hWord.Split(hWordSep);
                for (int i = 0; i < hWordArr.Length; ++i)
                {
                    String key = key1 + ";" + "hw=" + hWordArr[i];
                    if (!skList.Contains(key))
                        skList.Add(key);
                }
                                
                for (int i = 0; i < childs.Count; ++i)
                {
                    if (childs[i].type != NodeType.Terminal)
                    {
                        String key = key1 + ";" + "ct=" + childs[i].name.Trim();
                        if (!skList.Contains(key))
                            skList.Add(key);
                    }

                    childs[i].GetSearchKeyList(ref skList);
                }
            }
        }

        // Thai
        // Phuong thuc sau viet ngay 7/3/2009
        private int GetHeadIdx()
        {
            int headIdx = 0;  // gia tri mac dinh

            if (childs != null && childs.Count > 1)
            {
                // qui tac rieng cho S
                if (name == "S" || name == "SQ" || name.StartsWith("S-") || name.StartsWith("SQ-"))
                {
                    for (int i = 0; i < childs.Count; ++i)
                    {
                        String tag = childs[i].name;
                        if (tag=="VP" || tag.EndsWith("-PRD"))
                        {
                            headIdx = i;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < childs.Count; ++i)
                    {
                        String tag = childs[i].name;
                        if (tag.EndsWith("-H"))
                        {
                            headIdx = i;
                            break;
                        }
                    }
                }
            }

            return headIdx;
        }

        private void FindHeadIdx( ref List<int> oList )
        {
            //int headIdx = 0;  // gia tri mac dinh

            if (childs != null && childs.Count > 1)
            {
                // qui tac rieng cho S
                if (name == "S" || name == "SQ" || name.StartsWith("S-") || name.StartsWith("SQ-"))
                {
                    for (int i = 0; i < childs.Count; ++i)
                    {
                        String tag = childs[i].name;
                        if (tag == "VP" || tag.EndsWith("-PRD"))
                        {
                            //headIdx = i;
                            //break;
                            oList.Add(i);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < childs.Count; ++i)
                    {
                        String tag = childs[i].name;
                        if (tag.EndsWith("-H"))
                        {
                            //headIdx = i;
                            //break;
                            oList.Add(i);
                        }
                    }
                }
            }

            //return headIdx;
            if (oList.Count == 0)
            {
                // Xu ly dang VP -> VP C VP
                String X = GetCanonicalTag();
                for (int i = 0; i < childs.Count; ++i)
                {
                    String tag = childs[i].name;
                    if (tag == X)
                        oList.Add(i);
                }

                if (oList.Count == 0)
                    oList.Add(0);
            }
        }

        // Thai (8/3/2009)
        private String GetHeadWord()
        {
            // neu dang o nut la thi name chinh la tu
            if (childs == null || childs.Count == 0)
                return name.Trim();
            else
            {  // neu la nut trong thi lay qua head
                //return childs[GetHeadIdx()].GetHeadWord();

                List<int> headIdxList=new List<int>();
                FindHeadIdx(ref headIdxList);

                String headWordStr="";
                for(int i=0; i<headIdxList.Count; ++i){
                    if(i>0) headWordStr+= "+";
                    headWordStr+= childs[headIdxList[i]].GetHeadWord();
                }
                return headWordStr;
            }
        }

        private String GetFunctionTag()
        {
            if (name.Contains("-"))
            {
                int i = name.LastIndexOf('-');
                return name.Substring(i+1, name.Length - i-1);
            }
            return "";
        }

        // http://en.wikipedia.org/wiki/Canonical
        private String GetCanonicalTag()
        {
            if (name.Contains("-"))
            {
                int i = name.IndexOf('-');
                return name.Substring(0, i);
            }
            return name;
        }

        // Thai (8/3/2009)
        public void CreateDependencyNode(ref Node depFather)
        {
            if (childs != null && childs.Count>0)
            {
                //int hIdx = GetHeadIdx();

                List<int> headIdxList=new List<int>();
                FindHeadIdx(ref headIdxList);

                for (int i = 0; i < childs.Count; ++i)
                {
                    // dam bao cac nut khong bi lap lai
                    if (!headIdxList.Contains(i) || (depFather.name.Contains("+") && childs[i].GetHeadWord().Trim()!=depFather.name))
                    {
                        // tao nut con
                        Node depChild = new Node();
                        depChild.name = childs[i].GetHeadWord().Trim();
                        String funcTag = childs[i].GetFunctionTag();
                        if (funcTag.Length > 0)
                            depChild.name += "-" + funcTag;
                        depChild.childs = new List<Node>();

                        // mau sac
                        if (i < headIdxList[0])
                            depChild.modType = 0;
                        if (i > headIdxList[headIdxList.Count-1])
                            depChild.modType = 1;

                        // them vao danh sach con depFather
                        depFather.childs.Add(depChild);

                        // goi de qui
                        childs[i].CreateDependencyNode(ref depChild);
                    }
                    else
                        // goi de qui
                        childs[i].CreateDependencyNode(ref depFather);
                }
            } 
        }

        // Thai (8/3/2009)
        public Node CreateDependencyRoot()
        {
            Node root = new Node();
            root.name = GetHeadWord();
            root.childs = new List<Node>();
            return root;
        }

        public void ComputeStatistics(ref NodeComparStatistics statNT, ref NodeComparStatistics statPT, ref NodeComparStatistics statT)
        {
            if (type == NodeType.Nonterminal)
            {
                if (modType == 0)
                {
                    ++statNT.newNodeCount;
                }
                else
                    if (modType == 1)
                    {
                        ++statNT.modNodeCount;
                    }
            }
            else
                if (type == NodeType.Preterminal)
                {
                    if (modType == 0)
                    {
                        ++statPT.newNodeCount;
                    }
                    else
                        if (modType == 1)
                        {
                            ++statPT.modNodeCount;
                        }
                }
                else
                    if (type == NodeType.Terminal)
                    {
                        if (modType == 0)
                        {
                            ++statT.newNodeCount;
                        }
                        else
                            if (modType == 1)
                            {
                                ++statT.modNodeCount;
                            }
                    }

            if (childs != null && childs.Count > 0)
            {
                for (int i = 0; i < childs.Count; ++i)
                {
                    childs[i].ComputeStatistics(ref statNT, ref statPT, ref statT);
                }
            }
        }

        public void ComputeStatisticsDelete(ref NodeComparStatistics statNT, ref NodeComparStatistics statPT, ref NodeComparStatistics statT)
        {
            if (type == NodeType.Nonterminal)
            {
                if (modType == 0)
                {
                    ++statNT.delNodeCount;
                }
            }
            else
                if (type == NodeType.Preterminal)
                {
                    if (modType == 0)
                    {
                        ++statPT.delNodeCount;
                    }
                }
                else
                    if (type == NodeType.Terminal)
                    {
                        if (modType == 0)
                        {
                            ++statT.delNodeCount;
                        }
                    }

            if (childs != null && childs.Count > 0)
            {
                for (int i = 0; i < childs.Count; ++i)
                {
                    childs[i].ComputeStatisticsDelete(ref statNT, ref statPT, ref statT);
                }
            }
        }

        // nut goc co do cao la 0
        public void SetHight( int h )
        {
            hight = h;
            if (childs != null && childs.Count > 0)
            {
                for (int i = 0; i < childs.Count; ++i)
                    childs[i].SetHight(h + 1);
            } 
        }

        public String GetBracketedForm()
        {
            String str = "";
            if (type == NodeType.Nonterminal)
            { 
             // Lay khoang trong o dau
                String spaceStr = "";
                for (int i = 0; i < hight; ++i)
                    spaceStr += "     ";

                str += spaceStr;

             // In nhan
                str += "(" + name + " ";

             // In con
                if (childs != null && childs.Count > 0)
                {
                    int k;
                    
                    // cac con dau la preterminal thi in tren mot dong
                    for( k=0; k < childs.Count && childs[k].type == NodeType.Preterminal; ++k)
                    {
                        if (k > 0) str += ' ';
                        str += "(" + childs[k].name + " " + childs[k].childs[0].name + ")";
                    }

                    if (k < childs.Count)
                        str += '\n';

                    // cac con sau (nonterminal hoac preterminal) thi in tren cac dong rieng biet
                    for (int i = k; i < childs.Count; ++i)
                    {
                        str += childs[i].GetBracketedForm();
                        if (i < childs.Count-1)
                            str += "\n";
                    }
                }

                str += ")";
            }
            else 
                if(type==NodeType.Preterminal)
                {
                 // Lay khoang trong o dau
                    String spaceStr = "";
                    for (int i = 0; i < hight; ++i)
                        spaceStr += "     ";

                    str += spaceStr;

                 // Lay cap nhan-tu
                    str += "(" + name + " " + childs[0].name + ")";
                }

            return str;
        }

        public string GetSegmentedStr()
        {
	        string str="";
	        if( childs!=null && childs.Count>0 ){
		        for(int i=0; i<childs.Count; ++i){
			        string cstr=childs[i].GetSegmentedStr();
			        if(cstr.Length>0){
				        if(i>0) str+= " ";
				        str+= cstr;
			        }
		        }
	        }
	        else{
			        str = name.Trim();
                    if( str!="*T*" && str!="*E*" && str!="*0*" ){
                        str = str.Replace(' ', '_');
			        }
			        else
				        str = "";
	        }

	        return str;
        }
        
        public string GetPOSTaggedStr()
        {
	        string str="";
	        if( childs!=null && childs.Count>0 ){
                if (type == NodeType.Preterminal)
                {
                    str = childs[0].name.Trim();

                    if (str != "*T*" && str != "*E*" && str != "*0*")
                    {
                        str.Replace(' ', '_');
                        str += '/' + GetCanonicalTag();
                    }
                    else
                        str = "";
                }
                else
                {
                    for (int i = 0; i < childs.Count; ++i)
                    {
                        string cstr = childs[i].GetPOSTaggedStr();
                        if (cstr.Length > 0)
                        {
                            if (i > 0) str += " ";
                            str += cstr;
                        }
                    }
                }
	        }
/*	        else{
                str = name.Trim();
        			
		        if( str!="*T*" && str!="*E*" && str!="*0*" ){
                    str.Replace(' ', '_');
			        str += '/'+GetConstTag();
		        }
		        else
			        str = "";
	        }
*/
	        return str;
        }

        public void ExtractModifiedWords( ref List<string> oList )
        {
            if (modType!=2 && type == NodeType.Terminal)
            {
                string str = name.Trim();
                //if (str != "*T*" && str != "*E*" && str != "*0*")
                {
                    str = str.Replace(' ', '_');
                    oList.Add(str);
                }
            }

            if (childs != null && childs.Count > 0)
            {
                for (int i = 0; i < childs.Count; ++i)
                    childs[i].ExtractModifiedWords(ref oList);
            }
        }
 
 
    }
}
