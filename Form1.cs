﻿using System;
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
        String path_Config = System.AppDomain.CurrentDomain.BaseDirectory + "/CONFIG/";

        enum Identificador
        {
            ConstantesPIDTensão,
            ConstantesPIDCorrente,
            ParâmetrosSintetização,
            Parar,
            Iniciar
        };

        private SerialPort PortaSerial = new SerialPort();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizarPortas();
            LerInformacoesSerialSalva();
            LerInformacoesConfig();

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

        void Salvar_Dados_Config()
        {
            try
            {
                if (File.Exists(path_Config + "CONFIG" + ".txt"))
                {
                    File.WriteAllText(path_Config + "CONFIG" + ".txt", String.Empty);

                    using (var tw = new StreamWriter(path_Config + "CONFIG" + ".txt", true))
                    {
                        tw.WriteLine(txtKpTensao.Text + ";" + txtKiTensao.Text + ";" + txtKdTensao.Text + ";" +
                                     txtKpCorrente.Text + ";" + txtKiCorrente.Text + ";" + txtKdCorrente.Text + ";" +
                                     txtTensãoRMS.Text + ";" + txtCorrenteRMS.Text + ";" + cbxFrequencia.SelectedIndex + ";" +
                                     cbxFase.SelectedIndex + ";" + cbxFatorDePotencia.SelectedIndex);
                    }
                }
                else
                {
                    if (!File.Exists(path_Config + "CONFIG" + ".txt"))
                    {
                        File.Create(path_Config + "CONFIG" + ".txt").Dispose();

                        File.WriteAllText(path_Config + "CONFIG" + ".txt", String.Empty);

                        using (TextWriter tw = new StreamWriter(path_Config + "CONFIG" + ".txt"))
                        {
                            tw.WriteLine(txtKpTensao.Text + ";" + txtKiTensao.Text + ";" + txtKdTensao.Text + ";" +
                                     txtKpCorrente.Text + ";" + txtKiCorrente.Text + ";" + txtKdCorrente.Text + ";" +
                                     txtTensãoRMS.Text + ";" + txtCorrenteRMS.Text + ";" + cbxFrequencia.SelectedIndex + ";" +
                                     cbxFase.SelectedIndex + ";" + cbxFatorDePotencia.SelectedIndex);
                        }
                    }
                }
                LOG_TXT("Salvando informações de configuração (PID):  " + txtKpTensao.Text + ";" + txtKiTensao.Text + ";" + txtKdTensao.Text + ";" +
                                                                          txtKpCorrente.Text + ";" + txtKiCorrente.Text + ";" + txtKdCorrente.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível criar o arquivo de configuração!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LerInformacoesConfig()
        {
            if (File.Exists(path_Config + "CONFIG" + ".txt"))
            {
                if (File.ReadLines(path_Config + "CONFIG" + ".txt").Count() > 0)
                {
                    string[] LinhasDoTXT;
                    LinhasDoTXT = File.ReadAllLines(path_Config + "CONFIG" + ".txt");

                    foreach (string line in LinhasDoTXT)
                    {
                        try
                        {
                            string[] partes = line.Split(';');
                            txtKpTensao.Text = partes[0];
                            txtKiTensao.Text = partes[1];
                            txtKdTensao.Text = partes[2];
                            txtKpCorrente.Text = partes[3];
                            txtKiCorrente.Text = partes[4];
                            txtKdCorrente.Text = partes[5];
                            txtTensãoRMS.Text = partes[6];
                            txtCorrenteRMS.Text = partes[7];
                            cbxFrequencia.SelectedIndex = Int16.Parse(partes[8]);
                            cbxFase.SelectedIndex = Int16.Parse(partes[9]);
                            cbxFatorDePotencia.SelectedIndex = Int16.Parse(partes[10]);

                            LOG_TXT("Arquivo de configuração carregado com sucesso!");
                        }
                        catch (Exception ex)
                        {
                            LOG_TXT("Erro ao carregar arquivo de config: " + ex.Message);
                            MessageBox.Show("Erro ao carregar arquivo de config\r\n\r\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                    }
                }
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

        //Esta função recebe a trama de entrada COM a última vírgula
        //e retorna apenas o checksum
        //Para usar:
        /*
        String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
        PORTASERIAL.Write(TramaComChecksum + "\0");
        */
        string Calcula_checksum(string vetor)
        {
            string resposta = string.Empty;
            string checksum = string.Empty;
            //string CKremovido = string.Empty;
            //string[] pt = vetor.Split(',');        

            int DV = 0;

            for (int i = 0; i < vetor.Length; i++)
            {
                DV ^= vetor[i];
            }

            string hexValue = DV.ToString("x");
            resposta = hexValue;

            if (hexValue.Length == 1)
                resposta = "0" + hexValue[0];
            if (hexValue.Length == 3)
                resposta = "0" + hexValue[2];
            if (hexValue.Length == 4)
                resposta = (hexValue[2] + hexValue[4]).ToString();

            return resposta;
        }

        private void btnAplicarPIDTensao_Click(object sender, EventArgs e)
        {
            //Trama de aplicação das constatnes KP, KI e KD
            //Identificador, Kp, Ki, Kd, CS
            Salvar_Dados_Config();
           

            string TRAMA_ENVIO = (int)Identificador.ConstantesPIDTensão + "," +
                                 txtKpTensao.Text + "," +
                                 txtKiTensao.Text + "," +
                                 txtKdTensao.Text + ",";

            if(PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando PID_V: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de configuração PID para tensão não enviado devido porta serial fechada!");
            }
        }

        private void txtKpTensao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtKiTensao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtKdTensao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtKpCorrente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtKiCorrente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtKdCorrente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnAplicarPIDCorrente_Click(object sender, EventArgs e)
        {
            //Trama de aplicação das constatnes KP, KI e KD
            //Identificador, Kp, Ki, Kd, CS
            Salvar_Dados_Config();

            string TRAMA_ENVIO = (int)Identificador.ConstantesPIDCorrente + "," +
                                 txtKpCorrente.Text + "," +
                                 txtKiCorrente.Text + "," +
                                 txtKdCorrente.Text + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando PID_C: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de configuração PID para corrente não enviado devido porta serial fechada!");
            }
        }

        private void btnAplicarParametros_Click(object sender, EventArgs e)
        {
            //Trama de aplicação das constatnes KP, KI e KD
            //Identificador, Kp, Ki, Kd, CS
            Salvar_Dados_Config();

            string TRAMA_ENVIO = (int)Identificador.ParâmetrosSintetização + "," +
                                 txtTensãoRMS.Text + "," +
                                 txtCorrenteRMS.Text + "," +
                                 cbxFrequencia.SelectedIndex + "," + // 0 - 50Hz; 1 - 60Hz
                                 cbxFase.SelectedIndex + "," + // 0 - 0º; 1 - 120º
                                 cbxFatorDePotencia.SelectedIndex + ","; // 0 - 1.0; 1 - 0,5L; 2 - 0,5C; 3 - 0,8L; 4 - 0,8C

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Parametros Sint.: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de Parâmetros de sintetização não enviado devido porta serial fechada!");
            }
        }

        private void btnPararSintetização_Click(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = (int)Identificador.Parar + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Parar Sint.: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando para parar sintetização não enviado devido porta serial fechada!");
            }
        }

        private void btnIniciarSintetização_Click(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = (int)Identificador.Iniciar + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Iniciar Sint.: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando para iniciar sintetização não enviado devido porta serial fechada!");
            }
        }
    }
}
