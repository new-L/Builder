using System;
using System.Text;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            // содаем объект пекаря
            Baker baker = new Baker();
            // создаем билдер для ржаного хлеба
            BreadBuilder builder = new RyeBreadBuilder();
            // выпекаем
            Bread ryeBread = baker.Bake(builder);
            Console.WriteLine(ryeBread.ToString());
            // оздаем билдер для пшеничного хлеба
            builder = new WheatBreadBuilder();
            Bread wheatBread = baker.Bake(builder);
            Console.WriteLine(wheatBread.ToString());

            Console.Read();
        }
    }
    #region Компоненты
    //мука
    class Flour
    {
        // какого сорта мука
        public string Sort { get; set; }
    }
    // соль
    class Salt { }
    // пищевые добавки
    class Additives
    {
        public string Name { get; set; }
    }
    #endregion
    
    
    class Bread
    {
        // мука
        public Flour Flour { get; set; }
        // соль
        public Salt Salt { get; set; }
        // пищевые добавки
        public Additives Additives { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (Flour != null)
                sb.Append(Flour.Sort + "\n");
            if (Salt != null)
                sb.Append("Соль \n");
            if (Additives != null)
                sb.Append("Добавки: " + Additives.Name + " \n");
            return sb.ToString();
        }
    }

    
    // пекарь - director
    class Baker
    {
        public Bread Bake(BreadBuilder breadBuilder)
        {
            breadBuilder.CreateBread();
            breadBuilder.SetFlour();
            breadBuilder.SetSalt();
            breadBuilder.SetAdditives();
            return breadBuilder.Bread;
        }
    }

    #region Конкретные реализации Билдера

    
    // строитель для ржаного хлеба
    class RyeBreadBuilder : BreadBuilder
    {
        public override void SetFlour()
        {
            Bread.Flour = new Flour { Sort = "Ржаная мука 1 сорт" };
        }

        public override void SetSalt()
        {
            Bread.Salt = new Salt();
        }

        public override void SetAdditives()
        {
            // не используется, чтобы показать работу программы
        }
    }
    // строитель для пшеничного хлеба
    class WheatBreadBuilder : BreadBuilder
    {
        public override void SetFlour()
        {
            Bread.Flour = new Flour { Sort = "Пшеничная мука высший сорт" };
        }

        public override void SetSalt()
        {
            Bread.Salt = new Salt();
        }

        public override void SetAdditives()
        {
            Bread.Additives = new Additives { Name = "дрожжи" };
        }
    }

    #endregion





    // абстрактный класс строителя. Билдер - интерфейс для создания конкретных билдеров
    abstract class BreadBuilder
    {
        public Bread Bread { get; private set; }
        public void CreateBread()
        {
            Bread = new Bread();
        }
        public abstract void SetFlour();
        public abstract void SetSalt();
        public abstract void SetAdditives();
    }
}
