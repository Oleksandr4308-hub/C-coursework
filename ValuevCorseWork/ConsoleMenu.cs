using BLL.Interfaces;
using BLL.Models;

namespace PL
{
    public class ConsoleMenu
    {
        private readonly IWorkerService _workerService;
        private readonly IDepartmentService _deptService;
        private readonly IPositionService _posService;
        private readonly IProjectService _projService;

        public ConsoleMenu(IWorkerService workerService,
                           IDepartmentService deptService,
                           IPositionService posService,
                           IProjectService projService)
        {
            _workerService = workerService;
            _deptService = deptService;
            _posService = posService;
            _projService = projService;
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ВІДДІЛ КАДРІВ ===");
                Console.WriteLine("1. Управління робітниками");
                Console.WriteLine("2. Управління підрозділами");
                Console.WriteLine("3. Управління посадами");
                Console.WriteLine("4. Пошук");
                Console.WriteLine("0. Вийти");
                Console.Write("Ваш вибір: ");

                switch (Console.ReadLine())
                {
                    case "1": WorkersMenu(); break;
                    case "2": DepartmentsMenu(); break;
                    case "3": PositionsMenu(); break;
                    case "4": SearchMenu(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Невідомий вибір!");
                        break;
                }
            }
        }

        

        private void WorkersMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Робітники ===");
                Console.WriteLine("1. Додати робітника");
                Console.WriteLine("2. Видалити робітника");
                Console.WriteLine("3. Змінити дані робітника");
                Console.WriteLine("4. Переглянути робітника");
                Console.WriteLine("5. Переглянути проекти робітника");
                Console.WriteLine("6. Список всіх робітників");
                Console.WriteLine("0. Назад");
                Console.Write("Ваш вибір: ");

                switch (Console.ReadLine())
                {
                    case "1": AddWorker(); break;
                    case "2": DeleteWorker(); break;
                    case "3": UpdateWorker(); break;
                    case "4": ViewWorker(); break;
                    case "5": ViewWorkerProjects(); break;
                    case "6": WorkerListMenu(); break;
                    case "0": return;
                }
            }
        }

        private void WorkerListMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Список робітників ===");
            Console.WriteLine("1. Відсортувати по імені");
            Console.WriteLine("2. Відсортувати по прізвищу");
            Console.WriteLine("3. Відсортувати по заробітній платі (посада)");
            Console.WriteLine("4. Показати усіх без сортування");
            Console.Write("Вибір: ");

            IEnumerable<WorkerModel> list = Enumerable.Empty<WorkerModel>();

            switch (Console.ReadLine())
            {
                case "1": list = _workerService.GetAllWorkersSortedByName(); break;
                case "2": list = _workerService.GetAllWorkersSortedByLastName(); break;
                case "3": list = _workerService.GetAllWorkersSortedByPositionSalary(); break;
                case "4": list = _workerService.GetAllWorkers(); break;
            }

            foreach (var w in list)
                PrintWorker(w);

            Pause();
        }

        private void AddWorker()
        { 
            WorkerModel w = new();

            Console.Write("Ім’я: ");
            w.FirstName = Console.ReadLine()!;
            Console.Write("Прізвище: ");
            w.LastName = Console.ReadLine()!;
            Console.Write("Номер зарплатного рахунку: ");
            w.SalaryAccountNumber = Console.ReadLine()!;
            Console.Write("Підрозділ: ");
            w.DepartmentName = Console.ReadLine()!;
            Console.Write("Посада: ");
            w.PositionName = Console.ReadLine()!;
            Console.Write("Стаж (років): ");
            w.WorkExperienceYears = int.Parse(Console.ReadLine()!);

            _workerService.AddWorker(w);
            Console.WriteLine("Робітника додано.");
            Pause();
        }

        private void DeleteWorker()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine()!);
            _workerService.DeleteWorker(id);
            Console.WriteLine("Видалено.");
            Pause();
        }

        private void UpdateWorker()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine()!);

            var w = _workerService.GetWorker(id);
            
            if (w == null) 
            { 
                Console.WriteLine("Не знайдено"); 
                Pause(); 
                return; 
            }

            Console.Write("Нове ім’я: ");
            w.FirstName = Console.ReadLine()!;
            Console.Write("Нове прізвище: ");
            w.LastName = Console.ReadLine()!;
            Console.Write("Новий рахунок: ");
            w.SalaryAccountNumber = Console.ReadLine()!;
            Console.Write("Новий підрозділ: ");
            w.DepartmentName = Console.ReadLine()!;
            Console.Write("Нова посада: ");
            w.PositionName = Console.ReadLine()!;
            Console.Write("Новий стаж: ");
            w.WorkExperienceYears = int.Parse(Console.ReadLine()!);

            _workerService.UpdateWorker(w);
            Console.WriteLine("Оновлено.");
            Pause();
        }

        private void ViewWorker()
        {
            Console.Write("ID: ");
            var w = _workerService.GetWorker(int.Parse(Console.ReadLine()!));
            if (w == null) Console.WriteLine("Не знайдено");
            else PrintWorker(w);
            Pause();
        }

        private void ViewWorkerProjects()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine()!);
            var projects = _workerService.GetWorkerProjects(id);

            foreach (var p in projects)
                Console.WriteLine($"{p.Id}. {p.Name} — {p.Cost} грн");

            Pause();
        }

        private void PrintWorker(WorkerModel w)
        {
            Console.WriteLine($"[{w.Id}] {w.FirstName} {w.LastName}, Посада: {w.PositionName}, Підрозділ: {w.DepartmentName}, Стаж: {w.WorkExperienceYears} років");
        }

       

        private void DepartmentsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Підрозділи ===");
                Console.WriteLine("1. Додати підрозділ");
                Console.WriteLine("2. Змінити підрозділ");
                Console.WriteLine("3. Переглянути підрозділ");
                Console.WriteLine("4. Список робітників підрозділу");
                Console.WriteLine("0. Назад");

                switch (Console.ReadLine())
                {
                    case "1": AddDepartment(); break;
                    case "2": UpdateDepartment(); break;
                    case "3": ViewDepartment(); break;
                    case "4": ViewDepartmentWorkers(); break;
                    case "0": return;
                }
            }
        }

        private void AddDepartment()
        {
            Console.Write("Назва: ");
            _deptService.AddDepartment(new DepartmentModel { Name = Console.ReadLine()! });
            Console.WriteLine("Додано.");
            Pause();
        }

        private void UpdateDepartment()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine()!);
            var d = _deptService.GetDepartment(id);

            if (d == null) { Console.WriteLine("Не знайдено"); Pause(); return; }

            Console.Write("Нова назва: ");
            d.Name = Console.ReadLine()!;

            _deptService.UpdateDepartment(d);
            Console.WriteLine("Оновлено.");
            Pause();
        }

        private void ViewDepartment()
        {
            Console.Write("ID: ");
            var d = _deptService.GetDepartment(int.Parse(Console.ReadLine()!));

            if (d == null) Console.WriteLine("Не знайдено");
            else Console.WriteLine($"[{d.Id}] {d.Name}");

            Pause();
        }

        private void ViewDepartmentWorkers()
        {
            Console.Write("Назва підрозділу: ");
            string name = Console.ReadLine()!;

            Console.WriteLine("1. Сортувати по посаді");
            Console.WriteLine("2. Сортувати по вартості проектів");
            Console.WriteLine("3. Без сортування");
            Console.Write("Вибір: ");

            IEnumerable<WorkerModel> workers = Enumerable.Empty<WorkerModel>();

            switch (Console.ReadLine())
            {
                case "1": workers = _deptService.GetWorkersInDepartmentSortedByPosition(name); break;
                case "2": workers = _deptService.GetWorkersInDepartmentSortedByProjectCost(name); break;
                case "3": workers = _deptService.GetWorkersInDepartment(name); break;
            }

            foreach (var w in workers)
                PrintWorker(w);

            Pause();
        }


        private void PositionsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Посади ===");
                Console.WriteLine("1. Додати посаду");
                Console.WriteLine("2. Змінити посаду");
                Console.WriteLine("3. Топ-5 привабливих посад");
                Console.WriteLine("4. Найприбутковіший робітник на посаді");
                Console.WriteLine("0. Назад");

                switch (Console.ReadLine())
                {
                    case "1": AddPosition(); break;
                    case "2": UpdatePosition(); break;
                    case "3": ShowTop5Positions(); break;
                    case "4": ShowMostProfitableWorker(); break;
                    case "0": return;
                }
            }
        }

        private void AddPosition()
        {
            PositionModel p = new();

            Console.Write("Назва: ");
            p.Title = Console.ReadLine()!;
            Console.Write("Зарплата: ");
            p.Salary = decimal.Parse(Console.ReadLine()!);
            Console.Write("Годин на тиждень: ");
            p.WorkHours = int.Parse(Console.ReadLine()!);

            _posService.AddPosition(p);
            Console.WriteLine("Додано.");
            Pause();
        }

        private void UpdatePosition()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine()!);

            var p = _posService.GetPosition(id);
            if (p == null) { Console.WriteLine("Не знайдено"); Pause(); return; }

            Console.Write("Нова назва: ");
            p.Title = Console.ReadLine()!;
            Console.Write("Нова зарплата: ");
            p.Salary = decimal.Parse(Console.ReadLine()!);
            Console.Write("Нові години: ");
            p.WorkHours = int.Parse(Console.ReadLine()!);

            _posService.UpdatePosition(p);
            Console.WriteLine("Оновлено.");
            Pause();
        }

        private void ShowTop5Positions()
        {
            var list = _posService.Top5AttractivePositions();
            foreach (var p in list)
                Console.WriteLine($"{p.Title}: {p.Salary} грн / {p.WorkHours} год");
            Pause();
        }

        private void ShowMostProfitableWorker()
        {
            Console.Write("Назва посади: ");
            var w = _posService.MostProfitableWorkerOnPosition(Console.ReadLine()!);

            if (w == null) Console.WriteLine("Немає таких робітників");
            else PrintWorker(w);

            Pause();
        }

       

        private void SearchMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ПОШУК ===");
                Console.WriteLine("1. Пошук робітників по ключовому слову");
                Console.WriteLine("2. Пошук проектів по ключовому слову");
                Console.WriteLine("3. Пошук серед всіх даних");
                Console.WriteLine("4. Розширений пошук робітника");
                Console.WriteLine("0. Назад");

                switch (Console.ReadLine())
                {
                    case "1": SearchWorkers(); break;
                    case "2": SearchProjects(); break;
                    case "3": SearchAll(); break;
                    case "4": AdvancedWorkerSearch(); break;
                    case "0": return;
                }
            }
        }

        private void SearchWorkers()
        {
            Console.Write("Ключове слово: ");
            var list = _workerService.SearchWorkers(Console.ReadLine()!);

            foreach (var w in list)
                PrintWorker(w);

            Pause();
        }

        private void SearchProjects()
        {
            Console.Write("Ключове слово: ");
            var list = _projService.SearchProjects(Console.ReadLine()!);

            foreach (var p in list)
                Console.WriteLine($"{p.Id}. {p.Name} — {p.Cost} грн");

            Pause();
        }

        private void SearchAll()
        {
            Console.Write("Ключове слово: ");
            string key = Console.ReadLine()!;

            Console.WriteLine("--- Робітники ---");
            foreach (var w in _workerService.SearchWorkers(key))
                PrintWorker(w);

            Console.WriteLine("--- Проекти ---");
            foreach (var p in _projService.SearchProjects(key))
                Console.WriteLine($"{p.Id}. {p.Name} — {p.Cost}");

            
            Pause();
        }

        private void AdvancedWorkerSearch()
        {
            Console.Write("Прізвище: ");
            string lastName = Console.ReadLine()!;
            Console.Write("Рахунок: ");
            string account = Console.ReadLine()!;

            var list = _workerService.AdvancedSearch(lastName, account);

            foreach (var w in list)
                PrintWorker(w);

            Pause();
        }

       

        private void Pause()
        {
            Console.WriteLine("Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}
