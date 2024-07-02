namespace FonteTrifasicaPID
{
    partial class AjusteRMS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AjusteRMS));
            this.txtV1RMS = new System.Windows.Forms.TextBox();
            this.txtV2RMS = new System.Windows.Forms.TextBox();
            this.txtV3RMS = new System.Windows.Forms.TextBox();
            this.cbkReplicarV1 = new System.Windows.Forms.CheckBox();
            this.gpxAjuste = new System.Windows.Forms.GroupBox();
            this.gpxEscritaWgain = new System.Windows.Forms.GroupBox();
            this.lblFaseEmAjusteWgain = new System.Windows.Forms.Label();
            this.txtErrowGain = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.gpxLeituraRMS = new System.Windows.Forms.GroupBox();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.txtV1Lido = new System.Windows.Forms.TextBox();
            this.txtV2Lido = new System.Windows.Forms.TextBox();
            this.txtI1Lido = new System.Windows.Forms.TextBox();
            this.txtV3Lido = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtI2Lido = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtI3Lido = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnProximo = new System.Windows.Forms.Button();
            this.gpxEscritaRMS = new System.Windows.Forms.GroupBox();
            this.cbkReplicarI1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtI3RMS = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtI2RMS = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtI1RMS = new System.Windows.Forms.TextBox();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.btnAjustar = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnConectarSerial = new System.Windows.Forms.Button();
            this.cbxPorta = new System.Windows.Forms.ComboBox();
            this.btnRefreshSerial = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.pgbProgressoAjuste = new System.Windows.Forms.ProgressBar();
            this.lblAguarde = new System.Windows.Forms.Label();
            this.gpxAjuste.SuspendLayout();
            this.gpxEscritaWgain.SuspendLayout();
            this.gpxLeituraRMS.SuspendLayout();
            this.gpxEscritaRMS.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtV1RMS
            // 
            this.txtV1RMS.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtV1RMS.Location = new System.Drawing.Point(59, 22);
            this.txtV1RMS.Name = "txtV1RMS";
            this.txtV1RMS.Size = new System.Drawing.Size(135, 38);
            this.txtV1RMS.TabIndex = 0;
            this.txtV1RMS.Text = "240.000";
            this.txtV1RMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtV1RMS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtV1RMS_KeyPress);
            // 
            // txtV2RMS
            // 
            this.txtV2RMS.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtV2RMS.Location = new System.Drawing.Point(59, 66);
            this.txtV2RMS.Name = "txtV2RMS";
            this.txtV2RMS.Size = new System.Drawing.Size(135, 38);
            this.txtV2RMS.TabIndex = 1;
            this.txtV2RMS.Text = "240.000";
            this.txtV2RMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtV2RMS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtV2RMS_KeyPress);
            // 
            // txtV3RMS
            // 
            this.txtV3RMS.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtV3RMS.Location = new System.Drawing.Point(59, 110);
            this.txtV3RMS.Name = "txtV3RMS";
            this.txtV3RMS.Size = new System.Drawing.Size(135, 38);
            this.txtV3RMS.TabIndex = 2;
            this.txtV3RMS.Text = "240.000";
            this.txtV3RMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtV3RMS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtV3RMS_KeyPress);
            // 
            // cbkReplicarV1
            // 
            this.cbkReplicarV1.AutoSize = true;
            this.cbkReplicarV1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbkReplicarV1.Location = new System.Drawing.Point(200, 32);
            this.cbkReplicarV1.Name = "cbkReplicarV1";
            this.cbkReplicarV1.Size = new System.Drawing.Size(131, 29);
            this.cbkReplicarV1.TabIndex = 3;
            this.cbkReplicarV1.Text = "Replicar V1";
            this.cbkReplicarV1.UseVisualStyleBackColor = true;
            this.cbkReplicarV1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // gpxAjuste
            // 
            this.gpxAjuste.Controls.Add(this.gpxEscritaWgain);
            this.gpxAjuste.Controls.Add(this.btnAnterior);
            this.gpxAjuste.Controls.Add(this.gpxLeituraRMS);
            this.gpxAjuste.Controls.Add(this.btnProximo);
            this.gpxAjuste.Controls.Add(this.gpxEscritaRMS);
            this.gpxAjuste.Controls.Add(this.txtInfo);
            this.gpxAjuste.Controls.Add(this.btnAjustar);
            this.gpxAjuste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpxAjuste.Location = new System.Drawing.Point(12, 12);
            this.gpxAjuste.Name = "gpxAjuste";
            this.gpxAjuste.Size = new System.Drawing.Size(703, 559);
            this.gpxAjuste.TabIndex = 4;
            this.gpxAjuste.TabStop = false;
            this.gpxAjuste.Text = "Ajuste RMS";
            // 
            // gpxEscritaWgain
            // 
            this.gpxEscritaWgain.Controls.Add(this.lblFaseEmAjusteWgain);
            this.gpxEscritaWgain.Controls.Add(this.txtErrowGain);
            this.gpxEscritaWgain.Controls.Add(this.label15);
            this.gpxEscritaWgain.Location = new System.Drawing.Point(16, 22);
            this.gpxEscritaWgain.Name = "gpxEscritaWgain";
            this.gpxEscritaWgain.Size = new System.Drawing.Size(335, 337);
            this.gpxEscritaWgain.TabIndex = 18;
            this.gpxEscritaWgain.TabStop = false;
            this.gpxEscritaWgain.Text = "Escrita";
            // 
            // lblFaseEmAjusteWgain
            // 
            this.lblFaseEmAjusteWgain.AutoSize = true;
            this.lblFaseEmAjusteWgain.BackColor = System.Drawing.Color.LightGreen;
            this.lblFaseEmAjusteWgain.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaseEmAjusteWgain.Location = new System.Drawing.Point(115, 119);
            this.lblFaseEmAjusteWgain.Name = "lblFaseEmAjusteWgain";
            this.lblFaseEmAjusteWgain.Size = new System.Drawing.Size(102, 25);
            this.lblFaseEmAjusteWgain.TabIndex = 16;
            this.lblFaseEmAjusteWgain.Text = "Aguarde...";
            // 
            // txtErrowGain
            // 
            this.txtErrowGain.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtErrowGain.Location = new System.Drawing.Point(120, 70);
            this.txtErrowGain.Name = "txtErrowGain";
            this.txtErrowGain.Size = new System.Drawing.Size(135, 38);
            this.txtErrowGain.TabIndex = 14;
            this.txtErrowGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(53, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 31);
            this.label15.TabIndex = 15;
            this.label15.Text = "Erro";
            // 
            // btnAnterior
            // 
            this.btnAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnterior.Location = new System.Drawing.Point(580, 365);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(108, 56);
            this.btnAnterior.TabIndex = 17;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.UseVisualStyleBackColor = true;
            // 
            // gpxLeituraRMS
            // 
            this.gpxLeituraRMS.Controls.Add(this.btnAtualizar);
            this.gpxLeituraRMS.Controls.Add(this.txtV1Lido);
            this.gpxLeituraRMS.Controls.Add(this.txtV2Lido);
            this.gpxLeituraRMS.Controls.Add(this.txtI1Lido);
            this.gpxLeituraRMS.Controls.Add(this.txtV3Lido);
            this.gpxLeituraRMS.Controls.Add(this.label12);
            this.gpxLeituraRMS.Controls.Add(this.label7);
            this.gpxLeituraRMS.Controls.Add(this.txtI2Lido);
            this.gpxLeituraRMS.Controls.Add(this.label8);
            this.gpxLeituraRMS.Controls.Add(this.label11);
            this.gpxLeituraRMS.Controls.Add(this.label9);
            this.gpxLeituraRMS.Controls.Add(this.txtI3Lido);
            this.gpxLeituraRMS.Controls.Add(this.label10);
            this.gpxLeituraRMS.Location = new System.Drawing.Point(357, 22);
            this.gpxLeituraRMS.Name = "gpxLeituraRMS";
            this.gpxLeituraRMS.Size = new System.Drawing.Size(331, 337);
            this.gpxLeituraRMS.TabIndex = 18;
            this.gpxLeituraRMS.TabStop = false;
            this.gpxLeituraRMS.Text = "Leitura";
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizar.Location = new System.Drawing.Point(213, 140);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(108, 56);
            this.btnAtualizar.TabIndex = 0;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // txtV1Lido
            // 
            this.txtV1Lido.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtV1Lido.Location = new System.Drawing.Point(62, 22);
            this.txtV1Lido.Name = "txtV1Lido";
            this.txtV1Lido.ReadOnly = true;
            this.txtV1Lido.Size = new System.Drawing.Size(135, 38);
            this.txtV1Lido.TabIndex = 14;
            this.txtV1Lido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtV2Lido
            // 
            this.txtV2Lido.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtV2Lido.Location = new System.Drawing.Point(62, 66);
            this.txtV2Lido.Name = "txtV2Lido";
            this.txtV2Lido.ReadOnly = true;
            this.txtV2Lido.Size = new System.Drawing.Size(135, 38);
            this.txtV2Lido.TabIndex = 15;
            this.txtV2Lido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtI1Lido
            // 
            this.txtI1Lido.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtI1Lido.Location = new System.Drawing.Point(62, 190);
            this.txtI1Lido.Name = "txtI1Lido";
            this.txtI1Lido.ReadOnly = true;
            this.txtI1Lido.Size = new System.Drawing.Size(135, 38);
            this.txtI1Lido.TabIndex = 18;
            this.txtI1Lido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtV3Lido
            // 
            this.txtV3Lido.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtV3Lido.Location = new System.Drawing.Point(62, 110);
            this.txtV3Lido.Name = "txtV3Lido";
            this.txtV3Lido.ReadOnly = true;
            this.txtV3Lido.Size = new System.Drawing.Size(135, 38);
            this.txtV3Lido.TabIndex = 16;
            this.txtV3Lido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(19, 197);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 31);
            this.label12.TabIndex = 23;
            this.label12.Text = "I1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 31);
            this.label7.TabIndex = 17;
            this.label7.Text = "V1";
            // 
            // txtI2Lido
            // 
            this.txtI2Lido.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtI2Lido.Location = new System.Drawing.Point(62, 234);
            this.txtI2Lido.Name = "txtI2Lido";
            this.txtI2Lido.ReadOnly = true;
            this.txtI2Lido.Size = new System.Drawing.Size(135, 38);
            this.txtI2Lido.TabIndex = 20;
            this.txtI2Lido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 31);
            this.label8.TabIndex = 19;
            this.label8.Text = "V2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(19, 237);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 31);
            this.label11.TabIndex = 24;
            this.label11.Text = "I2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 31);
            this.label9.TabIndex = 21;
            this.label9.Text = "V3";
            // 
            // txtI3Lido
            // 
            this.txtI3Lido.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtI3Lido.Location = new System.Drawing.Point(62, 278);
            this.txtI3Lido.Name = "txtI3Lido";
            this.txtI3Lido.ReadOnly = true;
            this.txtI3Lido.Size = new System.Drawing.Size(135, 38);
            this.txtI3Lido.TabIndex = 22;
            this.txtI3Lido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(19, 281);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 31);
            this.label10.TabIndex = 25;
            this.label10.Text = "I3";
            // 
            // btnProximo
            // 
            this.btnProximo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProximo.Location = new System.Drawing.Point(580, 491);
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(108, 56);
            this.btnProximo.TabIndex = 1;
            this.btnProximo.Text = "Próximo";
            this.btnProximo.UseVisualStyleBackColor = true;
            this.btnProximo.Click += new System.EventHandler(this.btnProximo_Click);
            // 
            // gpxEscritaRMS
            // 
            this.gpxEscritaRMS.Controls.Add(this.txtV1RMS);
            this.gpxEscritaRMS.Controls.Add(this.cbkReplicarI1);
            this.gpxEscritaRMS.Controls.Add(this.cbkReplicarV1);
            this.gpxEscritaRMS.Controls.Add(this.txtV2RMS);
            this.gpxEscritaRMS.Controls.Add(this.txtV3RMS);
            this.gpxEscritaRMS.Controls.Add(this.label1);
            this.gpxEscritaRMS.Controls.Add(this.label2);
            this.gpxEscritaRMS.Controls.Add(this.label3);
            this.gpxEscritaRMS.Controls.Add(this.label4);
            this.gpxEscritaRMS.Controls.Add(this.txtI3RMS);
            this.gpxEscritaRMS.Controls.Add(this.label5);
            this.gpxEscritaRMS.Controls.Add(this.txtI2RMS);
            this.gpxEscritaRMS.Controls.Add(this.label6);
            this.gpxEscritaRMS.Controls.Add(this.txtI1RMS);
            this.gpxEscritaRMS.Location = new System.Drawing.Point(16, 22);
            this.gpxEscritaRMS.Name = "gpxEscritaRMS";
            this.gpxEscritaRMS.Size = new System.Drawing.Size(335, 337);
            this.gpxEscritaRMS.TabIndex = 19;
            this.gpxEscritaRMS.TabStop = false;
            this.gpxEscritaRMS.Text = "Escrita";
            // 
            // cbkReplicarI1
            // 
            this.cbkReplicarI1.AutoSize = true;
            this.cbkReplicarI1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbkReplicarI1.Location = new System.Drawing.Point(200, 195);
            this.cbkReplicarI1.Name = "cbkReplicarI1";
            this.cbkReplicarI1.Size = new System.Drawing.Size(122, 29);
            this.cbkReplicarI1.TabIndex = 7;
            this.cbkReplicarI1.Text = "Replicar I1";
            this.cbkReplicarI1.UseVisualStyleBackColor = true;
            this.cbkReplicarI1.CheckedChanged += new System.EventHandler(this.cbkReplicarI1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = "V1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 31);
            this.label2.TabIndex = 5;
            this.label2.Text = "V2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 31);
            this.label3.TabIndex = 6;
            this.label3.Text = "V3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 31);
            this.label4.TabIndex = 13;
            this.label4.Text = "I3";
            // 
            // txtI3RMS
            // 
            this.txtI3RMS.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtI3RMS.Location = new System.Drawing.Point(59, 278);
            this.txtI3RMS.Name = "txtI3RMS";
            this.txtI3RMS.Size = new System.Drawing.Size(135, 38);
            this.txtI3RMS.TabIndex = 6;
            this.txtI3RMS.Text = "5.000";
            this.txtI3RMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtI3RMS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtI3RMS_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 31);
            this.label5.TabIndex = 12;
            this.label5.Text = "I2";
            // 
            // txtI2RMS
            // 
            this.txtI2RMS.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtI2RMS.Location = new System.Drawing.Point(59, 234);
            this.txtI2RMS.Name = "txtI2RMS";
            this.txtI2RMS.Size = new System.Drawing.Size(135, 38);
            this.txtI2RMS.TabIndex = 5;
            this.txtI2RMS.Text = "5.000";
            this.txtI2RMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtI2RMS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtI2RMS_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 31);
            this.label6.TabIndex = 11;
            this.label6.Text = "I1";
            // 
            // txtI1RMS
            // 
            this.txtI1RMS.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtI1RMS.Location = new System.Drawing.Point(59, 190);
            this.txtI1RMS.Name = "txtI1RMS";
            this.txtI1RMS.Size = new System.Drawing.Size(135, 38);
            this.txtI1RMS.TabIndex = 4;
            this.txtI1RMS.Text = "5.000";
            this.txtI1RMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtI1RMS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtI1RMS_KeyPress);
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(16, 365);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(558, 182);
            this.txtInfo.TabIndex = 15;
            // 
            // btnAjustar
            // 
            this.btnAjustar.BackColor = System.Drawing.SystemColors.Info;
            this.btnAjustar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjustar.Location = new System.Drawing.Point(580, 428);
            this.btnAjustar.Name = "btnAjustar";
            this.btnAjustar.Size = new System.Drawing.Size(108, 56);
            this.btnAjustar.TabIndex = 0;
            this.btnAjustar.Text = "Ajustar";
            this.btnAjustar.UseVisualStyleBackColor = false;
            this.btnAjustar.Click += new System.EventHandler(this.btnAjustar_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnConectarSerial);
            this.groupBox5.Controls.Add(this.cbxPorta);
            this.groupBox5.Controls.Add(this.btnRefreshSerial);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.cbxBaudRate);
            this.groupBox5.Location = new System.Drawing.Point(721, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(97, 184);
            this.groupBox5.TabIndex = 20;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Serial";
            // 
            // btnConectarSerial
            // 
            this.btnConectarSerial.Location = new System.Drawing.Point(6, 149);
            this.btnConectarSerial.Name = "btnConectarSerial";
            this.btnConectarSerial.Size = new System.Drawing.Size(85, 23);
            this.btnConectarSerial.TabIndex = 18;
            this.btnConectarSerial.Text = "Conectar";
            this.btnConectarSerial.UseVisualStyleBackColor = true;
            this.btnConectarSerial.Click += new System.EventHandler(this.btnConectarSerial_Click);
            // 
            // cbxPorta
            // 
            this.cbxPorta.FormattingEnabled = true;
            this.cbxPorta.Location = new System.Drawing.Point(15, 31);
            this.cbxPorta.Name = "cbxPorta";
            this.cbxPorta.Size = new System.Drawing.Size(72, 21);
            this.cbxPorta.TabIndex = 13;
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
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "Porta";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 95);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "Baud Rate";
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
            // pgbProgressoAjuste
            // 
            this.pgbProgressoAjuste.BackColor = System.Drawing.SystemColors.Control;
            this.pgbProgressoAjuste.Location = new System.Drawing.Point(12, 577);
            this.pgbProgressoAjuste.Name = "pgbProgressoAjuste";
            this.pgbProgressoAjuste.Size = new System.Drawing.Size(703, 23);
            this.pgbProgressoAjuste.Step = 1;
            this.pgbProgressoAjuste.TabIndex = 21;
            // 
            // lblAguarde
            // 
            this.lblAguarde.AutoSize = true;
            this.lblAguarde.BackColor = System.Drawing.Color.Yellow;
            this.lblAguarde.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAguarde.Location = new System.Drawing.Point(326, 580);
            this.lblAguarde.Name = "lblAguarde";
            this.lblAguarde.Size = new System.Drawing.Size(74, 17);
            this.lblAguarde.TabIndex = 22;
            this.lblAguarde.Text = "Aguarde...";
            this.lblAguarde.Visible = false;
            // 
            // AjusteRMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(830, 614);
            this.Controls.Add(this.lblAguarde);
            this.Controls.Add(this.pgbProgressoAjuste);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.gpxAjuste);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AjusteRMS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AjusteRMS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AjusteRMS_FormClosing);
            this.Load += new System.EventHandler(this.AjusteRMS_Load);
            this.gpxAjuste.ResumeLayout(false);
            this.gpxAjuste.PerformLayout();
            this.gpxEscritaWgain.ResumeLayout(false);
            this.gpxEscritaWgain.PerformLayout();
            this.gpxLeituraRMS.ResumeLayout(false);
            this.gpxLeituraRMS.PerformLayout();
            this.gpxEscritaRMS.ResumeLayout(false);
            this.gpxEscritaRMS.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtV1RMS;
        private System.Windows.Forms.TextBox txtV2RMS;
        private System.Windows.Forms.TextBox txtV3RMS;
        private System.Windows.Forms.CheckBox cbkReplicarV1;
        private System.Windows.Forms.GroupBox gpxAjuste;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtI1RMS;
        private System.Windows.Forms.CheckBox cbkReplicarI1;
        private System.Windows.Forms.TextBox txtI2RMS;
        private System.Windows.Forms.TextBox txtI3RMS;
        private System.Windows.Forms.Button btnAjustar;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnProximo;
        private System.Windows.Forms.GroupBox gpxEscritaRMS;
        private System.Windows.Forms.GroupBox gpxLeituraRMS;
        private System.Windows.Forms.TextBox txtV1Lido;
        private System.Windows.Forms.TextBox txtV2Lido;
        private System.Windows.Forms.TextBox txtI1Lido;
        private System.Windows.Forms.TextBox txtV3Lido;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtI2Lido;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtI3Lido;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnConectarSerial;
        private System.Windows.Forms.ComboBox cbxPorta;
        private System.Windows.Forms.Button btnRefreshSerial;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.ProgressBar pgbProgressoAjuste;
        private System.Windows.Forms.GroupBox gpxEscritaWgain;
        private System.Windows.Forms.TextBox txtErrowGain;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblAguarde;
        private System.Windows.Forms.Label lblFaseEmAjusteWgain;
    }
}