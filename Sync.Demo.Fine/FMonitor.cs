using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sync.Demo.Fine
{
    public partial class FMonitor : Form
    {
        private static int _result;

        private static object _syncObj = new object();

        public FMonitor()
        {
            InitializeComponent();
        }

        private void AddResult()
        {
            _result = 0; //重置result
            var taskList = new List<Task>();
            for (var i = 0; i < 100; i++)
            {
                taskList.Add(Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(new Random().Next(10) * 100);  //模拟耗时操作
                    System.Threading.Monitor.Enter(_syncObj); //获取锁,允许一个且仅一个线程继续执行后面的语句,其他所有线程都将被阻止  
                    try
                    {
                        _result++;
                    }
                    finally
                    {
                        System.Threading.Monitor.Exit(_syncObj);  //释放锁
                    }
                }));
            }
            Task.WaitAll(taskList.ToArray());
            MessageBox.Show(_result.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddResult();
        }
    }
}
