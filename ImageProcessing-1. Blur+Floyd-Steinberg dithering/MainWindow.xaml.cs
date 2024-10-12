using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    public partial class MainWindow : Window
    {
        WriteableBitmap wb1, wb2;
        int g1width, g1height, g2width, g2height;

        public MainWindow()
        {
            InitializeComponent();

            Init();
        }
        private void Init()
        {
            wb1 = BitmapFactory.FromContent("img.jpg");
            g1.Source = wb1;

            g2width = g1width = (int)wb1.Width;
            g2height = g1height = (int)wb1.Height;

            wb2 = new WriteableBitmap(g2width, g2height, 96, 96, PixelFormats.Bgra32, null);
            g2.Source = wb2;
        }

        private void btnBlur_Click(object sender, RoutedEventArgs e) => Blur.GetBlur(wb1, wb2);

        private void btnFloydSteinberg_Click(object sender, RoutedEventArgs e)
        {
            Dethering.GetDethering(wb1, 1);
            Dethering.GetDethering(wb2, 1);
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Init();
        }
    }
}
