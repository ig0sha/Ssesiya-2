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

namespace Markdown
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Materials> materials = new List<Materials>();
        List<TypeMaterials> typeMaterials = new List<TypeMaterials>();
        public MainWindow()
        {
            InitializeComponent();
            FillTable();
        }

        private void FillTable()
        {
            
            // Подключение к БД
            using (MaterialsStoreEntities db = new MaterialsStoreEntities())
            {
                // Связи таблиц
                var material = (from p in db.TypeMaterials
                                join c in db.Materials
                                on p.ID_Type_Materials equals c.Type_material
                                select new
                                {
                                    Наименование = c.Name_Material,
                                    Тип = p.Name,
                                    Изображение = c.Image,
                                    Цена = c.Price,
                                    НаСкладе = c.QuantityInStock
                                });
                //Вывод в DataGridMaterial
                DataGridMaterial.ItemsSource = material.ToList();
            }
        }
    }
}
