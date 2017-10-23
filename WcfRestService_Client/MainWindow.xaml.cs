using RosreestrRestClient;
using RosreestrRestClient.Types;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WcfRestService_Client {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            var txt = textNumber.Text;
            object obj = null;
            if (Regex.IsMatch(txt, "^[0-9]{2}:[0-9]{2}:[0-9]{6,7}:[0-9]{1,}$")) {    // введён КН объекта недвижимости, например: 78:05:000552:12
                obj = RestRequester.GetByCadNum(txt);
            }
            else if (Regex.IsMatch(txt, "[А-Яа-яЁё]{2,}")) {  //  введён текст с двумя или более буквами
                obj = RestRequester.GetMacroRegions();
            }
            else {
                obj = RestRequester.SearchByNumber(txt);
            }
            treeView.Items.Clear();
            if (obj != null) {
                if (obj is IEnumerable) {
                    foreach (var result in obj as IEnumerable) {
                        var item = new TreeViewItem() { Header = txt };
                        FillTreeItem(item, result);
                        treeView.Items.Add(item);
                    }
                }
                else {
                    var item = new TreeViewItem() { Header = txt };
                    FillTreeItem(item, obj);
                    treeView.Items.Add(item);
                }
            }
            else {
                var item = new TreeViewItem() { Header = "По запросу \"" + txt + "\" ничего не найдено" };
                treeView.Items.Add(item);
            }
        }

        private void buttonAddrSearch_Click(object sender, RoutedEventArgs e) {
            var macroReg = (this.comboMacroRegion.SelectedItem as ComboBoxItem)?.Tag?.ToString();
            var street = this.textStreet.Text;
            var house = this.textHouse.Text;
            object obj = RosreestrRestClient.RestRequester.SearchByAddress(macroReg, streetName: street, houseNum: house);
            treeView.Items.Clear();
            if (obj != null) {
                if (obj is IEnumerable) {
                    foreach (var result in obj as IEnumerable) {
                        var item = new TreeViewItem() { Header = street };
                        FillTreeItem(item, result);
                        treeView.Items.Add(item);
                    }
                }
                else {
                    var item = new TreeViewItem() { Header = street };
                    FillTreeItem(item, obj);
                    treeView.Items.Add(item);
                }
            }
            else {
                var item = new TreeViewItem() { Header = "По запросу ничего не найдено" };
                treeView.Items.Add(item);
            }
        }

        private void FillTreeItem(TreeViewItem root, object obj) {
            var members = obj.GetType().GetProperties().ToDictionary(
                p => p.Name,
                p => { var v = p.GetValue(obj, null); return v; }
            );
            foreach (var member in members) {
                var item = new TreeViewItem() {
                    Header = member.Key + ": " + ((member.Value is decimal) ? string.Format("{0:0.00}", member.Value) : member.Value)
                };
                if (member.Value is IRosreestrData) {
                    FillTreeItem(item, member.Value);
                }
                if (member.Value is IEnumerable) {
                    foreach (var subitem in (member.Value as IEnumerable)) {
                        FillTreeItem(item, subitem);
                    }
                }
                root.Items.Add(item);
            }
        }

        private void comboMacroRegion_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (this.comboMacroRegion.SelectedItem == null) this.labelMacroPlaceHolder.Visibility = Visibility.Visible;
            else this.labelMacroPlaceHolder.Visibility = Visibility.Collapsed;
        }

        private void comboMacroRegion_Loaded(object sender, RoutedEventArgs e) {
            var regions = RosreestrRestClient.RestRequester.GetMacroRegions();
            foreach (var item in regions.OrderBy(x => x.Name)) {
                this.comboMacroRegion.Items.Add(new ComboBoxItem() { Tag = item.ID, Content = item.Name });
            }
        }
    }
}
