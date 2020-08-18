using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WechatLogger
{
    public partial class Form1 : Form
    {
        String lastSaved;
        List<string> messages = new List<string>();
        public Form1()
        {
            InitializeComponent();
            webBrowser1.Navigate("https://wx.qq.com/");
            this.lastSaved = this.webBrowser1.DocumentText;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(this.webBrowser1.DocumentText != this.lastSaved)
            {
                int i = 0;
                foreach (HtmlElement element in webBrowser1.Document.All)
                {
                    bool isSame = false;
                    if (element.GetAttribute("ng-bind-html").ToString() == "message.MMActualContent")
                    {
                        if (element.InnerText.Contains("撤回了一条消息"))
                        {

                        }
                        else
                        {
                            foreach (var message in messages)
                            {
                                if (message == element.InnerText + i.ToString())
                                {
                                    isSame = true;
                                    break;
                                }
                            }
                            if (isSame == false)
                            {
                                richTextBox1.AppendText(element.InnerText + Environment.NewLine);
                                messages.Add(element.InnerText + i.ToString());
                                i++;
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            messages.Clear();
        }
    }
}
