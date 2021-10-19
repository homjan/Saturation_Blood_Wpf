using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saturation_Blood_Wpf.model_saturation
{
    public static class Converter_Data_for_Livecharts
    {
        /// <summary>
        /// Выбрать из массива row1 каждый size-ый элемент, записать в другой массив и вернуть его
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="size"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long[,] Minimize_Row_1(long[,] row1, int size, int b) 
        {            
            int y_1 = row1.GetUpperBound(1) + 1;//canal

            double size_double = Convert.ToDouble(size);

            int x = Convert.ToInt32(Convert.ToDouble(b)/ size_double)+1;
            int y = y_1;

            long[,] row = new long[x, y];
            int number = 0;
            for (int i = 0; i < b; i++)
            {
                if (i%size==0)
                {
                    for (int j = 0; j < y; j++)
                    {
                        row[number, j] = row1[i, j];
                    }
                    number++;
                }
            }


            return row;
        
        }

        /// <summary>
        /// Выбрать из массива данных row каждый size-ый элемент, записать в другой массив и вернуть его
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static long[] Minimize_Row(long[] row1, int size)
        {
            int x_1 = row1.GetUpperBound(0) + 1;

            double size_double = Convert.ToDouble(size);

            int x = Convert.ToInt32(Convert.ToDouble(x_1) / size_double)+1;
            long[] row_x = new long[x];
            int number = 0;

            for (int i = 0; i < x_1; i++)
            {
                if (i % size == 0)
                {
                    row_x[number] = row1[i];
                    number++;
                }
            }

            return row_x;
        }

        /// <summary>
        ///  Выбрать из массива строк row1 каждый size-ый элемент, записать в другой массив и вернуть его
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string[] Minimize_data_X_for_Labels(string[] row1, int size) 
        {
            int x_1 = row1.GetUpperBound(0) + 1;           
                                                
            double size_double = Convert.ToDouble(size);

            int x = Convert.ToInt32(Convert.ToDouble(x_1) / size_double);
            string[] row_x = new string[x];
            int number = 0;

            for (int i = 0; i < x_1; i++)
            {
                if (i % size == 0)
                {                    
                        row_x[number] = row1[i];
                   
                    number++;
                }
            }

            return row_x;
        }

        /// <summary>
        /// Конвертировать время (1 столбец) в тект
        /// </summary>
        /// <param name="row1"></param>
        /// <returns></returns>
        public static string[] Convert_data_X_for_Labels(long[,] row1) 
        {
            int x_1 = row1.GetUpperBound(0) + 1;
            int y_1 = row1.GetUpperBound(1) + 1;//canal
                                                //        

            string[] row_x = new string[x_1];
            for (int i = 0; i < x_1; i++)
            {
                row_x[i] = Convert.ToString(Math.Round(Convert.ToDouble(row1[i, 0]/1000000.0), 2));
            }

            return row_x;

        }

        public static string[] Convert_data_Saturation_for_Labels(double[] row1)
        {
            int x_1 = row1.GetUpperBound(0) + 1;
           
            string[] row_x = new string[x_1];
            for (int i = 0; i < x_1; i++)
            {
                row_x[i] = Convert.ToString(Math.Round(Convert.ToDouble(row1[i] / 1000000.0), 2));
            }

            return row_x;

        }


        /*
                public static long[,] Unite_data_labels_specialpoint(long[,] row1, long[,] row2) 
                {
                    int y1 = row1.GetUpperBound(1) + 1;
                    int y2 = row2.GetUpperBound(1) + 1;
                    int x = row1.GetUpperBound(0) + 1;
                    long[,] unite_data = new long[x, y1+y2];
                    int n = 0;

                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y1; j++)
                        {
                            unite_data[i, n] = row1[i, j];
                        }
                    }

                    return unite_data;

                }*/


    }
}
