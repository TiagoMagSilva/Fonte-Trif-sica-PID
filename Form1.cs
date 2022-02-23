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

namespace FonteTrifasicaPID
{
    public partial class Form1 : Form
    {
        String path_SERIAL = System.AppDomain.CurrentDomain.BaseDirectory + "/SERIAL/";
        String path_LOG = System.AppDomain.CurrentDomain.BaseDirectory + "/LOG/";

        private SerialPort PortaSerial = new SerialPort();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizarPortas();

            chartTensao.Series[0].Points.AddXY(1, 2);
            chartTensao.Series[1].Points.AddXY(3, 4);
            chartTensao.Series[2].Points.AddXY(5, 6);

            chartTensao.Series[0].Points.AddXY(4, 2);
            chartTensao.Series[1].Points.AddXY(3, 5);
            chartTensao.Series[2].Points.AddXY(6, 7);
        }

        void AtualizarPortas()
        {
            string[] NomesDasPortas = SerialPort.GetPortNames();

            cbxPorta.Items.Clear();

            cbxPorta.Text = string.Empty;

            foreach (string porta in NomesDasPortas)
            {
                cbxPorta.Items.Add(porta);
            }

            if(cbxPorta.Items.Count > 0)
            {
                cbxPorta.SelectedIndex = 0;
            }

            cbxBaudRate.SelectedIndex = 8;            

            cbxBaudRate.Enabled = true;
        }

        void LerInformacoesSerialSalva()
        {
            if (File.Exists(path_SERIAL + "SERIAL" + ".txt"))
            {
                if (File.ReadLines(path_SERIAL + "SERIAL" + ".txt").Count() > 0)
                {
                    string[] LinhasDoTXT;
                    LinhasDoTXT = File.ReadAllLines(path_SERIAL + "SERIAL" + ".txt");

                    foreach (string line in LinhasDoTXT)
                    {
                        string[] partes = line.Split(';');
                        if (partes.Length == 2)
                        {
                            for (int i = 0; i < cbxPorta.Items.Count; i++)
                            {
                                string NomePorta = cbxPorta.GetItemText(cbxPorta.Items[i]);

                                if (partes[0] == NomePorta)
                                {
                                    cbxPorta.SelectedIndex = i;
                                    break;
                                }
                            }
                            for (int i = 0; i < cbxBaudRate.Items.Count; i++)
                            {
                                string Velocidade = cbxBaudRate.GetItemText(cbxBaudRate.Items[i]);

                                if (partes[1] == Velocidade)
                                {
                                    cbxBaudRate.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnRefreshSerial_Click(object sender, EventArgs e)
        {
            AtualizarPortas();
            LerInformacoesSerialSalva();
        }

        void Salvar_Dados_Serial()
        {
            try
            {
                if (File.Exists(path_SERIAL + "SERIAL" + ".txt"))
                {
                    File.WriteAllText(path_SERIAL + "SERIAL" + ".txt", String.Empty);

                    using (var tw = new StreamWriter(path_SERIAL + "SERIAL" + ".txt", true))
                    {
                        tw.WriteLine(cbxPorta.SelectedItem.ToString() + ";" + cbxBaudRate.SelectedItem.ToString());
                    }
                }
                else
                {
                    if (!File.Exists(path_SERIAL + "SERIAL" + ".txt"))
                    {
                        File.Create(path_SERIAL + "SERIAL" + ".txt").Dispose();

                        File.WriteAllText(path_SERIAL + "SERIAL" + ".txt", String.Empty);

                        using (TextWriter tw = new StreamWriter(path_SERIAL + "SERIAL" + ".txt"))
                        {
                            tw.WriteLine(cbxPorta.SelectedItem.ToString() + ";" + cbxBaudRate.SelectedItem.ToString());
                        }
                    }
                }
                LOG_TXT("Salvando informações da porta serial - Porta:  " + cbxPorta.SelectedItem.ToString() + " Velocidade: " + cbxBaudRate.SelectedItem.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível criar o arquivo de dados da porta serial!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConectarSerial_Click(object sender, EventArgs e)
        {
            if(!PortaSerial.IsOpen)
            {
                if (cbxPorta.Items.Count > 0)
                {
                    if (cbxPorta.SelectedIndex > -1 && cbxBaudRate.SelectedItem.ToString() != string.Empty)
                    {
                        try
                        {
                            PortaSerial = new SerialPort(cbxPorta.SelectedItem.ToString(),
                            Int32.Parse(cbxBaudRate.SelectedItem.ToString()), Parity.None, 8, StopBits.One);

                            PortaSerial.Open();

                            //MessageBox.Show("Tudo certo com a porta serial selecionada!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao abrir porta serial!\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Você precisa preencher os campos de Nome da Serial e Velocidade!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Selecione uma porta serial válida!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (PortaSerial.IsOpen)
                {
                    btnConectarSerial.Text = "Desconectar";
                    cbxBaudRate.Enabled = false;
                    cbxPorta.Enabled = false;
                    btnRefreshSerial.Enabled = false;
                    Salvar_Dados_Serial();
                }
            }
            else
            {
                PortaSerial.Close();
                btnConectarSerial.Text = "Conectar";
                cbxBaudRate.Enabled = true;
                cbxPorta.Enabled = true;
                btnRefreshSerial.Enabled = true;
            }            
        }

        void LOG_TXT(String MSG)
        {
            String NOME_LOG = System.DateTime.Today.ToString("yyMMdd");

            try
            {
                if (File.Exists(path_LOG + NOME_LOG + ".txt"))
                {
                    using (var tw = new StreamWriter(path_LOG + NOME_LOG + ".txt", true))
                    {
                        tw.WriteLine(System.DateTime.Now.ToString() + "  -  " + MSG);
                    }
                }
                else
                {
                    if (!File.Exists(path_LOG + NOME_LOG + ".txt"))
                    {
                        File.Create(path_LOG + NOME_LOG + ".txt").Dispose();

                        using (TextWriter tw = new StreamWriter(path_LOG + NOME_LOG + ".txt"))
                        {
                            tw.WriteLine(System.DateTime.Now.ToString() + "  -  " + MSG);
                        }
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Não foi possível criar o arquivo de LOG!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
