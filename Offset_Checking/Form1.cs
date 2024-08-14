using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Offset_Checking
{
    public partial class Form1 : Form
    {
        private Image originalImage;
        private PointF imagePosition = new PointF(0, 0);
        private float zoom = 1.0f;
        private Point lastMousePos;
        private bool dragging = false;
        private bool markingPoints = false;
        private bool draggingCenterMark = false;
        private PointF[] markedPoints = new PointF[4];
        private int currentPointIndex = 0;
        private PointF? centerMarkPosition = null;
        private float pixelSize = 1.0f; // 默认 PixelSize
        private RectangleF imageRect;
        private PointF? currentMousePosition = null; // 用來跟隨鼠標的線條終點

        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonOpenImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    originalImage = Image.FromFile(openFileDialog.FileName);
                    AutoZoomToFit();
                    imagePosition = new PointF(0, 0);
                    currentPointIndex = 0;
                    markedPoints = new PointF[4];
                    centerMarkPosition = null;
                    currentMousePosition = null;
                    UpdateCoordinatesLabel();
                    pictureBox.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load image: {ex.Message}");
                }
            }
        }

        private void AutoZoomToFit()
        {
            if (originalImage != null)
            {
                float imageAspect = (float)originalImage.Width / originalImage.Height;
                float boxAspect = (float)pictureBox.Width / pictureBox.Height;

                if (imageAspect > boxAspect)
                {
                    zoom = (float)pictureBox.Width / originalImage.Width;
                }
                else
                {
                    zoom = (float)pictureBox.Height / originalImage.Height;
                }

                imagePosition = new PointF(
                    (pictureBox.Width - originalImage.Width * zoom) / 2,
                    (pictureBox.Height - originalImage.Height * zoom) / 2
                );

                zoomTrackBar.Value = (int)(zoom * 10); // 根據初始縮放設置 TrackBar 值
            }
        }

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                // 如果滾輪向上滾動，放大圖像；如果向下滾動，縮小圖像
                float zoomFactor = e.Delta > 0 ? 1.1f : 1.0f / 1.1f;
                AdjustZoom(zoomFactor, e.Location);
            }
        }

        private void AdjustZoom(float factor, Point location)
        {
            float oldZoom = zoom;
            zoom *= factor;
            zoom = Math.Max(0.1f, Math.Min(zoom, 3.0f)); // 確保 zoom 在 0.1 到 3.0 之間

            imagePosition.X = location.X - (location.X - imagePosition.X) * (zoom / oldZoom);
            imagePosition.Y = location.Y - (location.Y - imagePosition.Y) * (zoom / oldZoom);

            zoomTrackBar.Value = (int)(zoom * 10); // 根據縮放比例設置 TrackBar 值

            pictureBox.Invalidate(); // 重新繪製圖片
        }


        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                lastMousePos = e.Location;
                dragging = true;
            }

            if (markingPoints && e.Button == MouseButtons.Left)
            {
                if (currentPointIndex < 4)
                {
                    markedPoints[currentPointIndex] = new PointF(
                        (e.X - imagePosition.X) / zoom,
                        (e.Y - imagePosition.Y) / zoom
                    );
                    currentPointIndex++;
                    UpdateCoordinatesLabel(); // 更新座標
                    if (currentPointIndex == 4)
                    {
                        markingPoints = false;
                        currentMousePosition = null;
                    }
                    pictureBox.Invalidate();
                }
            }

            if (centerMarkPosition.HasValue && e.Button == MouseButtons.Left)
            {
                PointF centerMarkScreenPos = new PointF(
                    centerMarkPosition.Value.X * zoom + imagePosition.X,
                    centerMarkPosition.Value.Y * zoom + imagePosition.Y
                );

                if (IsPointInCircle(e.Location, centerMarkScreenPos, 10))
                {
                    draggingCenterMark = true;
                }
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                var dx = e.X - lastMousePos.X;
                var dy = e.Y - lastMousePos.Y;

                imagePosition.X += dx;
                imagePosition.Y += dy;

                lastMousePos = e.Location;
                pictureBox.Invalidate();
            }

            if (markingPoints && currentPointIndex > 0 && currentPointIndex < 4)
            {
                currentMousePosition = new PointF(
                    (e.X - imagePosition.X) / zoom,
                    (e.Y - imagePosition.Y) / zoom
                );
                pictureBox.Invalidate();
            }

            if (draggingCenterMark && centerMarkPosition.HasValue)
            {
                centerMarkPosition = new PointF(
                    (e.X - imagePosition.X) / zoom,
                    (e.Y - imagePosition.Y) / zoom
                );

                UpdateCoordinatesLabel();
                pictureBox.Invalidate();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dragging = false;
            }

            if (e.Button == MouseButtons.Left)
            {
                draggingCenterMark = false;
            }
        }

        private void ButtonMarkPoints_Click(object sender, EventArgs e)
        {
            markingPoints = true;
            currentPointIndex = 0;
            markedPoints = new PointF[4];
            currentMousePosition = null;
            UpdateCoordinatesLabel();
            pictureBox.Invalidate();
        }

        private void ButtonAddCenterMark_Click(object sender, EventArgs e)
        {
            centerMarkPosition = new PointF(
                originalImage.Width / 2f,
                originalImage.Height / 2f
            );
            UpdateCoordinatesLabel();
            pictureBox.Invalidate();
        }

        private void ButtonSetCenterMark_Click(object sender, EventArgs e)
        {
            if (float.TryParse(textBoxCenterX.Text, out float x) && float.TryParse(textBoxCenterY.Text, out float y))
            {
                centerMarkPosition = new PointF(x, y);
                UpdateCoordinatesLabel();
                pictureBox.Invalidate();
            }
            else
            {
                MessageBox.Show("請輸入有效的 X 和 Y 座標。");
            }
        }

        private void TextBoxCenter_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && (textBox.Text == "X" || textBox.Text == "Y"))
            {
                textBox.Text = ""; // 清除佔位文字
            }
        }

        private void TextBoxCenter_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox == textBoxCenterX)
                {
                    textBox.Text = "X"; // 恢復佔位文字
                }
                else if (textBox == textBoxCenterY)
                {
                    textBox.Text = "Y"; // 恢復佔位文字
                }
            }
        }

        private void TextBoxPixelSize_Enter(object sender, EventArgs e)
        {
            if (textBoxPixelSize.Text == "Pixel Size")
            {
                textBoxPixelSize.Text = ""; // 清除佔位文字
            }
        }

        private void TextBoxPixelSize_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPixelSize.Text))
            {
                textBoxPixelSize.Text = "Pixel Size"; // 恢復佔位文字
            }
        }

        private void TextBoxPixelSize_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(textBoxPixelSize.Text, out float newSize))
            {
                pixelSize = newSize;
                UpdateCalculatedOffset();
            }
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (originalImage == null) return;

            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            imageRect = new RectangleF(imagePosition.X, imagePosition.Y, originalImage.Width * zoom, originalImage.Height * zoom);
            g.DrawImage(originalImage, imageRect);

            if (currentPointIndex > 0)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    for (int i = 0; i < currentPointIndex; i++)
                    {
                        PointF pt = new PointF(markedPoints[i].X * zoom + imagePosition.X, markedPoints[i].Y * zoom + imagePosition.Y);
                        g.DrawEllipse(pen, pt.X - 3, pt.Y - 3, 6, 6);

                        if (i > 0)
                        {
                            PointF prevPt = new PointF(markedPoints[i - 1].X * zoom + imagePosition.X, markedPoints[i - 1].Y * zoom + imagePosition.Y);
                            g.DrawLine(pen, prevPt, pt);
                        }
                    }

                    if (currentMousePosition.HasValue && currentPointIndex < 4)
                    {
                        PointF lastPoint = new PointF(markedPoints[currentPointIndex - 1].X * zoom + imagePosition.X, markedPoints[currentPointIndex - 1].Y * zoom + imagePosition.Y);
                        PointF currentPoint = new PointF(currentMousePosition.Value.X * zoom + imagePosition.X, currentMousePosition.Value.Y * zoom + imagePosition.Y);
                        g.DrawLine(pen, lastPoint, currentPoint);
                    }

                    if (currentPointIndex == 4)
                    {
                        // 點完第四點後，連接第四點和第一點
                        PointF firstPt = new PointF(markedPoints[0].X * zoom + imagePosition.X, markedPoints[0].Y * zoom + imagePosition.Y);
                        PointF lastPt = new PointF(markedPoints[3].X * zoom + imagePosition.X, markedPoints[3].Y * zoom + imagePosition.Y);
                        g.DrawLine(pen, lastPt, firstPt);
                    }
                }
            }

            if (centerMarkPosition.HasValue)
            {
                PointF centerMarkScreenPos = new PointF(
                    centerMarkPosition.Value.X * zoom + imagePosition.X,
                    centerMarkPosition.Value.Y * zoom + imagePosition.Y
                );
                g.FillEllipse(Brushes.Blue, centerMarkScreenPos.X - 5, centerMarkScreenPos.Y - 5, 10, 10);
            }
        }

        private bool IsPointInCircle(PointF point, PointF circleCenter, float radius)
        {
            float dx = point.X - circleCenter.X;
            float dy = point.Y - circleCenter.Y;
            return dx * dx + dy * dy <= radius * radius;
        }

        private void UpdateCoordinatesLabel()
        {
            string coordinatesText = "左上座標:\n";
            coordinatesText += currentPointIndex > 0 ? $"({markedPoints[0].X}, {markedPoints[0].Y})" : "";
            coordinatesText += "\n右上座標:\n";
            coordinatesText += currentPointIndex > 1 ? $"({markedPoints[1].X}, {markedPoints[1].Y})" : "";
            coordinatesText += "\n左下座標:\n";
            coordinatesText += currentPointIndex > 2 ? $"({markedPoints[2].X}, {markedPoints[2].Y})" : "";
            coordinatesText += "\n右下座標:\n";
            coordinatesText += currentPointIndex > 3 ? $"({markedPoints[3].X}, {markedPoints[3].Y})" : "";
            coordinatesText += "\n物料中心:\n";
            if (currentPointIndex == 4)
            {
                float centerX = (markedPoints[0].X + markedPoints[1].X + markedPoints[2].X + markedPoints[3].X) / 4;
                float centerY = (markedPoints[0].Y + markedPoints[1].Y + markedPoints[2].Y + markedPoints[3].Y) / 4;
                coordinatesText += $"({centerX}, {centerY})";
            }
            coordinatesText += "\n影像中心:\n";
            coordinatesText += centerMarkPosition.HasValue ? $"({centerMarkPosition.Value.X}, {centerMarkPosition.Value.Y})" : "";

            labelCoordinates.Text = coordinatesText;
            UpdateCalculatedOffset(); // 更新計算結果
        }

        private void UpdateCalculatedOffset()
        {
            if (currentPointIndex == 4 && centerMarkPosition.HasValue)
            {
                float centerX = (markedPoints[0].X + markedPoints[1].X + markedPoints[2].X + markedPoints[3].X) / 4;
                float centerY = (markedPoints[0].Y + markedPoints[1].Y + markedPoints[2].Y + markedPoints[3].Y) / 4;

                float offsetX = (centerX - centerMarkPosition.Value.X) * pixelSize;
                float offsetY = (centerY - centerMarkPosition.Value.Y) * pixelSize;

                labelCalculatedOffset.Text = $"計算結果:\nX: {offsetX}\nY: {offsetY}";
            }
            else
            {
                labelCalculatedOffset.Text = "計算結果: 無";
            }
        }

        private void ButtonZoomIn_Click(object sender, EventArgs e)
        {
            AdjustZoom(1.1f, new Point(pictureBox.Width / 2, pictureBox.Height / 2));
        }

        private void ButtonZoomOut_Click(object sender, EventArgs e)
        {
            AdjustZoom(1.0f / 1.1f, new Point(pictureBox.Width / 2, pictureBox.Height / 2));
        }

        private void ZoomTrackBar_Scroll(object sender, EventArgs e)
        {
            float newZoom = zoomTrackBar.Value / 10f;
            AdjustZoom(newZoom / zoom, new Point(pictureBox.Width / 2, pictureBox.Height / 2));
        }
    }
}
