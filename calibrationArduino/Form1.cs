using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using BSD.Dal;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace calibrationArduino
{
    public partial class Form1 : Form
    {
        private int click = 1;
        private bool check = true;
        private Label[] _lA = new Label[6];
        private Label[] _lB = new Label[6];
        private Label[] _lC = new Label[6];
        private Label[] _lD = new Label[6];
        private Label[] _lE = new Label[6];
        private string[] _device;
        private string[] _range;
        private string _textFromBoard;
        private string _portNo = "1113";

        public Form1()
        {
            InitializeComponent();
            addLabel();
            
        }

        public void serverThread()
        {
            UdpClient udpClient = new UdpClient(Convert.ToInt32(_portNo));
            while (check)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);
                _textFromBoard = returnData.ToString();
                this.Invoke(new EventHandler(updateLabel));
            }
            udpClient.Close();
            Thread.CurrentThread.Abort();

        }

        private void addLabel()
        {
            _lA[1] = lA1; _lA[2] = lA2; _lA[3] = lA3; _lA[4] = lA4; _lA[5] = lA5;
            _lB[1] = lB1; _lB[2] = lB2; _lB[3] = lB3; _lB[4] = lB4; _lB[5] = lB5;
            _lC[1] = lC1; _lC[2] = lC2; _lC[3] = lC3; _lC[4] = lC4; _lC[5] = lC5;
            _lD[1] = lD1; _lD[2] = lD2; _lD[3] = lD3; _lD[4] = lD4; _lD[5] = lD5;
            _lE[1] = lE1; _lE[2] = lE2; _lE[3] = lE3; _lE[4] = lE4; _lE[5] = lE5;

            setVisible("A", false);
            setVisible("B", false);
            setVisible("C", false);
            setVisible("D", false);
            setVisible("E", false);
        }

        private void updateLabel(object sender, EventArgs e)
        {
            wordSplitter();

        }

        private void A_CheckedChanged(object sender, EventArgs e)
        {
            if (A.Checked)      setVisible("A", true);
            else                setVisible("A", false);
        }

        private void setVisible(string device,bool status)
        {
            int count;

            if (device.Equals("A"))
            {
                for (count = 1; count <= 5; count++)
                {
                    _lA[count].Visible = status;
                }
            }
            else if (device.Equals("B"))
            {
                for (count = 1; count <= 5; count++)
                {
                    _lB[count].Visible = status;
                }
            }
            else if (device.Equals("C"))
            {
                for (count = 1; count <= 5; count++)
                {
                    _lC[count].Visible = status;
                }
            }
            else if (device.Equals("D"))
            {
                for (count = 1; count <= 5; count++)
                {
                    _lD[count].Visible = status;
                }
            }
            else if (device.Equals("E"))
            {
                for (count = 1; count <= 5; count++)
                {
                    _lE[count].Visible = status;
                }
            }
        }

        private void setVisileFalse()
        {
            setVisible("A", false);
            setVisible("B", false);
            setVisible("C", false);
            setVisible("D", false);
            setVisible("E", false);
        }

        private void B_CheckedChanged(object sender, EventArgs e)
        {
            if (B.Checked) setVisible("B", true);
            else setVisible("B", false);
        }

        private void C_CheckedChanged(object sender, EventArgs e)
        {
            if (C.Checked) setVisible("C", true);
            else setVisible("C", false);
        }

        private void D_CheckedChanged(object sender, EventArgs e)
        {
            if (D.Checked) setVisible("D", true);
            else setVisible("D", false);
        }

        private void E_CheckedChanged(object sender, EventArgs e)
        {
            if (E.Checked) setVisible("E", true);
            else setVisible("E", false);
        }

        private void wordSplitter()
        {
            int count;
            string[] word = _textFromBoard.Split(',');

            for (count = 1; count <= 5; count++)
            {
                _device[count] = word[2*count-2];
                _range[count] = word[2*count-1];
            }

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Thread thdUDPServer = new Thread(new ThreadStart(serverThread));
            check = true;
            thdUDPServer.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            int count;
            check = false;
            setVisileFalse();

            for (count = 1; count <= click; count++)
            {
                _lA[count].Visible = true;
                _lB[count].Visible = true;
                _lC[count].Visible = true;
                _lD[count].Visible = true;
                _lE[count].Visible = true;
            }
        }

        private void nButton_Click(object sender, EventArgs e)
        {
            int count;
            click++;

            if (click == 2) pButton.Visible = true;

            setVisileFalse();

            for (count = 1; count <= click; count++)
            {
                _lA[count].Visible = true;
                _lB[count].Visible = true;
                _lC[count].Visible = true;
                _lD[count].Visible = true;
                _lE[count].Visible = true;
            }

        }

        private void pButton_Click(object sender, EventArgs e)
        {
            click--;
            if (click == 1)         pButton.Visible = false;
            
        }
    }
}
