using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace populationDatabase1
{
    public class VM : INotifyPropertyChanged
    {
        public BindingList<City> Cities { get; set; }

        #region properties

        private City selectedCity;
        public City SelectedCity
        {
            get => selectedCity;
            set { selectedCity = value; NotifyChange(); }
        }

        private string newCityName;
        public string NewCityName
        {
            get => newCityName;
            set { newCityName = value; NotifyChange(); }
        }

        private double? newCityPopulation = null;
        public double? NewCityPopulation
        {
            get => newCityPopulation;
            set { newCityPopulation = value; NotifyChange(); }
        }

        private double? totalPopulation;
        public double? TotalPopulation
        {
            get => totalPopulation;
            set { totalPopulation = value; NotifyChange(); }
        }

        private double? averagePopulation;
        public double? AveragePopulation
        {
            get => averagePopulation;
            set { averagePopulation = value; NotifyChange(); }
        }

        private string highestCity;
        public string HighestCity
        {
            get => highestCity;
            set { highestCity = value; NotifyChange(); }
        }

        private double? highestPopulation;
        public double? HighestPopulation
        {
            get => highestPopulation;
            set { highestPopulation = value; NotifyChange(); }
        }

        private string lowestCity;
        public string LowestCity
        {
            get => lowestCity;
            set { lowestCity = value; NotifyChange(); }
        }

        private double? lowestPopulation;
        public double? LowestPopulation
        {
            get => lowestPopulation;
            set { lowestPopulation = value; NotifyChange(); }
        }

        #endregion

        #region CRUD Functions
        public VM()
        {
            Db.GetInstance().CreateDatabase();
            Db.GetInstance().CreateTable();

            //read database and store values into listview      
            Cities = new BindingList<City>(Db.GetInstance().Read());

            //populates table with values from csv file only if listview/database is empty
            if (Cities.Count == 0)
            {
                string[] lines = File.ReadAllLines("city.csv");

                for (int i = 1; i < lines.Length; i++)
                {
                    //remove quotation marks from csv file
                    var s = Regex.Replace(lines[i], "\"", string.Empty);
                    string[] elements = s.Split(',');
                    City cities = new City()
                    {
                        CityName = elements[0].Trim(),
                        Population = double.Parse(elements[1])
                    };
                    Db.GetInstance().Insert(cities);
                }
                //read database and store into listview
                Cities = new BindingList<City>(Db.GetInstance().Read());
            }
        }

        public void AddRow()
        {
            //create new city
            City AddedCity = new City()
            {
                CityName = NewCityName,
                Population = NewCityPopulation
            };

            //do not add new city to the listview and database if already exists 
            bool confirm_city = Cities.Any(city => city.CityName == NewCityName.Trim());
            if (!confirm_city && (!string.IsNullOrEmpty(NewCityName)))
            {
                Cities.Add(AddedCity);
                Db.GetInstance().Insert(AddedCity);
            }
        }

        public void DeleteRow()
        {
            Db.GetInstance().Delete(SelectedCity);
            Cities.Remove(SelectedCity);
        }

        public void EditRow()
        {
            Db.GetInstance().Update(SelectedCity);
        }
        #endregion

        #region Scalar Functions
        public void CalcTotal()
        {
            TotalPopulation = Db.GetInstance().Total();
        }

        public void CalcAverage()
        {
            AveragePopulation = Db.GetInstance().Average();
        }

        public void CalcHighest()
        {
            HighestPopulation = Db.GetInstance().Highest();
            HighestCity = Cities.Where(city => city.Population == HighestPopulation).Select(city => city.CityName).First();
        }

        public void CalcLowest()
        {
            LowestPopulation = Db.GetInstance().Lowest();
            LowestCity = Cities.Where(city => city.Population == LowestPopulation).Select(city => city.CityName).First();
        }

        public void Clear()
        {
            HighestCity = string.Empty;
            HighestPopulation = null;
            LowestCity = string.Empty;
            LowestPopulation = null;
            AveragePopulation = null;
            TotalPopulation = null;
            NewCityName = string.Empty;
            NewCityPopulation = null;

        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChange([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion

    }
}
