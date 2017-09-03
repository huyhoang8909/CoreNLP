using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Netron.Lithium;

namespace SyntaxTool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainerMainView = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer4ListBoxes = new System.Windows.Forms.SplitContainer();
            this.listBoxSent = new System.Windows.Forms.ListBox();
            this.splitContainerFileList = new System.Windows.Forms.SplitContainer();
            this.labelFileList = new System.Windows.Forms.Label();
            this.listViewFile = new System.Windows.Forms.ListView();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageTree = new System.Windows.Forms.TabPage();
            this.splitContainerTree = new System.Windows.Forms.SplitContainer();
            this.richTextBoxTree = new System.Windows.Forms.RichTextBox();
            this.buttonColor = new System.Windows.Forms.Button();
            this.buttonRedo = new System.Windows.Forms.Button();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.buttonNormalizeTextTree = new System.Windows.Forms.Button();
            this.radioButtonDependency = new System.Windows.Forms.RadioButton();
            this.radioButtonPhrase = new System.Windows.Forms.RadioButton();
            this.buttonViewGUITree = new System.Windows.Forms.Button();
            this.lithiumControl = new Netron.Lithium.LithiumControl();
            this.tabPageRawSent = new System.Windows.Forms.TabPage();
            this.splitContainerRawSent = new System.Windows.Forms.SplitContainer();
            this.richTextBoxRawSent = new System.Windows.Forms.RichTextBox();
            this.richTextBoxDoc = new System.Windows.Forms.RichTextBox();
            this.tabPageTreeAlign = new System.Windows.Forms.TabPage();
            this.splitContainerTreeAlign = new System.Windows.Forms.SplitContainer();
            this.buttonAgreement = new System.Windows.Forms.Button();
            this.buttonTreeLog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lithiumControl1 = new Netron.Lithium.LithiumControl();
            this.label2 = new System.Windows.Forms.Label();
            this.lithiumControl2 = new Netron.Lithium.LithiumControl();
            this.tabPagePOS = new System.Windows.Forms.TabPage();
            this.splitContainerPOS = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.richTextBoxPOS = new System.Windows.Forms.RichTextBox();
            this.richTextBoxPOS2 = new System.Windows.Forms.RichTextBox();
            this.buttonOpenPOSLog = new System.Windows.Forms.Button();
            this.tabPageWSeg = new System.Windows.Forms.TabPage();
            this.splitContainerWSeg = new System.Windows.Forms.SplitContainer();
            this.splitContainerWSeg_Edit = new System.Windows.Forms.SplitContainer();
            this.richTextBoxWSeg = new System.Windows.Forms.RichTextBox();
            this.richTextBoxWSeg2 = new System.Windows.Forms.RichTextBox();
            this.tabSentSplit = new System.Windows.Forms.TabPage();
            this.splitContainerSentSplit = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBoxSentSplit = new System.Windows.Forms.RichTextBox();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.richTextBoxStatistics = new System.Windows.Forms.RichTextBox();
            this.buttonStatistics = new System.Windows.Forms.Button();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.richTextBoxSearch = new System.Windows.Forms.RichTextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button13 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.button14 = new System.Windows.Forms.Button();
            this.buttonOpenLog = new System.Windows.Forms.Button();
            this.buttonNewLog = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonOpenDir = new System.Windows.Forms.Button();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog3 = new System.Windows.Forms.FolderBrowserDialog();
            this.comboBoxWorkingMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainerFirst = new System.Windows.Forms.SplitContainer();
            this.comboBoxFileName = new System.Windows.Forms.ComboBox();
            this.splitContainerMainView.Panel1.SuspendLayout();
            this.splitContainerMainView.Panel2.SuspendLayout();
            this.splitContainerMainView.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer4ListBoxes.Panel1.SuspendLayout();
            this.splitContainer4ListBoxes.Panel2.SuspendLayout();
            this.splitContainer4ListBoxes.SuspendLayout();
            this.splitContainerFileList.Panel1.SuspendLayout();
            this.splitContainerFileList.Panel2.SuspendLayout();
            this.splitContainerFileList.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPageTree.SuspendLayout();
            this.splitContainerTree.Panel1.SuspendLayout();
            this.splitContainerTree.Panel2.SuspendLayout();
            this.splitContainerTree.SuspendLayout();
            this.tabPageRawSent.SuspendLayout();
            this.splitContainerRawSent.Panel1.SuspendLayout();
            this.splitContainerRawSent.Panel2.SuspendLayout();
            this.splitContainerRawSent.SuspendLayout();
            this.tabPageTreeAlign.SuspendLayout();
            this.splitContainerTreeAlign.Panel1.SuspendLayout();
            this.splitContainerTreeAlign.Panel2.SuspendLayout();
            this.splitContainerTreeAlign.SuspendLayout();
            this.tabPagePOS.SuspendLayout();
            this.splitContainerPOS.Panel1.SuspendLayout();
            this.splitContainerPOS.Panel2.SuspendLayout();
            this.splitContainerPOS.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.tabPageWSeg.SuspendLayout();
            this.splitContainerWSeg.Panel1.SuspendLayout();
            this.splitContainerWSeg.SuspendLayout();
            this.splitContainerWSeg_Edit.Panel1.SuspendLayout();
            this.splitContainerWSeg_Edit.Panel2.SuspendLayout();
            this.splitContainerWSeg_Edit.SuspendLayout();
            this.tabSentSplit.SuspendLayout();
            this.splitContainerSentSplit.Panel1.SuspendLayout();
            this.splitContainerSentSplit.Panel2.SuspendLayout();
            this.splitContainerSentSplit.SuspendLayout();
            this.tabPageSearch.SuspendLayout();
            this.splitContainerFirst.Panel1.SuspendLayout();
            this.splitContainerFirst.Panel2.SuspendLayout();
            this.splitContainerFirst.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMainView
            // 
            resources.ApplyResources(this.splitContainerMainView, "splitContainerMainView");
            this.splitContainerMainView.Name = "splitContainerMainView";
            // 
            // splitContainerMainView.Panel1
            // 
            this.splitContainerMainView.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainerMainView.Panel2
            // 
            this.splitContainerMainView.Panel2.Controls.Add(this.tabControl2);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer4ListBoxes);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // splitContainer4ListBoxes
            // 
            resources.ApplyResources(this.splitContainer4ListBoxes, "splitContainer4ListBoxes");
            this.splitContainer4ListBoxes.Name = "splitContainer4ListBoxes";
            // 
            // splitContainer4ListBoxes.Panel1
            // 
            this.splitContainer4ListBoxes.Panel1.Controls.Add(this.listBoxSent);
            // 
            // splitContainer4ListBoxes.Panel2
            // 
            this.splitContainer4ListBoxes.Panel2.Controls.Add(this.splitContainerFileList);
            // 
            // listBoxSent
            // 
            resources.ApplyResources(this.listBoxSent, "listBoxSent");
            this.listBoxSent.FormattingEnabled = true;
            this.listBoxSent.Name = "listBoxSent";
            this.listBoxSent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxSent_MouseDoubleClick);
            // 
            // splitContainerFileList
            // 
            resources.ApplyResources(this.splitContainerFileList, "splitContainerFileList");
            this.splitContainerFileList.Name = "splitContainerFileList";
            // 
            // splitContainerFileList.Panel1
            // 
            this.splitContainerFileList.Panel1.Controls.Add(this.labelFileList);
            // 
            // splitContainerFileList.Panel2
            // 
            this.splitContainerFileList.Panel2.Controls.Add(this.listViewFile);
            // 
            // labelFileList
            // 
            resources.ApplyResources(this.labelFileList, "labelFileList");
            this.labelFileList.Name = "labelFileList";
            this.labelFileList.Click += new System.EventHandler(this.label3_Click);
            // 
            // listViewFile
            // 
            resources.ApplyResources(this.listViewFile, "listViewFile");
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            this.listViewFile.View = System.Windows.Forms.View.SmallIcon;
            this.listViewFile.DoubleClick += new System.EventHandler(this.listViewFile_DoubleClick);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPageTree);
            this.tabControl2.Controls.Add(this.tabPageRawSent);
            this.tabControl2.Controls.Add(this.tabPageTreeAlign);
            this.tabControl2.Controls.Add(this.tabPagePOS);
            this.tabControl2.Controls.Add(this.tabPageWSeg);
            this.tabControl2.Controls.Add(this.tabSentSplit);
            this.tabControl2.Controls.Add(this.tabPageSearch);
            resources.ApplyResources(this.tabControl2, "tabControl2");
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            // 
            // tabPageTree
            // 
            this.tabPageTree.Controls.Add(this.splitContainerTree);
            resources.ApplyResources(this.tabPageTree, "tabPageTree");
            this.tabPageTree.Name = "tabPageTree";
            this.tabPageTree.UseVisualStyleBackColor = true;
            // 
            // splitContainerTree
            // 
            this.splitContainerTree.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.splitContainerTree, "splitContainerTree");
            this.splitContainerTree.Name = "splitContainerTree";
            // 
            // splitContainerTree.Panel1
            // 
            this.splitContainerTree.Panel1.Controls.Add(this.richTextBoxTree);
            // 
            // splitContainerTree.Panel2
            // 
            this.splitContainerTree.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainerTree.Panel2.Controls.Add(this.buttonColor);
            this.splitContainerTree.Panel2.Controls.Add(this.buttonRedo);
            this.splitContainerTree.Panel2.Controls.Add(this.buttonUndo);
            this.splitContainerTree.Panel2.Controls.Add(this.buttonNormalizeTextTree);
            this.splitContainerTree.Panel2.Controls.Add(this.radioButtonDependency);
            this.splitContainerTree.Panel2.Controls.Add(this.radioButtonPhrase);
            this.splitContainerTree.Panel2.Controls.Add(this.buttonViewGUITree);
            this.splitContainerTree.Panel2.Controls.Add(this.lithiumControl);
            // 
            // richTextBoxTree
            // 
            resources.ApplyResources(this.richTextBoxTree, "richTextBoxTree");
            this.richTextBoxTree.Name = "richTextBoxTree";
            this.richTextBoxTree.MouseUp += new System.Windows.Forms.MouseEventHandler(this.richTextBoxTree_MouseUp);
            this.richTextBoxTree.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.richTextBoxTree_PreviewKeyDown);
            this.richTextBoxTree.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBoxTree_KeyUp);
            // 
            // buttonColor
            // 
            resources.ApplyResources(this.buttonColor, "buttonColor");
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);
            // 
            // buttonRedo
            // 
            resources.ApplyResources(this.buttonRedo, "buttonRedo");
            this.buttonRedo.Name = "buttonRedo";
            this.buttonRedo.UseVisualStyleBackColor = true;
            this.buttonRedo.Click += new System.EventHandler(this.buttonRedo_Click);
            // 
            // buttonUndo
            // 
            resources.ApplyResources(this.buttonUndo, "buttonUndo");
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.UseVisualStyleBackColor = true;
            this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
            // 
            // buttonNormalizeTextTree
            // 
            resources.ApplyResources(this.buttonNormalizeTextTree, "buttonNormalizeTextTree");
            this.buttonNormalizeTextTree.Name = "buttonNormalizeTextTree";
            this.buttonNormalizeTextTree.UseVisualStyleBackColor = true;
            this.buttonNormalizeTextTree.Click += new System.EventHandler(this.buttonNormalizeTextTree_Click);
            // 
            // radioButtonDependency
            // 
            resources.ApplyResources(this.radioButtonDependency, "radioButtonDependency");
            this.radioButtonDependency.Name = "radioButtonDependency";
            this.radioButtonDependency.TabStop = true;
            this.radioButtonDependency.UseVisualStyleBackColor = true;
            // 
            // radioButtonPhrase
            // 
            resources.ApplyResources(this.radioButtonPhrase, "radioButtonPhrase");
            this.radioButtonPhrase.Checked = true;
            this.radioButtonPhrase.Name = "radioButtonPhrase";
            this.radioButtonPhrase.TabStop = true;
            this.radioButtonPhrase.UseVisualStyleBackColor = true;
            // 
            // buttonViewGUITree
            // 
            resources.ApplyResources(this.buttonViewGUITree, "buttonViewGUITree");
            this.buttonViewGUITree.Name = "buttonViewGUITree";
            this.buttonViewGUITree.UseVisualStyleBackColor = true;
            this.buttonViewGUITree.Click += new System.EventHandler(this.buttonViewGUITree_Click);
            // 
            // lithiumControl
            // 
            resources.ApplyResources(this.lithiumControl, "lithiumControl");
            this.lithiumControl.BackColor = System.Drawing.SystemColors.Window;
            this.lithiumControl.BranchHeight = 70;
            this.lithiumControl.ConnectionType = Netron.Lithium.ConnectionType.Traditional;
            this.lithiumControl.LayoutDirection = Netron.Lithium.TreeDirection.Vertical;
            this.lithiumControl.LayoutEnabled = true;
            this.lithiumControl.Name = "lithiumControl";
            this.lithiumControl.WordSpacing = 20;
            // 
            // tabPageRawSent
            // 
            this.tabPageRawSent.Controls.Add(this.splitContainerRawSent);
            resources.ApplyResources(this.tabPageRawSent, "tabPageRawSent");
            this.tabPageRawSent.Name = "tabPageRawSent";
            this.tabPageRawSent.UseVisualStyleBackColor = true;
            // 
            // splitContainerRawSent
            // 
            this.splitContainerRawSent.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.splitContainerRawSent, "splitContainerRawSent");
            this.splitContainerRawSent.Name = "splitContainerRawSent";
            // 
            // splitContainerRawSent.Panel1
            // 
            this.splitContainerRawSent.Panel1.Controls.Add(this.richTextBoxRawSent);
            // 
            // splitContainerRawSent.Panel2
            // 
            this.splitContainerRawSent.Panel2.Controls.Add(this.richTextBoxDoc);
            // 
            // richTextBoxRawSent
            // 
            resources.ApplyResources(this.richTextBoxRawSent, "richTextBoxRawSent");
            this.richTextBoxRawSent.Name = "richTextBoxRawSent";
            // 
            // richTextBoxDoc
            // 
            resources.ApplyResources(this.richTextBoxDoc, "richTextBoxDoc");
            this.richTextBoxDoc.Name = "richTextBoxDoc";
            // 
            // tabPageTreeAlign
            // 
            resources.ApplyResources(this.tabPageTreeAlign, "tabPageTreeAlign");
            this.tabPageTreeAlign.Controls.Add(this.splitContainerTreeAlign);
            this.tabPageTreeAlign.Name = "tabPageTreeAlign";
            this.tabPageTreeAlign.UseVisualStyleBackColor = true;
            // 
            // splitContainerTreeAlign
            // 
            resources.ApplyResources(this.splitContainerTreeAlign, "splitContainerTreeAlign");
            this.splitContainerTreeAlign.Name = "splitContainerTreeAlign";
            // 
            // splitContainerTreeAlign.Panel1
            // 
            this.splitContainerTreeAlign.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainerTreeAlign.Panel1.Controls.Add(this.buttonAgreement);
            this.splitContainerTreeAlign.Panel1.Controls.Add(this.buttonTreeLog);
            this.splitContainerTreeAlign.Panel1.Controls.Add(this.label1);
            this.splitContainerTreeAlign.Panel1.Controls.Add(this.lithiumControl1);
            // 
            // splitContainerTreeAlign.Panel2
            // 
            this.splitContainerTreeAlign.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainerTreeAlign.Panel2.Controls.Add(this.label2);
            this.splitContainerTreeAlign.Panel2.Controls.Add(this.lithiumControl2);
            // 
            // buttonAgreement
            // 
            resources.ApplyResources(this.buttonAgreement, "buttonAgreement");
            this.buttonAgreement.Name = "buttonAgreement";
            this.buttonAgreement.UseVisualStyleBackColor = true;
            this.buttonAgreement.Click += new System.EventHandler(this.buttonAgreement_Click);
            // 
            // buttonTreeLog
            // 
            resources.ApplyResources(this.buttonTreeLog, "buttonTreeLog");
            this.buttonTreeLog.Name = "buttonTreeLog";
            this.buttonTreeLog.UseVisualStyleBackColor = true;
            this.buttonTreeLog.Click += new System.EventHandler(this.buttonTreeLog_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lithiumControl1
            // 
            resources.ApplyResources(this.lithiumControl1, "lithiumControl1");
            this.lithiumControl1.BackColor = System.Drawing.SystemColors.Window;
            this.lithiumControl1.BranchHeight = 70;
            this.lithiumControl1.ConnectionType = Netron.Lithium.ConnectionType.Traditional;
            this.lithiumControl1.LayoutDirection = Netron.Lithium.TreeDirection.Vertical;
            this.lithiumControl1.LayoutEnabled = true;
            this.lithiumControl1.Name = "lithiumControl1";
            this.lithiumControl1.WordSpacing = 20;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lithiumControl2
            // 
            resources.ApplyResources(this.lithiumControl2, "lithiumControl2");
            this.lithiumControl2.BackColor = System.Drawing.SystemColors.Window;
            this.lithiumControl2.BranchHeight = 70;
            this.lithiumControl2.ConnectionType = Netron.Lithium.ConnectionType.Traditional;
            this.lithiumControl2.LayoutDirection = Netron.Lithium.TreeDirection.Vertical;
            this.lithiumControl2.LayoutEnabled = true;
            this.lithiumControl2.Name = "lithiumControl2";
            this.lithiumControl2.WordSpacing = 20;
            // 
            // tabPagePOS
            // 
            this.tabPagePOS.Controls.Add(this.splitContainerPOS);
            resources.ApplyResources(this.tabPagePOS, "tabPagePOS");
            this.tabPagePOS.Name = "tabPagePOS";
            this.tabPagePOS.UseVisualStyleBackColor = true;
            // 
            // splitContainerPOS
            // 
            this.splitContainerPOS.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.splitContainerPOS, "splitContainerPOS");
            this.splitContainerPOS.Name = "splitContainerPOS";
            // 
            // splitContainerPOS.Panel1
            // 
            this.splitContainerPOS.Panel1.Controls.Add(this.splitContainer5);
            // 
            // splitContainerPOS.Panel2
            // 
            this.splitContainerPOS.Panel2.Controls.Add(this.buttonOpenPOSLog);
            // 
            // splitContainer5
            // 
            resources.ApplyResources(this.splitContainer5, "splitContainer5");
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.richTextBoxPOS);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.richTextBoxPOS2);
            // 
            // richTextBoxPOS
            // 
            resources.ApplyResources(this.richTextBoxPOS, "richTextBoxPOS");
            this.richTextBoxPOS.Name = "richTextBoxPOS";
            this.richTextBoxPOS.TextChanged += new System.EventHandler(this.richTextBoxPOS_TextChanged);
            // 
            // richTextBoxPOS2
            // 
            resources.ApplyResources(this.richTextBoxPOS2, "richTextBoxPOS2");
            this.richTextBoxPOS2.Name = "richTextBoxPOS2";
            // 
            // buttonOpenPOSLog
            // 
            resources.ApplyResources(this.buttonOpenPOSLog, "buttonOpenPOSLog");
            this.buttonOpenPOSLog.Name = "buttonOpenPOSLog";
            this.buttonOpenPOSLog.UseVisualStyleBackColor = true;
            this.buttonOpenPOSLog.Click += new System.EventHandler(this.buttonOpenPOSLog_Click);
            // 
            // tabPageWSeg
            // 
            resources.ApplyResources(this.tabPageWSeg, "tabPageWSeg");
            this.tabPageWSeg.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageWSeg.Controls.Add(this.splitContainerWSeg);
            this.tabPageWSeg.Name = "tabPageWSeg";
            this.tabPageWSeg.UseVisualStyleBackColor = true;
            // 
            // splitContainerWSeg
            // 
            resources.ApplyResources(this.splitContainerWSeg, "splitContainerWSeg");
            this.splitContainerWSeg.Name = "splitContainerWSeg";
            // 
            // splitContainerWSeg.Panel1
            // 
            this.splitContainerWSeg.Panel1.Controls.Add(this.splitContainerWSeg_Edit);
            // 
            // splitContainerWSeg_Edit
            // 
            resources.ApplyResources(this.splitContainerWSeg_Edit, "splitContainerWSeg_Edit");
            this.splitContainerWSeg_Edit.Name = "splitContainerWSeg_Edit";
            // 
            // splitContainerWSeg_Edit.Panel1
            // 
            this.splitContainerWSeg_Edit.Panel1.Controls.Add(this.richTextBoxWSeg);
            // 
            // splitContainerWSeg_Edit.Panel2
            // 
            this.splitContainerWSeg_Edit.Panel2.Controls.Add(this.richTextBoxWSeg2);
            // 
            // richTextBoxWSeg
            // 
            resources.ApplyResources(this.richTextBoxWSeg, "richTextBoxWSeg");
            this.richTextBoxWSeg.Name = "richTextBoxWSeg";
            // 
            // richTextBoxWSeg2
            // 
            resources.ApplyResources(this.richTextBoxWSeg2, "richTextBoxWSeg2");
            this.richTextBoxWSeg2.Name = "richTextBoxWSeg2";
            // 
            // tabSentSplit
            // 
            this.tabSentSplit.Controls.Add(this.splitContainerSentSplit);
            resources.ApplyResources(this.tabSentSplit, "tabSentSplit");
            this.tabSentSplit.Name = "tabSentSplit";
            this.tabSentSplit.UseVisualStyleBackColor = true;
            // 
            // splitContainerSentSplit
            // 
            resources.ApplyResources(this.splitContainerSentSplit, "splitContainerSentSplit");
            this.splitContainerSentSplit.Name = "splitContainerSentSplit";
            // 
            // splitContainerSentSplit.Panel1
            // 
            this.splitContainerSentSplit.Panel1.Controls.Add(this.label4);
            // 
            // splitContainerSentSplit.Panel2
            // 
            this.splitContainerSentSplit.Panel2.Controls.Add(this.richTextBoxSentSplit);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Name = "label4";
            // 
            // richTextBoxSentSplit
            // 
            resources.ApplyResources(this.richTextBoxSentSplit, "richTextBoxSentSplit");
            this.richTextBoxSentSplit.Name = "richTextBoxSentSplit";
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageSearch.Controls.Add(this.richTextBoxStatistics);
            this.tabPageSearch.Controls.Add(this.buttonStatistics);
            this.tabPageSearch.Controls.Add(this.buttonCheck);
            this.tabPageSearch.Controls.Add(this.buttonClear);
            this.tabPageSearch.Controls.Add(this.richTextBoxSearch);
            this.tabPageSearch.Controls.Add(this.buttonSearch);
            resources.ApplyResources(this.tabPageSearch, "tabPageSearch");
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // richTextBoxStatistics
            // 
            resources.ApplyResources(this.richTextBoxStatistics, "richTextBoxStatistics");
            this.richTextBoxStatistics.Name = "richTextBoxStatistics";
            // 
            // buttonStatistics
            // 
            resources.ApplyResources(this.buttonStatistics, "buttonStatistics");
            this.buttonStatistics.Name = "buttonStatistics";
            this.buttonStatistics.UseVisualStyleBackColor = true;
            this.buttonStatistics.Click += new System.EventHandler(this.buttonStatistics_Click);
            // 
            // buttonCheck
            // 
            resources.ApplyResources(this.buttonCheck, "buttonCheck");
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.ButtonCheckPOSClick);
            // 
            // buttonClear
            // 
            resources.ApplyResources(this.buttonClear, "buttonClear");
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.ButtonSearchClearClick);
            // 
            // richTextBoxSearch
            // 
            resources.ApplyResources(this.richTextBoxSearch, "richTextBoxSearch");
            this.richTextBoxSearch.Name = "richTextBoxSearch";
            // 
            // buttonSearch
            // 
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearchClick);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // toolStrip2
            // 
            resources.ApplyResources(this.toolStrip2, "toolStrip2");
            this.toolStrip2.Name = "toolStrip2";
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 1000;
            this.toolTip1.AutoPopDelay = 1000;
            this.toolTip1.InitialDelay = 1;
            this.toolTip1.ReshowDelay = 20;
            // 
            // buttonOpenFile
            // 
            resources.ApplyResources(this.buttonOpenFile, "buttonOpenFile");
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // buttonSaveFile
            // 
            resources.ApplyResources(this.buttonSaveFile, "buttonSaveFile");
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveDataFile_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Name = "label5";
            // 
            // button9
            // 
            resources.ApplyResources(this.button9, "button9");
            this.button9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button9.Name = "button9";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Name = "label6";
            // 
            // button10
            // 
            resources.ApplyResources(this.button10, "button10");
            this.button10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button10.Name = "button10";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Name = "label7";
            // 
            // button11
            // 
            resources.ApplyResources(this.button11, "button11");
            this.button11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button11.Name = "button11";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Name = "label8";
            // 
            // button12
            // 
            resources.ApplyResources(this.button12, "button12");
            this.button12.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button12.Name = "button12";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Name = "label9";
            // 
            // button13
            // 
            resources.ApplyResources(this.button13, "button13");
            this.button13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button13.Name = "button13";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label10.Name = "label10";
            // 
            // button14
            // 
            resources.ApplyResources(this.button14, "button14");
            this.button14.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button14.Name = "button14";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // buttonOpenLog
            // 
            resources.ApplyResources(this.buttonOpenLog, "buttonOpenLog");
            this.buttonOpenLog.Name = "buttonOpenLog";
            this.buttonOpenLog.UseVisualStyleBackColor = true;
            this.buttonOpenLog.Click += new System.EventHandler(this.buttonOpenLog_Click);
            // 
            // buttonNewLog
            // 
            resources.ApplyResources(this.buttonNewLog, "buttonNewLog");
            this.buttonNewLog.Name = "buttonNewLog";
            this.buttonNewLog.UseVisualStyleBackColor = true;
            this.buttonNewLog.Click += new System.EventHandler(this.buttonNewLog_Click);
            // 
            // buttonOpenDir
            // 
            resources.ApplyResources(this.buttonOpenDir, "buttonOpenDir");
            this.buttonOpenDir.Name = "buttonOpenDir";
            this.buttonOpenDir.UseVisualStyleBackColor = true;
            this.buttonOpenDir.Click += new System.EventHandler(this.buttonOpenDir_Click);
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // comboBoxWorkingMode
            // 
            this.comboBoxWorkingMode.FormattingEnabled = true;
            this.comboBoxWorkingMode.Items.AddRange(new object[] {
            resources.GetString("comboBoxWorkingMode.Items"),
            resources.GetString("comboBoxWorkingMode.Items1"),
            resources.GetString("comboBoxWorkingMode.Items2"),
            resources.GetString("comboBoxWorkingMode.Items3"),
            resources.GetString("comboBoxWorkingMode.Items4"),
            resources.GetString("comboBoxWorkingMode.Items5"),
            resources.GetString("comboBoxWorkingMode.Items6")});
            resources.ApplyResources(this.comboBoxWorkingMode, "comboBoxWorkingMode");
            this.comboBoxWorkingMode.Name = "comboBoxWorkingMode";
            this.comboBoxWorkingMode.SelectionChangeCommitted += new System.EventHandler(this.comboBoxWorkingMode_SelectionChangeCommitted);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // splitContainerFirst
            // 
            resources.ApplyResources(this.splitContainerFirst, "splitContainerFirst");
            this.splitContainerFirst.Name = "splitContainerFirst";
            // 
            // splitContainerFirst.Panel1
            // 
            this.splitContainerFirst.Panel1.Controls.Add(this.comboBoxFileName);
            this.splitContainerFirst.Panel1.Controls.Add(this.label3);
            this.splitContainerFirst.Panel1.Controls.Add(this.comboBoxWorkingMode);
            this.splitContainerFirst.Panel1.Controls.Add(this.buttonOpenFile);
            this.splitContainerFirst.Panel1.Controls.Add(this.buttonOpenDir);
            this.splitContainerFirst.Panel1.Controls.Add(this.buttonSaveFile);
            this.splitContainerFirst.Panel1.Controls.Add(this.buttonNewLog);
            this.splitContainerFirst.Panel1.Controls.Add(this.buttonOpenLog);
            // 
            // splitContainerFirst.Panel2
            // 
            this.splitContainerFirst.Panel2.Controls.Add(this.splitContainerMainView);
            // 
            // comboBoxFileName
            // 
            this.comboBoxFileName.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxFileName, "comboBoxFileName");
            this.comboBoxFileName.Name = "comboBoxFileName";
            this.comboBoxFileName.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFileName_SelectionChangeCommitted);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitContainerFirst);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.splitContainerMainView.Panel1.ResumeLayout(false);
            this.splitContainerMainView.Panel2.ResumeLayout(false);
            this.splitContainerMainView.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer4ListBoxes.Panel1.ResumeLayout(false);
            this.splitContainer4ListBoxes.Panel2.ResumeLayout(false);
            this.splitContainer4ListBoxes.ResumeLayout(false);
            this.splitContainerFileList.Panel1.ResumeLayout(false);
            this.splitContainerFileList.Panel1.PerformLayout();
            this.splitContainerFileList.Panel2.ResumeLayout(false);
            this.splitContainerFileList.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPageTree.ResumeLayout(false);
            this.splitContainerTree.Panel1.ResumeLayout(false);
            this.splitContainerTree.Panel2.ResumeLayout(false);
            this.splitContainerTree.Panel2.PerformLayout();
            this.splitContainerTree.ResumeLayout(false);
            this.tabPageRawSent.ResumeLayout(false);
            this.splitContainerRawSent.Panel1.ResumeLayout(false);
            this.splitContainerRawSent.Panel2.ResumeLayout(false);
            this.splitContainerRawSent.ResumeLayout(false);
            this.tabPageTreeAlign.ResumeLayout(false);
            this.splitContainerTreeAlign.Panel1.ResumeLayout(false);
            this.splitContainerTreeAlign.Panel1.PerformLayout();
            this.splitContainerTreeAlign.Panel2.ResumeLayout(false);
            this.splitContainerTreeAlign.Panel2.PerformLayout();
            this.splitContainerTreeAlign.ResumeLayout(false);
            this.tabPagePOS.ResumeLayout(false);
            this.splitContainerPOS.Panel1.ResumeLayout(false);
            this.splitContainerPOS.Panel2.ResumeLayout(false);
            this.splitContainerPOS.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.ResumeLayout(false);
            this.tabPageWSeg.ResumeLayout(false);
            this.splitContainerWSeg.Panel1.ResumeLayout(false);
            this.splitContainerWSeg.ResumeLayout(false);
            this.splitContainerWSeg_Edit.Panel1.ResumeLayout(false);
            this.splitContainerWSeg_Edit.Panel2.ResumeLayout(false);
            this.splitContainerWSeg_Edit.ResumeLayout(false);
            this.tabSentSplit.ResumeLayout(false);
            this.splitContainerSentSplit.Panel1.ResumeLayout(false);
            this.splitContainerSentSplit.Panel1.PerformLayout();
            this.splitContainerSentSplit.Panel2.ResumeLayout(false);
            this.splitContainerSentSplit.ResumeLayout(false);
            this.tabPageSearch.ResumeLayout(false);
            this.splitContainerFirst.Panel1.ResumeLayout(false);
            this.splitContainerFirst.Panel1.PerformLayout();
            this.splitContainerFirst.Panel2.ResumeLayout(false);
            this.splitContainerFirst.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #region các object private

        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainerMainView;
        private System.Windows.Forms.ToolTip toolTip1;
        
        #endregion 

        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Button buttonSaveFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPageTree;
        private System.Windows.Forms.SplitContainer splitContainerTree;
        private System.Windows.Forms.RichTextBox richTextBoxTree;
        private LithiumControl lithiumControl;
        private System.Windows.Forms.TabPage tabPageRawSent;
        private System.Windows.Forms.TabPage tabPagePOS;
        private System.Windows.Forms.TabPage tabPageWSeg;
        private System.Windows.Forms.TabPage tabPageSearch;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.RichTextBox richTextBoxSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TabPage tabPageTreeAlign;
        private System.Windows.Forms.SplitContainer splitContainerTreeAlign;
        private LithiumControl lithiumControl1;
        private LithiumControl lithiumControl2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonViewGUITree;
        private System.Windows.Forms.SplitContainer splitContainerPOS;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.RichTextBox richTextBoxPOS;
        private System.Windows.Forms.RichTextBox richTextBoxPOS2;
        private System.Windows.Forms.SplitContainer splitContainerRawSent;
        private System.Windows.Forms.RichTextBox richTextBoxRawSent;
        private System.Windows.Forms.Button buttonOpenLog;
        private System.Windows.Forms.Button buttonNewLog;
        private System.Windows.Forms.Button buttonTreeLog;
        private System.Windows.Forms.Button buttonOpenPOSLog;
        private System.Windows.Forms.Button buttonAgreement;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RadioButton radioButtonDependency;
        private System.Windows.Forms.RadioButton radioButtonPhrase;
        private System.Windows.Forms.SplitContainer splitContainer4ListBoxes;
        private System.Windows.Forms.ListBox listBoxSent;
        private System.Windows.Forms.SplitContainer splitContainerFileList;
        private System.Windows.Forms.Label labelFileList;
        private System.Windows.Forms.Button buttonOpenDir;
        private System.Windows.Forms.ListView listViewFile;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog3;
        private System.Windows.Forms.ComboBox comboBoxWorkingMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainerFirst;
        private System.Windows.Forms.ComboBox comboBoxFileName;
        private System.Windows.Forms.RichTextBox richTextBoxStatistics;
        private System.Windows.Forms.Button buttonStatistics;
        private System.Windows.Forms.Button buttonNormalizeTextTree;
        private System.Windows.Forms.RichTextBox richTextBoxDoc;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.Button buttonRedo;
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.SplitContainer splitContainerWSeg;
        private System.Windows.Forms.SplitContainer splitContainerWSeg_Edit;
        private System.Windows.Forms.RichTextBox richTextBoxWSeg;
        private System.Windows.Forms.RichTextBox richTextBoxWSeg2;
        private System.Windows.Forms.TabPage tabSentSplit;
        private System.Windows.Forms.SplitContainer splitContainerSentSplit;
        private System.Windows.Forms.RichTextBox richTextBoxSentSplit;
        private System.Windows.Forms.Label label4;



        //_____________________________________________________________//


    }
}