using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Units.Standard.Wpf
{
    /// <summary>
    /// Interaction logic for GroupBoxTemplate.xaml
    /// </summary>
    public partial class GroupBoxTemplate : UserControl
    {
        public GroupBoxTemplate()
        {
            InitializeComponent();
        }

        public UserControl GBContentControl
        {
            get { return (UserControl)GetValue(GBContentControlProperty); }
            set { SetValue(GBContentControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GBContentControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GBContentControlProperty =
            DependencyProperty.Register("GBContentControl", typeof(UserControl), typeof(GroupBoxTemplate));





        public string GBHeader
        {
            get { return (string)GetValue(GBHeaderProperty); }
            set { SetValue(GBHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GBHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GBHeaderProperty =
            DependencyProperty.Register("GBHeader", typeof(string), typeof(GroupBoxTemplate), new PropertyMetadata("Condenser Coil"));






        public Brush GBBrushColor
        {
            get { return (Brush)GetValue(GBBrushColorProperty); }
            set { SetValue(GBBrushColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GBBrushColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GBBrushColorProperty =
            DependencyProperty.Register("GBBrushColor", typeof(Brush), typeof(GroupBoxTemplate), new PropertyMetadata(new SolidColorBrush( Color.FromRgb(10,20,30))));







        public Brush GBBackground
        {
            get { return (Brush)GetValue(GBBackgroundProperty); }
            set { SetValue(GBBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GBBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GBBackgroundProperty =
            DependencyProperty.Register("GBBackground", typeof(Brush), typeof(GroupBoxTemplate), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(230, 110, 250))));





        public Brush GBHeaderForground
        {
            get { return (Brush)GetValue(GBHeaderForgroundProperty); }
            set { SetValue(GBHeaderForgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GBHeaderForground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GBHeaderForgroundProperty =
            DependencyProperty.Register("GBHeaderForground", typeof(Brush), typeof(GroupBoxTemplate), new PropertyMetadata(new SolidColorBrush( Color.FromRgb(10,20,30))));





        public double GBContentControlHeight
        {
            get { return (double)GetValue(GBContentControlHeightProperty); }
            set { SetValue(GBContentControlHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GBContentControlHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GBContentControlHeightProperty =
            DependencyProperty.Register("GBContentControlHeight", typeof(double), typeof(GroupBoxTemplate), new PropertyMetadata(double.NaN));


    }
}
