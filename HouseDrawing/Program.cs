using System.Drawing;
using System.Drawing.Imaging;
using Spectre.Console;
using Color = System.Drawing.Color;

namespace HouseDrawing
{
    class Program
    {
        static void Main()
        {
            // nastavení velikosti
            short width =AnsiConsole.Ask<short>("Zadej [bold]šířku[/] obrázku:");
            short height =AnsiConsole.Ask<short>("Zadej [bold]výšku[/] obrázku:");
            Bitmap bmp = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // pozadí na bílou barvu
                g.Clear(Color.White);
                
                // střecha
                Point[] roofPoints = {
                    new Point(width / 2, height / 6),  // Horní vrchol
                    new Point(width / 6, height / 2),  // Levý dolní vrchol
                    new Point(5 * width / 6, height / 2)  // Pravý dolní vrchol
                };
                g.FillPolygon(new SolidBrush(Color.Firebrick), roofPoints);

                // stěny
                g.FillRectangle(new SolidBrush(Color.Bisque), new Rectangle(width / 6, height / 2, 2 * width / 3, height / 3)); // dveře
                g.FillRectangle(new SolidBrush(Color.Sienna), new Rectangle(width / 2 - width / 15, 2 * height / 3, width / 7, height / 6));
                
                // okna (levý, pravý)
                Brush window = new SolidBrush(Color.Aqua);
                g.FillRectangle(window, new Rectangle(width / 4, height / 2 + height / 15, width / 10, width / 10));
                g.FillRectangle(window, new Rectangle(3 * width / 4 - width / 10, height / 2 + height / 15, width / 10, width / 10));
            }
            
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                fileName = AnsiConsole.Ask<string>("Zadejte [bold]název[/] souboru: ") + ".png";
            string filePath = Path.Combine(desktopPath, fileName);
            bmp.Save(filePath, ImageFormat.Png);
            
            // Uvolnění prostředků
            bmp.Dispose();
            
            AnsiConsole.MarkupLine($"[green]Soubor byl uložen na plochu jako [bold]'[underline]{fileName}[/]'[/][/]");
        }
    }
}