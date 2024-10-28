using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChooseDog
{
    public partial class Form1 : Form
    {
        // Узел дерева
        class Node
        {
            public string Question { get; set; }
            public string Parameter { get; set; }
            public Node Yes { get; set; }
            public Node No { get; set; }
            public Node IDK { get; set; }

            public Node(string question, string parameter = null)
            {
                Question = question;
                Parameter = parameter;
            }
        }

        private Node currentNode;
        private Node root;
        private Node coatCleaningLevel;
        private Node playfulnessLevel;

        private Dictionary<string, int> parameterValues;

        public Form1()
        {
            InitializeComponent();
            InitializeParameters();
            InitializeTree();
            ShowNextQuestion();
        }

        public void InitializeTree()
        {
            Node affectionateWithFamily = new Node("Важно ли для вас, чтобы пес был ласков к семье?", "Affectionate With Family");
            Node goodWithYoungChildren = new Node("Важно ли для вас, чтобы пес был ласков к детям?", "Good With Young Children");
            Node goodWithOtherDogs = new Node("Важно ли для вас, чтобы пес ладил с другими собаками?", "Good With Other Dogs");
            playfulnessLevel = new Node("Хотите ли вы играть с псом?", "Playfulness Level");
            Node trainabilityLevel = new Node("Собираетесь ли вы уделять время для обучения пса?", "Trainability Level");
            Node mentalStimulationNeeds = new Node("Готовы ли уделять больше 1 часа в день для обучения пса?", "Mental Stimulation Needs");
            Node watchdog = new Node("Нужен пес, чтобы сторожить вас?", "Watchdog/Protective Nature");
            Node opennessToStrangers = new Node("Важно ли для вас, чтобы пес был открыт к незнакомцам?", "Openness To Strangers");
            Node energyLevel = new Node("Любите ли вы активно проводить время?", "Energy Level");
            Node adaptabilityLevel = new Node("Часто ли вы меняете место жительства?", "Adaptability Level");
            coatCleaningLevel = new Node("Готовы ли вы тратить время на постоянный уход за собакой?", "Coat Grooming Frequency");
            Node barkingLevel = new Node("Вы можете себе позволить громкого пса?", "Barking Level");

            // Определяем связи между узлами (да/нет/не знаю)
            watchdog.Yes = affectionateWithFamily;
            watchdog.No = opennessToStrangers;
            watchdog.IDK = opennessToStrangers;

            opennessToStrangers.Yes = affectionateWithFamily;
            opennessToStrangers.No = affectionateWithFamily;
            opennessToStrangers.IDK = affectionateWithFamily;

            affectionateWithFamily.Yes = goodWithYoungChildren;
            affectionateWithFamily.No = playfulnessLevel;
            affectionateWithFamily.IDK = goodWithYoungChildren;

            goodWithYoungChildren.Yes = goodWithOtherDogs;
            goodWithYoungChildren.No = playfulnessLevel;
            goodWithYoungChildren.IDK = playfulnessLevel;

            goodWithOtherDogs.Yes = playfulnessLevel;
            goodWithOtherDogs.No = playfulnessLevel;
            goodWithOtherDogs.IDK = playfulnessLevel;

            playfulnessLevel.Yes = trainabilityLevel;
            playfulnessLevel.No = energyLevel;
            playfulnessLevel.IDK = energyLevel;

            trainabilityLevel.Yes = mentalStimulationNeeds;
            trainabilityLevel.No = adaptabilityLevel;
            trainabilityLevel.IDK = mentalStimulationNeeds;

            mentalStimulationNeeds.Yes = adaptabilityLevel;
            mentalStimulationNeeds.No = adaptabilityLevel;
            mentalStimulationNeeds.IDK = adaptabilityLevel;

            energyLevel.Yes = trainabilityLevel;
            energyLevel.No = adaptabilityLevel;
            energyLevel.IDK = trainabilityLevel;

            adaptabilityLevel.Yes = coatCleaningLevel;
            adaptabilityLevel.No = coatCleaningLevel;
            adaptabilityLevel.IDK = coatCleaningLevel;

            coatCleaningLevel.Yes = barkingLevel;
            coatCleaningLevel.No = barkingLevel;
            coatCleaningLevel.IDK = barkingLevel;

            // Устанавливаем корневой узел
            root = watchdog;
            currentNode = root; // Начинаем с корневого узла
        }

        // Инициализация параметров
        private void InitializeParameters()
        {
            // Инициализируем словарь параметров со значениями 0
            parameterValues = new Dictionary<string, int>
            {
                { "Affectionate With Family", 0 },//Ласковый с семьей / Affectionate With Family
                { "Good With Young Children", 0 },//Хорошо с маленькими детьми / Good With Young Children
                { "Good With Other Dogs", 0 },//Хорошо с другими собаками / Good With Other Dogs
                { "Shedding Level", 0 },//Уровень линьки / Shedding Level
                { "Coat Grooming Frequency", 0 },//Частота ухода за шерстью / Coat Grooming Frequency
                { "Drooling Level", 0 },//Уровень слюнотечения / Drooling Level
                { "Openness To Strangers", 0 },//Открытость к незнакомцам / Openness To Strangers
                { "Playfulness Level", 0 },//Уровень игривости / Playfulness Level
                { "Watchdog/Protective Nature", 0 },//Сторожевая собака/Защитный характер / Watchdog/Protective Nature
                { "Adaptability Level", 0 },//Уровень адаптивности / Adaptability Level
                { "Trainability Level", 0 },//Уровень обучаемости / Trainability Level
                { "Energy Level", 0 },//Уровень энергии / Energy Level
                { "Barking Level", 0 },//Уровень лая / Barking Level
                { "Mental Stimulation Needs", 0 }//Потребность в умственной стимуляции / Mental Stimulation Needs                   
            };
        }

        private void ShowNextQuestion()
        {
            if (currentNode != null)
            {
                label_Question.Text = currentNode.Question;
            }
            else
            {
                ShowResults();
            }
        }

        private void ShowResults()
        {
            StringBuilder result = new StringBuilder("Результаты параметров:\n");
            foreach (var parameter in parameterValues)
            {
                result.AppendLine($"{parameter.Key}: {parameter.Value}");
            }
            label_Question.Text = result.ToString();
            button_Yes.Enabled = false;
            button_IDK.Enabled = false;
            button_No.Enabled = false;

            ShowTopBreeds(); // Показать топ-3 породы после прохождения опроса
        }

        private void ShowTopBreeds()
        {
            try
            {
                var breeds = LoadBreedsFromCsv("C:\\Users\\egori\\OneDrive\\Рабочий стол\\breed_traits_new.csv");
                var topBreeds = GetTopMatchingBreeds(breeds);

                StringBuilder topBreedsResult = new StringBuilder("Топ-3 породы:\n");
                foreach (var breed in topBreeds)
                {
                    topBreedsResult.AppendLine($"{breed.breed} (Отклонение: {breed.Deviation}):");

                    foreach (var trait in breed.Traits)
                    {
                        topBreedsResult.AppendLine($"  - {trait.Key}: {trait.Value}");
                    }
                    topBreedsResult.AppendLine(); // Пустая строка для разделения пород
                }

                MessageBox.Show(topBreedsResult.ToString(), "Рекомендованные породы");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных о породах: {ex.Message}");
            }
        }

        private List<Breed> LoadBreedsFromCsv(string filePath)
        {
            var breeds = new List<Breed>();
            using (var reader = new StreamReader(filePath))
            {
                var headers = reader.ReadLine().Split(',');
                while (!reader.EndOfStream)
                {
                    var values = reader.ReadLine().Split(',');
                    var breed = new Breed
                    {
                        breed = values[0],
                        Traits = new Dictionary<string, int>()
                    };

                    for (int i = 1; i < headers.Length; i++)
                    {
                        if (int.TryParse(values[i], out int traitValue))
                        {
                            breed.Traits[headers[i]] = traitValue;
                        }
                    }
                    breeds.Add(breed);
                }
            }
            return breeds;
        }

        private List<Breed> GetTopMatchingBreeds(List<Breed> breeds)
        {
            foreach (var breed in breeds)
            {
                breed.Deviation = parameterValues
                    .Sum(p => Math.Abs(breed.Traits.ContainsKey(p.Key) ? breed.Traits[p.Key] - p.Value : 0));
            }
            return breeds.OrderBy(b => b.Deviation).Take(3).ToList();
        }

        // Класс породы для хранения данных из CSV
        private class Breed
        {
            public string breed { get; set; }
            public Dictionary<string, int> Traits { get; set; }
            public int Deviation { get; set; }
        }

        // Обработчик для кнопки "Да"
        private void button_Yes_Click(object sender, EventArgs e)
        {
            if (currentNode == coatCleaningLevel)
            {
                // Присваиваем параметру значение 2
                parameterValues["Drooling Level"] = 2;
                parameterValues["Coat Grooming Frequency"] = 2;
                parameterValues["Shedding Level"] = 2;

                // Переход на "Да"
                currentNode = currentNode.Yes;
                ShowNextQuestion();
            }

            else if (currentNode == playfulnessLevel)
            {
                parameterValues[currentNode.Parameter] = 2;
                parameterValues["Energy Level"] = 2;
                // Переход на "Да"
                currentNode = currentNode.Yes;
                ShowNextQuestion();
            }

            else if (currentNode != null)
            {

                // Присваиваем параметру значение 2
                parameterValues[currentNode.Parameter] = 2;
                // Переход на "Да"
                currentNode = currentNode.Yes;
                ShowNextQuestion();
            }
        }
        // Обработчик для кнопки "Не знаю"
        private void button_IDK_Click(object sender, EventArgs e)
        {
            if (currentNode == coatCleaningLevel)
            {
                // Присваиваем параметру значение 1
                parameterValues["Drooling Level"] = 1;
                parameterValues["Coat Grooming Frequency"] = 1;
                parameterValues["Shedding Level"] = 1;

                // Переход на "Да"
                currentNode = currentNode.Yes;
                ShowNextQuestion();
            }

            else if (currentNode == playfulnessLevel)
            {
                parameterValues[currentNode.Parameter] = 1;
                parameterValues["Energy Level"] = 1;
                // Переход на "Да"
                currentNode = currentNode.Yes;
                ShowNextQuestion();
            }

            else if (currentNode != null)
            {

                // Присваиваем параметру значение 1
                parameterValues[currentNode.Parameter] = 1;
                // Переход на "Не знаю"
                currentNode = currentNode.IDK;
                ShowNextQuestion();
            }
        }
        // Обработчик для кнопки "Нет"
        private void button_No_Click(object sender, EventArgs e)
        {
            if (currentNode != null)
            {
                // Переход на "Нет"
                currentNode = currentNode.No;
                ShowNextQuestion();
            }
        }

        private void label_Question_Click(object sender, EventArgs e)
        {

        }
    }
}
