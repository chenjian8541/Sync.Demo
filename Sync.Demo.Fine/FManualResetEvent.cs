using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Sync.Demo.Fine
{
    public partial class FManualResetEvent : Form
    {
        public FManualResetEvent()
        {
            InitializeComponent();
        }

        private readonly static ManualResetEvent _buyFish = new ManualResetEvent(false);

        private readonly static ManualResetEvent _fashingFish = new ManualResetEvent(false);

        /// <summary>
        /// 买鱼
        /// </summary>
        private void BuyFish()
        {
            UpdateMakeFishMsg("[买鱼]骑车去菜市场...");
            Thread.Sleep(2000);
            UpdateMakeFishMsg("[买鱼]挑鱼...");
            Thread.Sleep(2000);
            UpdateMakeFishMsg("[买鱼]称鱼...");
            Thread.Sleep(2000);
            UpdateMakeFishMsg("[买鱼]去鳞...");
            Thread.Sleep(1000);
            UpdateMakeFishMsg("[买鱼]付钱...");
            Thread.Sleep(1000);
            UpdateMakeFishMsg("[买鱼]完成...");
            _buyFish.Set();
        }

        /// <summary>
        /// 洗鱼 
        /// </summary>
        private void WashingFish()
        {
            _buyFish.WaitOne();  //需要等待“买鱼”完成
            UpdateMakeFishMsg("[洗鱼]打水...");
            Thread.Sleep(2000);
            UpdateMakeFishMsg("[洗鱼]清洗...");
            Thread.Sleep(2000);
            UpdateMakeFishMsg("[洗鱼]完成...");
            _fashingFish.Set();
        }

        /// <summary>
        /// 炒鱼
        /// </summary>
        private void FriedFish()
        {
            ManualResetEvent.WaitAll(new WaitHandle[] { _buyFish, _fashingFish });  //需要等待“买鱼”和“洗鱼”都完成
            UpdateMakeFishMsg("[炒鱼]开火...");
            Thread.Sleep(2000);
            UpdateMakeFishMsg("[炒鱼]把鱼放入锅中...");
            Thread.Sleep(2000);
            UpdateMakeFishMsg("[炒鱼]大火炒10分钟...");
            Thread.Sleep(2000);
            UpdateMakeFishMsg("[炒鱼]放盐...");
            Thread.Sleep(1000);
            UpdateMakeFishMsg("[炒鱼]制作炒鱼完成...");
        }

        /// <summary>
        /// 制作炒鱼
        /// </summary>
        private void MakeFish()
        {
            var buyFishThread = new Thread(new ThreadStart(BuyFish));
            buyFishThread.Start();
            var washingFishThread = new Thread(new ThreadStart(WashingFish));
            washingFishThread.Start();
            var friedFishThread = new Thread(new ThreadStart(FriedFish));
            friedFishThread.Start();
        }

        private void UpdateMakeFishMsg(string msg)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.listView1.Items.Add($"{msg}");
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            MakeFish();
        }
    }
}
