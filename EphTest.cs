using System;
using System.IO;
using IO.Astrodynamics;

class Program
{
    static void Main()
    {
        // 1. Инициализация и загрузка файла эфемерид EPM в формате SPICE (.bsp)
        string ephemerisPath = @"C:\Ephemerides\epm2021.bsp";
        
        // Загружаем ядро данных
        Instance.LoadKernels(new FileInfo(ephemerisPath));

        // 2. Задаем параметры расчета
        string target = "MARS";           // Целевой объект (например, Марс)
        string observer = "EARTH";        // Точка наблюдения (Земля)
        string frame = "ICRF";            // Инерциальная система координат
        string aberration = "NONE";       // Учет аберрации (NONE, LT, LT+S)

        // Время в формате Юлианской даты (TDB) или через строку ET (Ephemeris Time)
        double et = 0.0; // Секунды со стандартной эпохи J2000

        // 3. Получаем вектор состояния (позиция x,y,z и скорость vx,vy,vz)
        double[] state = new double[6];
        double lt; // Light time (время прохождения света)

        // Вызов нативного метода cspice через обертку
        CSPICE.spkezr_c(target, et, frame, aberration, observer, state, out lt);

        // Результаты в километрах и км/с
        Console.WriteLine($"Положение {target} относительно {observer}:");
        Console.WriteLine($"X: {state[0]} км, Y: {state[1]} км, Z: {state[2]} км");
    }
}
