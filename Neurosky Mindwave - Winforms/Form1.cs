using Neurosky;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neurosky_Mindwave___Winforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConectarDispositivo();
        }
        List<MindwaveReading> listMV = new List<MindwaveReading>();
        private  void ConectarDispositivo()
        {
            Neurosky.Mindwave.Device.CurrentValueChanged += Device_CurrentValueChanged;
            Neurosky.Mindwave.Device.StateChanged += Device_StateChanged;
            if (!Neurosky.Mindwave.Device.IsConnected)
            {
                Neurosky.Mindwave.Device.Start(MindwaveProtocol.ThinkGear, false);

            }

        }

        private void Device_StateChanged(object sender, Neurosky.MindwaveStateChangedEventArgs e)
        {

            if (e.CurrentState == MindwaveServiceState.DeviceNotFound)
            {
                ConectarDispositivo();
            }

            if (e.CurrentState == MindwaveServiceState.ConnectedWithNoDataYet || e.CurrentState == MindwaveServiceState.ConnectedWithData)
            {

            }
            //throw new NotImplementedException();
        }

        private void Device_CurrentValueChanged(object sender, Neurosky.MindwaveReadingEventArgs e)
        {
            
            var data = e.SensorReading;
            listMV.Add(data);

            var bindingList = new BindingList<MindwaveReading>(listMV);
            var source = new BindingSource(bindingList, null);
            grid.DataSource = source;

            if (data.eSenseMeditation > 0)
            {

            }


            //throw new NotImplementedException();
        }

        private void btnPausar_Click(object sender, EventArgs e)
        {
            if (!Neurosky.Mindwave.Device.IsPaused)
            {
                Neurosky.Mindwave.Device.Pause();
                btnPausar.Text = "Reanudar";
                btnLimpiarDatos.Enabled = true;
            }
            else
            {
                Neurosky.Mindwave.Device.Resume();
                btnPausar.Text = "Pausar";
                
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            grid.DataSource = null;
            listMV.Clear();
        }
    }
}
