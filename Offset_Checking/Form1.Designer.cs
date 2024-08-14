using System.Windows.Forms;

namespace Offset_Checking
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private PictureBox pictureBox;
        private Button buttonOpenImage;
        private Button buttonMarkPoints;
        private Button buttonAddCenterMark;
        private Button buttonSetCenterMark;
        private Button buttonZoomIn;
        private Button buttonZoomOut;
        private TrackBar zoomTrackBar; // 新增 TrackBar 控件
        private Panel panelRight;
        private Label labelCoordinates;
        private Label labelCalculatedOffset;
        private OpenFileDialog openFileDialog;
        private FlowLayoutPanel buttonPanel;
        private TextBox textBoxCenterX;
        private TextBox textBoxCenterY;
        private TextBox textBoxPixelSize;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonOpenImage = new System.Windows.Forms.Button();
            this.buttonMarkPoints = new System.Windows.Forms.Button();
            this.buttonAddCenterMark = new System.Windows.Forms.Button();
            this.buttonSetCenterMark = new System.Windows.Forms.Button();
            this.buttonZoomIn = new System.Windows.Forms.Button();
            this.buttonZoomOut = new System.Windows.Forms.Button();
            this.zoomTrackBar = new System.Windows.Forms.TrackBar(); // 初始化 TrackBar 控件
            this.panelRight = new System.Windows.Forms.Panel();
            this.labelCoordinates = new System.Windows.Forms.Label();
            this.labelCalculatedOffset = new System.Windows.Forms.Label();
            this.textBoxPixelSize = new System.Windows.Forms.TextBox();
            this.textBoxCenterX = new System.Windows.Forms.TextBox();
            this.textBoxCenterY = new System.Windows.Forms.TextBox();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).BeginInit();
            this.panelRight.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(700, 600);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseUp);
            this.pictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseWheel);
            // 
            // buttonOpenImage
            // 
            this.buttonOpenImage.AutoSize = true;
            this.buttonOpenImage.Location = new System.Drawing.Point(3, 3);
            this.buttonOpenImage.Name = "buttonOpenImage";
            this.buttonOpenImage.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenImage.TabIndex = 0;
            this.buttonOpenImage.Text = "Open Image";
            this.buttonOpenImage.Click += new System.EventHandler(this.ButtonOpenImage_Click);
            // 
            // buttonMarkPoints
            // 
            this.buttonMarkPoints.AutoSize = true;
            this.buttonMarkPoints.Location = new System.Drawing.Point(84, 3);
            this.buttonMarkPoints.Name = "buttonMarkPoints";
            this.buttonMarkPoints.Size = new System.Drawing.Size(75, 23);
            this.buttonMarkPoints.TabIndex = 1;
            this.buttonMarkPoints.Text = "Mark Points";
            this.buttonMarkPoints.Click += new System.EventHandler(this.ButtonMarkPoints_Click);
            // 
            // buttonAddCenterMark
            // 
            this.buttonAddCenterMark.AutoSize = true;
            this.buttonAddCenterMark.Location = new System.Drawing.Point(3, 32);
            this.buttonAddCenterMark.Name = "buttonAddCenterMark";
            this.buttonAddCenterMark.Size = new System.Drawing.Size(90, 20);
            this.buttonAddCenterMark.TabIndex = 2;
            this.buttonAddCenterMark.Text = "Add Center Mark";
            this.buttonAddCenterMark.Click += new System.EventHandler(this.ButtonAddCenterMark_Click);
            // 
            // buttonSetCenterMark
            // 
            this.buttonSetCenterMark.AutoSize = true;
            this.buttonSetCenterMark.Location = new System.Drawing.Point(109, 32);
            this.buttonSetCenterMark.Name = "buttonSetCenterMark";
            this.buttonSetCenterMark.Size = new System.Drawing.Size(90, 20);
            this.buttonSetCenterMark.TabIndex = 3;
            this.buttonSetCenterMark.Text = "Set Center Mark";
            this.buttonSetCenterMark.Click += new System.EventHandler(this.ButtonSetCenterMark_Click);
            // 
            // buttonZoomOut
            // 
            this.buttonZoomOut.AutoSize = true;
            this.buttonZoomOut.Location = new System.Drawing.Point(3, 61);
            this.buttonZoomOut.Name = "buttonZoomOut";
            this.buttonZoomOut.Size = new System.Drawing.Size(23, 23);
            this.buttonZoomOut.TabIndex = 4;
            this.buttonZoomOut.Text = "-";
            this.buttonZoomOut.Click += new System.EventHandler(this.ButtonZoomOut_Click);
            // 
            // zoomTrackBar
            // 
            this.zoomTrackBar.Location = new System.Drawing.Point(32, 61);
            this.zoomTrackBar.Name = "zoomTrackBar";
            this.zoomTrackBar.Size = new System.Drawing.Size(126, 45);
            this.zoomTrackBar.TabIndex = 5;
            this.zoomTrackBar.Minimum = 1;
            this.zoomTrackBar.Maximum = 30;
            this.zoomTrackBar.Value = 10; // 默認縮放比例
            this.zoomTrackBar.TickStyle = TickStyle.None;
            this.zoomTrackBar.Scroll += new System.EventHandler(this.ZoomTrackBar_Scroll);
            // 
            // buttonZoomIn
            // 
            this.buttonZoomIn.AutoSize = true;
            this.buttonZoomIn.Location = new System.Drawing.Point(164, 61);
            this.buttonZoomIn.Name = "buttonZoomIn";
            this.buttonZoomIn.Size = new System.Drawing.Size(23, 23);
            this.buttonZoomIn.TabIndex = 6;
            this.buttonZoomIn.Text = "+";
            this.buttonZoomIn.Click += new System.EventHandler(this.ButtonZoomIn_Click);
            // 
            // textBoxCenterX
            // 
            this.textBoxCenterX.Location = new System.Drawing.Point(3, 90);
            this.textBoxCenterX.Name = "textBoxCenterX";
            this.textBoxCenterX.Size = new System.Drawing.Size(75, 23);
            this.textBoxCenterX.TabIndex = 7;
            this.textBoxCenterX.Text = "X";
            this.textBoxCenterX.Enter += new System.EventHandler(this.TextBoxCenter_Enter);
            this.textBoxCenterX.Leave += new System.EventHandler(this.TextBoxCenter_Leave);
            // 
            // textBoxCenterY
            // 
            this.textBoxCenterY.Location = new System.Drawing.Point(84, 90);
            this.textBoxCenterY.Name = "textBoxCenterY";
            this.textBoxCenterY.Size = new System.Drawing.Size(75, 23);
            this.textBoxCenterY.TabIndex = 8;
            this.textBoxCenterY.Text = "Y";
            this.textBoxCenterY.Enter += new System.EventHandler(this.TextBoxCenter_Enter);
            this.textBoxCenterY.Leave += new System.EventHandler(this.TextBoxCenter_Leave);
            // 
            // textBoxPixelSize
            // 
            this.textBoxPixelSize.Location = new System.Drawing.Point(3, 119);
            this.textBoxPixelSize.Name = "textBoxPixelSize";
            this.textBoxPixelSize.Size = new System.Drawing.Size(156, 23);
            this.textBoxPixelSize.TabIndex = 9;
            this.textBoxPixelSize.Text = "Pixel Size";
            this.textBoxPixelSize.Enter += new System.EventHandler(this.TextBoxPixelSize_Enter);
            this.textBoxPixelSize.Leave += new System.EventHandler(this.TextBoxPixelSize_Leave);
            this.textBoxPixelSize.TextChanged += new System.EventHandler(this.TextBoxPixelSize_TextChanged);
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.labelCalculatedOffset);
            this.panelRight.Controls.Add(this.labelCoordinates);
            this.panelRight.Controls.Add(this.buttonPanel);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(700, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(200, 600);
            this.panelRight.TabIndex = 1;
            // 
            // labelCoordinates
            // 
            this.labelCoordinates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCoordinates.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelCoordinates.Location = new System.Drawing.Point(0, 142);
            this.labelCoordinates.Name = "labelCoordinates";
            this.labelCoordinates.Size = new System.Drawing.Size(200, 374);
            this.labelCoordinates.TabIndex = 0;
            this.labelCoordinates.Text = "左上座標:\n\n右上座標:\n\n左下座標:\n\n右下座標:\n\n物料中心:\n\n影像中心:";
            // 
            // labelCalculatedOffset
            // 
            this.labelCalculatedOffset.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelCalculatedOffset.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelCalculatedOffset.Location = new System.Drawing.Point(0, 516);
            this.labelCalculatedOffset.Name = "labelCalculatedOffset";
            this.labelCalculatedOffset.Size = new System.Drawing.Size(200, 84);
            this.labelCalculatedOffset.TabIndex = 1;
            this.labelCalculatedOffset.Text = "計算結果:";
            // 
            // buttonPanel
            // 
            this.buttonPanel.AutoSize = true;
            this.buttonPanel.Controls.Add(this.buttonOpenImage);
            this.buttonPanel.Controls.Add(this.buttonMarkPoints);
            this.buttonPanel.Controls.Add(this.buttonAddCenterMark);
            this.buttonPanel.Controls.Add(this.buttonSetCenterMark);
            this.buttonPanel.Controls.Add(this.buttonZoomOut);
            this.buttonPanel.Controls.Add(this.zoomTrackBar); // 加入 TrackBar 控件
            this.buttonPanel.Controls.Add(this.buttonZoomIn);
            this.buttonPanel.Controls.Add(this.textBoxCenterX);
            this.buttonPanel.Controls.Add(this.textBoxCenterY);
            this.buttonPanel.Controls.Add(this.textBoxPixelSize);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonPanel.Location = new System.Drawing.Point(0, 0);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(200, 142);
            this.buttonPanel.TabIndex = 1;
            this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.panelRight);
            this.Name = "Form1";
            this.Text = "Offset Checking";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.buttonPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
