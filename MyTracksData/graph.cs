using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;


namespace MyTracksData
{
    public partial class graph : Form
    {
      
        public graph()
        {
            InitializeComponent();
        }

        private void             graph_Load(object sender, EventArgs e)        { 
            reS();    
        }

        private void zgc_Load(object sender, EventArgs e) { reS(); }

        private void graph_Resize(object sender, EventArgs e)
        {
              // Leave a small margin around the outside of the control
          reS();
        }
        public void   reS(){
            zgc.Location = new Point(2,2);
            zgc.Size = new Size(this.ClientRectangle.Width - 10, this.ClientRectangle.Height - 4); 
        }

         }
}
