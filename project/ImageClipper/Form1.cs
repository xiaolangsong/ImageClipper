using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

/// <summary>
/// @Author : xiaolang.song
/// @Email  : 316002606@qq.com
/// @Github ：https://github.com/xiaolangsong
/// </summary>

namespace ImageClipper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int TileWidth = 254;
        const int TileHeigth = 128;
        const int HalfTileWidth = TileWidth / 2;
        const int HalfTileHeight = TileHeigth / 2;

        private void select_img_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = @"(*.gif;*.jpg;*.jpeg;*.png;*.psd)|*.gif;*.jpg;*.jpeg;*.png;*.psd";
            openFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                string savePath = GetSavePath();
                if (savePath == null)
                {
                    this.listBox1.Items.Add("Cancel operation");
                    return;
                }

                this.listBox1.Items.Add(openFileDialog.FileName);

                PointF p1 = new PointF(0, HalfTileHeight);
                PointF p2 = new PointF(HalfTileWidth, 0);
                PointF p3 = new PointF(TileWidth, HalfTileHeight);
                PointF p4 = new PointF(HalfTileWidth, TileHeigth);
                GraphicsPath path = new GraphicsPath();
                path.AddPolygon(new[] { p1, p2, p3, p4 });

                try
                {
                    using (Bitmap bitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false))
                    {
                        int column = CalculateWidthIndexCount(bitmap.Width, TileWidth);
                        int row = CalculateHeightIndex(bitmap.Height, TileHeigth);

                        this.listBox1.Items.Add("picture width" + bitmap.Width);
                        this.listBox1.Items.Add("picture height" + bitmap.Height);
                        this.listBox1.Items.Add("diamond height & width" + TileWidth + "," + TileHeigth);
                        this.listBox1.Items.Add("column = " + column);
                        this.listBox1.Items.Add("row = " + row);

                        TextureBrush brush = new TextureBrush(bitmap, WrapMode.Clamp);
                        for (int i = 0; i <= row; i++)
                        {
                            for (int j = 0; j < column; j++)
                            {
                                using (Bitmap bm = new Bitmap(TileWidth, TileHeigth))
                                {
                                    using (Graphics g = Graphics.FromImage(bm))
                                    {
                                        g.CompositingQuality = CompositingQuality.HighQuality;
                                        g.SmoothingMode = SmoothingMode.HighQuality;
                                        g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                                        g.PixelOffsetMode = PixelOffsetMode.Half;
                                        var stage = LogicToStage(i, j);
                                        brush.ResetTransform();
                                        brush.TranslateTransform(stage.X, stage.Y);
                                        g.FillRegion(brush, new Region(path));
                                        g.Save();
                                        string str = string.Format("/{0}_{1}.png", i, j);
                                        bm.Save(savePath + str, System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                }
                            }
                        }
                        brush.Dispose();
                        path.Dispose();
                    }
                    this.listBox1.Items.Add("clip finish");
                    this.listBox1.Items.Add("-----------------------");
                    MessageBox.Show("clip finish");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        /// <summary>
        /// GetSavePath
        /// </summary>
        /// <returns>string</returns>
        private string GetSavePath()
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.Description = "select save path";
            if (path.ShowDialog() == DialogResult.OK)
            {
                return path.SelectedPath;
            }
            return null;
        }


        /// <summary>
        /// Calculate the number of columns required
        /// </summary>
        /// <param name="width"></param>
        /// <param name="itemWidth"></param>
        /// <returns></returns>
        private int CalculateWidthIndexCount(int width, int itemWidth)
        {
            int count = 0;
            width -= (itemWidth / 2);
            count++;

            while (width > 0)
            {
                count++;
                width -= itemWidth;
            }
            return count;
        }

        /// <summary>
        /// Calculate the number of rows required
        /// </summary>
        /// <param name="height"></param>
        /// <param name="itemHeight"></param>
        /// <returns></returns>
        private int CalculateHeightIndex(int height, int itemHeight)
        {
            int count = 0;
            while (height > 0)
            {
                count++;
                height -= itemHeight / 2;
            }

            return count;
        }

        /// <summary>
        /// Logical coordinates to actual coordinates
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private Point LogicToStage(int i, int j)
        {
            int xOffset = 0;
            int yOffset = 0;
            int x = 0;
            int y = 0;
            if (i % 2 == 0)
            {
                xOffset = TileWidth / 2;
                yOffset = (TileHeigth / 2) * (i + 1);
            }
            else
            {
                yOffset = (TileHeigth / 2) * (i + 1);
            }

            x = j * -TileWidth;
            y = TileHeigth * i;

            Point stage = new Point(x + xOffset, -y + yOffset);
            return stage;
        }
    }
}
