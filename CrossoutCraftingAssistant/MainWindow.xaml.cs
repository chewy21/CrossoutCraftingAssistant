using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace CrossoutCraftingAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //__________//                                                  //__________//
        //__________//         INIT                                     //__________//
        //__________//                                                  //__________//


        //---------- Variable globales ----------//

        public static string apiUrlPrefix = "https://crossoutdb.com/api/v1/recipe/";
        public static string ImageUrlPrefix = "https://crossoutdb.com/img/items/";

        //test sur sauvegarde de la liste d'armes
        //public weaponList = new WeaponList();


        // init de la classe weaponlist
        string wljson = "";
        WeaponList weaponList = new WeaponList
        {
            weapons = new List<Weapon>()
        };

        int count = 0;




        public MainWindow()
        {
            InitializeComponent();

            // preset du punisher pour tests
            //txtItemID.Text = "909";

            // chargement de weaponlist.json dans la classe weaponlist
            wljson = LoadWeaponListFromJson();
            WeaponList weaponList = JsonConvert.DeserializeObject<WeaponList>(wljson);

            /*            // synchronisation de la classe weaponlist avec la combobox de selection d'arme
                        BindingSource bindingSource = new BindingSource();
                        bindingSource.DataSource = weaponList.weapons;
                        cbItemName.DataSource = bindingSource;
                        cbItemName.DisplayMember = "Name";
                        cbItemName.ValueMember = "Id";*/
            cbItemName.ItemsSource = weaponList.weapons;
            cbItemName.SelectedIndex = 0;
            Weapon selectedWeapon = (Weapon)cbItemName.SelectedItem;
            txtItemID.Text = selectedWeapon.Id;
            


        }

        //__________//                                                  //__________//
        //__________//         UI Function                              //__________//
        //__________//                                                  //__________//

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //lvRecipe.Items.Add(txtItemID.Text);

            // recuperation de l'id de l'item
            string id = txtItemID.Text;
            // telechargement du json de l'item
            string json = await SendWebRequestForJson(apiUrlPrefix + id);
            // chargement du json dans data

            // test json to class : ok
            FullRecipeClass crossoutDb = FullRecipeClass.FromJson(json);

            crossoutDb = await PopulateFullRecipeClass(crossoutDb);




            lvRecipe.Items.Add(crossoutDb.Recipe.Item);

            



        }

        private void cbItemName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected Weapon object
            Weapon selectedWeapon = (Weapon)cbItemName.SelectedItem;

            // Use the selectedWeapon object
            txtItemID.Text = selectedWeapon.Id;
        }


        //__________//                                                  //__________//
        //__________//         Internal Function                        //__________//
        //__________//                                                  //__________//

        private async Task<FullRecipeClass> PopulateFullRecipeClass(FullRecipeClass crossoutDb)
        {
            foreach (Recipe recipe in crossoutDb.Recipe.Ingredients)
            {
                
                string js = await SendWebRequestForJson(apiUrlPrefix + recipe.Item.Id);
                FullRecipeClass tempDb = FullRecipeClass.FromJson(js);
                if (tempDb.Recipe.Item.Craftable == 1 && tempDb.Recipe.Ingredients != null)
                {
                    // assigner les nouveau item de tempDb dans crossoutDb
                    recipe.Item = tempDb.Recipe.Item;
                    recipe.Ingredients = tempDb.Recipe.Ingredients;

                    // Recursively populate sub-recipes
                    tempDb = await PopulateFullRecipeClass(tempDb);
                
                }
                count++;
                lblProgress.Content = $"Items added: {count}";
            }
            return crossoutDb;
        }


        //---------- fonction de récupération de json sur api Cdb ----------//

        private async Task<string> SendWebRequestForJson(string url)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string json = await client.DownloadStringTaskAsync(new Uri(url));
                    return json;
                }
                catch (WebException ex)
                {
                    Console.WriteLine("Error downloading JSON from {0}: {1}", url, ex.Message);
                    //throw new ApiException("Failed to download JSON from API", ex);
                    return "";
                }
            }
        }

/*        private async Task<Image> SendWebRequestForPng(string url)
        {
            // Create a new WebClient object
            using (WebClient client = new WebClient())
            {
                // Download the image from the URL as a byte array
                byte[] imageData;
                try
                {
                    imageData = client.DownloadData(url);
                }
                catch (WebException ex)
                {
                    Console.WriteLine("Error downloading image from {0}: {1}", url, ex.Message);
                    return null;
                }

                // Create a new MemoryStream object from the byte array
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    // Create a new Image object from the MemoryStream
                    
                    Image image = Image.FromStream(ms);

                    return image;
                }
            }
        }*/


        //---------- fonction de sauvegarde de weaponList en json ----------//

        public void SaveWeaponListJsonToFile(string js)
        {
            //string filePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "weaponlist.json");
            //System.IO.File.WriteAllText(filePath, js);

            string directoryPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = System.IO.Path.Combine(directoryPath, "data", "weaponlist.json");
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePath)))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));
            }

            System.IO.File.WriteAllText(filePath, js);
            //System.IO.File.WriteAllText(@"C:\path.json", json);
        }

        public string LoadWeaponListFromJson()
        {
            string js = File.ReadAllText("data/weaponlist.json");


            return js;
        }


    } // end of public partial class MainWindow : Window
} // end of namespace CrossoutCraftingAssistant