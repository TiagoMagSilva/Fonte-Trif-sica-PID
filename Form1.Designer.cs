
namespace FonteTrifasicaPID
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chartTensao = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKpTensao = new System.Windows.Forms.TextBox();
            this.txtKiTensao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKdTensao = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAplicarPIDTensao = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtKpCorrente = new System.Windows.Forms.TextBox();
            this.btnAplicarPIDCorrente = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKdCorrente = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKiCorrente = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnLimparGraficoTensao = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtTensãoRMSC = new System.Windows.Forms.TextBox();
            this.txtTensãoRMSB = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtTensãoRMSA = new System.Windows.Forms.TextBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTempAcomodTensaoC = new System.Windows.Forms.TextBox();
            this.txtUndershootTensaoC = new System.Windows.Forms.TextBox();
            this.txtOvershootTensaoC = new System.Windows.Forms.TextBox();
            this.txtTempAcomodTensaoB = new System.Windows.Forms.TextBox();
            this.txtUndershootTensaoB = new System.Windows.Forms.TextBox();
            this.txtOvershootTensaoB = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTempAcomodTensaoA = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtUndershootTensaoA = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOvershootTensaoA = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnLimparGraficoCorrente = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtCorrenteRMSC = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtCorrenteRMSB = new System.Windows.Forms.TextBox();
            this.txtCorrenteRMSA = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.txtTempAcomodCorrenteC = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtUnderShootCorrenteC = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtOvershootCorrenteC = new System.Windows.Forms.TextBox();
            this.txtTempAcomodCorrenteB = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtUnderShootCorrenteB = new System.Windows.Forms.TextBox();
            this.txtOvershootCorrenteA = new System.Windows.Forms.TextBox();
            this.txtOvershootCorrenteB = new System.Windows.Forms.TextBox();
            this.txtUnderShootCorrenteA = new System.Windows.Forms.TextBox();
            this.txtTempAcomodCorrenteA = new System.Windows.Forms.TextBox();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRefreshSerial = new System.Windows.Forms.Button();
            this.cbxPorta = new System.Windows.Forms.ComboBox();
            this.btnConectarSerial = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.cbxFatorDePotencia = new System.Windows.Forms.ComboBox();
            this.cbxFrequencia = new System.Windows.Forms.ComboBox();
            this.txtTensãoRMS = new System.Windows.Forms.TextBox();
            this.txtCorrenteRMS = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.cbxFase = new System.Windows.Forms.ComboBox();
            this.btnIniciarSintetização = new System.Windows.Forms.Button();
            this.btnPararSintetização = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartTensao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartTensao
            // 
            chartArea1.Name = "ChartArea1";
            this.chartTensao.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartTensao.Legends.Add(legend1);
            this.chartTensao.Location = new System.Drawing.Point(6, 19);
            this.chartTensao.Name = "chartTensao";
            this.chartTensao.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.Red;
            series1.Legend = "Legend1";
            series1.LegendText = "Tensão A";
            series1.MarkerColor = System.Drawing.Color.Red;
            series1.Name = "TensaoA";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.Yellow;
            series2.Legend = "Legend1";
            series2.LegendText = "Tensão B";
            series2.MarkerColor = System.Drawing.Color.Yellow;
            series2.Name = "TensaoB";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Color = System.Drawing.Color.Blue;
            series3.Legend = "Legend1";
            series3.LegendText = "Tensão C";
            series3.MarkerColor = System.Drawing.Color.Blue;
            series3.Name = "TensaoC";
            this.chartTensao.Series.Add(series1);
            this.chartTensao.Series.Add(series2);
            this.chartTensao.Series.Add(series3);
            this.chartTensao.Size = new System.Drawing.Size(565, 302);
            this.chartTensao.TabIndex = 0;
            this.chartTensao.Text = "Tensão";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title1";
            title1.Text = "Tensão";
            this.chartTensao.Titles.Add(title1);
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(6, 19);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.LegendText = "Corrente A";
            series4.MarkerColor = System.Drawing.Color.Red;
            series4.Name = "CorrenteA";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Legend = "Legend1";
            series5.LegendText = "Corrente B";
            series5.MarkerColor = System.Drawing.Color.Yellow;
            series5.Name = "CorrenteB";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Legend = "Legend1";
            series6.LegendText = "Corrente C";
            series6.MarkerColor = System.Drawing.Color.Blue;
            series6.Name = "CorrenteC";
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(565, 302);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "Tensão";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            title2.Name = "Title1";
            title2.Text = "Corrente";
            this.chart1.Titles.Add(title2);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kp";
            // 
            // txtKpTensao
            // 
            this.txtKpTensao.Location = new System.Drawing.Point(39, 19);
            this.txtKpTensao.Name = "txtKpTensao";
            this.txtKpTensao.Size = new System.Drawing.Size(48, 20);
            this.txtKpTensao.TabIndex = 3;
            this.txtKpTensao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKpTensao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKpTensao_KeyPress);
            // 
            // txtKiTensao
            // 
            this.txtKiTensao.Location = new System.Drawing.Point(39, 45);
            this.txtKiTensao.Name = "txtKiTensao";
            this.txtKiTensao.Size = new System.Drawing.Size(48, 20);
            this.txtKiTensao.TabIndex = 5;
            this.txtKiTensao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKiTensao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKiTensao_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ki";
            // 
            // txtKdTensao
            // 
            this.txtKdTensao.Location = new System.Drawing.Point(39, 71);
            this.txtKdTensao.Name = "txtKdTensao";
            this.txtKdTensao.Size = new System.Drawing.Size(48, 20);
            this.txtKdTensao.TabIndex = 7;
            this.txtKdTensao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKdTensao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKdTensao_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Kd";
            // 
            // btnAplicarPIDTensao
            // 
            this.btnAplicarPIDTensao.Location = new System.Drawing.Point(93, 19);
            this.btnAplicarPIDTensao.Name = "btnAplicarPIDTensao";
            this.btnAplicarPIDTensao.Size = new System.Drawing.Size(75, 72);
            this.btnAplicarPIDTensao.TabIndex = 8;
            this.btnAplicarPIDTensao.Text = "Aplicar";
            this.btnAplicarPIDTensao.UseVisualStyleBackColor = true;
            this.btnAplicarPIDTensao.Click += new System.EventHandler(this.btnAplicarPIDTensao_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtKpTensao);
            this.groupBox1.Controls.Add(this.btnAplicarPIDTensao);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtKdTensao);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtKiTensao);
            this.groupBox1.Location = new System.Drawing.Point(577, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 100);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controlador PID (Tensão)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtKpCorrente);
            this.groupBox2.Controls.Add(this.btnAplicarPIDCorrente);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtKdCorrente);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtKiCorrente);
            this.groupBox2.Location = new System.Drawing.Point(577, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(179, 100);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controlador PID (Corrente)";
            // 
            // txtKpCorrente
            // 
            this.txtKpCorrente.Location = new System.Drawing.Point(39, 19);
            this.txtKpCorrente.Name = "txtKpCorrente";
            this.txtKpCorrente.Size = new System.Drawing.Size(48, 20);
            this.txtKpCorrente.TabIndex = 3;
            this.txtKpCorrente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKpCorrente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKpCorrente_KeyPress);
            // 
            // btnAplicarPIDCorrente
            // 
            this.btnAplicarPIDCorrente.Location = new System.Drawing.Point(93, 19);
            this.btnAplicarPIDCorrente.Name = "btnAplicarPIDCorrente";
            this.btnAplicarPIDCorrente.Size = new System.Drawing.Size(75, 72);
            this.btnAplicarPIDCorrente.TabIndex = 8;
            this.btnAplicarPIDCorrente.Text = "Aplicar";
            this.btnAplicarPIDCorrente.UseVisualStyleBackColor = true;
            this.btnAplicarPIDCorrente.Click += new System.EventHandler(this.btnAplicarPIDCorrente_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Kp";
            // 
            // txtKdCorrente
            // 
            this.txtKdCorrente.Location = new System.Drawing.Point(39, 71);
            this.txtKdCorrente.Name = "txtKdCorrente";
            this.txtKdCorrente.Size = new System.Drawing.Size(48, 20);
            this.txtKdCorrente.TabIndex = 7;
            this.txtKdCorrente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKdCorrente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKdCorrente_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ki";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Kd";
            // 
            // txtKiCorrente
            // 
            this.txtKiCorrente.Location = new System.Drawing.Point(39, 45);
            this.txtKiCorrente.Name = "txtKiCorrente";
            this.txtKiCorrente.Size = new System.Drawing.Size(48, 20);
            this.txtKiCorrente.TabIndex = 5;
            this.txtKiCorrente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKiCorrente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKiCorrente_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnLimparGraficoTensao);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.chartTensao);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(880, 332);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tensão";
            // 
            // btnLimparGraficoTensao
            // 
            this.btnLimparGraficoTensao.Location = new System.Drawing.Point(470, 292);
            this.btnLimparGraficoTensao.Name = "btnLimparGraficoTensao";
            this.btnLimparGraficoTensao.Size = new System.Drawing.Size(91, 23);
            this.btnLimparGraficoTensao.TabIndex = 11;
            this.btnLimparGraficoTensao.Text = "Limpar Gráfico";
            this.btnLimparGraficoTensao.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtTensãoRMSC);
            this.groupBox6.Controls.Add(this.txtTensãoRMSB);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.txtTensãoRMSA);
            this.groupBox6.Controls.Add(this.checkBox3);
            this.groupBox6.Controls.Add(this.checkBox2);
            this.groupBox6.Controls.Add(this.checkBox1);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.txtTempAcomodTensaoC);
            this.groupBox6.Controls.Add(this.txtUndershootTensaoC);
            this.groupBox6.Controls.Add(this.txtOvershootTensaoC);
            this.groupBox6.Controls.Add(this.txtTempAcomodTensaoB);
            this.groupBox6.Controls.Add(this.txtUndershootTensaoB);
            this.groupBox6.Controls.Add(this.txtOvershootTensaoB);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.txtTempAcomodTensaoA);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.txtUndershootTensaoA);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.txtOvershootTensaoA);
            this.groupBox6.Location = new System.Drawing.Point(577, 125);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(292, 196);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Resposta";
            // 
            // txtTensãoRMSC
            // 
            this.txtTensãoRMSC.Enabled = false;
            this.txtTensãoRMSC.Location = new System.Drawing.Point(221, 105);
            this.txtTensãoRMSC.Name = "txtTensãoRMSC";
            this.txtTensãoRMSC.Size = new System.Drawing.Size(60, 20);
            this.txtTensãoRMSC.TabIndex = 29;
            this.txtTensãoRMSC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTensãoRMSB
            // 
            this.txtTensãoRMSB.Enabled = false;
            this.txtTensãoRMSB.Location = new System.Drawing.Point(155, 105);
            this.txtTensãoRMSB.Name = "txtTensãoRMSB";
            this.txtTensãoRMSB.Size = new System.Drawing.Size(60, 20);
            this.txtTensãoRMSB.TabIndex = 28;
            this.txtTensãoRMSB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(17, 108);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(70, 13);
            this.label21.TabIndex = 27;
            this.label21.Text = "Tensão RMS";
            // 
            // txtTensãoRMSA
            // 
            this.txtTensãoRMSA.Enabled = false;
            this.txtTensãoRMSA.Location = new System.Drawing.Point(89, 105);
            this.txtTensãoRMSA.Name = "txtTensãoRMSA";
            this.txtTensãoRMSA.Size = new System.Drawing.Size(60, 20);
            this.txtTensãoRMSA.TabIndex = 26;
            this.txtTensãoRMSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(162, 173);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 17);
            this.checkBox3.TabIndex = 25;
            this.checkBox3.Text = "Tensão C";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(84, 173);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 17);
            this.checkBox2.TabIndex = 24;
            this.checkBox2.Text = "Tensão B";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 173);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 17);
            this.checkBox1.TabIndex = 23;
            this.checkBox1.Text = "Tensão A";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Blue;
            this.label17.Location = new System.Drawing.Point(239, 11);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(22, 13);
            this.label17.TabIndex = 22;
            this.label17.Text = "Vc";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Yellow;
            this.label16.Location = new System.Drawing.Point(174, 11);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(22, 13);
            this.label16.TabIndex = 21;
            this.label16.Text = "Vb";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(107, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(22, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "Va";
            // 
            // txtTempAcomodTensaoC
            // 
            this.txtTempAcomodTensaoC.Enabled = false;
            this.txtTempAcomodTensaoC.Location = new System.Drawing.Point(221, 79);
            this.txtTempAcomodTensaoC.Name = "txtTempAcomodTensaoC";
            this.txtTempAcomodTensaoC.Size = new System.Drawing.Size(60, 20);
            this.txtTempAcomodTensaoC.TabIndex = 19;
            this.txtTempAcomodTensaoC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtUndershootTensaoC
            // 
            this.txtUndershootTensaoC.Enabled = false;
            this.txtUndershootTensaoC.Location = new System.Drawing.Point(221, 53);
            this.txtUndershootTensaoC.Name = "txtUndershootTensaoC";
            this.txtUndershootTensaoC.Size = new System.Drawing.Size(60, 20);
            this.txtUndershootTensaoC.TabIndex = 18;
            this.txtUndershootTensaoC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOvershootTensaoC
            // 
            this.txtOvershootTensaoC.Enabled = false;
            this.txtOvershootTensaoC.Location = new System.Drawing.Point(221, 27);
            this.txtOvershootTensaoC.Name = "txtOvershootTensaoC";
            this.txtOvershootTensaoC.Size = new System.Drawing.Size(60, 20);
            this.txtOvershootTensaoC.TabIndex = 17;
            this.txtOvershootTensaoC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTempAcomodTensaoB
            // 
            this.txtTempAcomodTensaoB.Enabled = false;
            this.txtTempAcomodTensaoB.Location = new System.Drawing.Point(155, 79);
            this.txtTempAcomodTensaoB.Name = "txtTempAcomodTensaoB";
            this.txtTempAcomodTensaoB.Size = new System.Drawing.Size(60, 20);
            this.txtTempAcomodTensaoB.TabIndex = 16;
            this.txtTempAcomodTensaoB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtUndershootTensaoB
            // 
            this.txtUndershootTensaoB.Enabled = false;
            this.txtUndershootTensaoB.Location = new System.Drawing.Point(155, 53);
            this.txtUndershootTensaoB.Name = "txtUndershootTensaoB";
            this.txtUndershootTensaoB.Size = new System.Drawing.Size(60, 20);
            this.txtUndershootTensaoB.TabIndex = 15;
            this.txtUndershootTensaoB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOvershootTensaoB
            // 
            this.txtOvershootTensaoB.Enabled = false;
            this.txtOvershootTensaoB.Location = new System.Drawing.Point(155, 27);
            this.txtOvershootTensaoB.Name = "txtOvershootTensaoB";
            this.txtOvershootTensaoB.Size = new System.Drawing.Size(60, 20);
            this.txtOvershootTensaoB.TabIndex = 14;
            this.txtOvershootTensaoB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Temp. Acom.";
            // 
            // txtTempAcomodTensaoA
            // 
            this.txtTempAcomodTensaoA.Enabled = false;
            this.txtTempAcomodTensaoA.Location = new System.Drawing.Point(89, 79);
            this.txtTempAcomodTensaoA.Name = "txtTempAcomodTensaoA";
            this.txtTempAcomodTensaoA.Size = new System.Drawing.Size(60, 20);
            this.txtTempAcomodTensaoA.TabIndex = 12;
            this.txtTempAcomodTensaoA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "UnderShoot";
            // 
            // txtUndershootTensaoA
            // 
            this.txtUndershootTensaoA.Enabled = false;
            this.txtUndershootTensaoA.Location = new System.Drawing.Point(89, 53);
            this.txtUndershootTensaoA.Name = "txtUndershootTensaoA";
            this.txtUndershootTensaoA.Size = new System.Drawing.Size(60, 20);
            this.txtUndershootTensaoA.TabIndex = 10;
            this.txtUndershootTensaoA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "OverShoot";
            // 
            // txtOvershootTensaoA
            // 
            this.txtOvershootTensaoA.Enabled = false;
            this.txtOvershootTensaoA.Location = new System.Drawing.Point(89, 27);
            this.txtOvershootTensaoA.Name = "txtOvershootTensaoA";
            this.txtOvershootTensaoA.Size = new System.Drawing.Size(60, 20);
            this.txtOvershootTensaoA.TabIndex = 4;
            this.txtOvershootTensaoA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnLimparGraficoCorrente);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.chart1);
            this.groupBox4.Location = new System.Drawing.Point(12, 350);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(880, 329);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Corrente";
            // 
            // btnLimparGraficoCorrente
            // 
            this.btnLimparGraficoCorrente.Location = new System.Drawing.Point(470, 292);
            this.btnLimparGraficoCorrente.Name = "btnLimparGraficoCorrente";
            this.btnLimparGraficoCorrente.Size = new System.Drawing.Size(91, 23);
            this.btnLimparGraficoCorrente.TabIndex = 12;
            this.btnLimparGraficoCorrente.Text = "Limpar Gráfico";
            this.btnLimparGraficoCorrente.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtCorrenteRMSC);
            this.groupBox7.Controls.Add(this.label22);
            this.groupBox7.Controls.Add(this.txtCorrenteRMSB);
            this.groupBox7.Controls.Add(this.txtCorrenteRMSA);
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Controls.Add(this.checkBox4);
            this.groupBox7.Controls.Add(this.label19);
            this.groupBox7.Controls.Add(this.checkBox5);
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.Controls.Add(this.checkBox6);
            this.groupBox7.Controls.Add(this.txtTempAcomodCorrenteC);
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.txtUnderShootCorrenteC);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.txtOvershootCorrenteC);
            this.groupBox7.Controls.Add(this.txtTempAcomodCorrenteB);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.txtUnderShootCorrenteB);
            this.groupBox7.Controls.Add(this.txtOvershootCorrenteA);
            this.groupBox7.Controls.Add(this.txtOvershootCorrenteB);
            this.groupBox7.Controls.Add(this.txtUnderShootCorrenteA);
            this.groupBox7.Controls.Add(this.txtTempAcomodCorrenteA);
            this.groupBox7.Location = new System.Drawing.Point(577, 125);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(292, 196);
            this.groupBox7.TabIndex = 14;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Resposta";
            // 
            // txtCorrenteRMSC
            // 
            this.txtCorrenteRMSC.Enabled = false;
            this.txtCorrenteRMSC.Location = new System.Drawing.Point(221, 109);
            this.txtCorrenteRMSC.Name = "txtCorrenteRMSC";
            this.txtCorrenteRMSC.Size = new System.Drawing.Size(60, 20);
            this.txtCorrenteRMSC.TabIndex = 41;
            this.txtCorrenteRMSC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(9, 112);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(74, 13);
            this.label22.TabIndex = 38;
            this.label22.Text = "Corrente RMS";
            // 
            // txtCorrenteRMSB
            // 
            this.txtCorrenteRMSB.Enabled = false;
            this.txtCorrenteRMSB.Location = new System.Drawing.Point(155, 109);
            this.txtCorrenteRMSB.Name = "txtCorrenteRMSB";
            this.txtCorrenteRMSB.Size = new System.Drawing.Size(60, 20);
            this.txtCorrenteRMSB.TabIndex = 40;
            this.txtCorrenteRMSB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCorrenteRMSA
            // 
            this.txtCorrenteRMSA.Enabled = false;
            this.txtCorrenteRMSA.Location = new System.Drawing.Point(89, 109);
            this.txtCorrenteRMSA.Name = "txtCorrenteRMSA";
            this.txtCorrenteRMSA.Size = new System.Drawing.Size(60, 20);
            this.txtCorrenteRMSA.TabIndex = 39;
            this.txtCorrenteRMSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Blue;
            this.label18.Location = new System.Drawing.Point(239, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(18, 13);
            this.label18.TabIndex = 37;
            this.label18.Text = "Ic";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(162, 173);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(76, 17);
            this.checkBox4.TabIndex = 28;
            this.checkBox4.Text = "Corrente C";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Yellow;
            this.label19.Location = new System.Drawing.Point(174, 15);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(18, 13);
            this.label19.TabIndex = 36;
            this.label19.Text = "Ib";
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(84, 173);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(76, 17);
            this.checkBox5.TabIndex = 27;
            this.checkBox5.Text = "Corrente B";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(107, 15);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(18, 13);
            this.label20.TabIndex = 35;
            this.label20.Text = "Ia";
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(6, 173);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(76, 17);
            this.checkBox6.TabIndex = 26;
            this.checkBox6.Text = "Corrente A";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // txtTempAcomodCorrenteC
            // 
            this.txtTempAcomodCorrenteC.Enabled = false;
            this.txtTempAcomodCorrenteC.Location = new System.Drawing.Point(221, 83);
            this.txtTempAcomodCorrenteC.Name = "txtTempAcomodCorrenteC";
            this.txtTempAcomodCorrenteC.Size = new System.Drawing.Size(60, 20);
            this.txtTempAcomodCorrenteC.TabIndex = 34;
            this.txtTempAcomodCorrenteC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Temp. Acom.";
            // 
            // txtUnderShootCorrenteC
            // 
            this.txtUnderShootCorrenteC.Enabled = false;
            this.txtUnderShootCorrenteC.Location = new System.Drawing.Point(221, 57);
            this.txtUnderShootCorrenteC.Name = "txtUnderShootCorrenteC";
            this.txtUnderShootCorrenteC.Size = new System.Drawing.Size(60, 20);
            this.txtUnderShootCorrenteC.TabIndex = 33;
            this.txtUnderShootCorrenteC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "UnderShoot";
            // 
            // txtOvershootCorrenteC
            // 
            this.txtOvershootCorrenteC.Enabled = false;
            this.txtOvershootCorrenteC.Location = new System.Drawing.Point(221, 31);
            this.txtOvershootCorrenteC.Name = "txtOvershootCorrenteC";
            this.txtOvershootCorrenteC.Size = new System.Drawing.Size(60, 20);
            this.txtOvershootCorrenteC.TabIndex = 32;
            this.txtOvershootCorrenteC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTempAcomodCorrenteB
            // 
            this.txtTempAcomodCorrenteB.Enabled = false;
            this.txtTempAcomodCorrenteB.Location = new System.Drawing.Point(155, 83);
            this.txtTempAcomodCorrenteB.Name = "txtTempAcomodCorrenteB";
            this.txtTempAcomodCorrenteB.Size = new System.Drawing.Size(60, 20);
            this.txtTempAcomodCorrenteB.TabIndex = 31;
            this.txtTempAcomodCorrenteB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(25, 34);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 13);
            this.label14.TabIndex = 9;
            this.label14.Text = "OverShoot";
            // 
            // txtUnderShootCorrenteB
            // 
            this.txtUnderShootCorrenteB.Enabled = false;
            this.txtUnderShootCorrenteB.Location = new System.Drawing.Point(155, 57);
            this.txtUnderShootCorrenteB.Name = "txtUnderShootCorrenteB";
            this.txtUnderShootCorrenteB.Size = new System.Drawing.Size(60, 20);
            this.txtUnderShootCorrenteB.TabIndex = 30;
            this.txtUnderShootCorrenteB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOvershootCorrenteA
            // 
            this.txtOvershootCorrenteA.Enabled = false;
            this.txtOvershootCorrenteA.Location = new System.Drawing.Point(89, 31);
            this.txtOvershootCorrenteA.Name = "txtOvershootCorrenteA";
            this.txtOvershootCorrenteA.Size = new System.Drawing.Size(60, 20);
            this.txtOvershootCorrenteA.TabIndex = 26;
            this.txtOvershootCorrenteA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOvershootCorrenteB
            // 
            this.txtOvershootCorrenteB.Enabled = false;
            this.txtOvershootCorrenteB.Location = new System.Drawing.Point(155, 31);
            this.txtOvershootCorrenteB.Name = "txtOvershootCorrenteB";
            this.txtOvershootCorrenteB.Size = new System.Drawing.Size(60, 20);
            this.txtOvershootCorrenteB.TabIndex = 29;
            this.txtOvershootCorrenteB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtUnderShootCorrenteA
            // 
            this.txtUnderShootCorrenteA.Enabled = false;
            this.txtUnderShootCorrenteA.Location = new System.Drawing.Point(89, 57);
            this.txtUnderShootCorrenteA.Name = "txtUnderShootCorrenteA";
            this.txtUnderShootCorrenteA.Size = new System.Drawing.Size(60, 20);
            this.txtUnderShootCorrenteA.TabIndex = 27;
            this.txtUnderShootCorrenteA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTempAcomodCorrenteA
            // 
            this.txtTempAcomodCorrenteA.Enabled = false;
            this.txtTempAcomodCorrenteA.Location = new System.Drawing.Point(89, 83);
            this.txtTempAcomodCorrenteA.Name = "txtTempAcomodCorrenteA";
            this.txtTempAcomodCorrenteA.Size = new System.Drawing.Size(60, 20);
            this.txtTempAcomodCorrenteA.TabIndex = 28;
            this.txtTempAcomodCorrenteA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Items.AddRange(new object[] {
            "",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbxBaudRate.Location = new System.Drawing.Point(15, 111);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(72, 21);
            this.cbxBaudRate.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Baud Rate";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Porta";
            // 
            // btnRefreshSerial
            // 
            this.btnRefreshSerial.Location = new System.Drawing.Point(15, 58);
            this.btnRefreshSerial.Name = "btnRefreshSerial";
            this.btnRefreshSerial.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshSerial.TabIndex = 17;
            this.btnRefreshSerial.Text = "Refresh";
            this.btnRefreshSerial.UseVisualStyleBackColor = true;
            this.btnRefreshSerial.Click += new System.EventHandler(this.btnRefreshSerial_Click);
            // 
            // cbxPorta
            // 
            this.cbxPorta.FormattingEnabled = true;
            this.cbxPorta.Location = new System.Drawing.Point(15, 31);
            this.cbxPorta.Name = "cbxPorta";
            this.cbxPorta.Size = new System.Drawing.Size(72, 21);
            this.cbxPorta.TabIndex = 13;
            // 
            // btnConectarSerial
            // 
            this.btnConectarSerial.Location = new System.Drawing.Point(7, 149);
            this.btnConectarSerial.Name = "btnConectarSerial";
            this.btnConectarSerial.Size = new System.Drawing.Size(84, 23);
            this.btnConectarSerial.TabIndex = 18;
            this.btnConectarSerial.Text = "Conectar";
            this.btnConectarSerial.UseVisualStyleBackColor = true;
            this.btnConectarSerial.Click += new System.EventHandler(this.btnConectarSerial_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnConectarSerial);
            this.groupBox5.Controls.Add(this.cbxPorta);
            this.groupBox5.Controls.Add(this.btnRefreshSerial);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.cbxBaudRate);
            this.groupBox5.Location = new System.Drawing.Point(898, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(97, 184);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Serial";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label27);
            this.groupBox8.Controls.Add(this.cbxFase);
            this.groupBox8.Controls.Add(this.label26);
            this.groupBox8.Controls.Add(this.txtCorrenteRMS);
            this.groupBox8.Controls.Add(this.label25);
            this.groupBox8.Controls.Add(this.txtTensãoRMS);
            this.groupBox8.Controls.Add(this.button1);
            this.groupBox8.Controls.Add(this.cbxFrequencia);
            this.groupBox8.Controls.Add(this.label23);
            this.groupBox8.Controls.Add(this.label24);
            this.groupBox8.Controls.Add(this.cbxFatorDePotencia);
            this.groupBox8.Location = new System.Drawing.Point(898, 202);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(104, 281);
            this.groupBox8.TabIndex = 20;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Parâmetros";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 244);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Aplicar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(12, 19);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(59, 13);
            this.label23.TabIndex = 14;
            this.label23.Text = "Tensão (V)";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(12, 195);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(91, 13);
            this.label24.TabIndex = 16;
            this.label24.Text = "Fator de Potência";
            // 
            // cbxFatorDePotencia
            // 
            this.cbxFatorDePotencia.FormattingEnabled = true;
            this.cbxFatorDePotencia.Items.AddRange(new object[] {
            "1,0",
            "0,5L",
            "0,5C",
            "0,8L",
            "0,8C"});
            this.cbxFatorDePotencia.Location = new System.Drawing.Point(15, 215);
            this.cbxFatorDePotencia.Name = "cbxFatorDePotencia";
            this.cbxFatorDePotencia.Size = new System.Drawing.Size(72, 21);
            this.cbxFatorDePotencia.TabIndex = 15;
            // 
            // cbxFrequencia
            // 
            this.cbxFrequencia.FormattingEnabled = true;
            this.cbxFrequencia.Items.AddRange(new object[] {
            "50",
            "60"});
            this.cbxFrequencia.Location = new System.Drawing.Point(15, 122);
            this.cbxFrequencia.Name = "cbxFrequencia";
            this.cbxFrequencia.Size = new System.Drawing.Size(72, 21);
            this.cbxFrequencia.TabIndex = 13;
            // 
            // txtTensãoRMS
            // 
            this.txtTensãoRMS.Location = new System.Drawing.Point(15, 35);
            this.txtTensãoRMS.Name = "txtTensãoRMS";
            this.txtTensãoRMS.Size = new System.Drawing.Size(72, 20);
            this.txtTensãoRMS.TabIndex = 9;
            this.txtTensãoRMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTensãoRMS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // txtCorrenteRMS
            // 
            this.txtCorrenteRMS.Location = new System.Drawing.Point(15, 79);
            this.txtCorrenteRMS.Name = "txtCorrenteRMS";
            this.txtCorrenteRMS.Size = new System.Drawing.Size(72, 20);
            this.txtCorrenteRMS.TabIndex = 19;
            this.txtCorrenteRMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(12, 63);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(63, 13);
            this.label25.TabIndex = 20;
            this.label25.Text = "Corrente (A)";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(12, 106);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(84, 13);
            this.label26.TabIndex = 22;
            this.label26.Text = "Frequência (HZ)";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(12, 151);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(43, 13);
            this.label27.TabIndex = 24;
            this.label27.Text = "Fase (º)";
            // 
            // cbxFase
            // 
            this.cbxFase.FormattingEnabled = true;
            this.cbxFase.Items.AddRange(new object[] {
            "0º",
            "120º"});
            this.cbxFase.Location = new System.Drawing.Point(15, 167);
            this.cbxFase.Name = "cbxFase";
            this.cbxFase.Size = new System.Drawing.Size(72, 21);
            this.cbxFase.TabIndex = 23;
            // 
            // btnIniciarSintetização
            // 
            this.btnIniciarSintetização.BackColor = System.Drawing.Color.LightGreen;
            this.btnIniciarSintetização.Location = new System.Drawing.Point(12, 98);
            this.btnIniciarSintetização.Name = "btnIniciarSintetização";
            this.btnIniciarSintetização.Size = new System.Drawing.Size(84, 23);
            this.btnIniciarSintetização.TabIndex = 25;
            this.btnIniciarSintetização.Text = "Iniciar";
            this.btnIniciarSintetização.UseVisualStyleBackColor = false;
            // 
            // btnPararSintetização
            // 
            this.btnPararSintetização.BackColor = System.Drawing.Color.Crimson;
            this.btnPararSintetização.Location = new System.Drawing.Point(13, 22);
            this.btnPararSintetização.Name = "btnPararSintetização";
            this.btnPararSintetização.Size = new System.Drawing.Size(84, 63);
            this.btnPararSintetização.TabIndex = 26;
            this.btnPararSintetização.Text = "Parar";
            this.btnPararSintetização.UseVisualStyleBackColor = false;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnPararSintetização);
            this.groupBox9.Controls.Add(this.btnIniciarSintetização);
            this.groupBox9.Location = new System.Drawing.Point(898, 489);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(104, 134);
            this.groupBox9.TabIndex = 27;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Sintetização";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1014, 691);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controlador PID - Fonte trifásica";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartTensao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartTensao;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKpTensao;
        private System.Windows.Forms.TextBox txtKiTensao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKdTensao;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAplicarPIDTensao;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtKpCorrente;
        private System.Windows.Forms.Button btnAplicarPIDCorrente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtKdCorrente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtKiCorrente;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtTempAcomodTensaoC;
        private System.Windows.Forms.TextBox txtUndershootTensaoC;
        private System.Windows.Forms.TextBox txtOvershootTensaoC;
        private System.Windows.Forms.TextBox txtTempAcomodTensaoB;
        private System.Windows.Forms.TextBox txtUndershootTensaoB;
        private System.Windows.Forms.TextBox txtOvershootTensaoB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTempAcomodTensaoA;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtUndershootTensaoA;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOvershootTensaoA;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtTempAcomodCorrenteC;
        private System.Windows.Forms.TextBox txtUnderShootCorrenteC;
        private System.Windows.Forms.TextBox txtOvershootCorrenteC;
        private System.Windows.Forms.TextBox txtTempAcomodCorrenteB;
        private System.Windows.Forms.TextBox txtUnderShootCorrenteB;
        private System.Windows.Forms.TextBox txtOvershootCorrenteA;
        private System.Windows.Forms.TextBox txtOvershootCorrenteB;
        private System.Windows.Forms.TextBox txtUnderShootCorrenteA;
        private System.Windows.Forms.TextBox txtTempAcomodCorrenteA;
        private System.Windows.Forms.TextBox txtTensãoRMSC;
        private System.Windows.Forms.TextBox txtTensãoRMSB;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtTensãoRMSA;
        private System.Windows.Forms.TextBox txtCorrenteRMSC;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtCorrenteRMSB;
        private System.Windows.Forms.TextBox txtCorrenteRMSA;
        private System.Windows.Forms.Button btnLimparGraficoTensao;
        private System.Windows.Forms.Button btnLimparGraficoCorrente;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRefreshSerial;
        private System.Windows.Forms.ComboBox cbxPorta;
        private System.Windows.Forms.Button btnConectarSerial;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cbxFase;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtCorrenteRMS;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtTensãoRMS;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbxFrequencia;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cbxFatorDePotencia;
        private System.Windows.Forms.Button btnIniciarSintetização;
        private System.Windows.Forms.Button btnPararSintetização;
        private System.Windows.Forms.GroupBox groupBox9;
    }
}

