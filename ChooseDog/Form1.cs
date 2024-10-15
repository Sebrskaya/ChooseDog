using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ChooseDog
{

    public partial class Form1 : Form
    {
        // Словарь для сопоставления названий параметров из кода с колонками из CSV
        private readonly Dictionary<string, string> columnMapping = new Dictionary<string, string>
    {
        { "Ласковый с семьей", "Affectionate With Family" },
        { "Хорошо с маленькими детьми", "Good With Young Children" },
        { "Хорошо с другими собаками", "Good With Other Dogs" },
        { "Уровень линьки", "Shedding Level" },
        { "Частота ухода за шерстью", "Coat Grooming Frequency" },
        { "Уровень слюнотечения", "Drooling Level" },
        { "Открытость к незнакомцам", "Openness To Strangers" },
        { "Уровень игривости", "Playfulness Level" },
        { "Сторожевая собака/Защитный характер", "Watchdog/Protective Nature" },
        { "Уровень адаптивности", "Adaptability Level" },
        { "Уровень обучаемости", "Trainability Level" },
        { "Уровень энергии", "Energy Level" },
        { "Уровень лая", "Barking Level" },
        { "Потребность в умственной стимуляции", "Mental Stimulation Needs" }
    };

        // Узел дерева
        class Node
        {
            public string Question { get; set; }
            public Node Yes { get; set; }
            public Node No { get; set; }
            public Node IDK { get; set; }

            public Node(string question)
            {
                Question = question;
            }
        }

        private Node currentNode; // Текущий узел, где находимся в дереве
        private Node root; // Корневой узел

        // Словарь для хранения значений параметров
        private Dictionary<string, int> parameterValues;

        public Form1()
        {
            InitializeComponent();
            InitializeTree(); // Инициализация дерева
            InitializeParameters(); // Инициализация параметров
            ShowNextQuestion(); // Показать первый вопрос
        }

        // Инициализация дерева вопросов
        private void InitializeTree()
        {
            // Создаем узлы
            Node ласковыйССемьей = new Node("Ласковый с семьей");
            Node хорошоСМаленькимиДетьми = new Node("Хорошо с маленькими детьми");
            Node хорошоСДругимиСобаками = new Node("Хорошо с другими собаками");
            Node уровеньИгривости = new Node("Уровень игривости");
            Node уровеньОбучаемости = new Node("Уровень обучаемости");
            Node потребностьВУмственнойСтимуляции = new Node("Потребность в умственной стимуляции");
            Node сторожеваяСобака = new Node("Сторожевая собака/Защитный характер");
            Node открытостьКНезнакомцам = new Node("Открытость к незнакомцам");
            Node уровеньЭнергии = new Node("Уровень энергии");
            Node уровеньАдаптивности = new Node("Уровень адаптивности");
            Node уходЗаШерстью = new Node("Готовы ли вы тратить время на постоянный уход за собакой?");
            Node частотаУхода = new Node("Частота ухода за шерстью");
            Node уровеньЛиньки = new Node("Уровень линьки");
            Node уровеньСлюнотечения = new Node("Уровень слюнотечения");
            Node типШерсти = new Node("Тип шерсти");
            Node длинаШерсти = new Node("Длина шерсти");
            Node уровеньЛая = new Node("Уровень лая");

            // Определяем связи между узлами (да/нет/не знаю)
            ласковыйССемьей.Yes = хорошоСМаленькимиДетьми;
            ласковыйССемьей.No = уровеньИгривости;
            ласковыйССемьей.IDK = хорошоСМаленькимиДетьми;

            хорошоСМаленькимиДетьми.Yes = хорошоСДругимиСобаками;
            хорошоСМаленькимиДетьми.No = уровеньИгривости;
            хорошоСМаленькимиДетьми.IDK = уровеньИгривости;

            хорошоСДругимиСобаками.Yes = уровеньИгривости;
            хорошоСДругимиСобаками.No = уровеньИгривости;
            хорошоСДругимиСобаками.IDK = уровеньИгривости;

            уровеньИгривости.Yes = уровеньОбучаемости;
            уровеньИгривости.No = уровеньЭнергии;
            уровеньИгривости.IDK = уровеньЭнергии;

            уровеньОбучаемости.Yes = потребностьВУмственнойСтимуляции;
            уровеньОбучаемости.No = уровеньЭнергии;
            уровеньОбучаемости.IDK = уровеньЭнергии;

            потребностьВУмственнойСтимуляции.Yes = уходЗаШерстью;
            потребностьВУмственнойСтимуляции.No = уровеньАдаптивности;
            потребностьВУмственнойСтимуляции.IDK = уровеньАдаптивности;

            уровеньЭнергии.Yes = уровеньАдаптивности;
            уровеньЭнергии.No = уровеньАдаптивности;
            уровеньЭнергии.IDK = уровеньАдаптивности;

            уровеньАдаптивности.Yes = уходЗаШерстью;
            уровеньАдаптивности.No = уходЗаШерстью;
            уровеньАдаптивности.IDK = уходЗаШерстью;

            уходЗаШерстью.Yes = частотаУхода;
            уходЗаШерстью.No = типШерсти;
            уходЗаШерстью.IDK = типШерсти;

            частотаУхода.Yes = уровеньЛиньки;
            частотаУхода.No = уровеньСлюнотечения;
            частотаУхода.IDK = уровеньСлюнотечения;

            типШерсти.Yes = длинаШерсти;
            типШерсти.No = уровеньЛая;
            типШерсти.IDK = уровеньЛая;

            // Устанавливаем корневой узел
            root = ласковыйССемьей;
            currentNode = root; // Начинаем с корневого узла
        }

        // Инициализация параметров
        private void InitializeParameters()
        {
            // Инициализируем словарь параметров со значениями 0
            parameterValues = new Dictionary<string, int>
            {
                { "Affectionate With Family", 0 },//Ласковый с семьей
                { "Good With Young Children", 0 },//Хорошо с маленькими детьми
                { "Good With Other Dogs", 0 },//Хорошо с другими собаками
                { "Playfulness Level", 0 },//Уровень игривости
                { "Trainability Level", 0 },//Уровень обучаемости
                { "Mental Stimulation Needs", 0 },//Потребность в умственной стимуляции
                { "Watchdog/Protective Nature", 0 },//Сторожевая собака/Защитный характер
                { "Openness To Strangers", 0 },//Открытость к незнакомцам
                { "Energy Level", 0 },//Уровень энергии
                { "Adaptability Level", 0 },//Уровень адаптивности
                //{ "Готовы ли вы тратить время на постоянный уход за собакой?", 0 },//Готовы ли вы тратить время на постоянный уход за собакой?
                { "Coat Grooming Frequency", 0 },//Частота ухода за шерстью
                { "Shedding Level", 0 },//Уровень линьки
                { "Drooling Level", 0 },//Уровень слюнотечения
                { "Barking Level", 0 }//Уровень лая
            };
        }

        // Метод для показа следующего вопроса
        private void ShowNextQuestion()
        {
            if (currentNode != null)
            {
                label_Question.Text = currentNode.Question;
            }
            else
            {
                // Показываем результаты, когда дерево пройдено
                ShowResults();
            }
        }

        // Метод для отображения результатов
        // Обновленный метод ShowResults()
        // Обновленный метод ShowResults()
        private void ShowResults()
        {
            // Загружаем данные о породах из CSV
            DataTable breedDataTable = LoadBreedData();

            // Список для хранения оценок соответствия каждой породы
            List<Tuple<string, int>> breedScores = new List<Tuple<string, int>>();

            // Словарь для хранения баллов параметров для каждой породы
            Dictionary<string, Dictionary<string, int>> breedParameterScores = new Dictionary<string, Dictionary<string, int>>();

            // Вычисляем оценку соответствия для каждой породы
            foreach (DataRow breedData in breedDataTable.Rows)
            {
                int score = CalculateMatchScore(parameterValues, breedData);
                string breedName = breedData["Breed"].ToString();
                breedScores.Add(new Tuple<string, int>(breedName, score));

                // Хранение баллов параметров
                var parameterScores = new Dictionary<string, int>();
                foreach (var parameter in parameterValues)
                {
                    string parameterName = parameter.Key;
                    int userValue = parameter.Value;

                    // Получаем соответствующее имя столбца из CSV
                    if (columnMapping.ContainsKey(parameterName))
                    {
                        string columnName = columnMapping[parameterName];
                        int breedValue = Convert.ToInt32(breedData[columnName]);
                        parameterScores[parameterName] = (breedValue);
                    }
                }
                breedParameterScores[breedName] = parameterScores;
            }

            // Сортируем по убыванию оценки соответствия
            breedScores.Sort((a, b) => b.Item2.CompareTo(a.Item2));

            // Выводим топ-3 породы
            StringBuilder result = new StringBuilder("Подходящие породы:\n");
            for (int i = 0; i < 3 && i < breedScores.Count; i++)
            {
                string breedName = breedScores[i].Item1;
                int score = breedScores[i].Item2;
                result.AppendLine($"{breedName} (оценка: {score})");

                // Вывод баллов параметров для каждой из трех пород
                result.AppendLine("Баллы параметров:");
                foreach (var parameterScore in breedParameterScores[breedName])
                {
                    result.AppendLine($"{parameterScore.Key}: {parameterScore.Value}");
                }
            }

            label_Question.Text = result.ToString();
            button_Yes.Enabled = false;
            button_IDK.Enabled = false;
            button_No.Enabled = false;
        }




        // Обработчик для кнопки "Да"
        private void button_Yes_Click(object sender, EventArgs e)
        {
            if (currentNode != null)
            {
                // Присваиваем параметру значение 2
                parameterValues[currentNode.Question] = 2;

                // Переход на "Да"
                currentNode = currentNode.Yes;
                ShowNextQuestion();
            }
        }

        // Обработчик для кнопки "Не знаю"
        private void button_IDK_Click(object sender, EventArgs e)
        {
            if (currentNode != null)
            {
                // Присваиваем параметру значение 1
                parameterValues[currentNode.Question] = 1;

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
                // Присваиваем параметру значение 0 (по умолчанию, можно не менять)
                parameterValues[currentNode.Question] = 0;

                // Переход на "Нет"
                currentNode = currentNode.No;
                ShowNextQuestion();
            }
        }
        // Метод для вычисления оценки соответствия породы
        private int CalculateMatchScore(Dictionary<string, int> userPreferences, DataRow breedData)
        {
            int score = 0;
            // Словарь для сопоставления названий параметров из кода с колонками из CSV
            Dictionary<string, string> columnMapping = new Dictionary<string, string>
    {
        { "Ласковый с семьей", "Affectionate With Family" },
        { "Хорошо с маленькими детьми", "Good With Young Children" },
        { "Хорошо с другими собаками", "Good With Other Dogs" },
        { "Уровень линьки", "Shedding Level" },
        { "Частота ухода за шерстью", "Coat Grooming Frequency" },
        { "Уровень слюнотечения", "Drooling Level" },
        { "Открытость к незнакомцам", "Openness To Strangers" },
        { "Уровень игривости", "Playfulness Level" },
        { "Сторожевая собака/Защитный характер", "Watchdog/Protective Nature" },
        { "Уровень адаптивности", "Adaptability Level" },
        { "Уровень обучаемости", "Trainability Level" },
        { "Уровень энергии", "Energy Level" },
        { "Уровень лая", "Barking Level" },
        { "Потребность в умственной стимуляции", "Mental Stimulation Needs" }
    };

            foreach (var parameter in userPreferences)
            {
                string parameterName = parameter.Key;
                int userValue = parameter.Value;

                if (columnMapping.ContainsKey(parameterName))
                {
                    string columnName = columnMapping[parameterName];
                    int breedValue = Convert.ToInt32(breedData[columnName]);

                    // Увеличиваем оценку, если значения совпадают
                    if (userValue == breedValue)
                    {
                        score += 2;
                    }
                    else if (Math.Abs(userValue - breedValue) == 1)
                    {
                        // Если значения различаются на 1, добавляем 1 балл
                        score += 1;
                    }
                }
            }
            return score;
        }
        // Метод для загрузки данных о породах из CSV
        private DataTable LoadBreedData()
        {
            DataTable dataTable = new DataTable();
            string[] csvLines = System.IO.File.ReadAllLines("C:\\Users\\egori\\OneDrive\\Рабочий стол\\Учёба\\4 курс 7 семестр\\СППР\\Lab 1 Диагностическая ситсема принтия решений\\breed_traits_new.csv");
            if (csvLines.Length > 0)
            {
                // Заголовки
                string[] headers = csvLines[0].Split(',');
                foreach (string header in headers)
                {
                    dataTable.Columns.Add(header);
                }

                // Данные
                for (int i = 1; i < csvLines.Length; i++)
                {
                    string[] values = csvLines[i].Split(',');
                    dataTable.Rows.Add(values);
                }
            }
            return dataTable;
        }
        // Метод для вычисления оценки соответствия породы

    }
}
