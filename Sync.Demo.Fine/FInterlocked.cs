using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sync.Demo.Fine
{
    public partial class FInterlocked : Form
    {
        private static int _result;

        private int _addType;

        public FInterlocked()
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
                   if (_addType == 0)
                   {
                       _result++;
                   }
                   else
                   {
                       Interlocked.Increment(ref _result);  //使用Interlocked 对_result进行原子操作
                   }
               }));
            }
            Task.WaitAll(taskList.ToArray());
            MessageBox.Show(_result.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _addType = 0;
            AddResult();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _addType = 1;
            AddResult();
        }
    }
}
