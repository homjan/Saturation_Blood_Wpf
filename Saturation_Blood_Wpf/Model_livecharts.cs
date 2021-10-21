using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using LiveCharts.Wpf.Charts.Base;
using LiveCharts.Helpers;

namespace Saturation_Blood_Wpf
{
    public class Model_livecharts
    {
        const double PI_ = 3.1415926;
        const int time_numerical = 60;

        double N_shift_axis = 0;
        double max_time = 0;

        private Dictionary<string, bool> radbutton_settings_data;
        private Dictionary<string, string> textbox_data;
        private Dictionary<string, string> special_point_n;
        //private Dictionary<string, double> slider_settings;


        private int reg;
        private string reg_regulation;

        private int red_const;
        private int red_diff;
        private int IK_const;
        private int IK_diff;

        private int red_const_regulation;
        private int red_diff_regulation;
        private int IK_const_regulation;
        private int IK_diff_regulation;

        private int N_smoth;

        private double atten;
        private double k_const_RED;
        private double k_const_IK;

        private model_saturation.Livechart_Data LivechartData;
        private model_saturation.Initial_processing.Initial_Data InitialData;
        private model_saturation.Initial_processing.Divided_By_Periods_Data DividedRow;
        private model_saturation.Points.Special_point SpecialPoint;

        private model_saturation.Points.Special_point special_Point_red_const;
        private model_saturation.Points.Special_point special_Point_red_diff;
        private model_saturation.Points.Special_point special_Point_IK_const;
        private model_saturation.Points.Special_point special_Point_IK_diff;

        private model_saturation.Points.Reflector_Const_Data reflector_red_const;
        private model_saturation.Points.Reflector_Const_Data reflector_IK_const;
        private model_saturation.Saturation saturation;


        private long leftborder;
        private long rightborder;

        public Model_livecharts(Dictionary<string, string> textbox_,
            Dictionary<string, bool> radbutton_settings, Dictionary<string, string> special_point)
        {
            radbutton_settings_data = radbutton_settings;
            textbox_data = textbox_;
            special_point_n = special_point;
        }

        public void Set_size_data(int size_)
        {
            LivechartData.Set_saze_min(size_);
        }

        public void Set_Time_Border(int Left, int Right)
        {
            leftborder = Left * 1000000;
            rightborder = Right * 1000000;

        }

        public void Set_all_inner_parameters()
        {
            try
            {
                reg = Convert.ToInt32(textbox_data["canal_FPG_"]);
                reg_regulation = textbox_data["FPG_regul_"];
                atten = Convert.ToDouble(textbox_data["attenjuator_"].Replace('.', ','));
                k_const_RED = Convert.ToDouble(textbox_data["k_pow_red_"].Replace('.', ','));
                k_const_IK = Convert.ToDouble(textbox_data["k_pow_ik_"].Replace('.', ','));

                N_smoth = Convert.ToInt32(textbox_data["N_sl_"]);

                red_const = Convert.ToInt32(textbox_data["red_const_"]);
                red_diff = Convert.ToInt32(textbox_data["red_diff_"]);
                IK_const = Convert.ToInt32(textbox_data["ik_const_"]);
                IK_diff = Convert.ToInt32(textbox_data["ik_diff_"]);

                red_const_regulation = Convert.ToInt32(textbox_data["red_const_regul_"]);
                red_diff_regulation = Convert.ToInt32(textbox_data["red_diff_regul_"]);
                IK_const_regulation = Convert.ToInt32(textbox_data["ik_const_regul_"]);
                IK_diff_regulation = Convert.ToInt32(textbox_data["ik_diff_regul_"]);


            }
            catch (Exception)
            {
                MessageBox.Show("Введите правильные значения");
            }

        }

        /// <summary>
        /// Инициализаторы
        /// </summary>
        #region

        public void Inizilize_Base_Data() {

            InitialData = new model_saturation.Initial_processing.Initial_Data("test.txt", reg, reg + 1);
            LivechartData = new model_saturation.Livechart_Data(InitialData);
        }

        public void Inizilize_Base_Data_for_Special_Point()
        {
            InitialData = new model_saturation.Initial_processing.Initial_Data("test3.txt", reg, reg + 1);
            DividedRow = new model_saturation.Initial_processing.Divided_By_Periods_Data(InitialData, reg_regulation);
        }

        public void Inizilize_Special_Point()
        {
            SpecialPoint = new model_saturation.Points.Special_point(DividedRow, InitialData);          
        }

        public void Inizialize_Graph_Sp_Point()
        {
            LivechartData = new model_saturation.Livechart_Data(InitialData, SpecialPoint);
        }

        public void Inizialize_Graph_Max_Min() 
        {
            LivechartData = new model_saturation.Livechart_Data(InitialData, special_Point_red_const, 
                special_Point_red_diff, special_Point_IK_const, special_Point_IK_diff);        
        }


        public void Inizialize_Base_Data_for_max_min() 
        {
            InitialData = new model_saturation.Initial_processing.Initial_Data("test3.txt", red_const, red_const, 1000000);
        }

        public void Inizilize_Saturation() 
        {
            saturation = new model_saturation.Saturation(special_Point_red_const.Get_Spec_Point(), special_Point_red_diff.Get_Spec_Point(),
              special_Point_IK_const.Get_Spec_Point(), special_Point_IK_diff.Get_Spec_Point());

        }

        #endregion

        //Открыть файл
        #region
        public void Open_File_With_Data() {

            N_shift_axis = 0;

            string adres = "q";
            string adres2 = "q";
            string datapath = "w";

            int da5 = 0;

            StringBuilder buffer2 = new StringBuilder();

            OpenFileDialog qqq = new OpenFileDialog
            {
                Filter = "Файлы txt|*.txt"
            };

            if (qqq.ShowDialog() == true)
            {
                //adres = qqq.SelectedPath;
                adres = qqq.FileName;
                //       textBox4.Text = adres;
                buffer2.Insert(0, qqq.FileName);

                da5 = buffer2.Length;
                buffer2.Remove(da5 - 8, 8);
                adres2 = buffer2.ToString();
                datapath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

                System.IO.File.Copy(Path.Combine(qqq.InitialDirectory, qqq.FileName), Path.Combine(datapath, "test.txt"), true);
                try
                {
                    System.IO.File.Copy(adres2 + "Информация о пациенте.txt", Path.Combine(datapath, "Информация о пациенте.txt"), true);
                }
                catch
                {
                }
            }

        }
        public void Calculate_Base_Image() 
        {
            try
            {
                InitialData.Row1_Shift_Time_To_0();//Сдвигаем время к 0
                InitialData.Row1_Smothing();// Сглаживаем полученные данные
                InitialData.Row1_Write_In_File("test3.txt");

                LivechartData.MakeGraph_4_Canal_Observable();                

                max_time = Convert.ToDouble(InitialData.Get_row1_x_y(InitialData.Get_b() - 1, 0));

            }
            catch (Exception ex)
            {
                MessageBox.Show("Выбран неправильный файл");
            }
        }
        #endregion

        //Рассчитать сигнал и особые точки с 1 канала
        #region
        public void Calculate_Base_Image_With_Time_Limit(int Left_Limit, int Right_Limit)
        {
            try
            {
                InitialData.Row1_Shift_Time_To_0();//Сдвигаем время к 0
                InitialData.Row1_Smothing();// Сглаживаем полученные данные
                InitialData.Row1_Write_In_File("test3.txt");
                Set_Time_Border(Left_Limit, Right_Limit);               
                LivechartData.MakeGraph_4_Canal_Observable_minTime(leftborder, rightborder);

                max_time = Convert.ToDouble(InitialData.Get_row1_x_y(InitialData.Get_b() - 1, 0));

            }
            catch (Exception ex)
            {
                MessageBox.Show("Выбран неправильный файл");
            }
        }


        public void Calculate_One_Canal_Image_With_Time_Limit_one()
        {
           // try
           // {
            InitialData.Row1_Shift_Time_To_0();//Сдвигаем время к 0
            InitialData.Row1_Smothing();// Сглаживаем полученные данные
            InitialData.Row2_Calculate();
            InitialData.Row3_Average_Kanal_reg();            
                
            max_time = Convert.ToDouble(InitialData.Get_row1_x_y(InitialData.Get_b() - 1, 0));

            DividedRow.Calculate_Data_In_Period();

               
           // }
          //  catch (Exception ex)
          //  {
          //      MessageBox.Show("Выбран неправильный файл");
           // }
        }

        public void Calculate_One_Canal_Image_With_Time_Limit_two(int Left_Limit, int Right_Limit)
        {
            // try
            // {
            Set_Time_Border(Left_Limit, Right_Limit);
            LivechartData.MakeGraph_On_Canal_FPG(leftborder, rightborder);
            // }
            //  catch (Exception ex)
            //  {
            //      MessageBox.Show("Выбран неправильный файл");
            // }
        }

        public void Calculate_Special_Points() {
           
            if (radbutton_settings_data["base_algorithm_rud_"])
            {
                SpecialPoint.Return_Special_Point(reg_regulation);
            }
            if (radbutton_settings_data["neural_net_algorithm_rud_"])
            {
                SpecialPoint.Return_Special_Point_Neural_Network();
            }
            if (radbutton_settings_data["statistic_algorithm_rud_"])
            {
                SpecialPoint.Return_Special_Point_Statistic_Num();
            }

            SpecialPoint.Delete_Zero_From_Data();
            SpecialPoint.Delete_Equal_Data();

            Save_Special_Point();
        }
        #endregion

        // Рассчитать максимум минимум
        #region
        private model_saturation.Points.Special_point Calculate_Special_Points_Grang_Calculation(string regul, int pow_int, int canal_fpg)
        {
            InitialData.Row2_re_Calculate(pow_int);
            InitialData.Row3_Average_Chosen_Kanal(canal_fpg);

            model_saturation.Initial_processing.Divided_By_Periods_Data divided_row_red_const = 
                new model_saturation.Initial_processing.Divided_By_Periods_Data(InitialData, regul);
            divided_row_red_const.Calculate_Data_In_Period();

            model_saturation.Points.Special_point osob_point_1 = new model_saturation.Points.Special_point(divided_row_red_const, InitialData);
                       
            if (radbutton_settings_data["base_algorithm_rud_"])
            {
                osob_point_1.Return_Special_Point(regul);
            }
            if (radbutton_settings_data["neural_net_algorithm_rud_"])
            {
                osob_point_1.Return_Special_Point_Neural_Network();
            }
            if (radbutton_settings_data["statistic_algorithm_rud_"])
            {
                osob_point_1.Return_Special_Point_Statistic_Num();
            }

            osob_point_1.Delete_Zero_From_Data();
            osob_point_1.Delete_Equal_Data();

            return osob_point_1;
        }

        public void Calculate_Max_Min_Diff()  
        {
            InitialData.Row1_Shift_Time_To_0();//Сдвигаем время к 0
            for (int i = 0; i < N_smoth; i++)
            {
                InitialData.Row1_Smothing();
            }

          //  InitialData.Row1_Reflect_Chosen(red_const, radbutton_settings_data["invert_const_data_"]);
          //  InitialData.Row1_Reflect_Chosen(IK_const, radbutton_settings_data["invert_const_data_"]);
            InitialData.Row2_Calculate();
            InitialData.Row3_Average_Kanal_reg();

            special_Point_red_diff = Calculate_Special_Points_Grang_Calculation(textbox_data["red_diff_regul_"],
                1000000, Convert.ToInt32(textbox_data["red_diff_"]));
            special_Point_IK_diff = Calculate_Special_Points_Grang_Calculation(textbox_data["ik_diff_regul_"],
                1000000, Convert.ToInt32(textbox_data["ik_diff_"]));
       /*     special_Point_red_const = Calculate_Special_Points_Grang_Calculation(textbox_data["red_const_regul_"],
                10000000, Convert.ToInt32(textbox_data["red_const_"]));
            special_Point_IK_const = Calculate_Special_Points_Grang_Calculation(textbox_data["ik_const_regul_"],
                10000000, Convert.ToInt32(textbox_data["ik_const_"]));

            */
        }

        public void Calculate_Max_Min_Const()
        {
            if (radbutton_settings_data["calculate_const_on_diff_base_"])// Если - считаем на основе точек переменного сигнала
            {
                //Заменить reflector на function_additional??
                model_saturation.Points.Reflector_Const_Data reflector_Const_IK_2 = new model_saturation.Points.Reflector_Const_Data(special_Point_IK_diff.Get_Spec_Point(), IK_const);
                reflector_Const_IK_2.Set_Const_Special_Point_from_Diff(InitialData);
                special_Point_IK_const = new model_saturation.Points.Special_point(reflector_Const_IK_2.Get_Special_Point_Const());
                
                model_saturation.Points.Reflector_Const_Data reflector_Const_Red_2 = new model_saturation.Points.Reflector_Const_Data(special_Point_red_diff.Get_Spec_Point(), red_const);
                reflector_Const_Red_2.Set_Const_Special_Point_from_Diff(InitialData);                
                special_Point_red_const = new model_saturation.Points.Special_point(reflector_Const_Red_2.Get_Special_Point_Const());

            }
            else 
            {
                special_Point_red_const = Calculate_Special_Points_Grang_Calculation(textbox_data["red_const_regul_"],
                    10000000, Convert.ToInt32(textbox_data["red_const_"]));
                special_Point_IK_const = Calculate_Special_Points_Grang_Calculation(textbox_data["ik_const_regul_"],
                    10000000, Convert.ToInt32(textbox_data["ik_const_"]));

            }

        }

        public void Calculate_max_min_Image_With_Time_Limit(int Left_Limit, int Right_Limit)
        {
            // try
            // {
            Set_Time_Border(Left_Limit, Right_Limit);
            LivechartData.MakeGraph_On_Canal_Max_Min(leftborder, rightborder);
            // }
            //  catch (Exception ex)
            //  {
            //      MessageBox.Show("Выбран неправильный файл");
            // }
        }

        #endregion

        //Рассчитать сатурацию
        #region

        public void Calculate_Reflection() 
        {
            if (radbutton_settings_data["invert_const_data_"])
            {
                reflector_IK_const = new model_saturation.Points.Reflector_Const_Data(InitialData, special_Point_IK_diff.Get_Spec_Point(), IK_const);

                reflector_IK_const.Set_Const_Special_Point_from_Diff(InitialData);
                reflector_IK_const.Calculate_line_reflection();
                reflector_IK_const.Calculate_reflected_row();
                InitialData.Set_row1(reflector_IK_const.Get_row1());

                reflector_red_const = new model_saturation.Points.Reflector_Const_Data(InitialData, special_Point_red_diff.Get_Spec_Point(), red_const); ;

                reflector_red_const.Set_Const_Special_Point_from_Diff(InitialData);
                reflector_red_const.Calculate_line_reflection();
                reflector_red_const.Calculate_reflected_row();
                InitialData.Set_row1(reflector_red_const.Get_row1());
            }

        }

       

        public void Calculate_Saturation() 
        {           
            if (radbutton_settings_data["const_calculation_only_"])
            { 
                saturation.Set_K_Pow_Const(atten, k_const_RED, k_const_IK);
                saturation.Subscribe_Length_Special();
                saturation.Calculate_K_Pow_Diff();
                saturation.Calculate_Intensity();
                saturation.Calculate_Saturation_Time();
                saturation.Calculate_Saturation_1_Kalinina_Const();
            }

            if (radbutton_settings_data["const_and_dif_calculation_only_"])
            {
                saturation.Set_K_Pow_Const(atten, k_const_RED, k_const_IK);
                saturation.Subscribe_Length_Osob_Full();
                saturation.Calculate_K_Pow_Diff();
                saturation.Calculate_Intensity();
                saturation.Calculate_Saturation_Time();

                saturation.Calculate_Saturation_1_Kalinina_Diff();
            }

            saturation.Minimize_Saturation_Element();
            saturation.Filter_Saturation();
        }

        public void Make_Graph_Saturation() 
        {            
            // Set_Time_Border(Left_Limit, Right_Limit);
            LivechartData.Make_Graph_Saturation(saturation.Get_Saturation_Time(), saturation.Get_Saturation_1(), saturation.Get_full_length());
             
        }

        public void Make_Graph_Saturation_Four_Canal(int Left_Limit, int Right_Limit)
        {
            Set_Time_Border(Left_Limit, Right_Limit);
            LivechartData.MakeGraph_Saturation_Four_Canal(leftborder, rightborder, saturation.Get_Bordered_Saturation_Time(leftborder, rightborder), saturation.Get_Bordered_Saturation_1(leftborder, rightborder), saturation.Get_full_length());

        }

        public string[] Get_Saturation_Data() {

            return model_saturation.Function_additional.Made_Data_Saturation(saturation.Get_Saturation_Time(), saturation.Get_Saturation_1());
        }

        #endregion
        private void Save_Special_Point() 
        {
            model_saturation.Save_data save_Data = new model_saturation.Save_data();

            save_Data.Save_Osob_Point_Postr(SpecialPoint.Get_Spec_Point(), SpecialPoint.Get_spec_point_Length());
            /////////////////////////////////////////

            save_Data.Save_Osob_Point_Clear(
                SpecialPoint.Calculate_Shift_Special_Point(SpecialPoint.Get_Spec_Point(), 
                SpecialPoint.Get_spec_point_Length()), SpecialPoint.Get_spec_point_Length());

        }

        public void Save_Saturation() 
        {
            model_saturation.Save_data save_Data = new model_saturation.Save_data();
            save_Data.Save_Saturation(saturation.Get_Saturation_Time(), saturation.Get_Saturation_1());

        }


        //Вернуть данные
        #region
        public SeriesCollection Get_SeriesCollection_Model()
        {
            return LivechartData.Get_SeriesCollection();
        }

        public string Get_Axis_Title_Right_Model()
        {
            return LivechartData.Get_Axis_Title_Right();
        }

        public string Get_Axis_Title_Left_Model()
        {
            return LivechartData.Get_Axis_Title_Left();
        }

        public bool Get_Is_Show_Label_Model()
        {

            return LivechartData.Get_Is_Show_Label_();
        }

        public string[] Get_Labels_Model() {

            return LivechartData.Get_Labels();
        }

        public int Get_Length_Special_Point() 
        {
            return SpecialPoint.Get_spec_point_Length();
        }

        public double Get_Signal_Length() {
            return Convert.ToDouble( InitialData.Get_Max_Time());
        }

        public int[] Get_Length_Special_Point_four_Canal() {
            int[] len = new int[4];
            len[0] = special_Point_red_const.Get_spec_point_Length();
            len[1] = special_Point_red_diff.Get_spec_point_Length();
            len[2] = special_Point_IK_const.Get_spec_point_Length();
            len[3] = special_Point_IK_diff.Get_spec_point_Length();

            return len;           
        }
        #endregion
    }
}
