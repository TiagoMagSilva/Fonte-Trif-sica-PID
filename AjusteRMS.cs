using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FonteTrifasicaPID
{
    public partial class AjusteRMS : Form
    {
        String path_SERIAL = System.AppDomain.CurrentDomain.BaseDirectory + "/SERIAL/";
        String path_LOG = System.AppDomain.CurrentDomain.BaseDirectory + "/LOG/";

        private SerialPort PortaSerial = new SerialPort();

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
            ID_AtualizarDadosUnicos,
            ID_Ler_registros_ADE,
            ID_RespostaGAINRegAjusteADE,
            ID_RespostaOFFSETRegAjusteADE,
            ID_respostaPhaseRegAjusteADE,
            ID_Resetar_Reg_Ajuste_ADE,
            ID_RealizarAjusteManualADE,
            ID_LimparRegistrosSelecionadosADE,
            ID_AjusteManualdeRegistradoresADE,
            ID_Resposta_Potencias_convertidas,
            ID_Resposta_Potencias_digitais,
            ID_resposta_RMS,
            ID_Resposta_RMS_Digitais,
            ID_Controle_Reset_AmpPot,
            ID_ERRO_CorrenteInvertida,
            ID_Ajuste_VRMS_IRMS,
            ID_Progresso_AJuste,
            ID_ERRO_RMS_Nominal,
            ID_InicioAjusteWGAIN,
            ID_Ajuste_WGAIN,
            ID_Indicar_FaseEmAjuste,
            ID_Inicio_AjusteDeFase,
            ID_AjusteFase
        };

        enum Enum_AjusteAtual //Por enquanto só estamos ajustando RMS_Gain, W_Gain e fase
        {
            Ajustando_VRMS_IRMS,            
            Ajustando_WGAIN,
            Ajustando_Fase
        };

        Int16 AjusteAtual = (Int16)Enum_AjusteAtual.Ajustando_VRMS_IRMS;

        public AjusteRMS()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtV2RMS.ReadOnly = txtV3RMS.ReadOnly = cbkReplicarV1.Checked;
            txtV3RMS.Text = txtV2RMS.Text = txtV1RMS.Text;
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

        private void btnAjustar_Click(object sender, EventArgs e)
        {
            string TRAMA_ENVIO = string.Empty;
            String TramaComChecksum = string.Empty;

            if (PortaSerial.IsOpen)
            {
                lblAguarde.Visible = true;
                switch (AjusteAtual)
                {
                    case (Int16)Enum_AjusteAtual.Ajustando_VRMS_IRMS:

                        TRAMA_ENVIO = (Int16)Identificador.ID_Ajuste_VRMS_IRMS + "," +
                                      txtV1RMS.Text + "," + txtV2RMS.Text + "," + txtV3RMS.Text + "," +
                                      txtI1RMS.Text + "," + txtI2RMS.Text + "," + txtI3RMS.Text + ",";

                        TramaComChecksum = TRAMA_ENVIO + Crc16Ccitt(TRAMA_ENVIO);
                        LOG_TXT("Envio de comando de ajuste RMS: " + TramaComChecksum);
                        break;
                    case (Int16)Enum_AjusteAtual.Ajustando_WGAIN:
                        TRAMA_ENVIO = (Int16)Identificador.ID_Ajuste_WGAIN + "," + txtErrowGain.Text + ",";

                        TramaComChecksum = TRAMA_ENVIO + Crc16Ccitt(TRAMA_ENVIO);
                        LOG_TXT("Envio de comando de ajuste WGAIN: " + TramaComChecksum);
                        break;
                    case (Int16)Enum_AjusteAtual.Ajustando_Fase:
                        TRAMA_ENVIO = (Int16)Identificador.ID_AjusteFase + "," + txtErrowGain.Text + ",";

                        TramaComChecksum = TRAMA_ENVIO + Crc16Ccitt(TRAMA_ENVIO);
                        LOG_TXT("Envio de comando de ajuste WGAIN: " + TramaComChecksum);
                        break;
                    default:

                        break;
                }                
                PortaSerial.Write(TramaComChecksum + "\0");                
                PortaSerial.DiscardOutBuffer();

                btnAjustar.Enabled = false;
                btnAtualizar.Enabled = false;
                btnAnterior.Enabled = false;
                btnProximo.Enabled = false;
            }
            else
            {
                //LOG_TXT("Comando para comando de de ajuste manual de registradores do ADE não enviado devido porta serial fechada!");
            }
        }

        private void AjusteRMS_Load(object sender, EventArgs e)
        {
            gpxEscritaWgain.Visible = false;

            lblAguarde.Visible = false;

            txtInfo.Text = "Utilizando a MTU, aplique 240Vrms e 5Arms nos respectivos canais. Utilize FP = 1.\r\n" +
                            "Por comodidade, podemos conectar todos os canais de tensão em paralelo e todos os canais de corrente em série.\r\n" +
                            "Após a estabilização das tensões e correntes, digite os respectivos valores nos campos indicados e clique em \"Ajustar\".\r\n" +
                            "Você pode repetir o ajuste quantas vezes julgar necessário.\r\n" +
                            "Utilize o botão \"Atualizar\" para conferir se os valores lidos são iguais aos informados pela MTU.\r\n" +
                            "Após finalizar, clique em \"Próximo\".";            

            AtualizarPortas();
            LerInformacoesSerialSalva();
            if(!PortaSerial.IsOpen)
            {
                btnConectarSerial_Click(btnConectarSerial, EventArgs.Empty);
            }

            string TRAMA_ENVIO = (int)Identificador.ID_AjustarADE + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Crc16Ccitt(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                //LOG_TXT("Envio de comando Ajustar ADE: " + TramaComChecksum);

                PortaSerial.DiscardInBuffer();
            }
            else
            {
                //LOG_TXT("Comando para Ajsutar ADE não enviado devido porta serial fechada!");
            }
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

            if (cbxPorta.Items.Count > 0)
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

        void IdentificarPacote(string Pacote)
        {    
            string[] partes = Pacote.Split(',');
            Int16 codigoErro;
            String frase = string.Empty;

            if (partes.Length > 0)
            {
                switch (int.Parse(partes[0]))//Partes 0 contém o identificador!!
                {
                    #region Leitura dos valores Digitais e RMS das tensões e correntes
                    case (int)Identificador.ID_Resposta_RMS_Digitais:
                        txtV1Lido.Invoke(new Action(() =>
                        {   
                            txtV1Lido.Text = partes[7] + "A";
                            txtV2Lido.Text = partes[8] + "A";
                            txtV3Lido.Text = partes[9] + "A";

                            txtI1Lido.Text = partes[10] + "A";
                            txtI2Lido.Text = partes[11] + "A";
                            txtI3Lido.Text = partes[12] + "A";
                        }));
                        break;
                    #endregion

                    #region Erro de corrente invertida
                    case (int)Identificador.ID_ERRO_CorrenteInvertida:
                        codigoErro = Int16.Parse(partes[1]);
                        frase = string.Empty;

                        frase = "Verifique a conexão das correntes!\n\n";

                        if((codigoErro & 1) == 1)
                        {
                            frase += "Canal 1 INVERTIDO\n";
                        }
                        if ((codigoErro & 2) == 2)
                        {
                            frase += "Canal 2 INVERTIDO\n";
                        }
                        if ((codigoErro & 4) == 4)
                        {
                            frase += "Canal 3 INVERTIDO\n";
                        }
                        MessageBox.Show(frase, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        break;
                    #endregion

                    #region Progresso de ajuste
                    case (Int16)Identificador.ID_Progresso_AJuste:
                        pgbProgressoAjuste.Invoke(new Action(() =>
                        {
                            lblAguarde.Visible = false;
                            if (Int16.Parse(partes[1]) < 200)
                                pgbProgressoAjuste.Value = Int16.Parse(partes[1]);
                            else
                            {
                                btnAjustar.Enabled = true;
                                btnAtualizar.Enabled = true;
                                btnAnterior.Enabled = true;
                                btnProximo.Enabled = true;
                                pgbProgressoAjuste.Value = 0;
                            }                                
                        }));                        
                       
                        break;
                    #endregion

                    #region Erro de RMS Nominal
                    case (int)Identificador.ID_ERRO_RMS_Nominal:
                        codigoErro = Int16.Parse(partes[1]);
                        frase = string.Empty;

                        frase = "Verifique se os valores de corrente e tensão aplicados estão dentro da faixa de +-20% dos valores nominais.\n\n";

                        if ((codigoErro & 1) == 1)
                        {
                            frase += "Tensão 1 fora da margem de erro\n";
                        }
                        if ((codigoErro & 2) == 2)
                        {
                            frase += "Tensão 2 fora da margem de erro\n";
                        }
                        if ((codigoErro & 4) == 4)
                        {
                            frase += "Tensão 3 fora da margem de erro\n";
                        }
                        if ((codigoErro & 8) == 8)
                        {
                            frase += "Corrente 1 fora da margem de erro\n";
                        }
                        if ((codigoErro & 16) == 16)
                        {
                            frase += "Corrente 2 fora da margem de erro\n";
                        }
                        if ((codigoErro & 32) == 32)
                        {
                            frase += "Corrente 3 fora da margem de erro\n";
                        }
                        MessageBox.Show(frase, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        btnAjustar.Invoke(new Action(() =>
                        {
                            btnAjustar.Enabled = true;
                            btnAtualizar.Enabled = true;
                            btnAnterior.Enabled = true;
                            btnProximo.Enabled = true;
                            pgbProgressoAjuste.Value = 0;
                        }));    
                        break;
                    #endregion

                    #region Indicação de fase em ajuste
                    case (Int16)Identificador.ID_Indicar_FaseEmAjuste:
                        lblFaseEmAjusteWgain.Invoke(new Action(() =>
                        {
                            if(Int16.Parse(partes[1]) >= 0)
                            {
                                lblFaseEmAjusteWgain.Text = "Insira o erro da FASE " + (Int16.Parse(partes[1]) + 1).ToString();
                                pgbProgressoAjuste.Value = Int16.Parse(partes[1]) * 33;
                            }
                            else
                            {
                                lblFaseEmAjusteWgain.Text = "Ajuste Finalizado!";
                                pgbProgressoAjuste.Value = 0;
                            }                            

                            btnAjustar.Enabled = true;
                            btnAtualizar.Enabled = true;
                            btnAnterior.Enabled = true;
                            btnProximo.Enabled = true;
                            lblAguarde.Visible = false;
                        }));
                        break;
                    #endregion

                    default:
                        break;
                }
            }
        }

        private void btnRefreshSerial_Click(object sender, EventArgs e)
        {
            AtualizarPortas();
            LerInformacoesSerialSalva();
        }

        private ushort Crc16Ccitt(String trama)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(trama);
            const ushort poly = 4129;
            ushort[] table = new ushort[256];
            ushort initialValue = 0xffff;
            ushort temp, a;
            ushort crc = initialValue;
            for (int i = 0; i < table.Length; ++i)
            {
                temp = 0;
                a = (ushort)(i << 8);
                for (int j = 0; j < 8; ++j)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                        temp = (ushort)((temp << 1) ^ poly);
                    else
                        temp <<= 1;
                    a <<= 1;
                }
                table[i] = temp;
            }
            for (int i = 0; i < bytes.Length; ++i)
            {
                crc = (ushort)((crc << 8) ^ table[((crc >> 8) ^ (0xff & bytes[i]))]);
            }
            return crc;
        }

        private void PortaSerial_DadoRecebido(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string DadoRecebido = PortaSerial.ReadLine();
                Console.WriteLine("Dado recebido: " + DadoRecebido);
                //LOG_TXT("Dado Recebido: " + DadoRecebido);

                PortaSerial.DiscardInBuffer();

                int IndexVirgulaCS = DadoRecebido.LastIndexOf(',');
                int IndexfinalCS = DadoRecebido.LastIndexOf('\r');
                string TramaSemCS = string.Empty;
                ushort CS_rec = 0;

                if (IndexVirgulaCS != -1)
                {
                    TramaSemCS = DadoRecebido.Substring(0, IndexVirgulaCS) + ",";
                    CS_rec = ushort.Parse(DadoRecebido.Substring(IndexVirgulaCS + 1, (IndexfinalCS - IndexVirgulaCS - 1)));

                    //LOG_TXT("Trama Recebida sem CS: " + TramaSemCS);
                    //LOG_TXT("CS da Trama recebida: " + CS_rec);

                    ushort CS_calc = Crc16Ccitt(TramaSemCS);

                    if (CS_calc == CS_rec)
                    {
                        IdentificarPacote(TramaSemCS);
                    }
                    else
                    {
                        LOG_TXT("Checsum da trama recebida estava incorreto!");
                        LOG_TXT("Calc: |" + CS_calc + "| " + "Recebido: |" + CS_rec + "|");
                    }
                }
                else
                {
                    //LOG_TXT("A trama recebida não continha ','");
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnConectarSerial_Click(object sender, EventArgs e)
        {
            if (!PortaSerial.IsOpen)
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

                            //LOG_TXT("Porta serial aberta com sucesso!");

                            //MessageBox.Show("Tudo certo com a porta serial selecionada!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch (Exception ex)
                        {
                            //LOG_TXT("Erro ao abrir porta serial!");
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
                    //Salvar_Dados_Serial();
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

        private void cbkReplicarI1_CheckedChanged(object sender, EventArgs e)
        {
            txtI2RMS.ReadOnly = txtI3RMS.ReadOnly = cbkReplicarI1.Checked;
            txtI3RMS.Text = txtI2RMS.Text = txtI1RMS.Text;
        }

        private void txtV1RMS_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtV2RMS_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtV3RMS_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtI1RMS_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtI2RMS_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtI3RMS_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {   
            string TRAMA_ENVIO = (int)Identificador.ID_resposta_RMS + ",";

            if (PortaSerial.IsOpen)
            {
                String TramaComChecksum = TRAMA_ENVIO + Crc16Ccitt(TRAMA_ENVIO);
                PortaSerial.Write(TramaComChecksum + "\0");
                LOG_TXT("Envio de comando de ajuste manual de registradores do ADE: " + TramaComChecksum);
                PortaSerial.DiscardOutBuffer();
            }
            else
            {
                //LOG_TXT("Comando para comando de de ajuste manual de registradores do ADE não enviado devido porta serial fechada!");
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            String texto, TRAMA_ENVIO;
            AjusteAtual++;
            if (AjusteAtual > 2)
                AjusteAtual = 0;

            switch (AjusteAtual)
            {
                case (int)Enum_AjusteAtual.Ajustando_VRMS_IRMS:
                    gpxEscritaWgain.Enabled = false;
                    gpxEscritaWgain.Visible = false;

                    gpxEscritaRMS.Enabled = true;
                    gpxEscritaRMS.Visible = true;  
                    
                    gpxLeituraRMS.Enabled = true;
                    gpxLeituraRMS.Visible = true;

                    texto = "Utilizando a MTU, aplique 240Vrms e 5Arms nos respectivos canais. Utilize FP = 1.\r\n" +
                            "Por comodidade, podemos conectar todos os canais de tensão em paralelo e todos os canais de corrente em série.\r\n" +
                            "Após a estabilização das tensões e correntes, digite os respectivos valores nos campos indicados e clique em \"Ajustar\".\r\n" +
                            "Você pode repetir o ajuste quantas vezes julgar necessário.\r\n" +
                            "Utilize o botão \"Atualizar\" para conferir se os valores lidos são iguais aos informados pela MTU.\r\n" +
                            "Após finalizar, clique em \"Próximo\".";
                    txtInfo.Text = texto;
                    gpxAjuste.Text = "Ajsute do ganho RMS";
                    break;                
                case (int)Enum_AjusteAtual.Ajustando_WGAIN:
                    gpxEscritaRMS.Enabled = false;
                    gpxEscritaRMS.Visible = false;

                    gpxLeituraRMS.Enabled = false;
                    gpxLeituraRMS.Visible = false;

                    gpxEscritaWgain.Enabled = true;
                    gpxEscritaWgain.Visible = true;
                    texto = "Utilizando a MTU, aplique 240Vrms e 5Arms nos respectivos canais. Utilize FP = 1.\r\n" +
                            "Por comodidade, podemos conectar todos os canais de tensão em paralelo e todos os canais de corrente em série.\r\n" +
                            "Utilizando um cabo para leitura de pulsos, conecte a saída de pulsos da FTM na entrada de pulsos da MTU.\r\n" +
                            "Ajuste a constante de pulso da MTU para 8400 pulsos/kWh.\r\n" +
                            "O Ajuste será realizado em cada fase individulmente, desta forma, será solicitado que você entre 3 vezes com o erro, sendo uma vez para cada fase.\r\n" +
                            "Aguarde até que o desvio padrão se aproxime o máximo de 0 e clique em \"Ajustar\".\r\n" +
                            "É recomendado configurar a MTU para capturar ao menos 10 pulsos por erro.";
                    txtInfo.Text = texto;
                    gpxAjuste.Text = "Ajsute do ganho de potencia ativa";
                    lblFaseEmAjusteWgain.Text = "Aguarde...";

                    TRAMA_ENVIO = (int)Identificador.ID_InicioAjusteWGAIN + ",";

                    if (PortaSerial.IsOpen)
                    {
                        String TramaComChecksum = TRAMA_ENVIO + Crc16Ccitt(TRAMA_ENVIO);
                        PortaSerial.Write(TramaComChecksum + "\0");
                        LOG_TXT("Envio de comando de inicio de ajuste de WGAIN: " + TramaComChecksum);
                        PortaSerial.DiscardOutBuffer();
                    }
                    else
                    {
                        //LOG_TXT("Comando para comando de de ajuste manual de registradores do ADE não enviado devido porta serial fechada!");
                    }

                    break;
                case (int)Enum_AjusteAtual.Ajustando_Fase:
                    gpxEscritaRMS.Enabled = false;
                    gpxEscritaRMS.Visible = false;

                    gpxLeituraRMS.Enabled = false;
                    gpxLeituraRMS.Visible = false;

                    gpxEscritaWgain.Enabled = true;
                    gpxEscritaWgain.Visible = true;
                    texto = "Utilizando a MTU, aplique 240Vrms e 5Arms nos respectivos canais. Utilize FP = 0.5.\r\n" +
                            "Por comodidade, podemos conectar todos os canais de tensão em paralelo e todos os canais de corrente em série.\r\n" +
                            "Utilizando um cabo para leitura de pulsos, conecte a saída de pulsos da FTM na entrada de pulsos da MTU.\r\n" +
                            "Ajuste a constante de pulso da MTU para 8400 pulsos/kWh.\r\n" +
                            "O Ajuste será realizado em cada fase individulmente, desta forma, será solicitado que você entre 3 vezes com o erro, sendo uma vez para cada fase.\r\n" +
                            "Aguarde até que o desvio padrão se aproxime o máximo de 0 e clique em \"Ajustar\".\r\n" +
                            "É recomendado configurar a MTU para capturar ao menos 10 pulsos por erro.";
                    txtInfo.Text = texto;

                    gpxAjuste.Text = "Ajsute de fase";
                    lblFaseEmAjusteWgain.Text = "Aguarde...";

                    TRAMA_ENVIO = (int)Identificador.ID_Inicio_AjusteDeFase + ",";

                    if (PortaSerial.IsOpen)
                    {
                        String TramaComChecksum = TRAMA_ENVIO + Crc16Ccitt(TRAMA_ENVIO);
                        PortaSerial.Write(TramaComChecksum + "\0");
                        LOG_TXT("Envio de comando de inicio de ajuste de fase: " + TramaComChecksum);
                        PortaSerial.DiscardOutBuffer();
                    }
                    else
                    {
                        //LOG_TXT("Comando para comando de de ajuste manual de registradores do ADE não enviado devido porta serial fechada!");
                    }
                    break;
                default:
                    break;
            }            
        }

        private void AjusteRMS_FormClosing(object sender, FormClosingEventArgs e)
        {
            PortaSerial.Close();
        }
    }
}
