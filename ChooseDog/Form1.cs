using System;
using System.Collections.Generic;
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
                { "Ласковый с семьей", 0 },
                { "Хорошо с маленькими детьми", 0 },
                { "Хорошо с другими собаками", 0 },
                { "Уровень игривости", 0 },
                { "Уровень обучаемости", 0 },
                { "Потребность в умственной стимуляции", 0 },
                { "Сторожевая собака/Защитный характер", 0 },
                { "Открытость к незнакомцам", 0 },
                { "Уровень энергии", 0 },
                { "Уровень адаптивности", 0 },
                { "Готовы ли вы тратить время на постоянный уход за собакой?", 0 },
                { "Частота ухода за шерстью", 0 },
                { "Уровень линьки", 0 },
                { "Уровень слюнотечения", 0 },
                { "Тип шерсти", 0 },
                { "Длина шерсти", 0 },
                { "Уровень лая", 0 }
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
    }
}
