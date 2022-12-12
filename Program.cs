using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Синглтон
            Human comp = new Human();
            comp.Live("Сердце");
            Console.WriteLine(comp.Hrt.Name);

            comp.Hrt = Heart.getInstance("Больное сердце");
            Console.WriteLine(comp.Hrt.Name);
            Console.ReadLine();
            //Прототип
            INinja figure = new SandShinobi("Gaara", "Техники песка");
            INinja clonedFigure = figure.Clone();
            figure.GetInfo();
            clonedFigure.GetInfo();

            figure = new LeafShinobi("Kakashi", "Кунаи, шаринган");
            clonedFigure = figure.Clone();
            figure.GetInfo();
            clonedFigure.GetInfo();

            Console.Read();
            //Builder
            PenFactory Penfact = new PenFactory();
            PenBuilder builder1 = new IronSwordBuilder();
            Pen defaultRed = Penfact.Craft(builder1);
            Console.WriteLine($"Ручка создана. Материал: {defaultRed.Material.material}, цвет: {defaultRed.InkColor.color}, Тип механизма: {defaultRed.Type.type}");
            PenBuilder builder2 = new SilverSwordBuilder();
            Pen premiumBlack = Penfact.Craft(builder2);
            Console.WriteLine($"Ручка создана. Материал: {premiumBlack.Material.material}, цвет: {premiumBlack.InkColor.color}, Тип механизма: {premiumBlack.Type.type}");
            Console.ReadLine();

            Console.Read();
        }
    }
    //Синглтон
    class Human
    {
        public Heart Hrt { get; set; }
        public void Live(string hrtName)
        {
            Hrt = Heart.getInstance(hrtName);
        }
    }
    class Heart
    {
        private static Heart instance;

        public string Name { get; private set; }

        protected Heart(string name)
        {
            this.Name = name;
        }

        public static Heart getInstance(string name)
        {
            if (instance == null)
                instance = new Heart(name);
            return instance;
        }
    }
    //Прототип


    interface INinja
    {
        INinja Clone();
        void GetInfo();
    }

    class SandShinobi : INinja
    {
        string name;
        string weapon;
        public SandShinobi(string n, string t)
        {
            name = n;
            weapon = t;
        }

        public INinja Clone()
        {
            return new SandShinobi(this.name, this.weapon);
        }
        public void GetInfo()
        {
            Console.WriteLine("Шиноби: {0}, использует: {1}", name, weapon);
        }
    }

    class LeafShinobi : INinja
    {
        string name;
        string weapon;
        public LeafShinobi(string r, string t)
        {
            name = r;
            weapon = t;
        }

        public INinja Clone()
        {
            return new LeafShinobi(this.name, this.weapon);
        }
        public void GetInfo()
        {
            Console.WriteLine("Шиноби: {0}, использует: {1}", name, weapon);
        }
    }
    //builder

    class Material { public string material { get; set; } }
    class InkColor { public string color { get; set; } }
    class Type { public string type { get; set; } }
    class Pen
    {
        public Material Material { get; set; }
        public InkColor InkColor { get; set; }
        public Type Type { get; set; }
    }
    abstract class PenBuilder
    {
        public Pen Pen { get; private set; }
        public void CreatePen()
        {
            Pen = new Pen();
        }
        public abstract void SetMaterial();
        public abstract void SetColor();
        public abstract void SetType();
    }
    class PenFactory
    {
        public Pen Craft(PenBuilder swordBuilder)
        {
            swordBuilder.CreatePen();
            swordBuilder.SetMaterial();
            swordBuilder.SetColor();
            swordBuilder.SetType();
            return swordBuilder.Pen;
        }
    }
    class IronSwordBuilder : PenBuilder
    {
        public override void SetMaterial()
        {
            this.Pen.Material = new Material { material = "пластиковая" };
        }
        public override void SetColor()
        {
            this.Pen.InkColor = new InkColor { color = "Красные чернила" };
        }
        public override void SetType()
        {
            this.Pen.Type = new Type { type = "кнопочная" };
        }
    }
    class SilverSwordBuilder : PenBuilder
    {
        public override void SetMaterial()
        {
            this.Pen.Material = new Material { material = "Нержавеющая сталь" };
        }
        public override void SetColor()
        {
            this.Pen.InkColor = new InkColor { color = "черные чернила" };
        }
        public override void SetType()
        {
            this.Pen.Type = new Type { type = "поворотная" };
        }
    }
}
