using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace UserControls
{
    /// <summary>
    /// Interaction logic for ImageViewer.xaml
    /// </summary>
    public partial class ImageViewer : UserControl
    {
        public ImageViewer()
        {
            InitializeComponent();
            
        }

        public List<BitmapImage> Images
        {
            get { return _images; }
            private set
            {
                _images = value;
                currentIndex = 0;
                
            }
        }

        private int currentIndex = 0;
        //private BitmapImage _previousImage;
        //private BitmapImage _nextImage;
        //private BitmapImage _currentImage;
        private List<BitmapImage> _images = new List<BitmapImage>();

        public static readonly DependencyProperty PreviousImageProperty = DependencyProperty.Register(
            "PreviousImage", typeof (BitmapImage), typeof (ImageViewer), new PropertyMetadata(default(BitmapImage)));

        public BitmapImage PreviousImage
        {
            get { return (BitmapImage) GetValue(PreviousImageProperty); }
            set { SetValue(PreviousImageProperty, value); }
        }


        public static readonly DependencyProperty NextImageProperty = DependencyProperty.Register(
            "NextImage", typeof (BitmapImage), typeof (ImageViewer), new PropertyMetadata(default(BitmapImage)));

        public BitmapImage NextImage
        {
            get { return (BitmapImage) GetValue(NextImageProperty); }
            set { SetValue(NextImageProperty, value); }
        }



        public static readonly DependencyProperty CurrentImageProperty = DependencyProperty.Register(
            "CurrentImage", typeof (BitmapImage), typeof (ImageViewer), new PropertyMetadata(default(BitmapImage)));

        public BitmapImage CurrentImage
        {
            get { return (BitmapImage) GetValue(CurrentImageProperty); }
            set { SetValue(CurrentImageProperty, value); }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (Images.Count() - 1 < currentIndex + 1) return;
                currentIndex += 1;
            SetImages();
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex <= 0) return;
            currentIndex -= 1;
            SetImages();
        }

      

        private void SetImages()
        {
          if(Images.Count() - 1 >= currentIndex) CurrentImage = Images[currentIndex];
          NextImage =  Images.Count() > currentIndex + 1 ?  Images[currentIndex + 1] : null;
          PreviousImage =  currentIndex - 1 >= 0 ?  Images[currentIndex - 1] :  null;
          
        }

     
        private void ImageViewer_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var imgs = e.NewValue as List<BitmapImage>;
            if (imgs == null) return;
            Images = imgs;

            SetImages();
        }
    }
}
