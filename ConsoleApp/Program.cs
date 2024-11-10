using System;
using System.Collections.Generic;
using System.Linq;
using GoodsManagement; // Підключення DLL-бібліотеки

namespace ConsoleApp
{
    class Program
    {
        static DataStorage dataStorage = new DataStorage();

        static void Main(string[] args)
        {
            dataStorage.LoadData(); // Завантаження даних з файлу

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Управління категоріями");
                Console.WriteLine("2. Управління товарами");
                Console.WriteLine("3. Управління постачальниками");
                Console.WriteLine("4. Пошук");
                Console.WriteLine("5. Вихід");

                Console.Write("Введіть номер пункту: ");
                int choice = GetIntInput();

                switch (choice)
                {
                    case 1:
                        ManageCategories();
                        break;
                    case 2:
                        ManageProducts();
                        break;
                    case 3:
                        ManageSuppliers();
                        break;
                    case 4:
                        Search();
                        break;
                    case 5:
                        dataStorage.SaveData(); // Збереження даних в файл
                        Console.WriteLine("До побачення!");
                        return;
                    default:
                        Console.WriteLine("Неправильний вибір. Спробуйте знову.");
                        break;
                }
            }
        }

        static void ManageCategories()
        {
            while (true)
            {
                Console.WriteLine("\nУправління категоріями:");
                Console.WriteLine("1. Додати категорію");
                Console.WriteLine("2. Видалити категорію");
                Console.WriteLine("3. Змінити категорію");
                Console.WriteLine("4. Переглянути категорію");
                Console.WriteLine("5. Переглянути всі категорії");
                Console.WriteLine("6. Повернутися до головного меню");

                Console.Write("Введіть номер пункту: ");
                int choice = GetIntInput();

                switch (choice)
                {
                    case 1:
                        AddCategory();
                        break;
                    case 2:
                        DeleteCategory();
                        break;
                    case 3:
                        EditCategory();
                        break;
                    case 4:
                        ViewCategory();
                        break;
                    case 5:
                        DisplayCategories();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Неправильний вибір. Спробуйте знову.");
                        break;
                }
            }
        }

        static void AddCategory()
        {
            Console.Write("Введіть назву категорії: ");
            string name = GetStringInput();

            int nextId = dataStorage.Categories.Count > 0 ? dataStorage.Categories.Max(c => c.Id) + 1 : 1;
            dataStorage.Categories.Add(new Category(nextId, name));
            Console.WriteLine("Категорія успішно додана.");
        }

        static void DeleteCategory()
        {
            DisplayCategories();
            Console.Write("Введіть ID категорії для видалення: ");
            int id = GetIntInput();

            Category categoryToDelete = dataStorage.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryToDelete != null)
            {
                dataStorage.Categories.Remove(categoryToDelete);
                Console.WriteLine("Категорія успішно видалена.");
            }
            else
            {
                Console.WriteLine("Категорія з таким ID не знайдена.");
            }
        }

        static void EditCategory()
        {
            DisplayCategories();
            Console.Write("Введіть ID категорії для редагування: ");
            int id = GetIntInput();

            Category categoryToEdit = dataStorage.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryToEdit != null)
            {
                Console.Write("Введіть нову назву категорії: ");
                string newName = GetStringInput();
                categoryToEdit.Name = newName;
                Console.WriteLine("Категорія успішно змінена.");
            }
            else
            {
                Console.WriteLine("Категорія з таким ID не знайдена.");
            }
        }

        static void ViewCategory()
        {
            DisplayCategories();
            Console.Write("Введіть ID категорії для перегляду: ");
            int id = GetIntInput();

            Category categoryToView = dataStorage.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryToView != null)
            {
                Console.WriteLine($"ID: {categoryToView.Id}");
                Console.WriteLine($"Назва: {categoryToView.Name}");
            }
            else
            {
                Console.WriteLine("Категорія з таким ID не знайдена.");
            }
        }

        static void DisplayCategories()
        {
            if (dataStorage.Categories.Count == 0)
            {
                Console.WriteLine("Немає категорій для відображення.");
                return;
            }
            Console.WriteLine("\nСписок категорій:");
            foreach (Category category in dataStorage.Categories)
            {
                Console.WriteLine($"ID: {category.Id}, Назва: {category.Name}");
            }
        }

        static void ManageProducts()
        {
            while (true)
            {
                Console.WriteLine("\nУправління товарами:");
                Console.WriteLine("1. Додати товар");
                Console.WriteLine("2. Видалити товар");
                Console.WriteLine("3. Змінити товар");
                Console.WriteLine("4. Змінити кількість товару на складі");
                Console.WriteLine("5. Переглянути товар");
                Console.WriteLine("6. Переглянути список всіх товарів");
                Console.WriteLine("7. Повернутися до головного меню");

                Console.Write("Введіть номер пункту: ");
                int choice = GetIntInput();

                switch (choice)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        DeleteProduct();
                        break;
                    case 3:
                        EditProduct();
                        break;
                    case 4:
                        EditProductQuantity();
                        break;
                    case 5:
                        ViewProduct();
                        break;
                    case 6:
                        DisplayProducts();
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Неправильний вибір. Спробуйте знову.");
                        break;
                }
            }
        }

        static void AddProduct()
        {
            DisplayCategories();
            Console.Write("Введіть ID категорії для товару: ");
            int categoryId = GetIntInput();
            Category category = dataStorage.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                Console.WriteLine("Категорія з таким ID не знайдена.");
                return;
            }

            Console.Write("Введіть назву товару: ");
            string name = GetStringInput();
            Console.Write("Введіть бренд товару: ");
            string brand = GetStringInput();
            Console.Write("Введіть ціну товару: ");
            decimal price = GetDecimalInput();
            Console.Write("Введіть кількість товару на складі: ");
            int quantity = GetIntInput();

            int nextId = dataStorage.Products.Count > 0 ? dataStorage.Products.Max(p => p.Id) + 1 : 1;
            try
            {
                dataStorage.Products.Add(new Product(nextId, name, brand, price, quantity, category));
                Console.WriteLine("Товар успішно доданий.");
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DuplicateProductException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void DeleteProduct()
        {
            DisplayProducts();
            Console.Write("Введіть ID товару для видалення: ");
            int id = GetIntInput();

            Product productToDelete = dataStorage.Products.FirstOrDefault(p => p.Id == id);
            if (productToDelete != null)
            {
                dataStorage.Products.Remove(productToDelete);
                Console.WriteLine("Товар успішно видалений.");
            }
            else
            {
                Console.WriteLine("Товар з таким ID не знайдений.");
            }
        }

        static void EditProduct()
        {
            DisplayProducts();
            Console.Write("Введіть ID товару для редагування: ");
            int id = GetIntInput();

            Product productToEdit = dataStorage.Products.FirstOrDefault(p => p.Id == id);
            if (productToEdit != null)
            {
                Console.Write("Введіть нову назву товару: ");
                string newName = GetStringInput();
                Console.Write("Введіть новий бренд товару: ");
                string newBrand = GetStringInput();
                Console.Write("Введіть нову ціну товару: ");
                decimal newPrice = GetDecimalInput();
                Console.Write("Введіть нову категорію товару: ");
                int newCategoryId = GetIntInput();
                Category newCategory = dataStorage.Categories.FirstOrDefault(c => c.Id == newCategoryId);

                if (newCategory == null)
                {
                    Console.WriteLine("Категорія з таким ID не знайдена.");
                    return;
                }

                productToEdit.Name = newName;
                productToEdit.Brand = newBrand;
                productToEdit.Price = newPrice;
                productToEdit.Category = newCategory;
                Console.WriteLine("Товар успішно змінений.");
            }
            else
            {
                Console.WriteLine("Товар з таким ID не знайдений.");
            }
        }

        static void EditProductQuantity()
        {
            DisplayProducts();
            Console.Write("Введіть ID товару для зміни кількості: ");
            int id = GetIntInput();

            Product productToEdit = dataStorage.Products.FirstOrDefault(p => p.Id == id);
            if (productToEdit != null)
            {
                Console.Write("Введіть нову кількість товару: ");
                int newQuantity = GetIntInput();
                try
                {
                    productToEdit.Quantity = newQuantity;
                    Console.WriteLine("Кількість товару успішно змінена.");
                }
                catch (InvalidQuantityException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Товар з таким ID не знайдений.");
            }
        }

        static void ViewProduct()
        {
            DisplayProducts();
            Console.Write("Введіть ID товару для перегляду: ");
            int id = GetIntInput();

            Product productToView = dataStorage.Products.FirstOrDefault(p => p.Id == id);
            if (productToView != null)
            {
                Console.WriteLine($"ID: {productToView.Id}");
                Console.WriteLine($"Назва: {productToView.Name}");
                Console.WriteLine($"Бренд: {productToView.Brand}");
                Console.WriteLine($"Ціна: {productToView.Price}");
                Console.WriteLine($"Кількість: {productToView.Quantity}");
                Console.WriteLine($"Категорія: {productToView.Category.Name}");
            }
            else
            {
                Console.WriteLine("Товар з таким ID не знайдений.");
            }
        }

        static void DisplayProducts()
        {
            if (dataStorage.Products.Count == 0)
            {
                Console.WriteLine("Немає товарів для відображення.");
                return;
            }

            Console.WriteLine("\nСписок товарів:");
            foreach (Product product in dataStorage.Products)
            {
                Console.WriteLine($"ID: {product.Id}, Назва: {product.Name}, Бренд: {product.Brand}, Ціна: {product.Price}, Кількість: {product.Quantity}, Категорія: {product.Category.Name}");
            }
        }

        static void ManageSuppliers()
        {
            while (true)
            {
                Console.WriteLine("\nУправління постачальниками:");
                Console.WriteLine("1. Додати постачальника");
                Console.WriteLine("2. Видалити постачальника");
                Console.WriteLine("3. Змінити постачальника");
                Console.WriteLine("4. Переглянути постачальника");
                Console.WriteLine("5. Переглянути список всіх постачальників");
                Console.WriteLine("6. Повернутися до головного меню");

                Console.Write("Введіть номер пункту: ");
                int choice = GetIntInput();

                switch (choice)
                {
                    case 1:
                        AddSupplier();
                        break;
                    case 2:
                        DeleteSupplier();
                        break;
                    case 3:
                        EditSupplier();
                        break;
                    case 4:
                        ViewSupplier();
                        break;
                    case 5:
                        DisplaySuppliers();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Неправильний вибір. Спробуйте знову.");
                        break;
                }
            }
        }

        static void AddSupplier()
        {
            Console.Write("Введіть ім'я постачальника: ");
            string firstName = GetStringInput();
            Console.Write("Введіть прізвище постачальника: ");
            string lastName = GetStringInput();

            int nextId = dataStorage.Suppliers.Count > 0 ? dataStorage.Suppliers.Max(s => s.Id) + 1 : 1;
            dataStorage.Suppliers.Add(new Supplier(nextId, firstName, lastName));
            Console.WriteLine("Постачальник успішно доданий.");
        }

        static void DeleteSupplier()
        {
            DisplaySuppliers();
            Console.Write("Введіть ID постачальника для видалення: ");
            int id = GetIntInput();

            Supplier supplierToDelete = dataStorage.Suppliers.FirstOrDefault(s => s.Id == id);
            if (supplierToDelete != null)
            {
                dataStorage.Suppliers.Remove(supplierToDelete);
                Console.WriteLine("Постачальник успішно видалений.");
            }
            else
            {
                Console.WriteLine("Постачальник з таким ID не знайдений.");
            }
        }

        static void EditSupplier()
        {
            DisplaySuppliers();
            Console.Write("Введіть ID постачальника для редагування: ");
            int id = GetIntInput();

            Supplier supplierToEdit = dataStorage.Suppliers.FirstOrDefault(s => s.Id == id);
            if (supplierToEdit != null)
            {
                Console.Write("Введіть нове ім'я постачальника: ");
                string newFirstName = GetStringInput();
                Console.Write("Введіть нове прізвище постачальника: ");
                string newLastName = GetStringInput();

                supplierToEdit.FirstName = newFirstName;
                supplierToEdit.LastName = newLastName;
                Console.WriteLine("Постачальник успішно змінений.");
            }
            else
            {
                Console.WriteLine("Постачальник з таким ID не знайдений.");
            }
        }

        static void ViewSupplier()
        {
            DisplaySuppliers();
            Console.Write("Введіть ID постачальника для перегляду: ");
            int id = GetIntInput();

            Supplier supplierToView = dataStorage.Suppliers.FirstOrDefault(s => s.Id == id);
            if (supplierToView != null)
            {
                Console.WriteLine($"ID: {supplierToView.Id}");
                Console.WriteLine($"Ім'я: {supplierToView.FirstName}");
                Console.WriteLine($"Прізвище: {supplierToView.LastName}");
            }
            else
            {
                Console.WriteLine("Постачальник з таким ID не знайдений.");
            }
        }

        static void DisplaySuppliers()
        {
            if (dataStorage.Suppliers.Count == 0)
            {
                Console.WriteLine("Немає постачальників для відображення.");
                return;
            }
            Console.WriteLine("\nСписок постачальників:");
            foreach (Supplier supplier in dataStorage.Suppliers)
            {
                Console.WriteLine($"ID: {supplier.Id}, Ім'я: {supplier.FirstName}, Прізвище: {supplier.LastName}");
            }
        }

        static void Search()
        {
            Console.WriteLine("\nПошук:");
            Console.WriteLine("1. Пошук по ключовому слову серед товарів");
            Console.WriteLine("2. Пошук по ключовому слову серед постачальників");
            Console.WriteLine("3. Повернутися до головного меню");

            Console.Write("Введіть номер пункту: ");
            int choice = GetIntInput();

            switch (choice)
            {
                case 1:
                    SearchProducts();
                    break;
                case 2:
                    SearchSuppliers();
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("Неправильний вибір. Спробуйте знову.");
                    break;
            }
        }

        static void SearchProducts()
        {
            Console.Write("Введіть ключове слово для пошуку товарів: ");
            string keyword = GetStringInput();

            var foundProducts = dataStorage.Products.Where(p => p.Name.Contains(keyword) || p.Brand.Contains(keyword));

            if (foundProducts.Any())
            {
                Console.WriteLine("\nЗнайдені товари:");
                foreach (Product product in foundProducts)
                {
                    Console.WriteLine($"ID: {product.Id}, Назва: {product.Name}, Бренд: {product.Brand}, Ціна: {product.Price}, Кількість: {product.Quantity}, Категорія: {product.Category.Name}");
                }
            }
            else
            {
                Console.WriteLine("Товарів за заданим ключовим словом не знайдено.");
            }
        }

        static void SearchSuppliers()
        {
            Console.Write("Введіть ключове слово для пошуку постачальників: ");
            string keyword = GetStringInput();

            var foundSuppliers = dataStorage.Suppliers.Where(s => s.FirstName.Contains(keyword) || s.LastName.Contains(keyword));

            if (foundSuppliers.Any())
            {
                Console.WriteLine("\nЗнайдені постачальники:");
                foreach (Supplier supplier in foundSuppliers)
                {
                    Console.WriteLine($"ID: {supplier.Id}, Ім'я: {supplier.FirstName}, Прізвище: {supplier.LastName}");
                }
            }
            else
            {
                Console.WriteLine("Постачальників за заданим ключовим словом не знайдено.");
            }
        }

        static int GetIntInput()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                Console.WriteLine("Неправильний формат вводу. Введіть ціле число.");
            }
        }

        static string GetStringInput()
        {
            return Console.ReadLine();
        }

        static decimal GetDecimalInput()
        {
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal result))
                {
                    return result;
                }
                Console.WriteLine("Неправильний формат вводу. Введіть десяткове число.");
            }
        }
    }
}