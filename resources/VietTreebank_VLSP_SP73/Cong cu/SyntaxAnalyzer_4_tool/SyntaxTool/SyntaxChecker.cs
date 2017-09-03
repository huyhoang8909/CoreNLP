using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SyntaxTool
{
    class SyntaxChecker
    {
        // Xau chua thong bao loi
        public String m_errMessage = "";

        // Bien chua vi tri loi (chi dung cho ham CreateTreeObjFromVTBBracketedStr)
        public int m_i1 = -1;
        public int m_i2 = -1;

        public Tree CreateTreeObjFromVTBBracketedStr( String inp )
        {
            // Chuan hoa inp: bo dau xuong dong va tab
            inp = inp.Replace('\r', ' ');
            inp = inp.Replace('\n',' ');
            inp = inp.Replace('\t', ' ');

            // Khoi tao cac bien chua thong tin ve loi
            m_errMessage = "";
            m_i1 = m_i2 = -1;

	        Tree pTree=new Tree();

	        List<Node> stk=new List<Node>();

	        int i=0, j, l=inp.Length;

	        while (i<l) {
		        if ( inp[i]=='(' ) 
                {
			        // bo qua cac ' ' ngay sau '('
			        while( i+1<l && inp[i+1]==' ' )
				        ++i;

		         // read tag (sau mo ngoac luon la tag)
			        j = inp.IndexOf( ' ', i+1 );
			        int j1=inp.IndexOf( '(', i+1 );  // tai sao nhi? --> co truong hop sau nhan la '(' nen nguoi lam du lieu viet lien luon

			        // Thai, 22/2/2009
			        if( j>i ){  // co ' '
				        if(j1>i && j1<j)  // nhung giua i va dau cach la '(' --> dau cach phan tach nhan khac
					        j = j1;
				        // else binh thuong
			        }
			        else{  // loi, vi bao gio cung co dau cach o la
				        m_errMessage = "Lỗi thiếu dấu cách!";
                        m_i1 = i; m_i2 = l-1;
				        return null;
			        }

			        if ( j>i+1 ) {  // j==i+1 khi co hai dau '(' lien nhau
				        String s = inp.Substring(i+1, j-i-1);

				        // Kiem tra xem nhan co chua ')' khong
				        if( s.IndexOf(')')!=-1 || 
					        (s[0]=='-' && s.Length>1) ){  // neu de ky hieu nay se gay loi khi test Bikel parser
					        m_errMessage = "Lỗi nhãn chứa ')' hoặc có '-' ở đầu!";
                            m_i1 = i; m_i2 = j-1;
					        return null;
				        }

                        Node pTNode = new Node();
				        pTNode.name = s;
                        pTNode.childs = new List<Node>();

			         // add the new nodes to the node at the top of the stack
				        if ( stk.Count>0 ) {

					        stk[stk.Count-1].childs.Add( pTNode );
				        }
				        else{
					        if( pTree.root==null)
						        pTree.root = pTNode;
					        else{
						        m_errMessage = "Không xác định được nút cha! "+i.ToString();
                                m_i1 = i; m_i2 = j - 1;
						        return null;
					        }
				        }

			         // create a new rule and push it into the stack
				        stk.Add( pTNode );

				        //i = j+1;  // jump the blank
				        if( inp[j]==' ' ){
					        i = j+1;
					        while( inp[i]==' ' ) ++i;
				        }
				        else{  // '('
					        i = j;
				        }
			        }
			        else { // co loi
				        m_errMessage = "Lỗi hai dấu '(' liên tiếp do thừa '(' hoặc thiếu ký hiệu cú pháp!";
                        m_i1 = i; m_i2 = i+1;
				        return null;
			        }
		        }
		        else
			        if ( inp[i]==')' ) {  // Lay ra thanh phan

			         // pop a rule out of the stack and add it to the rule array
				        if ( stk.Count>0 ) {
					        stk.RemoveAt(stk.Count-1);
				        }
				        else{  // loi thua dau ngoac ')'
					        m_errMessage = "Lỗi thừa dấu đóng ngoặc ')'!";
                            m_i1 = i; m_i2 = i;
					        return null;
				        }

				        i++;
			        }
			        else 
				        if ( inp[i]!=' ' ) {  // read terminal symbol
					        j = inp.IndexOf( ')', i+1 );

					        if ( j>i ) {
                                Node pTNode = new Node();  // day la nut la nen khong can dua vao stack
                                pTNode.name = inp.Substring(i, j - i);
                                if (pTNode.name.IndexOf('(') != -1)
                                {
                                    m_errMessage = "Lỗi ký hiệu!";
                                    m_i1 = i; m_i2 = i + pTNode.name.IndexOf('(') - 1;
                                    return null;
                                }

						        if(stk.Count>0)                                    
							        stk[stk.Count-1].childs.Add(pTNode);
						        else{
							        m_errMessage = "Lỗi thiếu '(' và nhãn trước từ!";
                                    m_i1 = i; m_i2 = j-1;
							        return null;
						        }

						        i = j;
					        }
					        else {
						        m_errMessage = "Lỗi thiếu ')' sau từ!";
                                m_i1 = i; m_i2 = l-1;
						        return null;
					        }
				        }
				        else i++;  // ' '
	        }

            if (pTree.root == null)
            {
                m_errMessage = "Cây rỗng! ";
                m_i1 = -1; m_i2 = -1;
                return null;
            }

            if (stk.Count > 0)
            {
                m_errMessage = "Thiếu dấu đóng ngoặc ')' ở cuối cây!";
                m_i1 = -1; m_i2 = -1;
                return null;
            }

	        return pTree;
        }

        public bool CheckRoundBracket(Node node)
        {
            bool ok = true;

            if (node.name.Contains("(") || node.name.Contains(")") || (node.name.Trim().StartsWith("-") && node.name.Trim()!="-"))
                ok = false;
            
            if (node.childs != null && node.childs.Count > 0)
            {
                for (int i = 0; i < node.childs.Count; ++i)
                    if (!CheckRoundBracket(node.childs[i]))
                        ok = false;
            }

            return ok;
        }
    }
}
