namespace Jogo_da_Memória
{
    internal static class Program
    {

        static List<string> iconsNivel2 = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "K", "K",
            "b", "b", "v", "v", "w", "w", "z", "z",
            "h", "h", "f", "f", "s", "s", "x", "x",
            "A", "A", "B", "B", "C", "C", "D", "D",
            "E", "E", "F", "F"
        };


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form2(iconsNivel2));
        }
    }
}