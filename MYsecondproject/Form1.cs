using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MYsecondproject
{
    public partial class Form1 : Form
    {
        string gyroscope = null;
        public Form1()
        {
            InitializeComponent();
            try
            {
                port = new SerialPort("COM7");
                port.RtsEnable = true;
                port.DtrEnable = true;
                port.DataReceived += Port_DataReceived;
                port.Open();


            }
            catch (Exception ee)
            {
                MessageBox.Show("Оборудование не подключено!");
                Close();
            }



        }
        List<string> allCommands = new List<string>();
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;
            var data = port.ReadLine();
            gyroscope = data.Substring(6, 4).Trim();
            linkLabel1.BeginInvoke(new Action(()=> linkLabel1.Text = gyroscope));
            allCommands.Add(data.Trim());
            switch (data.Trim())
            {
                case "up":
                    //if (allCommands.Where(f => f == "up").Count() >= 50)
                    //{
                    VolumeChanger.VolumeUp();
                    allCommands.Clear();
                    //}
                    break;

                case "vertically":
                    linkLabel1.BeginInvoke(new Action(() => linkLabel1.Text = "Vertically"));
                    break;

                case "horizontally":
                    linkLabel1.BeginInvoke(new Action(() => linkLabel1.Text = "Horizontally"));
                    break;

                case "down":
                    //if (allCommands.Where(f => f == "down").Count() >= 50)
                    //{
                    VolumeChanger.VolumeDown();
                    allCommands.Clear();
                    //}
                    break;

            }
            //switch (data.Substring(6, 4).Trim())



        }
        SerialPort port;
        private void button3_Click(object sender, EventArgs e)
        {
            if (port.IsOpen)
                port.WriteLine("2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (port.IsOpen)
                port.WriteLine("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (port.IsOpen)
                port.WriteLine("0");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VolumeChanger.VolumeUp();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            VolumeChanger.VolumeDown();
        }
    }
}
