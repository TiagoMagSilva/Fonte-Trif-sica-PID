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
    /*
    public struct DadosRXPID
    {
        public float _Kp;
        public float _Ki;
        public float _Kd;

        public float EstadoQuarto
        {
            get { return _Kp; }
            set { _Kp = value; }
        }

        public float CodigoQuarto
        {
            get { return _Ki; }
            set { _Ki = value; }
        }

        public float Reservado1
        {
            get { return _Kd; }
            set { _Kd = value; }
        }
    }
    */
    
    public partial class Form1 : Form
    {
        String path_SERIAL = System.AppDomain.CurrentDomain.BaseDirectory + "/SERIAL/";
        String path_LOG = System.AppDomain.CurrentDomain.BaseDirectory + "/LOG/";
        String path_Config = System.AppDomain.CurrentDomain.BaseDirectory + "/CONFIG/";

        UInt16 PassoGraficoVR = 0;
        UInt16 PassoGraficoVS = 0;
        UInt16 PassoGraficoVT = 0;
        UInt16 PassoGraficoIR = 0;
        UInt16 PassoGraficoIS = 0;
        UInt16 PassoGraficoIT = 0;

        Boolean TRiseVa = false; //detecção do tempo de subida (t rise)
        Boolean TRiseVb = false; //detecção do tempo de subida (t rise)
        Boolean TRiseVc = false; //detecção do tempo de subida (t rise)
        Boolean TRiseIa = false; //detecção do tempo de subida (t rise)
        Boolean TRiseIb = false; //detecção do tempo de subida (t rise)
        Boolean TRiseIc = false; //detecção do tempo de subida (t rise)

        float VAMaxPosTrise = -1;
        float VBMaxPosTrise = -1;
        float VCMaxPosTrise = -1;

        enum Identificador
        {
            ConstantesPIDTensão,
            ConstantesPIDCorrente,
            ParâmetrosSintetização,
            Parar,
            Iniciar,
            ID_RX_PID_Tensão,
            ID_RX_PID_Corrente,
            ID_RX_AjusteFinoV,
            ID_RX_AjusteGrossoV,
            ID_RX_AjusteFinoI,
            ID_RX_AjusteGrossoI
        };

        //DadosRXPID Constantes_PID_Tensão;
        //DadosRXPID Constantes_PID_Corrente;

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

            chartTensao.ChartAreas["ChartArea1"].BackColor = Color.DarkGray;
            chartCorrente.ChartAreas["ChartArea1"].BackColor = Color.DarkGray;

            /*
            chartTensao.Series[0].Points.AddXY(1, 2);
            chartTensao.Series[1].Points.AddXY(3, 4);
            chartTensao.Series[2].Points.AddXY(5, 6);

            chartTensao.Series[0].Points.AddXY(4, 2);
            chartTensao.Series[1].Points.AddXY(3, 5);
            chartTensao.Series[2].Points.AddXY(6, 7); */
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
                                     cbxFase.SelectedIndex + ";" + cbxFatorDePotencia.SelectedIndex + ";" + txtKp10kV.Text + ";" +
                                     txtKi10kV.Text + ";" + txtKd10kV.Text + ";" + txtKp10Corrente.Text + ";" + txtKi10Corrente.Text
                                     + ";" + txtKd10Corrente.Text);
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
                            txtKp10kV.Text = partes[11];
                            txtKi10kV.Text = partes[12];
                            txtKd10kV.Text = partes[13];
                            txtKp10Corrente.Text = partes[14];
                            txtKi10Corrente.Text = partes[15];
                            txtKd10Corrente.Text = partes[16];

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

        void IdentificarPacote(string Pacote)
        {
            /* Pacote das constantes PID_V: Identificador.ConstantesPIDTensão, kp, ki, kd
             * Pacote das constantes PID_I: Identificador.ConstantesPIDCorrente, kp, ki, kd
             * Pacote dos parametros: Identificador.ParâmetrosSintetização, Tensão, Corrente, Index frequência, Index Fase, Index Fator de Potência
             * Pacote tensão e corrente RMS: Identificador.ID_RX_PID_Tensão, VA, VB, VC, CS
             *                               Identificador.ID_RX_PID_Corrente, IA, IB, IC, CS                                                                    S                 
             */
            string[] partes = Pacote.Split(',');
            int Fase;

            if(partes.Length > 0)
            {
                switch(int.Parse(partes[0]))//Partes 0 contém o identificador!!
                {
                    case (int)Identificador.ConstantesPIDTensão:
                        txtKpTensao.Invoke(new Action(() =>
                        {
                            txtKpTensao.Text = partes[1];
                        }));
                        txtKiTensao.Invoke(new Action(() =>
                        {
                            txtKiTensao.Text = partes[2];
                        }));
                        txtKdTensao.Invoke(new Action(() =>
                        {
                            txtKdTensao.Text = partes[3];
                        }));    
                        break;
                    case (int)Identificador.ConstantesPIDCorrente:
                        txtKpCorrente.Text = partes[1];
                        txtKiCorrente.Text = partes[2];
                        txtKdCorrente.Text = partes[3];
                        break;
                    case (int)Identificador.ParâmetrosSintetização:
                        txtTensãoRMS.Text = partes[1];
                        txtCorrenteRMS.Text = partes[2];
                        cbxFrequencia.SelectedIndex = int.Parse(partes[3]);
                        cbxFase.SelectedIndex = int.Parse(partes[4]);
                        cbxFatorDePotencia.SelectedIndex = int.Parse(partes[5]);
                        break;
                    case (int)Identificador.ID_RX_PID_Tensão:
                        if(cbxGraficoVA.Checked)
                        {
                            txtTensãoRMSA.Invoke(new Action(() =>
                            {
                                txtTensãoRMSA.Text = partes[1];
                            }));
                            chartTensao.Invoke(new Action(() =>
                            {
                                chartTensao.Series[0].Points.AddXY(PassoGraficoVR, double.Parse(partes[1]) / 100);
                                PassoGraficoVR++;
                            }));

                            txtTempAcomodTensaoA.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[1]) / 100 > int.Parse(txtTensãoRMS.Text) && !TRiseVa)
                                {
                                    txtTempAcomodTensaoA.Text = (PassoGraficoVR * 0.2).ToString() + "s";
                                    TRiseVa = true;
                                }
                            }));

                            txtOvershootTensaoA.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[1]) / 100 > double.Parse(txtOvershootTensaoA.Text))
                                {
                                    txtOvershootTensaoA.Text = (double.Parse(partes[1]) / 100).ToString();
                                }
                            }));

                            txtUndershootTensaoA.Invoke(new Action(() =>
                            {
                                if (TRiseVa)
                                {
                                    if (float.Parse(txtUndershootTensaoA.Text) > float.Parse(partes[1]) / 100 || float.Parse(txtUndershootTensaoA.Text) == -1)
                                    {
                                        txtUndershootTensaoA.Text = (float.Parse(partes[1]) / 100).ToString();
                                    }
                                }
                            }));

                            txtDifVa.Invoke(new Action(() => 
                            {
                                if(TRiseVa)
                                {
                                    if(float.Parse(partes[1]) / 100 > VAMaxPosTrise)
                                    {
                                        VAMaxPosTrise = float.Parse(partes[1]) / 100;
                                    }
                                    txtDifVa.Text = (VAMaxPosTrise - float.Parse(txtUndershootTensaoA.Text)).ToString("0.00");
                                    txtDifVa.Text += "/" + (((VAMaxPosTrise - float.Parse(txtUndershootTensaoA.Text))/float.Parse(txtTensãoRMS.Text))*100).ToString("0.00");
                                }
                                
                            }));
                        }
                        
                        if (cbxGraficoVB.Checked)
                        {
                            txtTensãoRMSB.Invoke(new Action(() =>
                            {
                                txtTensãoRMSB.Text = partes[2];
                            }));
                            chartTensao.Invoke(new Action(() =>
                            {
                                chartTensao.Series[1].Points.AddXY(PassoGraficoVS, double.Parse(partes[2]) / 100);
                                PassoGraficoVS++;
                            }));

                            txtTempAcomodTensaoB.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[2]) / 100 > int.Parse(txtTensãoRMS.Text) && !TRiseVb)
                                {
                                    txtTempAcomodTensaoB.Text = (PassoGraficoVS * 0.2).ToString() + "s";
                                    TRiseVb = true;
                                }
                            }));

                            txtOvershootTensaoB.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[2]) / 100 > double.Parse(txtOvershootTensaoB.Text))
                                {
                                    txtOvershootTensaoB.Text = (double.Parse(partes[2]) / 100).ToString();
                                }
                            }));

                            txtUndershootTensaoB.Invoke(new Action(() =>
                            {
                                if (TRiseVb)
                                {
                                    if (float.Parse(txtUndershootTensaoB.Text) > float.Parse(partes[1]) / 100 || float.Parse(txtUndershootTensaoB.Text) == -1)
                                    {
                                        txtUndershootTensaoB.Text = (float.Parse(partes[1]) / 100).ToString();
                                    }
                                }
                            }));

                            txtDifVb.Invoke(new Action(() =>
                            {
                                if (TRiseVb)
                                {
                                    if (float.Parse(partes[1]) / 100 > VBMaxPosTrise)
                                    {
                                        VBMaxPosTrise = float.Parse(partes[1]) / 100;
                                    }
                                    txtDifVb.Text = (VBMaxPosTrise - float.Parse(txtUndershootTensaoB.Text)).ToString("0.00");
                                    txtDifVb.Text += "/" + (((VBMaxPosTrise - float.Parse(txtUndershootTensaoB.Text)) / float.Parse(txtTensãoRMS.Text)) * 100).ToString("0.00");
                                }

                            }));
                        }

                        if (cbxGraficoVC.Checked)
                        {
                            txtTensãoRMSC.Invoke(new Action(() =>
                            {
                                txtTensãoRMSC.Text = partes[3];
                            }));
                            chartTensao.Invoke(new Action(() =>
                            {
                                chartTensao.Series[2].Points.AddXY(PassoGraficoVT, double.Parse(partes[3]) / 100);
                                PassoGraficoVT++;
                            }));

                            txtTempAcomodTensaoC.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[3]) / 100 > int.Parse(txtTensãoRMS.Text) && !TRiseVc)
                                {
                                    txtTempAcomodTensaoC.Text = (PassoGraficoVT * 0.2).ToString() + "s";
                                    TRiseVc = true;
                                }
                            }));

                            txtOvershootTensaoC.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[3]) / 100 > double.Parse(txtOvershootTensaoC.Text))
                                {
                                    txtOvershootTensaoC.Text = (double.Parse(partes[3]) / 100).ToString();
                                }
                            }));

                            txtUndershootTensaoC.Invoke(new Action(() =>
                            {
                                if (TRiseVc)
                                {
                                    if (float.Parse(txtUndershootTensaoC.Text) > float.Parse(partes[1]) / 100 || float.Parse(txtUndershootTensaoC.Text) == -1)
                                    {
                                        txtUndershootTensaoC.Text = (float.Parse(partes[1]) / 100).ToString();
                                    }
                                }
                            }));

                            txtDifVc.Invoke(new Action(() =>
                            {
                                if (TRiseVc)
                                {
                                    if (float.Parse(partes[1]) / 100 > VCMaxPosTrise)
                                    {
                                        VCMaxPosTrise = float.Parse(partes[1]) / 100;
                                    }
                                    txtDifVc.Text = (VCMaxPosTrise - float.Parse(txtUndershootTensaoC.Text)).ToString("0.00");
                                    txtDifVc.Text += "/" + (((VCMaxPosTrise - float.Parse(txtUndershootTensaoC.Text)) / float.Parse(txtTensãoRMS.Text)) * 100).ToString("0.00");
                                }

                            }));
                        }
                        break;
                    case (int)Identificador.ID_RX_PID_Corrente:
                        if (cbxGraficoIA.Checked)
                        {
                            txtCorrenteRMSA.Invoke(new Action(() =>
                            {
                                txtCorrenteRMSA.Text = partes[1];
                            }));
                            chartCorrente.Invoke(new Action(() =>
                            {
                                chartCorrente.Series[0].Points.AddXY(PassoGraficoIR, double.Parse(partes[1]) / 100);
                                PassoGraficoIR++;
                            }));

                            txtTempAcomodCorrenteA.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[1]) / 100 > int.Parse(txtCorrenteRMS.Text) && !TRiseIa)
                                {
                                    txtTempAcomodCorrenteA.Text = (PassoGraficoIR * 0.2).ToString() + "s";
                                    TRiseIa = true;
                                }
                            }));

                            txtOvershootCorrenteA.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[1]) / 100 > double.Parse(txtOvershootCorrenteA.Text))
                                {
                                    txtOvershootCorrenteA.Text = (double.Parse(partes[1]) / 100).ToString();
                                }
                            }));

                            txtUnderShootCorrenteA.Invoke(new Action(() =>
                            {
                                if (TRiseIa)
                                {
                                    if (float.Parse(txtUnderShootCorrenteA.Text) > float.Parse(partes[1]) / 100 || float.Parse(txtUnderShootCorrenteA.Text) == -1)
                                    {
                                        txtUnderShootCorrenteA.Text = (float.Parse(partes[1]) / 100).ToString();
                                    }
                                }
                            }));
                        }

                        if (cbxGraficoIB.Checked)
                        {
                            txtCorrenteRMSB.Invoke(new Action(() =>
                            {
                                txtCorrenteRMSB.Text = partes[2];
                            }));
                            chartCorrente.Invoke(new Action(() =>
                            {
                                chartCorrente.Series[1].Points.AddXY(PassoGraficoIS, double.Parse(partes[2]) / 100);
                                PassoGraficoIS++;
                            }));

                            txtTempAcomodCorrenteB.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[2]) / 100 > int.Parse(txtCorrenteRMS.Text) && !TRiseIb)
                                {
                                    txtTempAcomodCorrenteB.Text = (PassoGraficoIS * 02).ToString() + "s";
                                    TRiseIb = true;
                                }
                            }));

                            txtOvershootCorrenteB.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[2]) / 100 > double.Parse(txtOvershootCorrenteB.Text))
                                {
                                    txtOvershootCorrenteB.Text = (double.Parse(partes[2]) / 100).ToString();
                                }
                            }));

                            txtUnderShootCorrenteB.Invoke(new Action(() =>
                            {
                                if (TRiseIb)
                                {
                                    if (float.Parse(txtUnderShootCorrenteB.Text) > float.Parse(partes[1]) / 100 || float.Parse(txtUnderShootCorrenteB.Text) == -1)
                                    {
                                        txtUnderShootCorrenteB.Text = (float.Parse(partes[1]) / 100).ToString();
                                    }
                                }
                            }));
                        }

                        if (cbxGraficoIC.Checked)
                        {
                            txtCorrenteRMSC.Invoke(new Action(() =>
                            {
                                txtCorrenteRMSC.Text = partes[3];
                            }));
                            chartCorrente.Invoke(new Action(() =>
                            {
                                chartCorrente.Series[2].Points.AddXY(PassoGraficoIT, double.Parse(partes[3]) / 100);
                                PassoGraficoIT++;
                            }));

                            txtTempAcomodCorrenteC.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[3]) / 100 > int.Parse(txtCorrenteRMS.Text) && !TRiseIc)
                                {
                                    txtTempAcomodCorrenteC.Text = (PassoGraficoIT * 0.2).ToString() + "s";
                                    TRiseIc = true;
                                }
                            }));

                            txtOvershootCorrenteC.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[3]) / 100 > double.Parse(txtOvershootCorrenteC.Text))
                                {
                                    txtOvershootCorrenteC.Text = (double.Parse(partes[3]) / 100).ToString();
                                }
                            }));

                            txtUnderShootCorrenteC.Invoke(new Action(() =>
                            {
                                if (TRiseIc)
                                {
                                    if (float.Parse(txtUnderShootCorrenteC.Text) > float.Parse(partes[1]) / 100 || float.Parse(txtUnderShootCorrenteC.Text) == -1)
                                    {
                                        txtUnderShootCorrenteC.Text = (float.Parse(partes[1]) / 100).ToString();
                                    }
                                }
                            }));
                        }
                        break;
                    case (int)Identificador.ID_RX_AjusteFinoV:
                        Fase = int.Parse(partes[1]);
                        switch(Fase)
                        {
                            case 1://Fase R
                                
                                break;
                            case 2: //Fase S
                                chartTensao.Invoke(new Action(() =>
                                {
                                    chartTensao.Series[4].Points.AddXY(PassoGraficoVS, double.Parse(partes[2]) / 100);
                                }));
                                break;
                            case 3: //Fase T

                                break;

                            default:

                                break;
                        }
                        break;
                    case (int)Identificador.ID_RX_AjusteGrossoV:
                        Fase = int.Parse(partes[1]);
                        switch (Fase)
                        {
                            case 1://Fase R

                                break;
                            case 2: //Fase S
                                chartTensao.Invoke(new Action(() =>
                                {
                                    chartTensao.Series[7].Points.AddXY(PassoGraficoVS, double.Parse(partes[2]) / 100);
                                }));
                                break;
                            case 3: //Fase T

                                break;

                            default:

                                break;
                        }
                        break;
                    case (int)Identificador.ID_RX_AjusteFinoI:
                        Fase = int.Parse(partes[1]);
                        switch (Fase)
                        {
                            case 1://Fase R

                                break;
                            case 2: //Fase S
                                chartCorrente.Invoke(new Action(() =>
                                {
                                    chartCorrente.Series[4].Points.AddXY(PassoGraficoIS, double.Parse(partes[2]) / 100);
                                }));
                                break;
                            case 3: //Fase T

                                break;

                            default:

                                break;
                        }
                        break;
                    case (int)Identificador.ID_RX_AjusteGrossoI:
                        Fase = int.Parse(partes[1]);
                        switch (Fase)
                        {
                            case 1://Fase R

                                break;
                            case 2: //Fase S
                                chartCorrente.Invoke(new Action(() =>
                                {
                                    chartCorrente.Series[7].Points.AddXY(PassoGraficoIS, double.Parse(partes[2]) / 100);
                                }));
                                break;
                            case 3: //Fase T

                                break;

                            default:

                                break;
                        }
                        break;
                    default:

                        break;
                }
            }
        }

        private void PortaSerial_DadoRecebido(object sender, SerialDataReceivedEventArgs e)
        {
            string DadoRecebido = PortaSerial.ReadLine();
            Console.WriteLine("Dado recebido: " + DadoRecebido);
            LOG_TXT("Dado Recebido: " + DadoRecebido);

            int IndexVirgulaCS = DadoRecebido.LastIndexOf(',');
            string TramaSemCS = string.Empty;
            string CS = string.Empty;

            if (IndexVirgulaCS != -1)
            {
                TramaSemCS = DadoRecebido.Substring(0, IndexVirgulaCS) + ",";
                CS = DadoRecebido.Substring(IndexVirgulaCS + 1, 2);                

                LOG_TXT("Trama Recebida sem CS: " + TramaSemCS);
                LOG_TXT("CS da Trama recebida: " + CS);

                if (Calcula_checksum(TramaSemCS) == CS)
                {                    
                    IdentificarPacote(TramaSemCS);
                }
                else
                {
                    LOG_TXT("Checsum da trama recebida estava incorreto!");
                    LOG_TXT("Calc: |" + Calcula_checksum(TramaSemCS) + "| " + "Recebido: |" + CS + "|");
                }
            }
            else
            {
                LOG_TXT("A trama recebida não continha ','");
            }

            //string DadoRecebidoSemCS = 
            //IdentificarPacote(DadoRecebido);
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

                            PortaSerial.DataReceived += new SerialDataReceivedEventHandler(PortaSerial_DadoRecebido);

                            LOG_TXT("Porta serial aberta com sucesso!");

                            //MessageBox.Show("Tudo certo com a porta serial selecionada!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch (Exception ex)
                        {
                            LOG_TXT("Erro ao abrir porta serial!");
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
                                 txtKdTensao.Text + "," +
                                 txtKp10kV.Text + "," +
                                 txtKi10kV.Text + "," +
                                 txtKd10kV.Text + ",";

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
                                 txtKdCorrente.Text + "," +
                                 txtKp10Corrente.Text + "," +
                                 txtKi10Corrente.Text + "," +
                                 txtKd10Corrente.Text + ",";

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
            //Trama de aplicação ddos parâmetros de sintetização
            //Identificador, tensão, corrente, frequencia, fase, fator de potencia
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

            TRiseVa = false;

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

        private void btnLimparGraficoTensao_Click(object sender, EventArgs e)
        {
            PassoGraficoVR = 0;
            PassoGraficoVS = 0;
            PassoGraficoVT = 0;
            chartTensao.Series[0].Points.Clear();
            chartTensao.Series[1].Points.Clear();
            chartTensao.Series[2].Points.Clear();
            chartTensao.Series[3].Points.Clear();
            chartTensao.Series[4].Points.Clear();
            chartTensao.Series[5].Points.Clear();
            chartTensao.Series[6].Points.Clear();
            chartTensao.Series[7].Points.Clear();
            chartTensao.Series[8].Points.Clear();

            TRiseVa = false;
            TRiseVb = false;
            TRiseVc = false;
            txtTempAcomodTensaoA.Text = "";
            txtOvershootTensaoA.Text = "0";

            txtTempAcomodTensaoB.Text = "";
            txtOvershootTensaoB.Text = "0";

            txtTempAcomodTensaoC.Text = "";
            txtOvershootTensaoC.Text = "0";

            txtUndershootTensaoA.Text = "-1";
            txtUndershootTensaoB.Text = "-1";
            txtUndershootTensaoC.Text = "-1";

            VAMaxPosTrise = 0;
            VBMaxPosTrise = 0;
            VCMaxPosTrise = 0;
        }

        private void btnLimparGraficoCorrente_Click(object sender, EventArgs e)
        {
            PassoGraficoIR = 0;
            PassoGraficoIS = 0;
            PassoGraficoIT = 0;
            chartCorrente.Series[0].Points.Clear();
            chartCorrente.Series[1].Points.Clear();
            chartCorrente.Series[2].Points.Clear();
            chartCorrente.Series[3].Points.Clear();
            chartCorrente.Series[4].Points.Clear();
            chartCorrente.Series[5].Points.Clear();
            chartCorrente.Series[6].Points.Clear();
            chartCorrente.Series[7].Points.Clear();
            chartCorrente.Series[8].Points.Clear();

            TRiseIa = false;
            TRiseIb = false;
            TRiseIc = false;
            txtTempAcomodCorrenteA.Text = "";
            txtOvershootCorrenteA.Text = "0";

            txtTempAcomodCorrenteB.Text = "";
            txtOvershootCorrenteB.Text = "0";

            txtTempAcomodCorrenteC.Text = "";
            txtOvershootCorrenteC.Text = "0";

            txtUnderShootCorrenteA.Text = "-1";
            txtUnderShootCorrenteB.Text = "-1";
            txtUnderShootCorrenteC.Text = "-1";
        }

        private void txtKp10kV_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtKi10kV_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtKd10kV_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTensãoRMSA_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTensãoRMSB_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTensãoRMSC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtKp10Corrente_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtKp10Corrente_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtKi10Corrente_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtKd10Corrente_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
