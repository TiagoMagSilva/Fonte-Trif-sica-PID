using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        float AnguloFasorV1 = 0;
        float AnguloFasorV2 = 0;
        float AnguloFasorV3 = 0;
        float AnguloFasorI1 = 0;
        float AnguloFasorI2 = 0;
        float AnguloFasorI3 = 0;

        double AnguloIA;
        double AnguloIB;
        double AnguloIC;

        double TempoI1;
        double TempoI2;
        double TempoI3;

        float Frequencia;
        float Periodo;

        float FPI1Signed;
        float FPI2Signed;
        float FPI3Signed;

        float FaseVAB;
        float FaseVAC;

        bool PrintPhasor = false;

        UInt16 PassoGraficoVR = 0;
        UInt16 PassoGraficoVS = 0;
        UInt16 PassoGraficoVT = 0;
        UInt16 PassoGraficoIR = 0;
        UInt16 PassoGraficoIS = 0;
        UInt16 PassoGraficoIT = 0;
        UInt16 PassoGraficoFases = 0;

        Boolean TRiseVa = false; //detecção do tempo de subida (t rise)
        Boolean TRiseVb = false; //detecção do tempo de subida (t rise)
        Boolean TRiseVc = false; //detecção do tempo de subida (t rise)
        Boolean TRiseIa = false; //detecção do tempo de subida (t rise)
        Boolean TRiseIb = false; //detecção do tempo de subida (t rise)
        Boolean TRiseIc = false; //detecção do tempo de subida (t rise)

        float VAMaxPosTrise = -1;
        float VBMaxPosTrise = -1;
        float VCMaxPosTrise = -1;

        int AtualizaçãoVR_IR = 3;

        static string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        static string strWorkPath = Path.GetDirectoryName(strExeFilePath);

        Image ImagemAdianta = Image.FromFile(strWorkPath + @"\IMAGEM\SetaAdianta1.png");
        Image ImagemAtrasa = Image.FromFile(strWorkPath + @"\IMAGEM\SetaAtrasa1.png");        

        enum SM_LOGRequest_Members 
        { 
            SolicitaErrosDeSaida,
            SolicitaErrosDeComunicacao,
            SolicitaEventos,
            SolicitaTemporizacoes,
            SolicitaDadosUnicos,
            SemSolicitacoesDeLOG
        };

        enum SM_LOGStatus_Members
        {
            AguardandoLOG_ErrosDeSaida,
            AguardadndoLOG_ErrosDeComunicacao,
            AguardandoLOG_Eventos,
            AguardandoLOG_Temporizacoes,
            AguardadndoLOG_DadosUnicos,
            TodosOsLOGsRecebidos
        };

        int TentativasMAX = 2;
        int LOG_CounterLimit = 0;
        SM_LOGStatus_Members SM_LOGStatus = SM_LOGStatus_Members.AguardandoLOG_ErrosDeSaida; //máquina de estados para as respostas recebidas dos LOGs
        SM_LOGRequest_Members SM_LOGRequest = SM_LOGRequest_Members.SolicitaErrosDeSaida; //Máquina de estados para solicitação dos pacotes de informação de LOGs

        enum Identificador
        {
            ID_ConstantesPIDTensao,
            ID_ConstantesPIDCorrente,
            ID_ParametrosSintetizacao,
            ID_Parar,
            ID_Iniciar,
            ID_TX_PID_Tensao,
            ID_TX_PID_Corrente,
            ID_TX_AjusteFinoV,
            ID_TX_AjusteGrossoV,
            ID_TX_AjusteFinoI,
            ID_TX_AjsuteGrossoI,
            ID_Aplicar_DigPot_10k_Tensao,
            ID_Aplicar_DigPot_50k_Tensao,
            ID_Aplicar_DigPot_10k_Corrente,
            ID_Aplicar_DigPot_50k_Corrente,
            ID_TX_Mensagem_ERRO_AppRodando,
            ID_RESET_ESP32,
            ID_RESET_ADE,
            ID_RECONFIG_ADE,
            ID_Aplicar_FP160,
            ID_Aplicar_FPInd60,
            ID_Aplicar_FPCap60,
            ID_Aplicar_FP150,
            ID_Aplicar_FPInd50,
            ID_Aplicar_FPCap50,
            ID_Ler_FP_60,
            ID_Ler_FP_50,
            ID_Dir_Rev,
            ID_Correcao_Fase,
            ID_ConstantesPIDFases,
            ID_FaseManual,
            ID_SentidoCorrecao,
            ID_AjustarADE,
            ID_LOG_ErrosDeSaida,
            ID_LOG_ErrosDeComunicacao,
            ID_LOG_Eventos,
            ID_LOG_Temporizacoes,
            ID_LOG_DadosUnicos,
            ID_RESET_LOGs,
            ID_AtualizarDadosUnicos
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
                                     + ";" + txtKd10Corrente.Text + ";" + txtKpFases.Text + ";" + txtKiFases.Text + ";" + txtKdFases.Text);
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
                            txtKpFases.Text = partes[17];
                            txtKiFases.Text = partes[18];
                            txtKdFases.Text = partes[19];

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
                    #region Constantes PID Tensão
                    case (int)Identificador.ID_ConstantesPIDTensao:
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
                    #endregion
                    #region Constantes PID Corrente
                    case (int)Identificador.ID_ConstantesPIDCorrente:
                        txtKpCorrente.Invoke(new Action(() => 
                        {
                            txtKpCorrente.Text = partes[1];
                            txtKiCorrente.Text = partes[2];
                            txtKdCorrente.Text = partes[3];
                        }));                     
                        
                        break;
                        #endregion
                    #region Parametros Sintetização
                    case (int)Identificador.ID_ParametrosSintetizacao:
                        txtTensãoRMS.Text = partes[1];
                        txtCorrenteRMS.Text = partes[2];
                        cbxFrequencia.SelectedIndex = int.Parse(partes[3]);
                        cbxFase.SelectedIndex = int.Parse(partes[4]);
                        cbxFatorDePotencia.SelectedIndex = int.Parse(partes[5]);
                        break;
                    #endregion
                    #region TX PID Tensão
                    case (int)Identificador.ID_TX_PID_Tensao:
                        if(cbxGraficoVA.Checked)
                        {
                            txtTensãoRMSA.Invoke(new Action(() =>
                            {
                                if(PassoGraficoVR % AtualizaçãoVR_IR == 0)
                                    txtTensaoRMSVA.Text = txtTensãoRMSA.Text = partes[1];
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
                                if (PassoGraficoVS % AtualizaçãoVR_IR == 0)
                                    txtTensaoRMSVB.Text = txtTensãoRMSB.Text = partes[2];
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
                                if (PassoGraficoVT % AtualizaçãoVR_IR == 0)
                                    txtTensaoRMSVC.Text = txtTensãoRMSC.Text = partes[3];
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
                    #endregion
                    #region TX PID Corrente
                    case (int)Identificador.ID_TX_PID_Corrente:
                        if (cbxGraficoIA.Checked)
                        {
                            txtCorrenteRMSA.Invoke(new Action(() =>
                            {
                                if (PassoGraficoIR % AtualizaçãoVR_IR == 0)
                                    txtCorrenteRMSIA.Text = txtCorrenteRMSA.Text = partes[1];
                            }));
                            chartCorrente.Invoke(new Action(() =>
                            {
                                chartCorrente.Series[0].Points.AddXY(PassoGraficoIR, double.Parse(partes[1]) / 1000);
                                PassoGraficoIR++;
                            }));

                            txtTempAcomodCorrenteA.Invoke(new Action(() =>
                            {
                                /*
                                if (double.Parse(partes[1]) / 1000 > int.Parse(txtCorrenteRMS.Text) && !TRiseIa)
                                {
                                    txtTempAcomodCorrenteA.Text = (PassoGraficoIR * 0.2).ToString() + "s";
                                    TRiseIa = true;
                                }*/
                            }));

                            txtOvershootCorrenteA.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[1]) / 1000 > double.Parse(txtOvershootCorrenteA.Text))
                                {
                                    txtOvershootCorrenteA.Text = (double.Parse(partes[1]) / 1000).ToString();
                                }
                            }));

                            txtUnderShootCorrenteA.Invoke(new Action(() =>
                            {
                                if (TRiseIa)
                                {
                                    if (float.Parse(txtUnderShootCorrenteA.Text) > float.Parse(partes[1]) / 1000 || float.Parse(txtUnderShootCorrenteA.Text) == -1)
                                    {
                                        txtUnderShootCorrenteA.Text = (float.Parse(partes[1]) / 1000).ToString();
                                    }
                                }
                            }));
                        }

                        if (cbxGraficoIB.Checked)
                        {
                            txtCorrenteRMSB.Invoke(new Action(() =>
                            {
                                if (PassoGraficoIS % AtualizaçãoVR_IR == 0)
                                    txtCorrenteRMSIB.Text = txtCorrenteRMSB.Text = partes[2];
                            }));
                            chartCorrente.Invoke(new Action(() =>
                            {
                                chartCorrente.Series[1].Points.AddXY(PassoGraficoIS, double.Parse(partes[2]) / 1000);
                                PassoGraficoIS++;
                            }));

                            txtTempAcomodCorrenteB.Invoke(new Action(() =>
                            {
                                /*
                                if (double.Parse(partes[2]) / 1000 > int.Parse(txtCorrenteRMS.Text) && !TRiseIb)
                                {
                                    txtTempAcomodCorrenteB.Text = (PassoGraficoIS * 02).ToString() + "s";
                                    TRiseIb = true;
                                }*/
                            }));

                            txtOvershootCorrenteB.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[2]) / 1000 > double.Parse(txtOvershootCorrenteB.Text))
                                {
                                    txtOvershootCorrenteB.Text = (double.Parse(partes[2]) / 1000).ToString();
                                }
                            }));

                            txtUnderShootCorrenteB.Invoke(new Action(() =>
                            {
                                if (TRiseIb)
                                {
                                    if (float.Parse(txtUnderShootCorrenteB.Text) > float.Parse(partes[1]) / 1000 || float.Parse(txtUnderShootCorrenteB.Text) == -1)
                                    {
                                        txtUnderShootCorrenteB.Text = (float.Parse(partes[1]) / 1000).ToString();
                                    }
                                }
                            }));
                        }

                        if (cbxGraficoIC.Checked)
                        {
                            txtCorrenteRMSC.Invoke(new Action(() =>
                            {
                                if (PassoGraficoIT % AtualizaçãoVR_IR == 0)
                                    txtCorrenteRMSIC.Text = txtCorrenteRMSC.Text = partes[3];
                            }));
                            chartCorrente.Invoke(new Action(() =>
                            {
                                chartCorrente.Series[2].Points.AddXY(PassoGraficoIT, double.Parse(partes[3]) / 1000);
                                PassoGraficoIT++;
                            }));

                            txtTempAcomodCorrenteC.Invoke(new Action(() =>
                            {/*
                                if (double.Parse(partes[3]) / 1000 > int.Parse(txtCorrenteRMS.Text) && !TRiseIc)
                                {
                                    txtTempAcomodCorrenteC.Text = (PassoGraficoIT * 0.2).ToString() + "s";
                                    TRiseIc = true;
                                }*/
                            }));

                            txtOvershootCorrenteC.Invoke(new Action(() =>
                            {
                                if (double.Parse(partes[3]) / 1000 > double.Parse(txtOvershootCorrenteC.Text))
                                {
                                    txtOvershootCorrenteC.Text = (double.Parse(partes[3]) / 1000).ToString();
                                }
                            }));

                            txtUnderShootCorrenteC.Invoke(new Action(() =>
                            {
                                if (TRiseIc)
                                {
                                    if (float.Parse(txtUnderShootCorrenteC.Text) > float.Parse(partes[1]) / 1000 || float.Parse(txtUnderShootCorrenteC.Text) == -1)
                                    {
                                        txtUnderShootCorrenteC.Text = (float.Parse(partes[1]) / 1000).ToString();
                                    }
                                }
                            }));
                        }
                        break;
                    #endregion
                    #region Ajuste Fino V
                    case (int)Identificador.ID_TX_AjusteFinoV:
                        Fase = int.Parse(partes[1]);
                        switch(Fase)
                        {
                            case 1://Fase R
                                chartTensao.Invoke(new Action(() =>
                                {
                                    if(cbxGraficoVA.Checked)
                                    {
                                        chartTensao.Series[3].Points.AddXY(PassoGraficoVR, double.Parse(partes[2]) / 100);
                                    }                                    
                                }));
                                break;
                            case 2: //Fase S
                                chartTensao.Invoke(new Action(() =>
                                {
                                    if (cbxGraficoVB.Checked)
                                    {
                                        chartTensao.Series[4].Points.AddXY(PassoGraficoVS, double.Parse(partes[2]) / 100);
                                    }
                                }));
                                break;
                            case 3: //Fase T
                                chartTensao.Invoke(new Action(() =>
                                {
                                    if (cbxGraficoVC.Checked)
                                    {
                                        chartTensao.Series[5].Points.AddXY(PassoGraficoVT, double.Parse(partes[2]) / 100);
                                    }
                                }));
                                break;

                            default:

                                break;
                        }
                        break;
                    #endregion
                    #region Ajuste Grosso V
                    case (int)Identificador.ID_TX_AjusteGrossoV:
                        Fase = int.Parse(partes[1]);
                        switch (Fase)
                        {
                            case 1://Fase R
                                chartTensao.Invoke(new Action(() =>
                                {
                                    if (cbxGraficoVA.Checked)
                                    {
                                        chartTensao.Series[6].Points.AddXY(PassoGraficoVR, double.Parse(partes[2]) / 100);
                                    }
                                }));
                                break;
                            case 2: //Fase S
                                chartTensao.Invoke(new Action(() =>
                                {
                                    if (cbxGraficoVB.Checked)
                                    {
                                        chartTensao.Series[7].Points.AddXY(PassoGraficoVS, double.Parse(partes[2]) / 100);
                                    }
                                }));
                                break;
                            case 3: //Fase T
                                chartTensao.Invoke(new Action(() =>
                                {
                                    if (cbxGraficoVC.Checked)
                                    {
                                        chartTensao.Series[8].Points.AddXY(PassoGraficoVT, double.Parse(partes[2]) / 100);
                                    }
                                }));
                                break;

                            default:

                                break;
                        }
                        break;
                    #endregion
                    #region Ajuste Fino I
                    case (int)Identificador.ID_TX_AjusteFinoI:
                        try
                        {
                            Fase = int.Parse(partes[1]);
                            switch (Fase)
                            {
                                case 1://Fase R
                                    chartCorrente.Invoke(new Action(() =>
                                    {
                                        if (cbxGraficoIA.Checked)
                                        {
                                            chartCorrente.Series[3].Points.AddXY(PassoGraficoIR, double.Parse(partes[2]) / 100);
                                        }
                                    }));
                                    break;
                                case 2: //Fase S
                                    chartCorrente.Invoke(new Action(() =>
                                    {
                                        if (cbxGraficoIB.Checked)
                                        {
                                            chartCorrente.Series[4].Points.AddXY(PassoGraficoIS, double.Parse(partes[2]) / 100);
                                        }
                                    }));
                                    break;
                                case 3: //Fase T
                                    chartCorrente.Invoke(new Action(() =>
                                    {
                                        if (cbxGraficoIC.Checked)
                                        {
                                            chartCorrente.Series[5].Points.AddXY(PassoGraficoIT, double.Parse(partes[2]) / 100);
                                        }
                                    }));
                                    break;

                                default:

                                    break;
                            }
                        }
                        catch(Exception)
                        {

                        }
                        break;
                    #endregion
                    #region Ajuste Grosso I
                    case (int)Identificador.ID_TX_AjsuteGrossoI:
                        Fase = int.Parse(partes[1]);
                        switch (Fase)
                        {
                            case 1://Fase R
                                chartCorrente.Invoke(new Action(() =>
                                {
                                    if (cbxGraficoIA.Checked)
                                    {
                                        chartCorrente.Series[6].Points.AddXY(PassoGraficoIR, double.Parse(partes[2]) / 100);
                                    }
                                }));
                                break;
                            case 2: //Fase S
                                chartCorrente.Invoke(new Action(() =>
                                {
                                    if (cbxGraficoIB.Checked)
                                    {
                                        chartCorrente.Series[7].Points.AddXY(PassoGraficoIS, double.Parse(partes[2]) / 100);
                                    }
                                }));
                                break;
                            case 3: //Fase T
                                chartCorrente.Invoke(new Action(() =>
                                {
                                    if (cbxGraficoIC.Checked)
                                    {
                                        chartCorrente.Series[8].Points.AddXY(PassoGraficoIT, double.Parse(partes[2]) / 100);
                                    }
                                }));
                                break;

                            default:

                                break;
                        }
                        break;
                    #endregion
                    #region Ler FP 60
                    case (int)Identificador.ID_Ler_FP_60:
                        txtFP160A.Invoke(new Action(() =>
                        {
                            txtFP160A.Text = partes[1];
                            txtFP160B.Text = partes[2];
                            txtFP160C.Text = partes[3];

                            txtFPInd60A.Text = partes[4];
                            txtFPInd60B.Text = partes[5];
                            txtFPInd60C.Text = partes[6];

                            txtFPCap60A.Text = partes[7];
                            txtFPCap60B.Text = partes[8];
                            txtFPCap60C.Text = partes[9];
                        }));
                        
                        break;
                    #endregion
                    #region Ler FP 50
                    case (int)Identificador.ID_Ler_FP_50:
                        txtFP150A.Invoke(new Action(() =>
                        {
                            txtFP150A.Text = partes[1];
                            txtFP150B.Text = partes[2];
                            txtFP150C.Text = partes[3];

                            txtFPInd50A.Text = partes[4];
                            txtFPInd50B.Text = partes[5];
                            txtFPInd50C.Text = partes[6];

                            txtFPCap50A.Text = partes[7];
                            txtFPCap50B.Text = partes[8];
                            txtFPCap50C.Text = partes[9];
                        }));

                        break;
                    #endregion
                    #region Correção Fase
                    case (int)Identificador.ID_Correcao_Fase:
                        txtLeituraFP1.Invoke(new Action(() =>
                        {
                            ///// Recebendo do pacote e populando as variáveis globais ///////////////
                            Frequencia = float.Parse(partes[1]) / 1000;                            
                            Periodo = (1 / Frequencia) * 1000; // período está em ms

                            FPI1Signed = float.Parse(partes[2]) / 1000;
                            FPI2Signed = float.Parse(partes[3]) / 1000;
                            FPI3Signed = float.Parse(partes[4]) / 1000;

                            FaseVAB = float.Parse(partes[5]) / 1000;
                            FaseVAC = float.Parse(partes[6]) / 1000;

                            TempoI1 = double.Parse(partes[7]) / 1000000000; //Aqui teremos o tempo em ms
                            TempoI2 = double.Parse(partes[8]) / 1000000000; //Aqui teremos o tempo em ms
                            TempoI3 = double.Parse(partes[9]) / 1000000000; //Aqui teremos o tempo em ms

                            txtAngI1ESP.Text = partes[10];
                            txtAngI2ESP.Text = partes[11];
                            txtAngI3ESP.Text = partes[12];

                            ////////////////////////////////////////////////////////////////////////////


                            /////// Fazendo alguns cálculos de angulo em função do tempo //////////////
                            AnguloFasorV1 = 0;
                            AnguloFasorV2 = (AnguloFasorV1 - FaseVAB) + 360; // O ADE me dá o tempo depois do zero-crossing da tensão. Logo, é como se sempre a corrente estivesse atrasada!! Mesmo não estando
                            AnguloFasorV3 = (AnguloFasorV1 - FaseVAC) + 360; //

                            //Console.WriteLine("Angulo V1: " + AnguloFasorV1);
                            //Console.WriteLine("Angulo V2: " + AnguloFasorV2);
                            //Console.WriteLine("Angulo V3: " + AnguloFasorV3);

                            AnguloIA = double.Parse(partes[10])/1000;  //Sempre em sentido anti-horário e em relação ao fasor de tensão do canal
                            AnguloIB = double.Parse(partes[11])/1000;  //Sempre em sentido anti-horário e em relação ao fasor de tensão do canal
                            AnguloIC = double.Parse(partes[12])/1000;  //Sempre em sentido anti-horário e em relação ao fasor de tensão do canal

                            //Console.WriteLine("Angulo entre I1 e V1: " + AnguloIA);
                            //Console.WriteLine("Angulo entre I2 e V2: " + AnguloIB);
                            //Console.WriteLine("Angulo entre I3 e V3: " + AnguloIC);

                            AnguloFasorI1 = (float)(AnguloFasorV1 - AnguloIA);
                            AnguloFasorI2 = (float)(AnguloFasorV2 - AnguloIB);
                            AnguloFasorI3 = (float)(AnguloFasorV3 - AnguloIC);
                            
                            //Console.WriteLine("Angulo Fasor I1: " + AnguloFasorI1);
                            //Console.WriteLine("Angulo Fasor I2: " + AnguloFasorI2);
                            //Console.WriteLine("Angulo Fasor I3: " + AnguloFasorI3);
                            ///////////////////////////////////////////////////////////////////////////


                            /////// Escrevendo nos TXTs iniciais //////////
                            txtFrequenciaLida.Text = Frequencia.ToString("0.000");
                            txtPeríodo.Text = Periodo.ToString("0.000");

                            txtTempoI1.Text = TempoI1.ToString("0.000");
                            txtTempoI2.Text = TempoI2.ToString("0.000");
                            txtTempoI3.Text = TempoI3.ToString("0.000");

                            txtLeituraFP1.Text = FPI1Signed.ToString("0.00");
                            txtLeituraFP2.Text = FPI2Signed.ToString("0.00");
                            txtLeituraFP3.Text = FPI3Signed.ToString("0.00");

                            //txtAnguloFP1.Text = AnguloIA.ToString("0.000");
                            //txtAnguloFP2.Text = AnguloIB.ToString("0.000");
                            //txtAnguloFP3.Text = AnguloIC.ToString("0.000");

                            txtLeituraFaseVAB.Text = FaseVAB.ToString("0.000");
                            txtLeituraFaseVAC.Text = FaseVAC.ToString("0.000");

                            float FasorV1Tratado = 0, FasorV2Tratado = FaseVAB, FasorV3Tratado = FaseVAC;

                            FasorV2Tratado = 360 - FasorV2Tratado;
                            FasorV3Tratado = 360 - FasorV3Tratado;

                            if (FasorV2Tratado > 180)
                                FasorV2Tratado = (360 - FasorV2Tratado) * (-1);
                            else if (FasorV2Tratado < (-180))
                                FasorV2Tratado = 360 + FasorV2Tratado;

                            if (FasorV3Tratado > 180)
                                FasorV3Tratado = (360 - FasorV3Tratado) * (-1);
                            else if (FasorV3Tratado < (-180))
                                FasorV3Tratado = 360 + FasorV3Tratado;

                            labelV1.Text = FasorV1Tratado.ToString("0.000");
                            labelV2.Text = FasorV2Tratado.ToString("0.000");
                            labelV3.Text = FasorV3Tratado.ToString("0.000");

                            float FasorI1Tratado = AnguloFasorI1, FasorI2Tratado = AnguloFasorI2, FasorI3Tratado = AnguloFasorI3;

                            if (AnguloFasorI1 > 180)
                                FasorI1Tratado = (360 - AnguloFasorI1) * (-1);
                            else if (AnguloFasorI1 < (-180))
                                FasorI1Tratado = 360 + AnguloFasorI1;

                            if (AnguloFasorI2 > 180)
                                FasorI2Tratado = (360 - AnguloFasorI2) * (-1);
                            else if (AnguloFasorI2 < (-180))
                                FasorI2Tratado = 360 + AnguloFasorI2;

                            if (AnguloFasorI3 > 180)
                                FasorI3Tratado = (360 - AnguloFasorI3) * (-1);
                            else if (AnguloFasorI3 < (-180))
                                FasorI3Tratado = 360 + AnguloFasorI3;

                            labelI1.Text = FasorI1Tratado.ToString("0.000");
                            labelI2.Text = FasorI2Tratado.ToString("0.000");
                            labelI3.Text = FasorI3Tratado.ToString("0.000");
                            /////////////////////////////////////////////////////////////


                            //////// Populando o gráfico e o diagrama fasorial ////////////////////////
                            PrintPhasor = true;
                            timerClearPhasor.Stop();
                            timerClearPhasor.Start();
                            timerClearPhasor.Enabled = true;
                            DiagramaFasorial.Invalidate();

                            if (cbkFaseIA.Checked)
                                chartFases.Series[3].Points.AddXY(PassoGraficoFases, FasorI1Tratado);
                            if(cbkFaseIB.Checked)
                                chartFases.Series[4].Points.AddXY(PassoGraficoFases, FasorI2Tratado);
                            if(cbkFaseIC.Checked)
                                chartFases.Series[5].Points.AddXY(PassoGraficoFases, FasorI3Tratado);

                            PassoGraficoFases++;
                            ////////////////////////////////////////////////////////////////////////////
                            ///
                            if(cbxFP1.Checked)
                            {
                                if(AnguloIA > 270)
                                {
                                    txtDifI1.Text = (360 - AnguloIA).ToString("0.000");
                                }
                                else
                                {
                                    txtDifI1.Text = AnguloIA.ToString("0.000");
                                }

                                if (AnguloIB > 270)
                                {
                                    txtDifI2.Text = (360 - AnguloIB).ToString("0.000");
                                }
                                else
                                {
                                    txtDifI2.Text = AnguloIB.ToString("0.000");
                                }

                                if (AnguloIC > 270)
                                {
                                    txtDifI3.Text = (360 - AnguloIC).ToString("0.000");
                                }
                                else
                                {
                                    txtDifI3.Text = AnguloIC.ToString("0.000");
                                }
                            }

                        }
                        ));
                        break;
                    #endregion
                    #region Sentido Correção
                    case (int)Identificador.ID_SentidoCorrecao:
                        pbxI1.Invoke(new Action(() =>
                        {
                            float CorreçãoV2 = float.Parse(partes[1]);
                            float CorreçãoV3 = float.Parse(partes[2]);
                            float CorreçãoI1 = float.Parse(partes[3]);
                            float CorreçãoI2 = float.Parse(partes[4]);
                            float CorreçãoI3 = float.Parse(partes[5]);

                            if (CorreçãoV2 != 0)
                            {
                                if (CorreçãoV2 < 0)
                                {
                                    pbxV2.Image = ImagemAtrasa;
                                }
                                else
                                {
                                    pbxV2.Image = ImagemAdianta;
                                }
                            }
                            else
                            {
                                pbxV2.Image = null;
                            }

                            if (CorreçãoV3 != 0)
                            {
                                if (CorreçãoV3 < 0)
                                {
                                    pbxV3.Image = ImagemAtrasa;
                                }
                                else
                                {
                                    pbxV3.Image = ImagemAdianta;
                                }
                            }
                            else
                            {
                                pbxV3.Image = null;
                            }

                            if (CorreçãoI1 != 0)
                            {
                                if (CorreçãoI1 < 0)
                                {
                                    pbxI1.Image = ImagemAtrasa;
                                }
                                else
                                {
                                    pbxI1.Image = ImagemAdianta;
                                }
                            }
                            else
                            {
                                pbxI1.Image = null;
                            }

                            if(CorreçãoI2 != 0)
                            {
                                if (CorreçãoI2 < 0)
                                {
                                    pbxI2.Image = ImagemAtrasa;
                                }
                                else
                                {
                                    pbxI2.Image = ImagemAdianta;
                                }
                            }
                            else
                            {
                                pbxI2.Image = null;
                            }

                            if(CorreçãoI3 != 0)
                            {
                                if (CorreçãoI3 < 0)
                                {
                                    pbxI3.Image = ImagemAtrasa;
                                }
                                else
                                {
                                    pbxI3.Image = ImagemAdianta;
                                }
                            }
                            else
                            {
                                pbxI3.Image = null;
                            }
                        }
                        ));
                        break;
                    #endregion
                    #region LOG Erros de Saída
                    case (int)Identificador.ID_LOG_ErrosDeSaida:
                        txtErrosCCAI_1.Invoke(new Action(() =>
                        {
                            txtErrosCCAI_1.Text = partes[1];
                            txtErrosCCAI2.Text = partes[2];
                            txtErrosCCAI3.Text = partes[3];

                            txtUVPOLPI1.Text = partes[4];
                            txtUVPOLPI2.Text = partes[5];
                            txtUVPOLPI3.Text = partes[6];

                            txtOTWI1.Text = partes[7];
                            txtOTWI2.Text = partes[8];
                            txtOTWI3.Text = partes[9];

                            SM_LOGStatus = SM_LOGStatus_Members.AguardadndoLOG_ErrosDeComunicacao;
                        }
                        ));
                        break;
                    #endregion
                    #region Erros De Comunicação
                    case (int)Identificador.ID_LOG_ErrosDeComunicacao:
                        txtErrosCCAI_1.Invoke(new Action(() =>
                        {
                            txtErrosADE.Text = partes[1];

                            txtErrosIO_Expander.Text = partes[2];
                            txtErrosMUX_I2C.Text = partes[3];

                            txtErrosDigPotI1.Text = partes[4];
                            txtErrosDigPotI2.Text = partes[5];
                            txtErrosDigPotI3.Text = partes[6];

                            txtErrosDigPotV1.Text = partes[7];
                            txtErrosDigPotV2.Text = partes[8];
                            txtErrosDigPotV3.Text = partes[9];

                            txtErrosDisplayLCD.Text = partes[10];

                            SM_LOGStatus = SM_LOGStatus_Members.AguardandoLOG_Eventos;
                        }
                        ));
                        break;
                    #endregion
                    #region LOG Eventos
                    case (int)Identificador.ID_LOG_Eventos:
                        txtQTreligada.Invoke(new Action(() =>
                        {
                            txtQTreligada.Text = partes[1];
                            txtQTInicioSintetizacao.Text = partes[2];
                            txtQTFinalizacaoSintetizacao.Text = partes[3];                            

                            SM_LOGStatus = SM_LOGStatus_Members.AguardandoLOG_Temporizacoes;
                        }
                        ));
                        break;
                    #endregion
                    #region LOG Temporizações
                    case (int)Identificador.ID_LOG_Temporizacoes:
                        txtTempoLigadaSemSintetizar.Invoke(new Action(() =>
                        {
                            txtTempoLigadaSemSintetizar.Text = partes[1];
                            txtTempoLigadaSintetizando.Text = partes[2];
                            txtTempoUltimaSintetizacao.Text = partes[3];

                            txtTempoTotal.Text = (Int32.Parse(partes[1]) + Int32.Parse(partes[2])).ToString();

                            SM_LOGStatus = SM_LOGStatus_Members.AguardadndoLOG_DadosUnicos;
                        }
                        ));
                        break;
                    #endregion
                    #region LOG Dados Unicos
                    case (int)Identificador.ID_LOG_DadosUnicos:
                        txtVersaoFWSint.Invoke(new Action(() =>
                        {
                            txtVersaoFWSint.Text = partes[1];
                            txtVersaoFWCont.Text = partes[2];
                            txtNumeroDeSerie.Text = partes[3];
                            txtCliente.Text = partes[4];
                            txtDataExpedicao.Text = partes[5];

                            SM_LOGStatus = SM_LOGStatus_Members.TodosOsLOGsRecebidos;
                        }
                        ));
                        break;
                    #endregion
                    default:

                        break;
                }
            }
        }

        private void PortaSerial_DadoRecebido(object sender, SerialDataReceivedEventArgs e)
        {
            try
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
            }
            catch(Exception)
            {

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
           

            string TRAMA_ENVIO = (int)Identificador.ID_ConstantesPIDTensao + "," +
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

            string TRAMA_ENVIO = (int)Identificador.ID_ConstantesPIDCorrente + "," +
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

            string TRAMA_ENVIO = (int)Identificador.ID_ParametrosSintetizacao + "," +
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
            string TRAMA_ENVIO = (int)Identificador.ID_Parar + ",";

            TRiseVa = false;

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Parar Sint.: " + TramaComChecksum);

                PortaSerial.DiscardInBuffer();
            }
            else
            {
                LOG_TXT("Comando para parar sintetização não enviado devido porta serial fechada!");
            }            
        }

        private void btnIniciarSintetização_Click(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = (int)Identificador.ID_Iniciar + ",";

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

            txtTensãoRMSA.Text = "";
            txtTensãoRMSB.Text = "";
            txtTensãoRMSC.Text = "";

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

            txtCorrenteRMSA.Text = "";
            txtCorrenteRMSB.Text = "";
            txtCorrenteRMSC.Text = "";
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

        private void btnResetarESP32_Click(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = (int)Identificador.ID_RESET_ESP32 + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando resetar ESP32: " + TramaComChecksum);

                PortaSerial.DiscardInBuffer();
            }
            else
            {
                LOG_TXT("Comando para resetar ESP32 não enviado devido porta serial fechada!");
            }
        }

        private void btnResetaADE_Click(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = (int)Identificador.ID_RESET_ADE + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando reset ADE: " + TramaComChecksum);

                PortaSerial.DiscardInBuffer();
            }
            else
            {
                LOG_TXT("Comando para resetar ADE não enviado devido porta serial fechada!");
            }
        }

        private void btnReconfigurarADE_Click(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = (int)Identificador.ID_RECONFIG_ADE + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando recnfigurar ADE: " + TramaComChecksum);

                PortaSerial.DiscardInBuffer();
            }
            else
            {
                LOG_TXT("Comando para reconfigurar ADE não enviado devido porta serial fechada!");
            }
        }

        private void btnAplicar50ktensao_Click(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = (int)Identificador.ID_Aplicar_DigPot_50k_Tensao + "," +
                                 nud50kVA.Value + "," +
                                 nud50kVB.Value + "," +
                                 nud50kVC.Value + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando DigPotGrosso Tensão: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de configuração DigPotGrosso Tensão não enviado devido porta serial fechada!");
            }
        }

        private void btnAplicar10ktensao_Click(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = (int)Identificador.ID_Aplicar_DigPot_10k_Tensao + "," +
                                 nud10kVA.Value + "," +
                                 nud10kVB.Value + "," +
                                 nud10kVC.Value + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando DigPotFino Tensão: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de configuração DigPotFino Tensão não enviado devido porta serial fechada!");
            }
        }

        private void btnAplicar50kCorrente_Click(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = (int)Identificador.ID_Aplicar_DigPot_50k_Corrente + "," +
                                 nud50kIA.Value + "," +
                                 nud50kIB.Value + "," +
                                 nud50kIC.Value + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando DigPotGrosso Corrente: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de configuração DigPotGrosso Corrente não enviado devido porta serial fechada!");
            }
        }

        private void btnAplicar10kcorrente_Click(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = (int)Identificador.ID_Aplicar_DigPot_10k_Corrente + "," +
                                 nud10kIA.Value + "," +
                                 nud10kIB.Value + "," +
                                 nud10kIC.Value + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando DigPotFino Corrente: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de configuração DigPotFino Corrente não enviado devido porta serial fechada!");
            }
        }

        private void txtFP160A_KeyPress(object sender, KeyPressEventArgs e)
        {
            string senderText = (sender as TextBox).Text;
            string senderName = (sender as TextBox).Name;
            string[] splitByDecimal = senderText.Split('.');
            int cursorPosition = (sender as TextBox).SelectionStart;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar)
                && senderText.IndexOf('.') < cursorPosition
                && splitByDecimal.Length > 1
                && splitByDecimal[1].Length == 3)
            {
                e.Handled = true;
            }
        }

        private void btnAplicar60FP1_Click(object sender, EventArgs e)
        {
            //Trama de aplicação ddos parâmetros de ajuste do fator de potência
            //Identificador, FP1_FaseA, FP1_FaseB, FP1_FaseC, CS        

            string TRAMA_ENVIO = (int)Identificador.ID_Aplicar_FP160 + "," +
                                 txtFP160A.Text + "," +
                                 txtFP160B.Text + "," +
                                 txtFP160C.Text + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Ajuste FP 1 60Hz.: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de ajuste de FP 1 60Hz não enviado devido porta serial fechada!");
            }
        }

        private void btnAplicar60FPInd_Click(object sender, EventArgs e)
        {
            //Trama de aplicação ddos parâmetros de ajuste do fator de potência
            //Identificador, FPInd_FaseA, FPInd_FaseB, FPInd_FaseC, CS        

            string TRAMA_ENVIO = (int)Identificador.ID_Aplicar_FPInd60 + "," +
                                 txtFPInd60A.Text + "," +
                                 txtFPInd60B.Text + "," +
                                 txtFPInd60C.Text + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Ajuste FP Ind 60Hz.: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de ajuste de FP Ind 60Hz não enviado devido porta serial fechada!");
            }
        }

        private void btnAplicar60FPCap_Click(object sender, EventArgs e)
        {
            //Trama de aplicação ddos parâmetros de ajuste do fator de potência
            //Identificador, FP1_FaseA, FP1_FaseB, FP1_FaseC, CS        

            string TRAMA_ENVIO = (int)Identificador.ID_Aplicar_FPCap60 + "," +
                                 txtFPCap60A.Text + "," +
                                 txtFPCap60B.Text + "," +
                                 txtFPCap60C.Text + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Ajuste FP Cap 60Hz.: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de ajuste de FP Cap 60Hz não enviado devido porta serial fechada!");
            }
        }

        private void button8btnAplicar50FP1_Click(object sender, EventArgs e)
        {
            //Trama de aplicação ddos parâmetros de ajuste do fator de potência
            //Identificador, FP1_FaseA, FP1_FaseB, FP1_FaseC, CS        

            string TRAMA_ENVIO = (int)Identificador.ID_Aplicar_FP150 + "," +
                                 txtFP150A.Text + "," +
                                 txtFP150B.Text + "," +
                                 txtFP150C.Text + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Ajuste FP 1 50Hz.: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de ajuste de FP 1 50Hz não enviado devido porta serial fechada!");
            }
        }

        private void button8btnAplicar50FPInd_Click(object sender, EventArgs e)
        {
            //Trama de aplicação ddos parâmetros de ajuste do fator de potência
            //Identificador, FPInd_FaseA, FPInd_FaseB, FPInd_FaseC, CS        

            string TRAMA_ENVIO = (int)Identificador.ID_Aplicar_FPInd50 + "," +
                                 txtFPInd50A.Text + "," +
                                 txtFPInd50B.Text + "," +
                                 txtFPInd50C.Text + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Ajuste FP Ind 50Hz.: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de ajuste de FP Ind 50Hz não enviado devido porta serial fechada!");
            }
        }

        private void button8btnAplicar50FPCap_Click(object sender, EventArgs e)
        {
            //Trama de aplicação ddos parâmetros de ajuste do fator de potência
            //Identificador, FP1_FaseA, FP1_FaseB, FP1_FaseC, CS        

            string TRAMA_ENVIO = (int)Identificador.ID_Aplicar_FPCap50 + "," +
                                 txtFPCap50A.Text + "," +
                                 txtFPCap50B.Text + "," +
                                 txtFPCap50C.Text + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Ajuste FP Cap 50Hz.: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de ajuste de FP Cap 50Hz não enviado devido porta serial fechada!");
            }
        }

        private void btnLer60_Click(object sender, EventArgs e)
        {
            //Trama de solicitação dos parâmetros de ajuste do fator de potência 60Hz
            //Identificador, CS        

            string TRAMA_ENVIO = (int)Identificador.ID_Ler_FP_60 + "," ;

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando de solicitação de Ajuste FP 60Hz.: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de solicitação de ajuste de FP 60Hz não enviado devido porta serial fechada!");
            }
        }

        private void btnLer50_Click(object sender, EventArgs e)
        {
            //Trama de solicitação dos parâmetros de ajuste do fator de potência 50Hz
            //Identificador, CS        

            string TRAMA_ENVIO = (int)Identificador.ID_Ler_FP_50 + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando de solicitação de Ajuste FP 50Hz.: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de solicitação de ajuste de FP 50Hz não enviado devido porta serial fechada!");
            }
        }

        private void btnLimpar60_Click(object sender, EventArgs e)
        {
            txtFP160A.Text = txtFP160B.Text = txtFP160C.Text = string.Empty;
            txtFPInd60A.Text = txtFPInd60B.Text = txtFPInd60C.Text = string.Empty;
            txtFPCap60A.Text = txtFPCap60B.Text = txtFPCap60C.Text = string.Empty;
        }

        private void btnLimpar50_Click(object sender, EventArgs e)
        {
            txtFP150A.Text = txtFP150B.Text = txtFP150C.Text = string.Empty;
            txtFPInd50A.Text = txtFPInd50B.Text = txtFPInd50C.Text = string.Empty;
            txtFPCap50A.Text = txtFPCap50B.Text = txtFPCap50C.Text = string.Empty;
        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void btnAplicarCorrecaoFase_Click(object sender, EventArgs e)
        {
            //Trama de aplicação dos parâmetros de ajuste de fase
            //Identificador, CorreçãoFaseVB, CorreçãoFaseVC, CorreçãoFaseIA, CorreçãoFaseIB, CorreçãoFaseIC, CS        

            string TRAMA_ENVIO = (int)Identificador.ID_Correcao_Fase + "," +
                                 txtAjusteVB.Text + "," +
                                 txtAjusteVC.Text + "," +
                                 txtAjusteIA.Text + "," +
                                 txtAjusteIB.Text + "," +
                                 txtAjusteIC.Text + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Ajuste de Fase: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de ajuste de fase não enviado devido porta serial fechada!");
            }
        }

        private void txtAjusteVB_KeyPress(object sender, KeyPressEventArgs e)
        {
            string senderText = (sender as TextBox).Text;
            string senderName = (sender as TextBox).Name;
            string[] splitByDecimal = senderText.Split('.');
            int cursorPosition = (sender as TextBox).SelectionStart;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar)
                && senderText.IndexOf('.') < cursorPosition
                && splitByDecimal.Length > 1
                && splitByDecimal[1].Length == 4)
            {
                e.Handled = true;
            }
        }

        private void label65_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void btnAplicarKPKIKDFases_Click(object sender, EventArgs e)
        {
            //Trama de aplicação das constatnes KP, KI e KD
            //Identificador, Kp, Ki, Kd, CS
            Salvar_Dados_Config();


            string TRAMA_ENVIO = (int)Identificador.ID_ConstantesPIDFases + "," +
                                 txtKpFases.Text + "," +
                                 txtKiFases.Text + "," +
                                 txtKdFases.Text + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando PID_F: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de configuração PID para fases não enviado devido porta serial fechada!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PassoGraficoFases = 0;
            chartFases.Series[0].Points.Clear();
            chartFases.Series[1].Points.Clear();
            chartFases.Series[2].Points.Clear();
            chartFases.Series[3].Points.Clear();
            chartFases.Series[4].Points.Clear();
            chartFases.Series[5].Points.Clear();            
        }

        private void DiagramaFasorial_Paint(object sender, PaintEventArgs e)
        {
            float[] dashvalues = { 4, 4, 4, 4 };

            e.Graphics.DrawEllipse(new Pen(Color.Black), new Rectangle(30, 40, 200, 200)); //Borda circular do diagrama fasorial

            if(PrintPhasor)
            {
                using (Pen PenPhasorV1 = new Pen(Color.Red))
                using (GraphicsPath capPath = new GraphicsPath())
                {
                    //PenPhasorV1.DashPattern = dashvalues;

                    // A triangle
                    capPath.AddLine(-5, 0, 5, 0);
                    capPath.AddLine(-5, 0, 0, 5);
                    capPath.AddLine(0, 5, 5, 0);
                    capPath.AddString("1", new FontFamily("Arial"), (int)FontStyle.Regular, 10, new Point(-3, 6), StringFormat.GenericDefault);

                    PenPhasorV1.CustomEndCap = new CustomLineCap(null, capPath);

                    //Console.WriteLine("Desenhando V1");
                    e.Graphics.DrawLine(PenPhasorV1, 130, 140, GetPhasorX(AnguloFasorV1), GetPhasorY(AnguloFasorV1));
                }

                using (Pen PenPhasorV2 = new Pen(Color.Yellow))
                using (GraphicsPath capPath = new GraphicsPath())
                {
                    //PenPhasorV2.DashPattern = dashvalues;

                    // A triangle
                    capPath.AddLine(-5, 0, 5, 0);
                    capPath.AddLine(-5, 0, 0, 5);
                    capPath.AddLine(0, 5, 5, 0);
                    capPath.AddString("2", new FontFamily("Arial"), (int)FontStyle.Regular, 10, new Point(-3, 6), StringFormat.GenericDefault);

                    PenPhasorV2.CustomEndCap = new CustomLineCap(null, capPath);

                    //Console.WriteLine("Desenhando V2");
                    e.Graphics.DrawLine(PenPhasorV2, 130, 140, GetPhasorX(AnguloFasorV2), GetPhasorY(AnguloFasorV2));
                }

                using (Pen PenPhasorV3 = new Pen(Color.Blue))
                using (GraphicsPath capPath = new GraphicsPath())
                {
                    //PenPhasorV2.DashPattern = dashvalues;

                    // A triangle
                    capPath.AddLine(-5, 0, 5, 0);
                    capPath.AddLine(-5, 0, 0, 5);
                    capPath.AddLine(0, 5, 5, 0);
                    capPath.AddString("3", new FontFamily("Arial"), (int)FontStyle.Regular, 10, new Point(-3, 6), StringFormat.GenericDefault);

                    PenPhasorV3.CustomEndCap = new CustomLineCap(null, capPath);

                    //Console.WriteLine("Desenhando V3");
                    e.Graphics.DrawLine(PenPhasorV3, 130, 140, GetPhasorX(AnguloFasorV3), GetPhasorY(AnguloFasorV3));
                }

                using (Pen PenPhasorI1 = new Pen(Color.Red))
                using (GraphicsPath capPath = new GraphicsPath())
                {
                    PenPhasorI1.DashPattern = dashvalues;

                    // A triangle
                    capPath.AddLine(-5, 0, 5, 0);
                    capPath.AddLine(-5, 0, 0, 5);
                    capPath.AddLine(0, 5, 5, 0);
                    capPath.AddString("1", new FontFamily("Arial"), (int)FontStyle.Regular, 10, new Point(-3, 6), StringFormat.GenericDefault);

                    PenPhasorI1.CustomEndCap = new CustomLineCap(null, capPath);

                    //Console.WriteLine("Desenhando I1");
                    e.Graphics.DrawLine(PenPhasorI1, 130, 140, GetPhasorX(AnguloFasorI1), GetPhasorY(AnguloFasorI1));
                }

                using (Pen PenPhasorI2 = new Pen(Color.Yellow))
                using (GraphicsPath capPath = new GraphicsPath())
                {
                    PenPhasorI2.DashPattern = dashvalues;

                    // A triangle
                    capPath.AddLine(-5, 0, 5, 0);
                    capPath.AddLine(-5, 0, 0, 5);
                    capPath.AddLine(0, 5, 5, 0);
                    capPath.AddString("2", new FontFamily("Arial"), (int)FontStyle.Regular, 10, new Point(-3, 6), StringFormat.GenericDefault);

                    PenPhasorI2.CustomEndCap = new CustomLineCap(null, capPath);

                   // Console.WriteLine("Desenhando I2");
                    e.Graphics.DrawLine(PenPhasorI2, 130, 140, GetPhasorX(AnguloFasorI2), GetPhasorY(AnguloFasorI2));
                }

                using (Pen PenPhasorI3 = new Pen(Color.Blue))
                using (GraphicsPath capPath = new GraphicsPath())
                {
                    PenPhasorI3.DashPattern = dashvalues;

                    // A triangle
                    capPath.AddLine(-5, 0, 5, 0);
                    capPath.AddLine(-5, 0, 0, 5);
                    capPath.AddLine(0, 5, 5, 0);
                    capPath.AddString("3", new FontFamily("Arial"), (int)FontStyle.Regular, 10, new Point(-3, 6), StringFormat.GenericDefault);

                    PenPhasorI3.CustomEndCap = new CustomLineCap(null, capPath);
                    
                    //Console.WriteLine("Desenhando I3");
                    e.Graphics.DrawLine(PenPhasorI3, 130, 140, GetPhasorX(AnguloFasorI3), GetPhasorY(AnguloFasorI3));
                }
            }    
        }

        private float GetPhasorX(float Angle)
        {
            float X = 0;
            int Tamanho = 80;
            int XCentro = 130;

            // Angle = 360 - Angle;

            //Console.WriteLine("Fasor X: " + Angle);

            while (Angle > 360)
                Angle -= 360;            

            while(Angle < 0)            
                Angle += 360;            

            if (Angle >= 0 && Angle <= 90)
            {
                X = (float)(Math.Cos(Angle * Math.PI / 180) * (Tamanho) + (XCentro));
            }

            else if (Angle > 90 && Angle <= 270)
            {
                X = (float)(Math.Cos(Angle * Math.PI / 180) * (Tamanho) + (XCentro));
            }

            else if (Angle > 270 && Angle <= 360)
            {
                
                X = (float)(Math.Cos(Angle * Math.PI / 180) * (Tamanho) + (XCentro));
            }

            //Console.WriteLine("X: " + X);

            return X;
        }

        private float GetPhasorY(float Angle)
        {
            float Y = 0;
            int Tamanho = 80;
            int YCentro = 140;

            //Angle = 360 - Angle;

            //Console.WriteLine("Fasor Y: " + Angle);

            while (Angle > 360)
                Angle -= 360;           

            while (Angle < 0)            
                Angle += 360;            

            if (Angle >= 0 && Angle <= 180)
            {
                Y = (float)((YCentro) - Math.Sin(Angle * Math.PI / 180) * (Tamanho));
            }

            else if (Angle > 180 && Angle <= 360)
            {
                Y = (float)(Math.Sin(Angle * Math.PI / 180) * (-Tamanho) + (YCentro));
            }

            //Console.WriteLine("Y: " + Y);

            return Y;
        }

        private void timerClearPhasor_Tick(object sender, EventArgs e)
        {
            PrintPhasor = false;
            timerClearPhasor.Stop();
            DiagramaFasorial.Invalidate();
            pbxI1.Image = null;
            pbxI2.Image = null;
            pbxI3.Image = null;
            pbxV2.Image = null;
            pbxV3.Image = null;
        }

        private void btnAplicarFaseManual_Click(object sender, EventArgs e)
        {
            //Trama de aplicação e sintetização com fase Manual
            //Identificador, Angulo(°), CS

            string TRAMA_ENVIO = (int)Identificador.ID_FaseManual + "," +
                                 txtFaseManual.Text + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando FaseManual: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de FaseManual não enviado devido porta serial fechada!");
            }
        }

        private void btnAplicarMaxMinCharttensão_Click(object sender, EventArgs e)
        {
            chartTensao.ChartAreas[0].AxisY.Maximum = double.Parse(txtmaxChart.Text);
            chartTensao.ChartAreas[0].AxisY.Minimum = double.Parse(txtMinChart.Text);
        }

        private void BtnAjustarADE_Click(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = (int)Identificador.ID_AjustarADE + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Ajustar ADE: " + TramaComChecksum);

                PortaSerial.DiscardInBuffer();
            }
            else
            {
                LOG_TXT("Comando para Ajsutar ADE não enviado devido porta serial fechada!");
            }
        }

        private void btnAplicarMinMaxCorrente_Click(object sender, EventArgs e)
        {
            chartCorrente.ChartAreas[0].AxisY.Maximum = double.Parse(txtMaxChartCorrente.Text);
            chartCorrente.ChartAreas[0].AxisY.Minimum = double.Parse(txtMinChartCorrente.Text);
        }

        private void btnLerLOGEquipamento_Click(object sender, EventArgs e)
        {
            if (timerSolicitaLOGsEquipamento.Enabled)
            {
                MessageBox.Show("Aguarde a finalização da úlçtima solicitação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //SolicitaLOG((int)Identificador.ID_LOG_ErrosDeSaida);
                timerSolicitaLOGsEquipamento.Enabled = true;
                SM_LOGRequest = SM_LOGRequest_Members.SolicitaErrosDeSaida;
                SM_LOGStatus = SM_LOGStatus_Members.AguardandoLOG_ErrosDeSaida;
            }
        }

        private void SolicitaLOG(int ID)
        {
            //Trama de solicitação dos LOGs de funcionamento do equipamento
            //Identificador, CS        

            string TRAMA_ENVIO = ID + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando de solicitação LOG do equipamento.: " + TramaComChecksum);
            }
            else
            {
                LOG_TXT("Comando de solicitação de LOG não enviado devido porta serial fechada!");
            }
        }

        private void timerSolicitaLOGsEquipamento_Tick(object sender, EventArgs e)
        {
            switch (SM_LOGRequest)
            {
                case SM_LOGRequest_Members.SolicitaErrosDeSaida:
                    if((LOG_CounterLimit <= TentativasMAX) && (SM_LOGStatus == SM_LOGStatus_Members.AguardandoLOG_ErrosDeSaida))
                    {
                        LOG_CounterLimit++;
                        SolicitaLOG((int)Identificador.ID_LOG_ErrosDeSaida);
                    }
                    else if(LOG_CounterLimit > TentativasMAX)
                    {
                        timerSolicitaLOGsEquipamento.Enabled = false;
                        MessageBox.Show("Expirado tempo de resposta do equipamento (1)!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LOG_TXT("Erro de comunicação e solicitação de LOG. Não recebemos nem o primeiro pacote (Erros de Saída)");
                        LOG_CounterLimit = 0;
                    }
                    else
                    {
                        SM_LOGRequest = SM_LOGRequest_Members.SolicitaErrosDeComunicacao;
                        LOG_CounterLimit = 0;
                        SolicitaLOG((int)Identificador.ID_LOG_ErrosDeComunicacao);
                    }
                    break;
                case SM_LOGRequest_Members.SolicitaErrosDeComunicacao:
                    if ((LOG_CounterLimit <= TentativasMAX) && (SM_LOGStatus == SM_LOGStatus_Members.AguardadndoLOG_ErrosDeComunicacao))
                    {
                        LOG_CounterLimit++;
                        SolicitaLOG((int)Identificador.ID_LOG_ErrosDeComunicacao);
                    }
                    else if (LOG_CounterLimit > TentativasMAX)
                    {
                        timerSolicitaLOGsEquipamento.Enabled = false;
                        MessageBox.Show("Expirado tempo de resposta do equipamento (2)!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LOG_TXT("Erro de comunicação e solicitação de LOG. Recebemos o primeiro pacote (Erros de Saída) mas não recebemos o segundo pacote (Erros de comunicação)");
                        LOG_CounterLimit = 0;
                    }
                    else
                    {
                        SM_LOGRequest = SM_LOGRequest_Members.SolicitaEventos;
                        LOG_CounterLimit = 0;
                        SolicitaLOG((int)Identificador.ID_LOG_Eventos);
                    }
                    break;
                case SM_LOGRequest_Members.SolicitaEventos:
                    if ((LOG_CounterLimit <= TentativasMAX) && (SM_LOGStatus == SM_LOGStatus_Members.AguardandoLOG_Eventos))
                    {
                        LOG_CounterLimit++;
                        SolicitaLOG((int)Identificador.ID_LOG_Eventos);
                    }
                    else if (LOG_CounterLimit > TentativasMAX)
                    {
                        timerSolicitaLOGsEquipamento.Enabled = false;
                        MessageBox.Show("Expirado tempo de resposta do equipamento (3)!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LOG_TXT("Erro de comunicação e solicitação de LOG. Recebemos os dois primeiros pacotes mas não recebemos o terceiro (LOG de Eventos)");
                        LOG_CounterLimit = 0;
                    }
                    else
                    {
                        SM_LOGRequest = SM_LOGRequest_Members.SolicitaTemporizacoes;
                        LOG_CounterLimit = 0;
                        SolicitaLOG((int)Identificador.ID_LOG_Temporizacoes);
                    }
                    break;
                case SM_LOGRequest_Members.SolicitaTemporizacoes:
                    if ((LOG_CounterLimit <= TentativasMAX) && (SM_LOGStatus == SM_LOGStatus_Members.AguardandoLOG_Temporizacoes))
                    {
                        LOG_CounterLimit++;
                        SolicitaLOG((int)Identificador.ID_LOG_Temporizacoes);
                    }
                    else if (LOG_CounterLimit > TentativasMAX)
                    {
                        timerSolicitaLOGsEquipamento.Enabled = false;
                        MessageBox.Show("Expirado tempo de resposta do equipamento (4)!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LOG_TXT("Erro de comunicação e solicitação de LOG. Recebemos os dois três primeiros pacotes mas não recebemos o quarto (LOG de Temporizações)");
                        LOG_CounterLimit = 0;
                    }
                    else
                    {
                        SM_LOGRequest = SM_LOGRequest_Members.SolicitaDadosUnicos;
                        LOG_CounterLimit = 0;
                        SolicitaLOG((int)Identificador.ID_LOG_DadosUnicos);
                    }
                    break;
                case SM_LOGRequest_Members.SolicitaDadosUnicos:
                    if ((LOG_CounterLimit <= TentativasMAX) && (SM_LOGStatus == SM_LOGStatus_Members.AguardadndoLOG_DadosUnicos))
                    {
                        LOG_CounterLimit++;
                        SolicitaLOG((int)Identificador.ID_LOG_DadosUnicos);
                    }
                    else if (LOG_CounterLimit > TentativasMAX)
                    {
                        timerSolicitaLOGsEquipamento.Enabled = false;
                        MessageBox.Show("Expirado tempo de resposta do equipamento (5)!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LOG_TXT("Erro de comunicação e solicitação de LOG. Recebemos os dois quatro primeiros pacotes mas não recebemos o quinto (LOG de Dados Únicos)");
                        LOG_CounterLimit = 0;
                    }
                    else
                    {
                        SM_LOGRequest = SM_LOGRequest_Members.SemSolicitacoesDeLOG;
                        LOG_CounterLimit = 0;                        
                    }
                    break;
                default:
                    timerSolicitaLOGsEquipamento.Enabled = false;
                    MessageBox.Show("Dados lidos com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LOG_TXT("Dados de LOG lidos com sucesso!");
                    break;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtErrosCCAI_1.Text = txtErrosCCAI2.Text = txtErrosCCAI3.Text = txtUVPOLPI1.Text = txtUVPOLPI2.Text =
                txtUVPOLPI3.Text = txtOTWI1.Text = txtOTWI2.Text = txtOTWI3.Text = String.Empty;
            txtErrosADE.Text = txtErrosIO_Expander.Text = txtErrosMUX_I2C.Text = txtErrosDigPotI1.Text =
                txtErrosDigPotI2.Text = txtErrosDigPotI3.Text = txtErrosDigPotV1.Text = txtErrosDigPotV2.Text = 
                txtErrosDigPotV3.Text = txtErrosDisplayLCD.Text = String.Empty;
            txtQTreligada.Text = txtQTInicioSintetizacao.Text = txtQTFinalizacaoSintetizacao.Text = String.Empty;
            txtTempoLigadaSemSintetizar.Text = txtTempoLigadaSintetizando.Text = txtTempoUltimaSintetizacao.Text = txtTempoTotal.Text = String.Empty;
            txtVersaoFWCont.Text = txtVersaoFWSint.Text = txtNumeroDeSerie.Text = txtCliente.Text = txtDataExpedicao.Text = String.Empty;
        }

        private void btnResetEqp_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja resetar o LOG do equipamento??\n" +
                                                        "Esta é uma operação irreversível!", "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                //Trama de solicitação de reset dos LOGs do Equipamento
                //Identificador, CS        

                string TRAMA_ENVIO = (int)Identificador.ID_RESET_LOGs + ",";

                if (PortaSerial.IsOpen)
                {
                    String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                    PortaSerial.Write(TramaComChecksum + "\0");
                    LOG_TXT("Envio de comando de solicitação de RESET dos LOGs do equipamento.: " + TramaComChecksum);
                }
                else
                {
                    LOG_TXT("Comando de solicitação de RESET de LOG não enviado devido porta serial fechada!");
                }
            }            
        }

        private void btnEnviarParaEqupamento_Click(object sender, EventArgs e)
        {
            if(txtNumeroDeSeriaEnviar.Text != string.Empty && txtClienteEnviar.Text != string.Empty && txtDataExpedicaoEnviar.Text != string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja substituir as informações únicas do equipamento?\n" +
                                                        "Esta é uma operação irreversível!", "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //Trama de Atualização dos dados únicos do equipamento
                    //Identificador, CS        

                    string TRAMA_ENVIO = (int)Identificador.ID_AtualizarDadosUnicos + "," + txtNumeroDeSeriaEnviar.Text + "," + txtClienteEnviar.Text + "," + txtDataExpedicaoEnviar.Text + ",";

                    if (PortaSerial.IsOpen)
                    {
                        String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                        PortaSerial.Write(TramaComChecksum + "\0");
                        LOG_TXT("Envio de comando de Atualização dos dados únicos do equipamento.: " + TramaComChecksum);
                    }
                    else
                    {
                        LOG_TXT("Comando de atualização de dados únicos não enviado devido porta serial fechada!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Considere inserir todas as informações primeiro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                timerRepeatStop.Enabled = true;
            }
            else
                timerRepeatStop.Enabled = false;
        }

        private void timerRepeatStop_Tick(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = (int)Identificador.ID_Parar + ",";

            TRiseVa = false;

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Calcula_checksum(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando Parar Sint.: " + TramaComChecksum);

                PortaSerial.DiscardInBuffer();
            }
            else
            {
                LOG_TXT("Comando para parar sintetização não enviado devido porta serial fechada!");
            }
        }

        private void nud10kIA_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
