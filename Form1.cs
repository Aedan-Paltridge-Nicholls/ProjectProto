using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            toolStripComboBox1.Text = richTextBox1.Font.ToString();
            toolStripComboBox2.Text = richTextBox1.Font.Size.ToString();
            CurrentFontStyle = "Regular";
            toolStripComboBox3.Text = "100%";
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private PrintPreviewDialog printpreviewDialog1 = new PrintPreviewDialog();
        private PrintDocument printDocument1 = new PrintDocument();
        private string documentContents;
        private string stringToPrint;
        private void ReadDocument()
        {
            
            printDocument1.DocumentName = OpenFileName;
            using (FileStream stream2 = new FileStream(OpenFileName, FileMode.Open))
            using (StreamReader reader2 = new StreamReader(stream2))
            {
                documentContents = reader2.ReadToEnd();
            }
            stringToPrint = documentContents;
        }
        void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;
            e.Graphics.MeasureString(stringToPrint, this.Font,
                e.MarginBounds.Size, StringFormat.GenericTypographic,
                out charactersOnPage, out linesPerPage);
            e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
            e.MarginBounds, StringFormat.GenericTypographic);
            stringToPrint = stringToPrint.Substring(charactersOnPage);
            e.HasMorePages = (stringToPrint.Length > 0);
            if (!e.HasMorePages)
                stringToPrint = documentContents;
        }
        private void printPreveiwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFileName != null)
            { printDocument1.PrintPage +=
                new PrintPageEventHandler(printDocument1_PrintPage);
                ReadDocument();
                printpreviewDialog1.Document = printDocument1;
                printpreviewDialog1.ShowDialog();
            }
            else 
            {
                MessageBox.Show("Please Open or create a document,\n before trying to Preview a print",
                    "Print Preview Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFileName != null)
            {printDocument1.PrintPage +=
                new PrintPageEventHandler(printDocument1_PrintPage);
                using (PrintDialog printDialog = new PrintDialog())
                {
                    printDialog.AllowSomePages = true;
                    printDialog.AllowSelection = true;
                    if (printDialog.ShowDialog() == DialogResult.OK)
                    { ReadDocument(); printDocument1.Print(); }
                }
            }
            else
            {
                MessageBox.Show("Please Open or create a document,\n before trying to print",
                    "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string OpenFileName;
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            OpenFileName = saveFileDialog1.FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    richTextBox1.SaveFile(myStream + ".txt", RichTextBoxStreamType.PlainText);
                    myStream.Close();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFileName != null)
            {
                richTextBox1.SaveFile(OpenFileName, RichTextBoxStreamType.PlainText);
            }
            else
            {
                Stream myStream;
                saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                OpenFileName = saveFileDialog1.FileName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        richTextBox1.SaveFile(myStream+".txt", RichTextBoxStreamType.RichText);
                        myStream.Close();
                    }
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenFileName = openFileDialog1.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }
       
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            Form1 form1 = this;         
            frm2.ShowDialog();
                      
            string newfileName = Form2.imputText;
            string filebegining = @"C:\Users\nicho\Documents\" ;
            string FileEnd = ".txt";
            string newfilepath = filebegining+ newfileName + FileEnd;
            if (File.Exists(newfilepath))
            { MessageBox.Show("File name allready extsts ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else if (File.Exists(OpenFileName))
            { MessageBox.Show("File name allready extsts ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {FileStream fs = File.Create(newfilepath);
                OpenFileName = newfilepath;
            }

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        

        private void rulerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gridlinesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backroundColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1 = new ColorDialog();
            DialogResult colorDialogresult = colorDialog1.ShowDialog();
            colorDialog1.ShowHelp = false;
            if (colorDialogresult == DialogResult.OK)
            { richTextBox1.BackColor = colorDialog1.Color;}
            
        
        }

        private void forgroundColeourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1 = new ColorDialog();
            DialogResult colorDialogresult = colorDialog1.ShowDialog();
            colorDialog1.ShowHelp = false;
            if (colorDialogresult == DialogResult.OK)
            { richTextBox1.ForeColor = colorDialog1.Color; }
        }

        

       

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {

        }


        public string CurrentFontStyle; 
       

        private void regularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font(richTextBox1.Font.Name,richTextBox1.Font.Size,FontStyle.Regular);
            CurrentFontStyle = "Regular";
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font(richTextBox1.Font.Name, richTextBox1.Font.Size, FontStyle.Italic);
            CurrentFontStyle = "Italic";
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font(richTextBox1.Font.Name, richTextBox1.Font.Size, FontStyle.Bold);
            CurrentFontStyle = "Bold";
        }

       

        private void toolStripButton9_Click_1(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void toolStripButton7_Click_1(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void toolStripButton10_Click_1(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        

        private void toolStripComboBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string Newfont = toolStripComboBox1.Text;
            richTextBox1.Font = new Font(Newfont, richTextBox1.Font.Size);
        }
        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            int NewfontSize = (Convert.ToInt32(toolStripComboBox2.Text));
            if(CurrentFontStyle == "Regular")
            { richTextBox1.Font = new Font(richTextBox1.Font.Name, NewfontSize, FontStyle.Regular); }
            else if (CurrentFontStyle == "Italic")
            { richTextBox1.Font = new Font(richTextBox1.Font.Name, NewfontSize, FontStyle.Italic); }
            else if (CurrentFontStyle == "Bold")
            { richTextBox1.Font = new Font(richTextBox1.Font.Name, NewfontSize, FontStyle.Bold); }
        }
        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            float newfontsize = richTextBox1.Font.Size;
            newfontsize++;
            if (CurrentFontStyle == "Regular")
            { richTextBox1.Font = new Font(richTextBox1.Font.Name, newfontsize, FontStyle.Regular); }
            else if (CurrentFontStyle == "Italic")
            { richTextBox1.Font = new Font(richTextBox1.Font.Name, newfontsize, FontStyle.Italic); }
            else if (CurrentFontStyle == "Bold")
            { richTextBox1.Font = new Font(richTextBox1.Font.Name, newfontsize, FontStyle.Bold); }
            toolStripComboBox2.Text = richTextBox1.Font.Size.ToString();
        }

        private void toolStripButton8_Click_1(object sender, EventArgs e)
        {
            float newfontsize = richTextBox1.Font.Size;
            newfontsize--;
            if (CurrentFontStyle == "Regular")
            { richTextBox1.Font = new Font(richTextBox1.Font.Name, newfontsize, FontStyle.Regular); }
            else if (CurrentFontStyle == "Italic")
            { richTextBox1.Font = new Font(richTextBox1.Font.Name, newfontsize, FontStyle.Italic); }
            else if (CurrentFontStyle == "Bold")
            { richTextBox1.Font = new Font(richTextBox1.Font.Name, newfontsize, FontStyle.Bold); }
            toolStripComboBox2.Text = richTextBox1.Font.Size.ToString();
        }

        private void toolStripButton6_ButtonClick(object sender, EventArgs e)
        {

        }


        private void Cut_Click_1(object sender, EventArgs e)
        {
           richTextBox1.Cut();  
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void toolStripDropDownButtonFontStyle_ButtonClick(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            while (toolStripComboBox3.SelectedIndex != -1)
            {
                switch (toolStripComboBox3.SelectedIndex)
                {
                    case 0:
                        {richTextBox1.ZoomFactor = 0.25f; toolStripComboBox3.Text = "25%"; break;}
                    case 1:
                        {richTextBox1.ZoomFactor = 0.50f; toolStripComboBox3.Text = "50%";break;}
                    case 2:
                        {richTextBox1.ZoomFactor = 0.75f; toolStripComboBox3.Text = "75%";break;}
                    case 3:
                        {richTextBox1.ZoomFactor = 1;     toolStripComboBox3.Text = "100%";break;}
                    case 4:
                        {richTextBox1.ZoomFactor = 1.25f; toolStripComboBox3.Text = "125%";break;}
                    case 5:
                        {richTextBox1.ZoomFactor = 1.50f; toolStripComboBox3.Text = "150%";break;}
                    case 6:
                        {richTextBox1.ZoomFactor = 1.75f; toolStripComboBox3.Text = "175%";break;}                       
                }
              break;
            }
        }
    }   
}
