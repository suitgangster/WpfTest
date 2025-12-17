using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTest.ViewModels;

namespace WpfTest.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void StartAnimation_Click(object sender, RoutedEventArgs e)
        {
            // 创建 PointAnimation
            PointAnimation pointAnimation = new PointAnimation();

            // 设置起点和终点
            pointAnimation.From = new Point(50, 150);    // 起点坐标
            pointAnimation.To = new Point(350, 150);     // 终点坐标

            // 设置动画属性
            pointAnimation.Duration = TimeSpan.FromSeconds(2);
            pointAnimation.AutoReverse = true;
            pointAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // 添加缓动函数使动画更自然
            pointAnimation.EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut };

            // 开始动画
            //MovingEllipse.BeginAnimation(EllipseGeometry.CenterProperty, pointAnimation);
        }
    }
}