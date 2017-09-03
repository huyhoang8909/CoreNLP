using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace SyntaxTool
{
    public partial class Form1 : Form
    {
        public String fileName = "";
        public String rawFileName = "";
        public String parsedFileName = "";

        List<String> rawSentenceList = new List<string>();
        List<String> parsedSentenceList = new List<string>();
        int currentIndex = 0;
        

        #region phần khai báo các đối tượng từ điển và DoSyntax cần dùng
        private Grammar grammar; //văn phạm đầu vào
        private Chart chart; //bộ công cụ dùng để chartParsing
        private List<Tree> listOfTrees; //danh sách các Tree phục vụ cho lithium hiển thị dễ dàng
        private int currentTree = 0;    //đánh số cây hiện thời   
#endregion phần khai báo các đối tượng từ điển và DoSyntax cần dùng
        //______________________________________________________________________________//
        public Form1()
        {
            InitializeComponent();
        }
        //______________________________________________________________________________//

        //______________________________________________________________________________//

        #region các thao tác với menu File

        private void UpdateComboBox()
        {
            //đầu tiên là xóa các Items cũ trong ComboBox
            //this.toolStripComboBox1.Items.Clear();
            //đưa các syntax vào trong hộp comboBox
            for (int i = 0; i < this.grammar.rules.Count; i++)
            {
                //this.toolStripComboBox1.Items.Add(this.grammar.rules[i]);
            }
        }

        private void UpdateTreeView1()
        {
            //tiếp theo là xóa treeview cũ đi
           // this.treeView1.Nodes.Clear();
            //tiếp theo là load file syntax này vào khung hiển thị
            for (int i = 0; i < this.grammar.rules.Count; i++)
            {
             //   this.treeView1.Nodes.Add(this.grammar.rules[i].ToString());
            }
            //cập nhật treeView1
           // this.treeView1.Update();
           // this.treeView1.Show();
            //this.groupBox1.Text = "Syntaxs List: " + this.grammar.rules.Count.ToString() + " rules";
        }



        #endregion các thao tác với menu File


        private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.richTextBox1.Text = "";
            this.richTextBox1.Update();
            this.lithiumControl.NewDiagram();
            this.lithiumControl.Root.Text = "Cau";
            Netron.Lithium.ShapeBase root = this.lithiumControl.Root;
            //gọi hàm chartparsing, đếm xem có bao nhiêu cây kết quả
            this.listOfTrees = this.ChartParsing(e.Node.Text);
            //kiểm tra xem có kết quả trả về ko, nếu có thì hiển thị ra
            if (this.listOfTrees != null)
            {
                if (this.listOfTrees.Count > 0)
                {
                    //thông báo cho người sử dụng biết có bao nhiêu cây kết quả
                    this.label1.Text = "Number Of Trees: " + this.listOfTrees.Count.ToString();
                    this.label3.Text = this.label1.Text;
                    this.currentTree = 0;
                    this.label2.Text = "Tree 1";
                    this.label4.Text = this.label2.Text;
                    //tiếp theo là thêm các hình cây con vào root
                    if (this.listOfTrees[0].root.childs != null)
                    {
                        insertSubtreesToRoot(root, this.listOfTrees[0].root.childs);
                    }
                    //lấy ra text của cây
                    String textTree = "\n\n\n\n\n"+ this.listOfTrees[0].root.ToString("\t");                    
                    //hiển thị text
                    this.richTextBox1.Text = textTree;
                    this.richTextBox1.Update();
                }
            }
            else
            {
                this.label1.Text = "Number Of Trees: 0";
                this.label2.Text = "Tree 0";
                this.label3.Text = this.label1.Text;
                this.label4.Text = this.label2.Text;
            }
            //hiển thị cây
            this.lithiumControl.Root.Visible = true;
            this.lithiumControl.ExpandAll();
            this.lithiumControl.DrawTree();
        }
        //Hàm thêm các cây con vào root , phục vụ cho việc vẽ cây
        private bool insertSubtreesToRoot(Netron.Lithium.ShapeBase root, List<Node> listOfChilds)
        {
            if (listOfChilds != null)
            {
                //kiểm tra xem có subtree nào được thêm vào ko
                if (listOfChilds.Count > 0)
                {
                    //lặp lần lượt add các subtree vào
                    for (int i = 0; i < listOfChilds.Count; i++)
                    {
                        if (listOfChilds[i] != null)
                        {
                            //Tạo ra một ShapeBase cho subtree
                            Netron.Lithium.ShapeBase subtree = root.AddChild(listOfChilds[i].name);
                            //tiếp tục thêm các cây con của node đang xét
                            if (listOfChilds[i].childs != null)
                            {
                                insertSubtreesToRoot(subtree, listOfChilds[i].childs);
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //nếu như có đường dẫn tới file mặc định thì mở ra
            if (File.Exists("syntax.txt"))
            {
                //khởi tạo bộ Grammar
                this.grammar = new Grammar("syntax.txt");
                //cập nhật TreeView1;
                UpdateTreeView1();
                //cập nhật comboBox
                UpdateComboBox();
                //khởi tạo bộ Chart
                this.chart = new Chart(grammar);
                //khởi tạo danh sách các cây kết quả
                this.listOfTrees = new List<Tree>();
            }
            //nếu như có đường dẫn tới file Example mặc định thì mở ra
            if (File.Exists("Examples.txt"))
            {
                this.readExamplesFile("Examples.txt");
            }
            //this.toolStripTextBox1.Focus();
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            e.Node.BeginEdit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //lưu lại các syntax vào vị trí hiện thời
            //this.doSyntaxer.writeListSyntaxToCurrentFile();
            //lưu lại các ví dụ
            
        }

        private void toolStripComboBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //kiểm tra xem listSyntax đã có phần tử nào hay chưa
            if (this.grammar.rules.Count == 0)
                return;
            //kiểm tra xem có phải người dùng gõ phím enter hay ko
            if (e.KeyChar == 13)
            {
                //lấy ra những ký tự người dùng vừa gõ trên comboBox
                string s = "";// this.toolStripComboBox1.Text;
                //tìm kiếm trong listSyntax ở cấp độ hai
                List<string> resultOfSearch = new List<string>();;
                for (int i = 0; i < this.grammar.rules.Count; i++)
                {
                    String rule = this.grammar.rules[i].ToString();
                    if (rule.Contains(s))
                        resultOfSearch.Add(rule);
                }
                //kiểm tra xem kết quả tìm kiếm có null hay ko
                if (resultOfSearch != null)
                {
                    //nếu khác null thì cập nhật comboBox
                    //this.toolStripComboBox1.Items.Clear();
                    //đưa ra các kết quả tìm được
                    //this.toolStripComboBox1.Items.AddRange(resultOfSearch.ToArray());
                    //lấy ra tất cả các node trong treeView
                    //TreeNode[] resultInTreeView = new TreeNode () ;//this.treeView1.Nodes.Find("", true);
                    ////xóa các treeNode đã bị bôi đen từ trước
                    //for (int k = 0; k < resultInTreeView.Length; k++)
                    //{
                    //    resultInTreeView[k].BackColor = Color.Transparent;
                    //}
                    ////bôi đen các treeNode thỏa mãn
                    //for (int i = 0; i < resultOfSearch.Count; i++)
                    //{
                    //    for (int j = 0; j < resultInTreeView.Length; j++)
                    //    {
                    //        if (resultOfSearch[i] == resultInTreeView[j].Text)
                    //        {
                    //            resultInTreeView[j].BackColor = Color.SeaGreen;
                    //        }
                    //    }
                    //}
                }
                //sửa đổi lại text trong hộp comboBox về khoảng trắng
                //this.toolStripComboBox1.Text = "";
                //this.toolStripComboBox1.Focus();
            }
            //ngược lại không làm gì cả
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            //xóa text cũ đi
            //this.toolStripComboBox1.Text = "";
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //save tòan bộ file chương trình ở đường dẫn mới do người dùng lựa chọn
            //đầu tiên là hỏi xem có save file Syntax không?

            //Tiếp theo là save hình cây

            //Thứ ba....
        }

        private bool readExamplesFile(string path)
        {
            //đầu tiên phải kiểm tra xem file có tồn tại hay ko
            if (File.Exists(path))
            {
                try
                {
                    //Tạo một bộ đọc file
                    StreamReader strFileSyntax = File.OpenText(path);
                    //xóa thông tin cũ ở TreeView Example đi
                    //this.treeView2.Nodes.Clear();
                    //đọc dữ liệu từ file và đưa vào TreeView Example
                    string s = strFileSyntax.ReadLine();
                    while (s != null)
                    {
                      //  this.treeView2.Nodes.Add(s);
                        s = strFileSyntax.ReadLine();
                    }
                    //this.treeView2.Show();
                }
                catch (Exception e)
                {
                    throw e;
                }
                return true;
            }
            else
                return false;
        }

        private void treeView2_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            e.Node.BeginEdit();
        }

        //_____________________________________________________________________________//

        //Hàm thêm các cây con vào root , phục vụ cho việc vẽ cây
        private bool insertSubtreesToRoot(Netron.Lithium.ShapeBase root, NodeOfSyntaxTree[] nodeRootOfSubTree)
        {
            if (nodeRootOfSubTree != null)
            {
                //kiểm tra xem có subtree nào được thêm vào ko
                if (nodeRootOfSubTree.Length > 0)
                {
                    //lặp lần lượt add các subtree vào
                    for (int i = 0; i < nodeRootOfSubTree.Length; i++)
                    {
                        //Tạo ra một ShapeBase cho subtree
                        Netron.Lithium.ShapeBase subtree = root.AddChild(nodeRootOfSubTree[i].getNameOfNode());
                        //tiếp tục thêm các cây con của node đang xét
                        NodeOfSyntaxTree[] tam = nodeRootOfSubTree[i].getChilds();
                        //gọi đệ quy để thêm subtree
                        this.insertSubtreesToRoot(subtree, tam);
                    }
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        private void lithiumControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        #region Chart Parsing
        private List<Tree> ChartParsing(string inputString)
        {
            if (inputString != null)
            {
                return this.chart.ChartParsing(inputString);
            }
            return null;
        }

        #endregion

        private void addExampeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Thêm một ví dụ vào để kiểm tra
            //this.treeView2.Nodes.Add("thêm ví dụ vào đây");
            //this.treeView2.Update();
            //int i = this.treeView2.Nodes.Count;
            //this.treeView2.Nodes[i - 1].BeginEdit();
        }


        private void openSyntaxFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            //đầu tiên là mở file syntax
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //nếu như có đường dẫn tới file mặc định thì mở ra
                if (File.Exists(this.openFileDialog1.FileName))
                {
                    //khởi tạo bộ Grammar
                    this.grammar = new Grammar(this.openFileDialog1.FileName);
                    //cập nhật TreeView1;
                    UpdateTreeView1();
                    //cập nhật comboBox
                    UpdateComboBox();
                    //khởi tạo bộ Chart
                    this.chart = new Chart(grammar);
                    //khởi tạo danh sách các cây kết quả
                    this.listOfTrees = new List<Tree>();
                }
                //nếu như có đường dẫn tới file Example mặc định thì mở ra
                if (File.Exists("Examples.txt"))
                {
                    this.readExamplesFile("Examples.txt");
                }
                //this.toolStripTextBox1.Focus();
            }
        }


        private void openExamplesFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //đầu tiên là mở file Examples
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.readExamplesFile(this.openFileDialog1.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
       
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //kiểm tra xem có kết quả trả về ko, nếu có thì hiển thị ra
            if (this.listOfTrees != null)
            {
                if (this.listOfTrees.Count > 0)
                {
                    //thông báo cho người sử dụng biết có bao nhiêu cây kết quả
                    this.label1.Text = "Number Of Trees: " + this.listOfTrees.Count.ToString();
                    //kiểm tra currentTree
                    if (this.currentTree == (this.listOfTrees.Count - 1))
                        this.currentTree = 0;
                    else
                        this.currentTree++;
                    //Bây giờ là hiển thị SyntaxTree
                    //___________________________________//
                    this.lithiumControl.NewDiagram();
                    //tạo root cơ sở
                    this.lithiumControl.Root.Text = this.listOfTrees[this.currentTree].root.name;
                    Netron.Lithium.ShapeBase root = this.lithiumControl.Root;
                    //tiếp theo là thêm các hình cây con vào root
                    if (this.listOfTrees[0].root.childs != null)
                    {
                        insertSubtreesToRoot(root, this.listOfTrees[this.currentTree].root.childs);
                        //hiển thị cây
                        this.lithiumControl.Root.Visible = true;
                        this.lithiumControl.ExpandAll();
                        this.lithiumControl.DrawTree();
                    }
                    this.label2.Text = "Tree " + (this.currentTree + 1).ToString();
                }
            }           
        }

        private void saveSyntaxFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.grammar.saveGrammar();
        }

    
        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //if (this.toolStripTextBox1.Text.Equals("") == false)
                //{
                //    this.richTextBox1.Text = "";
                //    this.richTextBox1.Update();
                //    this.lithiumControl.NewDiagram();
                //    this.lithiumControl.Root.Text = "Cau";
                //    Netron.Lithium.ShapeBase root = this.lithiumControl.Root;
                //    //gọi hàm chartparsing, đếm xem có bao nhiêu cây kết quả
                //    this.listOfTrees = this.ChartParsing(this.toolStripTextBox1.Text);
                //    //kiểm tra xem có kết quả trả về ko, nếu có thì hiển thị ra
                //    if (this.listOfTrees != null)
                //    {
                //        if (this.listOfTrees.Count > 0)
                //        {
                //            //thông báo cho người sử dụng biết có bao nhiêu cây kết quả
                //            this.label1.Text = "Number Of Trees: " + this.listOfTrees.Count.ToString();
                //            this.label3.Text = this.label1.Text;
                //            this.currentTree = 0;
                //            this.label2.Text = "Tree 1";
                //            this.label4.Text = this.label2.Text;
                //            //tiếp theo là thêm các hình cây con vào root
                //            if (this.listOfTrees[0].root.childs != null)
                //            {
                //                insertSubtreesToRoot(root, this.listOfTrees[0].root.childs);
                //            }
                //            //lấy ra text của cây
                //            String textTree = "\n\n\n\n\n" + this.listOfTrees[0].root.ToString("\t");
                //            //hiển thị text
                //            this.richTextBox1.Text = textTree;
                //            this.richTextBox1.Update();
                //        }
                //    }
                //    else
                //    {
                //        this.label1.Text = "Number Of Trees: 0";
                //        this.label2.Text = "Tree 0";
                //        this.label3.Text = this.label1.Text;
                //        this.label4.Text = this.label2.Text;
                //    }
                //    this.lithiumControl.Root.Visible = true;
                //    this.lithiumControl.ExpandAll();
                //    this.lithiumControl.DrawTree();
                //    this.treeView2.Nodes.Add(this.toolStripTextBox1.Text);

                //    this.toolStripTextBox1.Clear();
                //    this.toolStripTextBox1.Focus();
                //}
            }
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void lithiumControl_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //kiểm tra xem có kết quả trả về ko, nếu có thì hiển thị ra
            if (this.listOfTrees != null)
            {
                if (this.listOfTrees.Count > 0)
                {
                    //thông báo cho người sử dụng biết có bao nhiêu cây kết quả
                    this.label1.Text = "Number Of Trees: " + this.listOfTrees.Count.ToString();
                    //kiểm tra currentTree
                    if (this.currentTree == (this.listOfTrees.Count - 1))
                        this.currentTree = 0;
                    else
                        this.currentTree++;
                    //Bây giờ là hiển thị SyntaxTree
                    //___________________________________//
                    this.lithiumControl.NewDiagram();
                    //tạo root cơ sở
                    this.lithiumControl.Root.Text = this.listOfTrees[this.currentTree].root.name;
                    Netron.Lithium.ShapeBase root = this.lithiumControl.Root;
                    //tiếp theo là thêm các hình cây con vào root
                    if (this.listOfTrees[0].root.childs != null)
                    {
                        insertSubtreesToRoot(root, this.listOfTrees[this.currentTree].root.childs);
                        //hiển thị cây
                        this.lithiumControl.Root.Visible = true;
                        this.lithiumControl.ExpandAll();
                        this.lithiumControl.DrawTree();
                    }
                    this.label2.Text = "Tree " + (this.currentTree + 1).ToString();
                }
            } 
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //kiểm tra xem có kết quả trả về ko, nếu có thì hiển thị ra
            if (this.listOfTrees != null)
            {
                if (this.listOfTrees.Count > 0)
                {
                    //thông báo cho người sử dụng biết có bao nhiêu cây kết quả
                    this.label1.Text = "Number Of Trees: " + this.listOfTrees.Count.ToString();
                    //kiểm tra currentTree
                    if (this.currentTree == 0)
                        this.currentTree = this.listOfTrees.Count - 1;
                    else
                        this.currentTree--;
                    //Bây giờ là hiển thị SyntaxTree
                    //___________________________________//
                    this.lithiumControl.NewDiagram();
                    //tạo root cơ sở
                    this.lithiumControl.Root.Text = this.listOfTrees[this.currentTree].root.name;
                    Netron.Lithium.ShapeBase root = this.lithiumControl.Root;
                    //tiếp theo là thêm các hình cây con vào root
                    if (this.listOfTrees[0].root.childs != null)
                    {
                        insertSubtreesToRoot(root, this.listOfTrees[this.currentTree].root.childs);
                        //hiển thị cây
                        this.lithiumControl.Root.Visible = true;
                        this.lithiumControl.ExpandAll();
                        this.lithiumControl.DrawTree();
                    }
                    this.label2.Text = "Tree " + (this.currentTree + 1).ToString();
                }
            }  
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //kiểm tra xem có kết quả trả về ko, nếu có thì hiển thị ra
            if (this.listOfTrees != null)
            {
                if (this.listOfTrees.Count > 0)
                {
                    //thông báo cho người sử dụng biết có bao nhiêu cây kết quả
                    this.label1.Text = "Number Of Trees: " + this.listOfTrees.Count.ToString();
                    this.label3.Text = this.label1.Text;
                    //kiểm tra currentTree
                    if (this.currentTree == 0)
                        this.currentTree = this.listOfTrees.Count - 1;
                    else
                        this.currentTree--;
                    //Bây giờ là hiển thị SyntaxTree
                    //___________________________________//
                    this.lithiumControl.NewDiagram();
                    //tạo root cơ sở
                    this.lithiumControl.Root.Text = this.listOfTrees[this.currentTree].root.name;
                    Netron.Lithium.ShapeBase root = this.lithiumControl.Root;
                    //tiếp theo là thêm các hình cây con vào root
                    if (this.listOfTrees[0].root.childs != null)
                    {
                        insertSubtreesToRoot(root, this.listOfTrees[this.currentTree].root.childs);
                        //hiển thị cây
                        this.lithiumControl.Root.Visible = true;
                        this.lithiumControl.ExpandAll();
                        this.lithiumControl.DrawTree();
                    }
                    this.label2.Text = "Tree " + (this.currentTree + 1).ToString();
                    this.label4.Text = this.label2.Text;
                    this.label4.Update();
                    //lấy text Tree để hiển thị
                    this.richTextBox1.Text = "\n\n\n\n\n" + this.listOfTrees[this.currentTree].root.ToString("\t");
                    this.richTextBox1.Update();
                }
            }  
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //kiểm tra xem có kết quả trả về ko, nếu có thì hiển thị ra
            if (this.listOfTrees != null)
            {
                if (this.listOfTrees.Count > 0)
                {
                    //thông báo cho người sử dụng biết có bao nhiêu cây kết quả
                    this.label1.Text = "Number Of Trees: " + this.listOfTrees.Count.ToString();
                    this.label3.Text = this.label1.Text;
                    //kiểm tra currentTree
                    if (this.currentTree == (this.listOfTrees.Count - 1))
                        this.currentTree = 0;
                    else
                        this.currentTree++;
                    //Bây giờ là hiển thị SyntaxTree
                    //___________________________________//
                    this.lithiumControl.NewDiagram();
                    //tạo root cơ sở
                    this.lithiumControl.Root.Text = this.listOfTrees[this.currentTree].root.name;
                    Netron.Lithium.ShapeBase root = this.lithiumControl.Root;
                    //tiếp theo là thêm các hình cây con vào root
                    if (this.listOfTrees[0].root.childs != null)
                    {
                        insertSubtreesToRoot(root, this.listOfTrees[this.currentTree].root.childs);
                        //hiển thị cây
                        this.lithiumControl.Root.Visible = true;
                        this.lithiumControl.ExpandAll();
                        this.lithiumControl.DrawTree();
                    }
                    this.label2.Text = "Tree " + (this.currentTree + 1).ToString();
                    this.label4.Text = this.label2.Text;
                    this.label4.Update();
                    //lấy text Tree để hiển thị
                    this.richTextBox1.Text = "\n\n\n\n\n" + this.listOfTrees[this.currentTree].root.ToString("\t");
                    this.richTextBox1.Update();
                }
            }
        }

        String getTextOfTree(Netron.Lithium.ShapeBase root)
        {
            return "";
        
        }

        private void viewTree()
        { 
            //lấy string trong hộp rich textbox ra
            String stringToView = this.richTextBox2.Text;
            // chuẩn hóa lại string:
            // thêm khoảng trắng vào các dấu ( )
            stringToView = "(" + stringToView + ")"; 
            stringToView = stringToView.Replace("(", " ( ");
            stringToView = stringToView.Replace(")"," ) ");

            if (stringToView != null)
            { 
                #region chuẩn lại string thu được (xóa khoảng trắng, xuống dòng, tab)
                string[] separators = new string[]{" ", "\n", "\t"};
                string[] temps = stringToView.Split(separators, 
                    StringSplitOptions.RemoveEmptyEntries);
                List<String> lsToGetTree = new List<string>();
                String leafStr1 = "";
                String leafStr2 = "";
                String leafStr3 = "";

                for (int i = 1; i < temps.Length - 1; i++)
                {
                    //if()
                    lsToGetTree.Add(temps[i]);

                    // cap nhat lai cac leaf
                    leafStr1 = leafStr2;
                    leafStr2 = leafStr3;
                    leafStr3 = temps[i];
                }
                #endregion
                if (lsToGetTree.Count > 2)
                {
                    Node newNode = new Node(lsToGetTree);
                    Tree newTree = new Tree(newNode);
                    //bây giờ thì hiển thị
                    this.listOfTrees = new List<Tree>();
                    this.listOfTrees.Add(newTree);

                    #region hiển thị cây
                    this.richTextBox1.Text = "";
                    this.richTextBox1.Update();
                    this.lithiumControl.NewDiagram();
                    this.lithiumControl.Root.Text = newTree.root.name;
                    Netron.Lithium.ShapeBase root = this.lithiumControl.Root;
                    //kiểm tra xem có kết quả trả về ko, nếu có thì hiển thị ra
                    if (this.listOfTrees != null)
                    {
                        if (this.listOfTrees.Count > 0)
                        {
                            //thông báo cho người sử dụng biết có bao nhiêu cây kết quả
                            this.label1.Text = "Number Of Trees: " + this.listOfTrees.Count.ToString();
                            this.label3.Text = this.label1.Text;
                            this.currentTree = 0;
                            this.label2.Text = "Tree 1";
                            this.label4.Text = this.label2.Text;
                            //tiếp theo là thêm các hình cây con vào root
                            if (this.listOfTrees[0].root.childs != null)
                            {
                                insertSubtreesToRoot(root, this.listOfTrees[0].root.childs);
                            }
                            //lấy ra text của cây
                            String textTree = "\n\n\n\n\n" + this.listOfTrees[0].root.ToString("\t");

                            String REstring = "\\([^\\(\\)]*\\)";
                            String temp="";
                            Regex reg1 = new Regex(REstring);
                            MatchCollection matchCollection1 = reg1.Matches(textTree);
                            foreach (Match match in matchCollection1)
                            {
                                String oldStr = match.Value;
                                String newStr = oldStr.Replace('(',' ');
                                newStr = newStr.Replace(')',' '); 
                                textTree = textTree.Replace(oldStr,newStr);
                                
                            }
            

                            //this.richTextBox1.Text = newtext;
                            this.richTextBox1.Text = textTree;

                            this.richTextBox1.Update();
                        }
                    }
                    else
                    {
                        this.label1.Text = "Number Of Trees: 0";
                        this.label2.Text = "Tree 0";
                        this.label3.Text = this.label1.Text;
                        this.label4.Text = this.label2.Text;
                    }
                    //hiển thị cây
                    this.lithiumControl.Root.Visible = true;
                    this.lithiumControl.ExpandAll();
                    this.lithiumControl.DrawTree();
                    #endregion
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                viewTree();
            }
            catch
            {}
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void buttonBrower_Click(object sender, EventArgs e)
        {
            // xóa dữ liệu, khôi phục lại từ đầu
            rawSentenceList.Clear();
            parsedSentenceList.Clear();

            // xóa danh sách listbox
            listBox1.Items.Clear();
            listBox1.Update();
            //

            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.ShowDialog();
            textBox1.Text = fileDialog.FileName;
            fileName = fileDialog.FileName;
            // mở file
            

            if (fileName.Contains(".raw"))
            {
                rawFileName = fileName;
                parsedFileName = fileName.Replace(".raw", ".prd");
            }
            else if (fileName.Contains(".prd"))
            {
                parsedFileName = fileName;
                rawFileName = fileName.Replace(".prd", ".raw");
            }
            else
                return;

            // đưa dữ liệu vào trong cơ sở dữ liệu
            // rawtext
            StreamReader reader1 = new StreamReader(rawFileName);
            while (!reader1.EndOfStream)
            {
                String str = reader1.ReadLine();
                if (str.Contains("<s>"))
                {
                    String s1 = str.Replace("<s>", "");
                    s1 = s1.Replace("</s>","");
                    rawSentenceList.Add( s1.Trim());
                }
            
            }
            reader1.Close();

            // parsed text
            StreamReader reader2 = new StreamReader(parsedFileName);
            String parsedtext = "";
            while (!reader2.EndOfStream)
            {
                String str = reader2.ReadLine();
                if (str.Contains("<s>"))
                {
                    parsedtext = "";
                }
                else if (str.Contains("</s>"))
                {
                    // kết thúc một câu
                    parsedSentenceList.Add(parsedtext);
                }
                else
                {
                    parsedtext += str + "\n";
                }
            }
            // kết thúc
            // đưa vào danh sách listbox
            for (int i = 0; i < rawSentenceList.Count; i++)
            {
                String str = rawSentenceList[i];
                listBox1.Items.Add(i.ToString()+": "+str);
                listBox1.Update();
            }

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // khi click thì sẽ hiện parsed text tương ứng 
            int index  = listBox1.SelectedIndex;
            currentIndex = index;
            String parsedtext = parsedSentenceList[index];
            this.richTextBox2.Text = parsedtext;
            this.richTextBox3.Text = rawSentenceList[index];
        }

        private void SaveSentence_Click(object sender, EventArgs e)
        {
            // save câu vừa sửa
            rawSentenceList[currentIndex] = this.richTextBox3.Text.Trim();
            parsedSentenceList[currentIndex] = this.richTextBox2.Text.Trim();
            listBox1.Items[currentIndex] = currentIndex.ToString() + ": " + rawSentenceList[currentIndex];

            SaveToFile(); // ghi luon vao file

            MessageBox.Show("Save ok!"); 
        }

        private void SaveToFile()
        {
            // cập nhật lại file
            StreamWriter writer1 = new StreamWriter(rawFileName);
            for (int i = 0; i < rawSentenceList.Count; i++)
            {
                writer1.WriteLine("<s> "+rawSentenceList[i]+" </s>");
            }
            writer1.Close();

            // parsed file
            StreamWriter writer2 = new StreamWriter(parsedFileName);
            for (int i = 0; i < parsedSentenceList.Count; i++)
            {
                writer2.WriteLine("<s>");
                writer2.WriteLine(parsedSentenceList[i]);
                writer2.WriteLine("</s>");
            }
            writer2.Close();
        }


        private void SaveAll_Click(object sender, EventArgs e)
        {
            // cập nhật lại file
            StreamWriter writer1 = new StreamWriter(rawFileName);
            for (int i = 0; i < rawSentenceList.Count; i++)
            {
                writer1.WriteLine("<s> "+rawSentenceList[i]+" </s>");
            }
            writer1.Close();

            // parsed file
            StreamWriter writer2 = new StreamWriter(parsedFileName);
            for (int i = 0; i < parsedSentenceList.Count; i++)
            {
                writer2.WriteLine("<s>");
                writer2.WriteLine(parsedSentenceList[i]);
                writer2.WriteLine("</s>");
            }
            writer2.Close();
            MessageBox.Show("Save files finished!");
        }

        private void button_HideTree_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hide tree");
        }

        private void button_hidetree_Click_1(object sender, EventArgs e)
        {
            // dau nut phia  duoi
            //tabControl1.Hide();
            //int h = tabControl1.Height;
            Size size = tabControl1.Size;
            size.Height -= 100;
            tabControl1.TabPages[1].AutoScroll = true;//.AutoScrollMinSize.Height =100;//.Height -= 100;

            //MessageBox.Show("aaa");

        }

        // save khi sua o Text_Tree_View
        private void Save_Text_Tree_View_Click(object sender, EventArgs e)
        {
            try
            {
                // xem có sinh ra cây ko

                // save câu vừa sửa
                this.richTextBox2.Text = this.richTextBox1.Text.Trim();


                //MessageBox.Show("Save ok!"); 

                // Cap nhat lai view
                viewTree();

                if (this.listOfTrees[0].root.childs.Count < 1)
                {
                    MessageBox.Show("Cây không chuẩn, xin hãy sửa lại!");
                    return;
                }
                rawSentenceList[currentIndex] = this.richTextBox3.Text.Trim();
                parsedSentenceList[currentIndex] = this.richTextBox1.Text.Trim();

                listBox1.Items[currentIndex] = currentIndex.ToString() + ": " + rawSentenceList[currentIndex];

                SaveToFile(); // ghi luon vao file

            }
            catch { }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chương trình hỗ trợ soạn thảo Viet TreeBank.","Copyrigh 2008 VGroup");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}